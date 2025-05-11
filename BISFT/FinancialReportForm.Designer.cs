namespace BISFT
{
    partial class FinancialReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 50000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 70000D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 10000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 15000D);
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.lblTitle = new System.Windows.Forms.Label();
            this.chartRevenueProfit = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblTotalSales = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.chartCategoryRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvTopProducts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProfitMargin = new System.Windows.Forms.Label();
            this.btnExportCSV = new System.Windows.Forms.Button();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.grpComparison = new System.Windows.Forms.GroupBox();
            this.cmbCompareType = new System.Windows.Forms.ComboBox();
            this.lblComparisonResult = new System.Windows.Forms.Label();
            this.btnCompare = new System.Windows.Forms.Button();
            this.dgvLowProducts = new System.Windows.Forms.DataGridView();
            this.btnBackToDashboard = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCategoryRevenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(788, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(218, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Financial Report";
            // 
            // chartRevenueProfit
            // 
            chartArea1.Name = "MainArea";
            this.chartRevenueProfit.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartRevenueProfit.Legends.Add(legend1);
            this.chartRevenueProfit.Location = new System.Drawing.Point(166, 186);
            this.chartRevenueProfit.Name = "chartRevenueProfit";
            series1.BorderWidth = 2;
            series1.ChartArea = "MainArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Green;
            series1.Legend = "Legend1";
            series1.Name = "Revenue";
            dataPoint1.AxisLabel = "2024-01";
            dataPoint2.AxisLabel = "2024-02";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series2.BorderWidth = 2;
            series2.ChartArea = "MainArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.OrangeRed;
            series2.Legend = "Legend1";
            series2.Name = "Profit";
            dataPoint3.AxisLabel = "2024-01";
            dataPoint4.AxisLabel = "2024-02";
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            this.chartRevenueProfit.Series.Add(series1);
            this.chartRevenueProfit.Series.Add(series2);
            this.chartRevenueProfit.Size = new System.Drawing.Size(623, 378);
            this.chartRevenueProfit.TabIndex = 1;
            this.chartRevenueProfit.Text = "chartRevenueProfit";
            // 
            // lblTotalSales
            // 
            this.lblTotalSales.AutoSize = true;
            this.lblTotalSales.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSales.Location = new System.Drawing.Point(181, 79);
            this.lblTotalSales.Name = "lblTotalSales";
            this.lblTotalSales.Size = new System.Drawing.Size(88, 25);
            this.lblTotalSales.TabIndex = 2;
            this.lblTotalSales.Text = "Sales: 0";
            this.lblTotalSales.Click += new System.EventHandler(this.lblTotalSales_Click);
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenue.Location = new System.Drawing.Point(341, 79);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(125, 25);
            this.lblTotalRevenue.TabIndex = 3;
            this.lblTotalRevenue.Text = "Revenue: 0";
            // 
            // lblTotalProfit
            // 
            this.lblTotalProfit.AutoSize = true;
            this.lblTotalProfit.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProfit.Location = new System.Drawing.Point(655, 79);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(96, 25);
            this.lblTotalProfit.TabIndex = 4;
            this.lblTotalProfit.Text = "Profit: 0";
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalItems.Location = new System.Drawing.Point(942, 79);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(94, 25);
            this.lblTotalItems.TabIndex = 5;
            this.lblTotalItems.Text = "Items: 0";
            // 
            // chartCategoryRevenue
            // 
            legend2.Name = "Legend1";
            this.chartCategoryRevenue.Legends.Add(legend2);
            this.chartCategoryRevenue.Location = new System.Drawing.Point(795, 186);
            this.chartCategoryRevenue.Name = "chartCategoryRevenue";
            this.chartCategoryRevenue.Size = new System.Drawing.Size(385, 378);
            this.chartCategoryRevenue.TabIndex = 6;
            this.chartCategoryRevenue.Text = "chart1";
            // 
            // dgvTopProducts
            // 
            this.dgvTopProducts.AllowUserToAddRows = false;
            this.dgvTopProducts.AllowUserToDeleteRows = false;
            this.dgvTopProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopProducts.Location = new System.Drawing.Point(1184, 186);
            this.dgvTopProducts.Name = "dgvTopProducts";
            this.dgvTopProducts.ReadOnly = true;
            this.dgvTopProducts.RowHeadersVisible = false;
            this.dgvTopProducts.RowHeadersWidth = 51;
            this.dgvTopProducts.RowTemplate.Height = 24;
            this.dgvTopProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopProducts.Size = new System.Drawing.Size(295, 301);
            this.dgvTopProducts.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1228, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Profit Margin: ";
            // 
            // lblProfitMargin
            // 
            this.lblProfitMargin.AutoSize = true;
            this.lblProfitMargin.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfitMargin.Location = new System.Drawing.Point(1411, 79);
            this.lblProfitMargin.Name = "lblProfitMargin";
            this.lblProfitMargin.Size = new System.Drawing.Size(48, 25);
            this.lblProfitMargin.TabIndex = 9;
            this.lblProfitMargin.Text = "0%";
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Location = new System.Drawing.Point(1277, 503);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(127, 61);
            this.btnExportCSV.TabIndex = 10;
            this.btnExportCSV.Text = "Export as CSV ";
            this.btnExportCSV.UseVisualStyleBackColor = true;
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(1666, 78);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(161, 32);
            this.btnExportPDF.TabIndex = 11;
            this.btnExportPDF.Text = "Export as PDF";
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click_1);
            // 
            // grpComparison
            // 
            this.grpComparison.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpComparison.Location = new System.Drawing.Point(1485, 431);
            this.grpComparison.Name = "grpComparison";
            this.grpComparison.Size = new System.Drawing.Size(160, 56);
            this.grpComparison.TabIndex = 12;
            this.grpComparison.TabStop = false;
            this.grpComparison.Text = "Compare Periods";
            // 
            // cmbCompareType
            // 
            this.cmbCompareType.FormattingEnabled = true;
            this.cmbCompareType.Items.AddRange(new object[] {
            "This Month vs Last Month",
            "This Month vs Same Month Last Year"});
            this.cmbCompareType.Location = new System.Drawing.Point(1651, 463);
            this.cmbCompareType.Name = "cmbCompareType";
            this.cmbCompareType.Size = new System.Drawing.Size(188, 24);
            this.cmbCompareType.TabIndex = 13;
            // 
            // lblComparisonResult
            // 
            this.lblComparisonResult.AutoSize = true;
            this.lblComparisonResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComparisonResult.Location = new System.Drawing.Point(1497, 510);
            this.lblComparisonResult.Name = "lblComparisonResult";
            this.lblComparisonResult.Size = new System.Drawing.Size(0, 16);
            this.lblComparisonResult.TabIndex = 14;
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(1712, 434);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(127, 23);
            this.btnCompare.TabIndex = 15;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // dgvLowProducts
            // 
            this.dgvLowProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLowProducts.Location = new System.Drawing.Point(1485, 186);
            this.dgvLowProducts.Name = "dgvLowProducts";
            this.dgvLowProducts.RowHeadersWidth = 51;
            this.dgvLowProducts.RowTemplate.Height = 24;
            this.dgvLowProducts.Size = new System.Drawing.Size(342, 205);
            this.dgvLowProducts.TabIndex = 16;
            // 
            // btnBackToDashboard
            // 
            this.btnBackToDashboard.Location = new System.Drawing.Point(23, 13);
            this.btnBackToDashboard.Name = "btnBackToDashboard";
            this.btnBackToDashboard.Size = new System.Drawing.Size(100, 32);
            this.btnBackToDashboard.TabIndex = 17;
            this.btnBackToDashboard.Text = "Back";
            this.btnBackToDashboard.UseVisualStyleBackColor = true;
            this.btnBackToDashboard.Click += new System.EventHandler(this.btnBackToDashboard_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(232, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "From:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(620, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "To:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(293, 139);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(281, 22);
            this.dtpFromDate.TabIndex = 20;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(660, 139);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(275, 22);
            this.dtpToDate.TabIndex = 21;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(947, 137);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(233, 23);
            this.btnGenerate.TabIndex = 22;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // FinancialReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1872, 657);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBackToDashboard);
            this.Controls.Add(this.dgvLowProducts);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.lblComparisonResult);
            this.Controls.Add(this.cmbCompareType);
            this.Controls.Add(this.grpComparison);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.btnExportCSV);
            this.Controls.Add(this.lblProfitMargin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTopProducts);
            this.Controls.Add(this.chartCategoryRevenue);
            this.Controls.Add(this.lblTotalItems);
            this.Controls.Add(this.lblTotalProfit);
            this.Controls.Add(this.lblTotalRevenue);
            this.Controls.Add(this.lblTotalSales);
            this.Controls.Add(this.chartRevenueProfit);
            this.Controls.Add(this.lblTitle);
            this.Name = "FinancialReportForm";
            this.Text = "FinancialReportForm";
            this.Load += new System.EventHandler(this.FinancialReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCategoryRevenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueProfit;
        private System.Windows.Forms.Label lblTotalSales;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblTotalProfit;
        private System.Windows.Forms.Label lblTotalItems;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCategoryRevenue;
        private System.Windows.Forms.DataGridView dgvTopProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProfitMargin;
        private System.Windows.Forms.Button btnExportCSV;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.GroupBox grpComparison;
        private System.Windows.Forms.ComboBox cmbCompareType;
        private System.Windows.Forms.Label lblComparisonResult;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.DataGridView dgvLowProducts;
        private System.Windows.Forms.Button btnBackToDashboard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnGenerate;
    }
}
