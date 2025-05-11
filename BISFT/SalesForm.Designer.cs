namespace BISFT
{
    partial class SalesForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.btnAddSale = new System.Windows.Forms.Button();
            this.lblTotalSales = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.chartSalesForecast = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSalesForecast)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1415, 548);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Search 🔎";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(643, 542);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(750, 26);
            this.txtSearch.TabIndex = 12;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(29, 111);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(180, 59);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(290, 542);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 32);
            this.label5.TabIndex = 30;
            this.label5.Text = "Sales";
            // 
            // dgvSales
            // 
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AllowUserToDeleteRows = false;
            this.dgvSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSales.BackgroundColor = System.Drawing.Color.Snow;
            this.dgvSales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.Location = new System.Drawing.Point(281, 584);
            this.dgvSales.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowHeadersWidth = 62;
            this.dgvSales.RowTemplate.Height = 28;
            this.dgvSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSales.Size = new System.Drawing.Size(1631, 455);
            this.dgvSales.TabIndex = 9;
            // 
            // btnAddSale
            // 
            this.btnAddSale.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnAddSale.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddSale.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddSale.Location = new System.Drawing.Point(1747, 519);
            this.btnAddSale.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddSale.Name = "btnAddSale";
            this.btnAddSale.Size = new System.Drawing.Size(165, 53);
            this.btnAddSale.TabIndex = 27;
            this.btnAddSale.Text = "Add Sale";
            this.btnAddSale.UseVisualStyleBackColor = false;
            this.btnAddSale.Click += new System.EventHandler(this.btnAddSale_Click);
            // 
            // lblTotalSales
            // 
            this.lblTotalSales.AutoSize = true;
            this.lblTotalSales.Location = new System.Drawing.Point(1071, 65);
            this.lblTotalSales.Name = "lblTotalSales";
            this.lblTotalSales.Size = new System.Drawing.Size(40, 20);
            this.lblTotalSales.TabIndex = 1;
            this.lblTotalSales.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(905, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Sales";
            // 
            // cmbProduct
            // 
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(1647, 127);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(265, 28);
            this.cmbProduct.TabIndex = 37;
            // 
            // chartSalesForecast
            // 
            chartArea2.Name = "ChartArea1";
            this.chartSalesForecast.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartSalesForecast.Legends.Add(legend2);
            this.chartSalesForecast.Location = new System.Drawing.Point(281, 176);
            this.chartSalesForecast.Name = "chartSalesForecast";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartSalesForecast.Series.Add(series2);
            this.chartSalesForecast.Size = new System.Drawing.Size(1631, 300);
            this.chartSalesForecast.TabIndex = 38;
            this.chartSalesForecast.Text = "chart1";
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1050);
            this.Controls.Add(this.chartSalesForecast);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDashboard);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTotalSales);
            this.Controls.Add(this.cmbProduct);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddSale);
            this.Controls.Add(this.dgvSales);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SalesForm";
            this.Text = "SalesForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSalesForecast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.Button btnAddSale;
        private System.Windows.Forms.Label lblTotalSales;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalesForecast;
    }
}