using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace BISFT
{
    public static class DataBaseAccess
    {
        public static event EventHandler InventoryUpdated;

        private static void OnInventoryUpdated()
        {
            InventoryUpdated?.Invoke(null, EventArgs.Empty);
        }

        public static SqlConnection GetConnection()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BISFTdb"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Database connection string is not configured properly.");
                    return null;
                }
                return new SqlConnection(connectionString);
            }
            catch (ConfigurationErrorsException ex)
            {
                MessageBox.Show("Error loading the database configuration: " + ex.Message);
                return null;
            }
        }


        public static DataTable GetAllInventoryItems()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    string query = "SELECT * FROM InventoryItems";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    connection.Open();
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message);
                }
            }
            return dataTable;
        }

        public static void InsertInventoryItem(string product, string description, int quantity, string availability,
                                               string category, decimal purchasePrice, decimal sellingPrice,
                                               string supplier, int thresholdValue, DateTime? expiryDate)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = @"
                INSERT INTO InventoryItems
                (Product, Description, Quantity, Availability, Category, PurchasePrice, SellingPrice, 
                 Supplier, ThresholdValue, ExpiryDate, DateAdded, LastUpdated)
                VALUES
                (@Product, @Description, @Quantity, @Availability, @Category, @PurchasePrice, @SellingPrice,
                 @Supplier, @ThresholdValue, @ExpiryDate, GETDATE(), GETDATE())";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Product", product);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@Availability", availability);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@PurchasePrice", purchasePrice);
                command.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                command.Parameters.AddWithValue("@Supplier", supplier);
                command.Parameters.AddWithValue("@ThresholdValue", thresholdValue);
                command.Parameters.AddWithValue("@ExpiryDate", expiryDate ?? (object)DBNull.Value);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        OnInventoryUpdated();  
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to insert inventory item. " + ex.Message);
                }
            }
        }

        public static void DeleteInventoryItem(int itemId)
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "DELETE FROM InventoryItems WHERE ItemID = @ItemID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemID", itemId);

                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    OnInventoryUpdated();  
                }
            }
        }

        public static DataTable SearchInventoryItemsById(int itemId)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand("SELECT * FROM InventoryItems WHERE ItemID = @ItemID", connection);
                command.Parameters.AddWithValue("@ItemID", itemId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        public static DataTable GetTop10LowStockProducts()
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                string query = @"
            SELECT TOP 10 Product, Quantity, ThresholdValue
            FROM InventoryItems
            WHERE Quantity <= ThresholdValue
            ORDER BY Quantity ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);
                }
            }
            return table;
        }


        public static int GetTotalProductsCount()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM InventoryItems", connection);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public static int GetLowStockCount()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM InventoryItems WHERE Quantity <= ThresholdValue", connection);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public static List<(DateTime Date, decimal TotalAmount)> GetSalesDataForChart()
        {
            var results = new List<(DateTime, decimal)>();

            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"
            SELECT SaleDate, SUM(TotalAmount) AS TotalAmount
            FROM Sales
            GROUP BY SaleDate
            ORDER BY SaleDate";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    decimal total = reader.GetDecimal(1);
                    results.Add((date, total));
                }
            }

            return results;
        }

        


    }
}