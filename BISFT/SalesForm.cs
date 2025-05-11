using BISFT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BISFT
{
    public partial class SalesForm: Form
    {
        public SalesForm()
        {
            InitializeComponent();
            this.Load += SalesForm_Load;

            dgvSales.CellClick += dgvSales_CellClick; 

        }

        private void DeleteSale(int saleId)
        {
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    List<(string product, int quantity)> itemsToRestore = new List<(string, int)>();

                    string getItemsQuery = "select productname, quantity from saleitems where saleid = @id";
                    using (SqlCommand getItemsCmd = new SqlCommand(getItemsQuery, con, transaction))
                    {
                        getItemsCmd.Parameters.AddWithValue("@id", saleId);
                        using (SqlDataReader reader = getItemsCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string product = reader["productname"].ToString();
                                int quantity = Convert.ToInt32(reader["quantity"]);
                                itemsToRestore.Add((product, quantity));
                            }
                        }
                    }

                    foreach (var item in itemsToRestore)
                    {
                        string updateStockQuery = "update inventoryitems set quantity = quantity + @qty where product = @prod";
                        using (SqlCommand stockCmd = new SqlCommand(updateStockQuery, con, transaction))
                        {
                            stockCmd.Parameters.AddWithValue("@qty", item.quantity);
                            stockCmd.Parameters.AddWithValue("@prod", item.product);
                            stockCmd.ExecuteNonQuery();
                        }
                    }

                    string deleteSaleQuery = "delete from sales where saleid = @id";
                    using (SqlCommand cmd = new SqlCommand(deleteSaleQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", saleId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error deleting sale: " + ex.Message);
                }
            }
        }

        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                string clickedColumn = dgvSales.Columns[e.ColumnIndex].Name;

                if (clickedColumn == "Delete")
                {
                    int saleId = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["SaleID"].Value);

                    DialogResult result = MessageBox.Show(
                        "Are you sure you want to delete this sale?",
                        "Confirm Deletion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        DeleteSale(saleId);
                        LoadSales();
                    }
                }
                else if (clickedColumn == "Update")
                {
                    int saleId = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["SaleID"].Value);
                    string customerName = dgvSales.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString();
                    DateTime saleDate = DateTime.ParseExact(
                        dgvSales.Rows[e.RowIndex].Cells["SaleDate"].Value.ToString(),
                        "dd-MM-yyyy",
                        System.Globalization.CultureInfo.InvariantCulture
                    );
                    decimal totalPrice = Convert.ToDecimal(dgvSales.Rows[e.RowIndex].Cells["TotalPrice"].Value);

                    UpdateSaleForm updateForm = new UpdateSaleForm(saleId, customerName, saleDate, totalPrice);
                    updateForm.FormClosed += (s, args) => LoadSales();
                    updateForm.ShowDialog();
                }
                else if (clickedColumn == "View Details") 
                {
                    int saleId = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["SaleID"].Value);

                    ViewDetailsForm detailsForm = new ViewDetailsForm(saleId);
                    detailsForm.ShowDialog();
                }
            }
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close(); 
        }


        private void SetupSalesGrid()
        {
            dgvSales.Columns.Clear();
            dgvSales.Rows.Clear();
            dgvSales.AutoGenerateColumns = false;

            dgvSales.Columns.Add("SaleID", "Sale ID");
            dgvSales.Columns.Add("CustomerName", "Customer Name");
            dgvSales.Columns.Add("SaleDate", "Sale Date");
            dgvSales.Columns.Add("TotalPrice", "Total Price");

            DataGridViewButtonColumn viewBtn = new DataGridViewButtonColumn();
            viewBtn.Name = "View Details";
            viewBtn.HeaderText = "";
            viewBtn.Text = "View Details";
            viewBtn.UseColumnTextForButtonValue = true;
            dgvSales.Columns.Add(viewBtn);

            DataGridViewButtonColumn updateBtn = new DataGridViewButtonColumn();
            updateBtn.Name = "Update";
            updateBtn.Text = "Update";
            updateBtn.UseColumnTextForButtonValue = true;
            dgvSales.Columns.Add(updateBtn);

            DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.Name = "Delete"; 
            deleteBtn.Text = "Delete";
            deleteBtn.UseColumnTextForButtonValue = true;
            deleteBtn.HeaderText = ""; 
            dgvSales.Columns.Add(deleteBtn);
        }
        private void LoadSales()
        {
            dgvSales.Rows.Clear();
            decimal totalSales = 0;

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = "select saleid, customername, saledate, totalamount from sales";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int rowIndex = dgvSales.Rows.Add(
                            reader["saleid"],
                            reader["customername"],
                            Convert.ToDateTime(reader["saledate"]).ToString("dd-MM-yyyy"),
                            reader["totalamount"],
                            "View Details", "Update", "Delete"
                        );

                        totalSales += Convert.ToDecimal(reader["totalamount"]);
                    }
                }
            }

            lblTotalSales.Text = $"Total Sales: Rs. {totalSales:N2}";
        }
        private void CalculateTotalSales()
        {

            decimal totalSales = 0;

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = "select sum(totalamount) from sales";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalSales = Convert.ToDecimal(result);
                    }
                }
            }

            lblTotalSales.Text = $"Total Sales: Rs. {totalSales:N2}";
        } 
        private void SalesForm_Load(object sender, EventArgs e)
        {
            SetupSalesGrid();
            LoadSales();
            CalculateTotalSales();
            PopulateProductDropdown();

        }

        private List<(DateTime Date, int ForecastedQuantity)> GetSalesForecastDataForProduct(string productName)
        {
            var data = new List<(DateTime, int)>();

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = @"
            SELECT f.ForecastDate, f.ForecastedQuantity
            FROM ProductForecastHistory f
            JOIN InventoryItems i ON f.ItemId = i.ItemID
            WHERE i.Product = @productName
            ORDER BY f.ForecastDate";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@productName", productName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime date = reader.GetDateTime(0);
                        int forecastQuantity = reader.GetInt32(1);
                        data.Add((date, forecastQuantity));
                    }
                }
            }

            return data;
        }

        private void LoadSalesForecastChart(string productName)
        {
            // Clear existing chart data
            chartSalesForecast.Series.Clear();
            chartSalesForecast.ChartAreas.Clear();
            chartSalesForecast.Legends.Clear();

            // Create a chart area
            ChartArea chartArea = new ChartArea();
            chartSalesForecast.ChartAreas.Add(chartArea);

            // Create a new series for sales forecast data
            Series series = new Series
            {
                Name = "SalesForecast",
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.Cyan,
                IsVisibleInLegend = false
            };

            // Fetch forecast data for the selected product
            var forecastData = GetSalesForecastDataForProduct(productName);

            // Add data points to the series
            foreach (var data in forecastData)
            {
                series.Points.AddXY(data.Date.ToShortDateString(), data.ForecastedQuantity);
            }

            // Add the series to the chart
            chartSalesForecast.Series.Add(series);
        }


        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected product name
            string selectedProduct = cmbProduct.SelectedItem.ToString();

            // Call method to load the chart for the selected product
            LoadSalesForecastChart(selectedProduct);
        }

        private void PopulateProductDropdown()
        {
            // Clear existing items
            cmbProduct.Items.Clear();

            // Fetch products from the database
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = "SELECT DISTINCT Product FROM InventoryItems";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Add product names to ComboBox
                        cmbProduct.Items.Add(reader["Product"].ToString());
                    }
                }
            }

            // Set the default selected item to the first product
            if (cmbProduct.Items.Count > 0)
            {
                cmbProduct.SelectedIndex = 0;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string input = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                LoadSales(); 
                return;
            }

            dgvSales.Rows.Clear();

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();

                string query;
                SqlCommand cmd;

                if (int.TryParse(input, out int saleId))
                {
                    query = "select saleid, customername, saledate, totalamount from sales where saleid = @id";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", saleId);
                }
                else
                {
                    query = "select saleid, customername, saledate, totalamount from sales where customername like @name";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", "%" + input + "%");
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvSales.Rows.Add(
                            reader["saleid"],
                            reader["customername"],
                            Convert.ToDateTime(reader["saledate"]).ToString("dd-MM-yyyy"),
                            reader["totalamount"],
                            "View Details", "Update", "Delete"
                        );
                    }
                }
            }
        }

        private void btnAddSale_Click(object sender, EventArgs e)
        {
            AddSaleForm form = new AddSaleForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadSales(); 
                CalculateTotalSales(); 
            }
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide(); 
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
