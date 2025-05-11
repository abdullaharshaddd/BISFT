using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BISFT;


namespace BISFT
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            this.Bounds = Screen.PrimaryScreen.WorkingArea;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadClusterChart();
            LoadLowStockProducts();
            LoadTotalSalesTrendChart();
            LoadNextMonthForecastValue();
            LoadPredictedLowStock();
            LoadSummaryStats(); // ✅ NEW
        }

        private void btncustomers_Click(object sender, EventArgs e)
        {
            var customerPage = new CustomerPage();
            customerPage.FormClosed += (s, args) => this.Show();
            customerPage.Show();
            this.Hide();
        }

        private void btnsales_Click(object sender, EventArgs e)
        {
            SalesForm salesForm = new SalesForm();
            salesForm.FormClosed += (s, args) => this.Show();
            salesForm.Show();
            this.Hide();
        }

        private void btnChatbot_Click(object sender, EventArgs e)
        {
            var chatbotPage = new ChatbotForm();
            chatbotPage.FormClosed += (s, args) => this.Show();
            chatbotPage.Show();
            this.Hide();
        }

        private void btninventory_Click(object sender, EventArgs e)
        {
            InventoryForm inventoryForm = new InventoryForm();
            inventoryForm.FormClosed += (s, args) => this.Show();
            inventoryForm.Show();
            this.Hide();
        }

        private void chartClusters_Click(object sender, EventArgs e) { }

        private void btnRetrainModel_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerSegmentationTrainer.TrainModel();
                MessageBox.Show("✅ Model retrained successfully!", "Retrain Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadClusterChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Retraining failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClusterChart()
        {
            var clusterData = CustomerSegmentationTrainer.GetClusterDistribution();

            chartClusters.Series.Clear();
            chartClusters.Series.Add("Customers");
            chartClusters.Series["Customers"].ChartType = SeriesChartType.Pie;
            chartClusters.Series["Customers"]["PieLabelStyle"] = "Disabled";

            foreach (var cluster in clusterData)
            {
                string label = CustomerSegmentationTrainer.GetClusterLabel(cluster.Key);
                chartClusters.Series["Customers"].Points.AddXY(label, cluster.Value);
            }

            chartClusters.Legends[0].Enabled = true;
            chartClusters.Legends[0].Docking = Docking.Right;
        }

        private void LoadLowStockProducts()
        {
            DataTable lowStock = DataBaseAccess.GetTop10LowStockProducts();
            dgvLowStock.DataSource = null;
            dgvLowStock.Columns.Clear();
            dgvLowStock.DataSource = lowStock;
            foreach (DataGridViewRow row in dgvLowStock.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.MistyRose;
            }
        }

        private void dgvLowStock_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void LoadTotalSalesTrendChart()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            var salesData = new List<(DateTime SaleDate, decimal TotalSales)>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"
            SELECT CAST(SaleDate AS DATE) AS SaleDay, SUM(TotalAmount) AS TotalSales
            FROM Sales
            GROUP BY CAST(SaleDate AS DATE)
            ORDER BY SaleDay";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    decimal total = reader.GetDecimal(1);
                    salesData.Add((date, total));
                }
            }

            chartSalesTrend.Series.Clear();
            chartSalesTrend.ChartAreas.Clear();
            chartSalesTrend.Legends.Clear();

            chartSalesTrend.ChartAreas.Add(new ChartArea("MainArea"));

            var series = new Series
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.DarkCyan,
                XValueType = ChartValueType.Date,
                IsVisibleInLegend = false
            };

            foreach (var entry in salesData)
            {
                series.Points.AddXY(entry.SaleDate.ToShortDateString(), entry.TotalSales);
            }

            chartSalesTrend.Series.Add(series);
            chartSalesTrend.Titles.Clear();
            chartSalesTrend.Titles.Add("Sales Analytics");
            chartSalesTrend.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Regular);

            var area = chartSalesTrend.ChartAreas["MainArea"];
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            area.AxisY.Title = "Sales Amount (₨)";
            area.AxisX.TitleFont = new Font("Segoe UI", 10, FontStyle.Regular);
            area.AxisY.TitleFont = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        private void LoadNextMonthForecastValue()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            DateTime nextMonthDate = DateTime.Today.AddMonths(1);
            string nextMonthDisplay = nextMonthDate.ToString("MMMM d, yyyy");
            string nextMonthDbFormat = nextMonthDate.ToString("yyyy-MM");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT TOP 1 PredictedSales FROM MonthlySalesForecast WHERE ForecastMonth = @month";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@month", nextMonthDbFormat);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        decimal forecastValue = Convert.ToDecimal(result);
                        lblTotalForecast.Text = $"Rs. {forecastValue:N0}";
                        lblForecastDate.Text = $"📅 {nextMonthDisplay}";
                    }
                    else
                    {
                        lblTotalForecast.Text = "N/A";
                        lblForecastDate.Text = "📅 No forecast";
                    }
                }
            }
        }

        private void LoadTomorrowForecastValue()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            DateTime tomorrow = DateTime.Today.AddDays(1);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"
            SELECT TOP 1 PredictedSales 
            FROM DailySalesForecast 
            WHERE ForecastDate = @date";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@date", tomorrow);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        decimal forecastValue = Convert.ToDecimal(result);
                        lblTotalForecast.Text = $"Tomorrow's Forecast: Rs. {forecastValue:N2}";
                    }
                    else
                    {
                        lblTotalForecast.Text = "Tomorrow's Forecast: N/A";
                    }
                }
            }
        }

        private void btnRetrainForecast_Click(object sender, EventArgs e)
        {
            try
            {
                SalesForecastTrainer.TrainAndForecastSales();
                MessageBox.Show("✅ Forecast retrained and updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNextMonthForecastValue();
                LoadPredictedLowStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Forecast retrain failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPredictedLowStock()
        {
            DataTable table = new DataTable();

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = @"
            SELECT i.Product, i.Quantity AS CurrentStock, pf.ForecastedQuantity, i.ThresholdValue
            FROM ProductForecastHistory pf
            JOIN InventoryItems i ON pf.ItemId = i.ItemID
            WHERE (i.Quantity - pf.ForecastedQuantity) <= i.ThresholdValue
            ORDER BY (i.Quantity - pf.ForecastedQuantity)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            dgvPredictedLowStock.DataSource = null;
            dgvPredictedLowStock.Columns.Clear();
            dgvPredictedLowStock.DataSource = table;

            foreach (DataGridViewRow row in dgvPredictedLowStock.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
        }

        // ✅✅ NEW: Summary Stats Loader
        private void LoadSummaryStats()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;

            decimal totalRevenue = 0;
            decimal totalProfit = 0;
            int totalSales = 0;
            int totalItems = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"
SELECT 
    ISNULL(SUM(s.TotalAmount), 0) AS TotalRevenue,
    ISNULL(COUNT(s.SaleID), 0) AS TotalSales
FROM Sales s;

SELECT 
    ISNULL(SUM((si.PricePerUnit - ii.PurchasePrice) * si.Quantity), 0) AS TotalProfit
FROM SaleItems si
JOIN InventoryItems ii ON si.ProductName = ii.Product;

SELECT 
    ISNULL(COUNT(*), 0) AS TotalItems
FROM InventoryItems;
";




                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // First result: Revenue & Sales
                    if (reader.Read())
                    {
                        totalRevenue = reader.GetDecimal(0);
                        totalSales = reader.GetInt32(1);
                    }

                    // Move to second result: Profit
                    if (reader.NextResult() && reader.Read())
                    {
                        totalProfit = reader.GetDecimal(0);
                    }

                    // Move to third result: Total Items
                    if (reader.NextResult() && reader.Read())
                    {
                        totalItems = reader.GetInt32(0);
                    }
                }
            }

            lblTotalRevenue.Text = $"Rs. {totalRevenue:N0}";
            lblTotalProfit.Text = $"Rs. {totalProfit:N0}";
            lblTotalSales.Text = totalSales.ToString();
            lblTotalItems.Text = totalItems.ToString();
        }

        private void btnOpenFinancialReport_Click(object sender, EventArgs e)
        {
            FinancialReportForm reportForm = new FinancialReportForm();
            reportForm.ShowDialog(); // or .Show() if you want it non-modal
        }

    }
}
