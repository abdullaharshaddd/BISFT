using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class DatabaseService
{
    private readonly string connectionString = ConfigurationManager.ConnectionStrings["BISFTdb"]?.ConnectionString;

    public async Task<int> GetTotalInventoryItemsAsync()
    {
        string query = "SELECT COUNT(*) FROM InventoryItems";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                return (int)await cmd.ExecuteScalarAsync();
            }
        }
    }

    public async Task<string> GetProductDetailsAsync(string productName)
    {
        string query = @"
            SELECT Product, Quantity, SellingPrice, ThresholdValue
            FROM InventoryItems
            WHERE Product = @name";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", productName);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return $"Product: {reader["Product"]}, Stock: {reader["Quantity"]}, Price: Rs. {reader["SellingPrice"]}, Threshold: {reader["ThresholdValue"]}";
                    }
                    else
                    {
                        return "Product not found.";
                    }
                }
            }
        }
    }

    public async Task<int> GetTotalCustomersAsync()
    {
        string query = "SELECT COUNT(*) FROM Customers";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                return (int)await cmd.ExecuteScalarAsync();
            }
        }
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        string query = "SELECT SUM(TotalAmount) FROM Sales";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                object result = await cmd.ExecuteScalarAsync();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }
    }

    public async Task<decimal> GetTodaysSalesAsync()
    {
        string query = @"
            SELECT SUM(TotalAmount)
            FROM Sales
            WHERE CAST(SaleDate AS DATE) = CAST(GETDATE() AS DATE)";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                object result = await cmd.ExecuteScalarAsync();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }
    }

    public async Task<string> GetLowStockAlertsAsync()
    {
        string query = @"
            SELECT i.Product, i.Quantity, pf.ForecastedQuantity, i.ThresholdValue
            FROM ProductForecastHistory pf
            JOIN InventoryItems i ON pf.ItemId = i.ItemID
            WHERE (i.Quantity - pf.ForecastedQuantity) <= i.ThresholdValue";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                string result = "";
                while (await reader.ReadAsync())
                {
                    result += $"⚠️ {reader["Product"]}: Current={reader["Quantity"]}, Forecast={reader["ForecastedQuantity"]}, Threshold={reader["ThresholdValue"]}\n";
                }

                return string.IsNullOrEmpty(result) ? "All products are above threshold based on forecast." : result;
            }
        }
    }

    public async Task<string> GetHighestStockProductAsync()
    {
        string query = "SELECT TOP 1 Product, Quantity FROM InventoryItems ORDER BY Quantity DESC";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return $"📦 Highest stock product: {reader["Product"]} (Quantity: {reader["Quantity"]})";
                }
                return "No products found.";
            }
        }
    }

    public async Task<string> GetLowestStockProductAsync()
    {
        string query = "SELECT TOP 1 Product, Quantity FROM InventoryItems ORDER BY Quantity ASC";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return $"📉 Lowest stock product: {reader["Product"]} (Quantity: {reader["Quantity"]})";
                }
                return "No products found.";
            }
        }
    }

    public async Task<string> GetTopSellingProductAsync()
    {
        string query = @"
            SELECT TOP 1 ProductName, SUM(Quantity * PricePerUnit) AS TotalRevenue
            FROM SaleItems
            GROUP BY ProductName
            ORDER BY TotalRevenue DESC";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return $"💰 Top-selling product: {reader["ProductName"]} (Total Revenue: Rs. {reader["TotalRevenue"]:N2})";
                }
                return "No sales data found.";
            }
        }
    }
    public async Task<string> GetCustomerSegmentsSummaryAsync()
    {
        string query = @"
    SELECT CustomerType, COUNT(*) AS CustomerCount
    FROM Customers
    GROUP BY CustomerType";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (!reader.HasRows)
                    return "No customer segmentation data found.";

                string result = "📊 Customer Segment Summary:\n";
                while (await reader.ReadAsync())
                {
                    result += $"• {reader["CustomerType"]}: {reader["CustomerCount"]} customers\n";
                }
                return result;
            }
        }
    }
    // Get least selling product by revenue
    public async Task<string> GetLeastSellingProductAsync()
    {
        string query = @"
        SELECT TOP 1 ProductName, SUM(Quantity * PricePerUnit) AS TotalRevenue
        FROM SaleItems
        GROUP BY ProductName
        ORDER BY TotalRevenue ASC";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return $"📉 Least-selling product: {reader["ProductName"]} (Total Revenue: Rs. {reader["TotalRevenue"]:N2})";
                }
                return "No sales data found.";
            }
        }
    }

    // Get most profitable product
    public async Task<string> GetMostProfitableProductAsync()
    {
        string query = @"
        SELECT TOP 1 si.ProductName,
            SUM((si.PricePerUnit - ii.PurchasePrice) * si.Quantity) AS Profit
        FROM SaleItems si
        JOIN InventoryItems ii ON si.ProductName = ii.Product
        GROUP BY si.ProductName
        ORDER BY Profit DESC";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return $"🏆 Most profitable product: {reader["ProductName"]} (Profit: Rs. {reader["Profit"]:N2})";
                }
                return "No profit data available.";
            }
        }
    }


    // Get least profitable product
    public async Task<string> GetLeastProfitableProductAsync()
    {
        string query = @"
        SELECT TOP 1 si.ProductName,
            SUM((si.PricePerUnit - ii.PurchasePrice) * si.Quantity) AS Profit
        FROM SaleItems si
        JOIN InventoryItems ii ON si.ProductName = ii.Product
        GROUP BY si.ProductName
        ORDER BY Profit ASC";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return $"💔 Least profitable product: {reader["ProductName"]} (Profit: Rs. {reader["Profit"]:N2})";
                }
                return "No profit data available.";
            }
        }
    }



}
