using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ML;
using Microsoft.ML.Data;


public class CustomerData
{
    [LoadColumn(1)] public float NumOrders { get; set; }
    [LoadColumn(2)] public float TotalSales { get; set; }
    [LoadColumn(3)] public float TotalQuantity { get; set; }
    [LoadColumn(4)] public float RecencyDays { get; set; }
}

public class CustomerPrediction
{
    [ColumnName("PredictedLabel")]
    public uint ClusterId { get; set; }
}

