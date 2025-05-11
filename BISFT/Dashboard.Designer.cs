namespace BISFT
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.button2 = new System.Windows.Forms.Button();
            this.chartClusters = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnRetrainModel = new System.Windows.Forms.Button();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.lblTotalForecast = new System.Windows.Forms.Label();
            this.btnsales = new System.Windows.Forms.Button();
            this.btncustomers = new System.Windows.Forms.Button();
            this.btninventory = new System.Windows.Forms.Button();
            this.btnRetrainForecast = new System.Windows.Forms.Button();
            this.chartSalesTrend = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvPredictedLowStock = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnChatbot = new System.Windows.Forms.Button();
            this.btnOpenFinancialReport = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblForecastDate = new System.Windows.Forms.Label();
            this.lblTotalSales = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.lblTotalItems = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartClusters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSalesTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPredictedLowStock)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(597, 303);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(8, 8);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // chartClusters
            // 
            this.chartClusters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartClusters.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartClusters.Legends.Add(legend1);
            this.chartClusters.Location = new System.Drawing.Point(1244, 280);
            this.chartClusters.Name = "chartClusters";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartClusters.Series.Add(series1);
            this.chartClusters.Size = new System.Drawing.Size(442, 286);
            this.chartClusters.TabIndex = 4;
            this.chartClusters.Text = "chart1";
            this.chartClusters.Click += new System.EventHandler(this.chartClusters_Click);
            // 
            // btnRetrainModel
            // 
            this.btnRetrainModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRetrainModel.Location = new System.Drawing.Point(3, 56);
            this.btnRetrainModel.Name = "btnRetrainModel";
            this.btnRetrainModel.Size = new System.Drawing.Size(197, 48);
            this.btnRetrainModel.TabIndex = 5;
            this.btnRetrainModel.Text = "Retrain Models";
            this.btnRetrainModel.UseVisualStyleBackColor = true;
            this.btnRetrainModel.Click += new System.EventHandler(this.btnRetrainModel_Click);
            // 
            // dgvLowStock
            // 
            this.dgvLowStock.AllowUserToAddRows = false;
            this.dgvLowStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLowStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLowStock.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvLowStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLowStock.Location = new System.Drawing.Point(1244, 610);
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.ReadOnly = true;
            this.dgvLowStock.RowHeadersVisible = false;
            this.dgvLowStock.RowHeadersWidth = 51;
            this.dgvLowStock.RowTemplate.Height = 24;
            this.dgvLowStock.Size = new System.Drawing.Size(442, 205);
            this.dgvLowStock.TabIndex = 6;
            this.dgvLowStock.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLowStock_CellContentClick);
            // 
            // lblTotalForecast
            // 
            this.lblTotalForecast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalForecast.AutoSize = true;
            this.lblTotalForecast.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalForecast.Location = new System.Drawing.Point(1668, 165);
            this.lblTotalForecast.Name = "lblTotalForecast";
            this.lblTotalForecast.Size = new System.Drawing.Size(173, 28);
            this.lblTotalForecast.TabIndex = 7;
            this.lblTotalForecast.Text = "Forecast Loading...";
            // 
            // btnsales
            // 
            this.btnsales.BackColor = System.Drawing.Color.Transparent;
            this.btnsales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsales.Location = new System.Drawing.Point(3, 47);
            this.btnsales.Name = "btnsales";
            this.btnsales.Size = new System.Drawing.Size(197, 38);
            this.btnsales.TabIndex = 3;
            this.btnsales.Text = "Sales";
            this.btnsales.UseVisualStyleBackColor = false;
            this.btnsales.Click += new System.EventHandler(this.btnsales_Click);
            // 
            // btncustomers
            // 
            this.btncustomers.BackColor = System.Drawing.Color.Transparent;
            this.btncustomers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncustomers.Location = new System.Drawing.Point(3, 91);
            this.btncustomers.Name = "btncustomers";
            this.btncustomers.Size = new System.Drawing.Size(197, 41);
            this.btncustomers.TabIndex = 2;
            this.btncustomers.Text = "Customers";
            this.btncustomers.UseVisualStyleBackColor = false;
            this.btncustomers.Click += new System.EventHandler(this.btncustomers_Click);
            // 
            // btninventory
            // 
            this.btninventory.BackColor = System.Drawing.Color.Transparent;
            this.btninventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btninventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btninventory.Location = new System.Drawing.Point(3, 3);
            this.btninventory.Name = "btninventory";
            this.btninventory.Size = new System.Drawing.Size(197, 38);
            this.btninventory.TabIndex = 0;
            this.btninventory.Text = "Inventory";
            this.btninventory.UseVisualStyleBackColor = false;
            this.btninventory.Click += new System.EventHandler(this.btninventory_Click);
            // 
            // btnRetrainForecast
            // 
            this.btnRetrainForecast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRetrainForecast.Location = new System.Drawing.Point(3, 3);
            this.btnRetrainForecast.Name = "btnRetrainForecast";
            this.btnRetrainForecast.Size = new System.Drawing.Size(197, 47);
            this.btnRetrainForecast.TabIndex = 8;
            this.btnRetrainForecast.Text = "🔁 Retrain Forecast Model";
            this.btnRetrainForecast.UseVisualStyleBackColor = true;
            this.btnRetrainForecast.Click += new System.EventHandler(this.btnRetrainForecast_Click);
            // 
            // chartSalesTrend
            // 
            this.chartSalesTrend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartSalesTrend.BackColor = System.Drawing.Color.Transparent;
            this.chartSalesTrend.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea1";
            this.chartSalesTrend.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartSalesTrend.Legends.Add(legend2);
            this.chartSalesTrend.Location = new System.Drawing.Point(354, 242);
            this.chartSalesTrend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartSalesTrend.Name = "chartSalesTrend";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartSalesTrend.Series.Add(series2);
            this.chartSalesTrend.Size = new System.Drawing.Size(856, 338);
            this.chartSalesTrend.TabIndex = 10;
            this.chartSalesTrend.Text = "chart1";
            // 
            // dgvPredictedLowStock
            // 
            this.dgvPredictedLowStock.AllowUserToAddRows = false;
            this.dgvPredictedLowStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPredictedLowStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPredictedLowStock.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPredictedLowStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPredictedLowStock.Location = new System.Drawing.Point(369, 610);
            this.dgvPredictedLowStock.Name = "dgvPredictedLowStock";
            this.dgvPredictedLowStock.ReadOnly = true;
            this.dgvPredictedLowStock.RowHeadersVisible = false;
            this.dgvPredictedLowStock.RowHeadersWidth = 51;
            this.dgvPredictedLowStock.RowTemplate.Height = 24;
            this.dgvPredictedLowStock.Size = new System.Drawing.Size(841, 205);
            this.dgvPredictedLowStock.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnChatbot, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btninventory, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnsales, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btncustomers, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnOpenFinancialReport, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(66, 218);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(203, 222);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // btnChatbot
            // 
            this.btnChatbot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChatbot.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnChatbot.Location = new System.Drawing.Point(3, 179);
            this.btnChatbot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChatbot.Name = "btnChatbot";
            this.btnChatbot.Size = new System.Drawing.Size(197, 41);
            this.btnChatbot.TabIndex = 5;
            this.btnChatbot.Text = "AI Assistant";
            this.btnChatbot.UseVisualStyleBackColor = true;
            this.btnChatbot.Click += new System.EventHandler(this.btnChatbot_Click);
            // 
            // btnOpenFinancialReport
            // 
            this.btnOpenFinancialReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFinancialReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOpenFinancialReport.Location = new System.Drawing.Point(3, 137);
            this.btnOpenFinancialReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenFinancialReport.Name = "btnOpenFinancialReport";
            this.btnOpenFinancialReport.Size = new System.Drawing.Size(197, 38);
            this.btnOpenFinancialReport.TabIndex = 4;
            this.btnOpenFinancialReport.Text = "Financial Report";
            this.btnOpenFinancialReport.UseVisualStyleBackColor = true;
            this.btnOpenFinancialReport.Click += new System.EventHandler(this.btnOpenFinancialReport_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.5F));
            this.tableLayoutPanel2.Controls.Add(this.btnRetrainForecast, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnRetrainModel, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(66, 510);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(203, 107);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // lblForecastDate
            // 
            this.lblForecastDate.AutoSize = true;
            this.lblForecastDate.Location = new System.Drawing.Point(1579, 94);
            this.lblForecastDate.Name = "lblForecastDate";
            this.lblForecastDate.Size = new System.Drawing.Size(36, 16);
            this.lblForecastDate.TabIndex = 14;
            this.lblForecastDate.Text = "Date";
            // 
            // lblTotalSales
            // 
            this.lblTotalSales.AutoSize = true;
            this.lblTotalSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSales.Location = new System.Drawing.Point(464, 164);
            this.lblTotalSales.Name = "lblTotalSales";
            this.lblTotalSales.Size = new System.Drawing.Size(26, 29);
            this.lblTotalSales.TabIndex = 15;
            this.lblTotalSales.Text = "0";
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenue.Location = new System.Drawing.Point(753, 164);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(67, 29);
            this.lblTotalRevenue.TabIndex = 16;
            this.lblTotalRevenue.Text = "Rs. 0";
            // 
            // lblTotalProfit
            // 
            this.lblTotalProfit.AutoSize = true;
            this.lblTotalProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProfit.Location = new System.Drawing.Point(1057, 165);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(67, 29);
            this.lblTotalProfit.TabIndex = 17;
            this.lblTotalProfit.Text = "Rs. 0";
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalItems.Location = new System.Drawing.Point(1377, 164);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(26, 29);
            this.lblTotalItems.TabIndex = 18;
            this.lblTotalItems.Text = "0";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1710, 840);
            this.Controls.Add(this.lblTotalItems);
            this.Controls.Add(this.lblTotalProfit);
            this.Controls.Add(this.lblTotalRevenue);
            this.Controls.Add(this.lblTotalSales);
            this.Controls.Add(this.lblForecastDate);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dgvPredictedLowStock);
            this.Controls.Add(this.chartSalesTrend);
            this.Controls.Add(this.lblTotalForecast);
            this.Controls.Add(this.dgvLowStock);
            this.Controls.Add(this.chartClusters);
            this.Controls.Add(this.button2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartClusters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSalesTrend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPredictedLowStock)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btninventory;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btncustomers;
        private System.Windows.Forms.Button btnsales;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartClusters;
        private System.Windows.Forms.Button btnRetrainModel;
        private System.Windows.Forms.DataGridView dgvLowStock;
        private System.Windows.Forms.Label lblTotalForecast;
        private System.Windows.Forms.Button btnRetrainForecast;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalesTrend;
        private System.Windows.Forms.DataGridView dgvPredictedLowStock;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblForecastDate;
        private System.Windows.Forms.Button btnChatbot;
        private System.Windows.Forms.Button btnOpenFinancialReport;
        private System.Windows.Forms.Label lblTotalSales;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblTotalProfit;
        private System.Windows.Forms.Label lblTotalItems;
    }
}

