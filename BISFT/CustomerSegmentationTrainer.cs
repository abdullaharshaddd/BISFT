using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Data.SqlClient;
using System.Configuration;



public class CustomerSegmentationTrainer
{
    //private static readonly string dataPath = "Aggregated_Customer_Data.csv";
    private static readonly string modelPath = "CustomerSegmentationModel.zip";

    public static List<CustomerData> LoadCustomerDataFromDatabase()
    {
        var customerDataList = new List<CustomerData>();

        string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;


        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = @"
                            SELECT 
                                COUNT(s.saleid) AS NumOrders,
                                SUM(si.quantity * si.priceperunit) AS TotalSales,
                                SUM(si.quantity) AS TotalQuantity,
                                DATEDIFF(DAY, MAX(s.saledate), GETDATE()) AS RecencyDays
                            FROM saleitems si
                            JOIN sales s ON si.saleid = s.saleid
                            GROUP BY s.customername";


            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var data = new CustomerData
                {
                    NumOrders = Convert.ToSingle(reader["NumOrders"]),
                    TotalSales = Convert.ToSingle(reader["TotalSales"]),
                    TotalQuantity = Convert.ToSingle(reader["TotalQuantity"]),
                    RecencyDays = Convert.ToSingle(reader["RecencyDays"]),
                };
                customerDataList.Add(data);
            }
        }

        return customerDataList;
    }
    public static void TrainModel()
    {
        var mlContext = new MLContext();

        // Load data
        var customerList = LoadCustomerDataFromDatabase();

        int clusterCount = Math.Min(4, customerList.Count);

        if (clusterCount < 2)
        {
            Console.WriteLine("❌ Not enough data to train a clustering model. Need at least 2 data points.");
            return;
        }

        var dataView = mlContext.Data.LoadFromEnumerable(customerList);

        var pipeline = mlContext.Transforms.Concatenate("Features",
                                nameof(CustomerData.NumOrders),
                                nameof(CustomerData.TotalSales),
                                nameof(CustomerData.TotalQuantity),
                                nameof(CustomerData.RecencyDays))
                            .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                            .Append(mlContext.Clustering.Trainers.KMeans("Features", numberOfClusters: clusterCount));

        var model = pipeline.Fit(dataView);
        mlContext.Model.Save(model, dataView.Schema, modelPath);

        Console.WriteLine("✅ Model trained and saved with clustering!");
    }


    //public static void TrainModel()
    //{
    //    var mlContext = new MLContext();

    //    // Load data
    //    IDataView dataView = mlContext.Data.LoadFromTextFile<CustomerData>(
    //        path: dataPath,
    //        hasHeader: true,
    //        separatorChar: ',');

    //    // Build pipeline
    //    var pipeline = mlContext.Transforms.Concatenate("Features",
    //                        nameof(CustomerData.NumOrders),
    //                        nameof(CustomerData.TotalSales),
    //                        nameof(CustomerData.TotalQuantity),
    //                        nameof(CustomerData.RecencyDays))
    //                    .Append(mlContext.Clustering.Trainers.KMeans(numberOfClusters: 4));

    //    // Train model
    //    var model = pipeline.Fit(dataView);

    //    // Save model
    //    mlContext.Model.Save(model, dataView.Schema, modelPath);

    //    Console.WriteLine("✅ Customer segmentation model trained and saved!");
    //}
    public static void PredictClusters()
    {
        var mlContext = new MLContext();

        // Load model
        DataViewSchema modelSchema;
        ITransformer trainedModel = mlContext.Model.Load("CustomerSegmentationModel.zip", out modelSchema);

        // Load the same data
        IDataView dataView = mlContext.Data.LoadFromTextFile<CustomerData>(
            path: "Aggregated_Customer_Data.csv",
            hasHeader: true,
            separatorChar: ',');

        // Make predictions
        IDataView predictions = trainedModel.Transform(dataView);

        var results = mlContext.Data.CreateEnumerable<CustomerPrediction>(predictions, reuseRowObject: false).ToList();

        int i = 1;
        foreach (var pred in results)
        {
            Console.WriteLine($"Customer {i++}: Cluster {pred.ClusterId}");
        }
    }
    public static string GetClusterLabel(uint clusterId)
    {
        switch (clusterId)
        {
            case 1:
                return "Premium Spenders";
            case 2:
                return "Regular Loyalists";
            case 3:
                return "Low Engagement";
            case 4:
                return "New or One-Time Buyers";
            default:
                return "Uncategorized";
        }
    }



    public static Dictionary<uint, int> GetClusterDistribution()
    {
        var mlContext = new MLContext();
        ITransformer model = mlContext.Model.Load("CustomerSegmentationModel.zip", out _);

        IDataView dataView = mlContext.Data.LoadFromTextFile<CustomerData>(
            path: "Aggregated_Customer_Data.csv",
            hasHeader: true,
            separatorChar: ',');

        IDataView predictions = model.Transform(dataView);
        var results = mlContext.Data.CreateEnumerable<CustomerPrediction>(predictions, reuseRowObject: false).ToList();

        var clusterCounts = results
            .GroupBy(p => p.ClusterId)
            .ToDictionary(g => g.Key, g => g.Count());

        return clusterCounts;
    }


}
