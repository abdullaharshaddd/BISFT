using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PdfSharp.Pdf;
using PdfSharp.Drawing;


namespace BISFT
{
    public partial class FinancialReportForm : Form
    {
        public FinancialReportForm()
        {
            InitializeComponent();
            this.Bounds = Screen.PrimaryScreen.WorkingArea;
        }


        private void FinancialReportForm_Load(object sender, EventArgs e)
        {
            LoadRevenueProfitChart();
            LoadSummaryStats();
            LoadCategoryWiseRevenueChart();
            LoadTop5Products();
            LoadProfitMargin();
            btnCompare.Click += btnCompare_Click;
            LoadLowPerformingProducts();
            btnBackToDashboard.Click += btnBackToDashboard_Click;

        }

        private void LoadRevenueProfitChart()
        {
            chartRevenueProfit.Series.Clear();
            chartRevenueProfit.ChartAreas.Clear();
            chartRevenueProfit.Titles.Clear();

            ChartArea chartArea = new ChartArea("MainArea");
            chartRevenueProfit.ChartAreas.Add(chartArea);

            Series revenueSeries = new Series("Revenue")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                BorderWidth = 2,
                XValueType = ChartValueType.String
            };

            Series profitSeries = new Series("Profit")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.OrangeRed,
                BorderWidth = 2,
                XValueType = ChartValueType.String
            };

            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;

            string query = @"
                SELECT 
                    FORMAT(S.SaleDate, 'yyyy-MM') AS SaleMonth,
                    SUM(SI.Quantity * SI.PricePerUnit) AS Revenue,
                    SUM(SI.Quantity * (SI.PricePerUnit - ISNULL(II.PurchasePrice, 0))) AS Profit
                FROM SaleItems SI
                INNER JOIN Sales S ON SI.SaleID = S.SaleID
                LEFT JOIN InventoryItems II ON SI.ProductName = II.Product
                GROUP BY FORMAT(S.SaleDate, 'yyyy-MM')
                ORDER BY SaleMonth;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string month = reader["SaleMonth"].ToString();
                            decimal revenue = reader.GetDecimal(reader.GetOrdinal("Revenue"));
                            decimal profit = reader.GetDecimal(reader.GetOrdinal("Profit"));

                            revenueSeries.Points.AddXY(month, revenue);
                            profitSeries.Points.AddXY(month, profit);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading chart data:\n" + ex.Message);
                }
            }

            chartRevenueProfit.Series.Add(revenueSeries);
            chartRevenueProfit.Series.Add(profitSeries);
            chartRevenueProfit.Titles.Add("Monthly Revenue vs Profit");
        }

        private void GenerateReportByDate()
        {
            chartRevenueProfit.Series.Clear();
            chartRevenueProfit.ChartAreas.Clear();
            chartRevenueProfit.Titles.Clear();

            ChartArea chartArea = new ChartArea("MainArea");
            chartRevenueProfit.ChartAreas.Add(chartArea);

            Series revenueSeries = new Series("Revenue")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                BorderWidth = 2,
                XValueType = ChartValueType.String
            };

            Series profitSeries = new Series("Profit")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.OrangeRed,
                BorderWidth = 2,
                XValueType = ChartValueType.String
            };

            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;

            string query = @"
        SELECT 
            FORMAT(S.SaleDate, 'yyyy-MM-dd') AS SaleDate,
            SUM(SI.Quantity * SI.PricePerUnit) AS Revenue,
            SUM(SI.Quantity * (SI.PricePerUnit - ISNULL(II.PurchasePrice, 0))) AS Profit
        FROM SaleItems SI
        INNER JOIN Sales S ON SI.SaleID = S.SaleID
        LEFT JOIN InventoryItems II ON SI.ProductName = II.Product
        WHERE S.SaleDate BETWEEN @FromDate AND @ToDate
        GROUP BY FORMAT(S.SaleDate, 'yyyy-MM-dd')
        ORDER BY SaleDate;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FromDate", dtpFromDate.Value.Date);
                cmd.Parameters.AddWithValue("@ToDate", dtpToDate.Value.Date);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string date = reader["SaleDate"].ToString();
                            decimal revenue = reader.GetDecimal(reader.GetOrdinal("Revenue"));
                            decimal profit = reader.GetDecimal(reader.GetOrdinal("Profit"));

                            revenueSeries.Points.AddXY(date, revenue);
                            profitSeries.Points.AddXY(date, profit);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error generating report:\n" + ex.Message);
                }
            }

            chartRevenueProfit.Series.Add(revenueSeries);
            chartRevenueProfit.Series.Add(profitSeries);
            chartRevenueProfit.Titles.Add("Revenue vs Profit (Filtered)");
        }


        private void LoadSummaryStats()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;

            string query = @"
                SELECT 
                    COUNT(DISTINCT SI.SaleID) AS TotalSales,
                    SUM(SI.Quantity * SI.PricePerUnit) AS TotalRevenue,
                    SUM(SI.Quantity * (SI.PricePerUnit - ISNULL(II.PurchasePrice, 0))) AS TotalProfit,
                    SUM(SI.Quantity) AS TotalItems
                FROM SaleItems SI
                LEFT JOIN InventoryItems II ON SI.ProductName = II.Product;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTotalSales.Text = "Sales: " + reader["TotalSales"].ToString();
                            lblTotalRevenue.Text = "Revenue: " + string.Format("{0:C}", reader["TotalRevenue"]);
                            lblTotalProfit.Text = "Profit: " + string.Format("{0:C}", reader["TotalProfit"]);
                            lblTotalItems.Text = "Items: " + reader["TotalItems"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading summary stats:\n" + ex.Message);
                }
            }
        }

        private void LoadCategoryWiseRevenueChart()
        {
            chartCategoryRevenue.Series.Clear();
            chartCategoryRevenue.ChartAreas.Clear();
            chartCategoryRevenue.Titles.Clear();

            ChartArea area = new ChartArea("PieArea");
            chartCategoryRevenue.ChartAreas.Add(area);

            Series series = new Series("CategoryRevenue")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black
            };

            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            string query = @"
                SELECT 
                    II.Category,
                    SUM(SI.Quantity * SI.PricePerUnit) AS TotalRevenue
                FROM SaleItems SI
                JOIN InventoryItems II ON SI.ProductName = II.Product
                GROUP BY II.Category
                ORDER BY TotalRevenue DESC;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string category = reader["Category"].ToString();
                            decimal revenue = reader.GetDecimal(reader.GetOrdinal("TotalRevenue"));
                            series.Points.AddXY(category, revenue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading category-wise revenue:\n" + ex.Message);
                }
            }

            chartCategoryRevenue.Series.Add(series);
            chartCategoryRevenue.Titles.Add("Revenue by Product Category");
        }

        private void LoadTop5Products()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            string query = @"
        SELECT 
            ProductName,
            SUM(Quantity * PricePerUnit) AS TotalRevenue
        FROM SaleItems
        GROUP BY ProductName
        ORDER BY TotalRevenue DESC;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvTopProducts.DataSource = dt;

                        dgvTopProducts.Columns[0].HeaderText = "Product";
                        dgvTopProducts.Columns[1].HeaderText = "Revenue (₨)";
                        dgvTopProducts.Columns[1].DefaultCellStyle.Format = "N2";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading top products:\n" + ex.Message);
                }
            }
        }


        private void LoadLowPerformingProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            string query = @"
        SELECT TOP 5 
            ProductName,
            SUM(SI.Quantity * (SI.PricePerUnit - ISNULL(II.PurchasePrice, 0))) AS TotalProfit
        FROM SaleItems SI
        LEFT JOIN InventoryItems II ON SI.ProductName = II.Product
        GROUP BY ProductName
        ORDER BY TotalProfit ASC;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvLowProducts.DataSource = dt;

                        // Set column headers just like Top Products
                        dgvLowProducts.Columns[0].HeaderText = "Product";
                        dgvLowProducts.Columns[1].HeaderText = "Profit (₨)";
                        dgvLowProducts.Columns[1].DefaultCellStyle.Format = "N2";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading low-performing products:\n" + ex.Message);
                }
            }
        }


        private void LoadProfitMargin()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;

            string query = @"
                SELECT 
                    SUM(SI.Quantity * SI.PricePerUnit) AS TotalRevenue,
                    SUM(SI.Quantity * (SI.PricePerUnit - ISNULL(II.PurchasePrice, 0))) AS TotalProfit
                FROM SaleItems SI
                LEFT JOIN InventoryItems II ON SI.ProductName = II.Product;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal totalRevenue = reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);
                            decimal totalProfit = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1);

                            string marginText = "N/A";
                            if (totalRevenue > 0)
                            {
                                decimal margin = (totalProfit / totalRevenue) * 100;
                                marginText = $"{margin:F2}%";
                            }

                            lblProfitMargin.Text = marginText;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading profit margin:\n" + ex.Message);
                }
            }
        }


        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            if (dgvTopProducts.Rows.Count == 0 && dgvLowProducts.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = "ProductPerformanceReport.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        // Write headers
                        sw.WriteLine("Top Product,Revenue (₨),Low Product,Profit (₨)");

                        int maxRows = Math.Max(dgvTopProducts.Rows.Count, dgvLowProducts.Rows.Count);

                        for (int i = 0; i < maxRows; i++)
                        {
                            string topProduct = "", topRevenue = "", lowProduct = "", lowProfit = "";

                            if (i < dgvTopProducts.Rows.Count)
                            {
                                topProduct = dgvTopProducts.Rows[i].Cells[0].Value?.ToString();
                                topRevenue = dgvTopProducts.Rows[i].Cells[1].Value?.ToString();
                            }

                            if (i < dgvLowProducts.Rows.Count)
                            {
                                lowProduct = dgvLowProducts.Rows[i].Cells[0].Value?.ToString();
                                lowProfit = dgvLowProducts.Rows[i].Cells[1].Value?.ToString();
                            }

                            sw.WriteLine($"{topProduct},{topRevenue},{lowProduct},{lowProfit}");
                        }
                    }

                    MessageBox.Show("✅ CSV export completed with side-by-side layout!", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting:\n" + ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        // ✅ PDF Export logic now wired to the right event name
        private void btnExportPDF_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF files (*.pdf)|*.pdf";
                    sfd.FileName = "FinancialReport.pdf";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap bmp = new Bitmap(this.Width, this.Height);
                        this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));

                        PdfDocument doc = new PdfDocument();
                        PdfPage page = doc.AddPage();
                        page.Width = XUnit.FromPoint(bmp.Width);
                        page.Height = XUnit.FromPoint(bmp.Height);

                        XGraphics gfx = XGraphics.FromPdfPage(page);
                        XImage img = XImage.FromGdiPlusImage(bmp);
                        gfx.DrawImage(img, 0, 0);

                        doc.Save(sfd.FileName);
                        MessageBox.Show("✅ PDF exported successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to PDF:\n" + ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblTotalSales_Click(object sender, EventArgs e)
        {

        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            string selectedOption = cmbCompareType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedOption))
            {
                MessageBox.Show("Please select a comparison type.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            DateTime now = DateTime.Now;
            string currentMonth = now.ToString("yyyy-MM");
            string previousMonth = now.AddMonths(-1).ToString("yyyy-MM");
            string lastYearSameMonth = now.AddYears(-1).ToString("yyyy-MM");



       string query = @"
    SELECT 
        FORMAT(S.SaleDate, 'yyyy-MM') AS SaleMonth,
        SUM(SI.Quantity * SI.PricePerUnit) AS Revenue
    FROM SaleItems SI
    INNER JOIN Sales S ON SI.SaleID = S.SaleID
    WHERE FORMAT(S.SaleDate, 'yyyy-MM') IN (@Month1, @Month2)
    GROUP BY FORMAT(S.SaleDate, 'yyyy-MM');";


            string month1 = "", month2 = "";
            string labelText = "";

            if (selectedOption.Contains("Last Month"))
            {
                month1 = currentMonth;
                month2 = previousMonth;
                labelText = "This Month vs Last Month: ";
            }
            else
            {
                month1 = currentMonth;
                month2 = lastYearSameMonth;
                labelText = "This Month vs Same Month Last Year: ";
            }

            decimal revenue1 = 0, revenue2 = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Month1", month1);
                cmd.Parameters.AddWithValue("@Month2", month2);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string month = reader["SaleMonth"].ToString();
                            decimal revenue = reader.GetDecimal(reader.GetOrdinal("Revenue"));
                            if (month == month1)
                                revenue1 = revenue;
                            else if (month == month2)
                                revenue2 = revenue;
                        }
                    }

                    decimal diff = revenue1 - revenue2;
                    string trend = diff > 0 ? "📈 Increase" : (diff < 0 ? "📉 Decrease" : "No Change");
                    lblComparisonResult.Text = $"{labelText} {trend} ({Math.Abs(diff):C})";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during comparison:\n" + ex.Message);
                }
            }
        }

        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            this.Hide(); // hide current form
            Dashboard dashboardForm = new Dashboard();
            dashboardForm.Show();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateReportByDate();
        }

    }
}
