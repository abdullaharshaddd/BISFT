using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;

using Microsoft.ML.Transforms.TimeSeries;
using System.Configuration;

namespace BISFT
{
    public class SalesData
    {
        public float TotalSales { get; set; }
    }
    public class SalesForecastPrediction
    {
        [VectorType(7)] // For 7-day forecast
        public float[] ForecastedSales { get; set; }
    }

    class SalesForecastTrainer
    {
        //Loaded sales data
        public static List<SalesData> LoadSalesDataFromDatabase()
        {
            var salesData = new List<SalesData>();

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = @"
            SELECT CAST(SaleDate AS DATE) AS Day, SUM(TotalAmount) AS TotalSales
            FROM Sales
            GROUP BY CAST(SaleDate AS DATE)
            ORDER BY Day";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        decimal totalSales = reader.GetDecimal(1);
                        salesData.Add(new SalesData
                        {
                            TotalSales = (float)totalSales
                        });
                    }
                }
            }

            return salesData;
        }



        // Model 1: Total Sales Forecasting (₨ per day)
        //Built SSA forecasting pipeline
        public static void TrainAndForecastSales()
        {
            var mlContext = new MLContext();

            // Step 1: Load historical data
            var salesList = LoadSalesDataFromDatabase();
            var dataView = mlContext.Data.LoadFromEnumerable(salesList);

            // Step 2: Define forecasting pipeline
            var pipeline = mlContext.Forecasting.ForecastBySsa(
                outputColumnName: nameof(SalesForecastPrediction.ForecastedSales),
                inputColumnName: nameof(SalesData.TotalSales),
                windowSize: 7,
                seriesLength: 30,
                trainSize: salesList.Count,
                horizon: 7);

            // Step 3: Train model
            var model = pipeline.Fit(dataView);

            // Step 4: Create forecast
            var forecastEngine = model.CreateTimeSeriesEngine<SalesData, SalesForecastPrediction>(mlContext);
            var forecast = forecastEngine.Predict();

            // Step 5: Save forecast to DB
            SaveMonthlyForecastToDatabase(forecast.ForecastedSales);

            var predictions = PredictPerProductQuantities();
            SaveProductForecastResults(predictions);
        }


        //Saved predictions to DB
        private static void SaveMonthlyForecastToDatabase(float[] forecastedSales)
        {
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();

                // Optional: Clear old monthly forecasts
                string deleteQuery = "DELETE FROM MonthlySalesForecast";
                using (SqlCommand delCmd = new SqlCommand(deleteQuery, con))
                {
                    delCmd.ExecuteNonQuery();
                }

                DateTime forecastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1); // Start from next month

                foreach (var predicted in forecastedSales)
                {
                    string insertQuery = @"
                INSERT INTO MonthlySalesForecast (ForecastMonth, PredictedSales)
                VALUES (@ForecastMonth, @PredictedSales)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        string formattedMonth = forecastMonth.ToString("yyyy-MM");
                        cmd.Parameters.AddWithValue("@ForecastMonth", formattedMonth);
                        cmd.Parameters.AddWithValue("@PredictedSales", Math.Round(predicted, 2));
                        cmd.ExecuteNonQuery();
                    }

                    forecastMonth = forecastMonth.AddMonths(1);
                }
            }
        }

        private static void SaveForecastToDatabase(float[] forecastedSales)
        {
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();

                // Optional: Clear old forecasts
                string deleteQuery = "DELETE FROM DailySalesForecast";
                using (SqlCommand delCmd = new SqlCommand(deleteQuery, con))
                {
                    delCmd.ExecuteNonQuery();
                }

                DateTime forecastDate = DateTime.Today.AddDays(1); // Start from tomorrow

                foreach (var predicted in forecastedSales)
                {
                    string insertQuery = @"
                INSERT INTO DailySalesForecast (ForecastDate, PredictedSales)
                VALUES (@ForecastDate, @PredictedSales)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ForecastDate", forecastDate);
                        cmd.Parameters.AddWithValue("@PredictedSales", Math.Round(predicted, 2));
                        cmd.ExecuteNonQuery();
                    }

                    forecastDate = forecastDate.AddDays(1);
                }
            }
        }


        //odel 2: Per-Product Quantity Forecasting // 

        //Gets fast-moving, low-stock products
        public static List<(int ItemId, string Product, int CurrentStock)> GetProductsForQuantityForecast()
        {
            var selected = new List<(int, string, int)>();

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = @"
            SELECT TOP 10
                i.ItemID,
                i.Product,
                i.Quantity,
                SUM(si.Quantity) AS TotalSold
            FROM SaleItems si
            JOIN InventoryItems i ON si.ProductName = i.Product
            GROUP BY i.ItemID, i.Product, i.Quantity, i.ThresholdValue
            HAVING i.Quantity <= i.ThresholdValue + 15
            ORDER BY TotalSold DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int itemId = reader.GetInt32(0);
                        string product = reader.GetString(1);
                        int quantity = reader.GetInt32(2);

                        selected.Add((itemId, product, quantity));
                    }
                }
            }

            return selected;
        }

        //Trains a model per product, predicts next-day quantity
        public static Dictionary<int, float> PredictPerProductQuantities()
        {
            var mlContext = new MLContext();
            var result = new Dictionary<int, float>();

            var products = GetProductsForQuantityForecast();

            foreach (var (itemId, productName, _) in products)
            {
                // Step 1: Load that product’s past sales data
                var salesData = new List<SalesData>();

                using (SqlConnection con = DataBaseAccess.GetConnection())
                {
                    con.Open();
                    string query = @"
                SELECT s.SaleDate, si.Quantity
                FROM SaleItems si
                JOIN Sales s ON si.SaleID = s.SaleID
                WHERE si.ProductName = @product
                ORDER BY s.SaleDate";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@product", productName);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int qty = reader.GetInt32(1);
                                salesData.Add(new SalesData { TotalSales = qty });
                            }
                        }
                    }
                }

                // Skip if not enough data
                if (salesData.Count < 15) continue;

                // Step 2: Train SSA model
                var dataView = mlContext.Data.LoadFromEnumerable(salesData);

                var pipeline = mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: nameof(SalesForecastPrediction.ForecastedSales),
                    inputColumnName: nameof(SalesData.TotalSales),
                    windowSize: 7,
                    seriesLength: 30,
                    trainSize: salesData.Count,
                    horizon: 1); // tomorrow

                var model = pipeline.Fit(dataView);
                var engine = model.CreateTimeSeriesEngine<SalesData, SalesForecastPrediction>(mlContext);
                var forecast = engine.Predict();

                result[itemId] = forecast.ForecastedSales[0]; // Save prediction for this item
            }

            return result;
        }

        //Saves those predicted quantities to ProductForecastHistory
        public static void SaveProductForecastResults(Dictionary<int, float> forecastResults)
        {
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();

                string deleteQuery = "DELETE FROM ProductForecastHistory";
                using (SqlCommand delCmd = new SqlCommand(deleteQuery, con))
                {
                    delCmd.ExecuteNonQuery();
                }

                foreach (var entry in forecastResults)
                {
                    int itemId = entry.Key;
                    float forecastQty = entry.Value;

                    string insertQuery = @"
                INSERT INTO ProductForecastHistory (ItemId, ForecastedQuantity, ForecastDate)
                VALUES (@ItemId, @Qty, @Date)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ItemId", itemId);
                        cmd.Parameters.AddWithValue("@Qty", Math.Round(forecastQty, 2));
                        cmd.Parameters.AddWithValue("@Date", DateTime.Today.AddDays(1)); // tomorrow
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public async Task<decimal> GetNextMonthForecastAsync()
        {
            string nextMonth = DateTime.Today.AddMonths(1).ToString("yyyy-MM");
            using (var con = DataBaseAccess.GetConnection())
            {
                await con.OpenAsync();
                var cmd = new SqlCommand(
                    "SELECT PredictedSales FROM MonthlySalesForecast WHERE ForecastMonth = @month",
                    con);
                cmd.Parameters.AddWithValue("@month", nextMonth);
                object result = await cmd.ExecuteScalarAsync();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

    }

}
