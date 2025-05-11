namespace BISFT
{
    partial class InventoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryForm));
            this.btnorders = new System.Windows.Forms.Button();
            this.btndashboard = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Availibility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchasePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThresholdValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateAdded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastUpdated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnadd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdateProduct = new System.Windows.Forms.Button();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.lblTotalProductsCount = new System.Windows.Forms.Label();
            this.lblLowStockC = new System.Windows.Forms.Label();
            this.btncustomers = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnorders
            // 
            this.btnorders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnorders.BackgroundImage = global::BISFT.Properties.Resources.or;
            this.btnorders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnorders.Location = new System.Drawing.Point(30, 379);
            this.btnorders.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnorders.Name = "btnorders";
            this.btnorders.Size = new System.Drawing.Size(183, 60);
            this.btnorders.TabIndex = 4;
            this.btnorders.UseVisualStyleBackColor = false;
            this.btnorders.Click += new System.EventHandler(this.btnorders_Click);
            // 
            // btndashboard
            // 
            this.btndashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btndashboard.BackgroundImage = global::BISFT.Properties.Resources.dash;
            this.btndashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndashboard.Location = new System.Drawing.Point(30, 252);
            this.btndashboard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btndashboard.Name = "btndashboard";
            this.btndashboard.Size = new System.Drawing.Size(183, 61);
            this.btndashboard.TabIndex = 0;
            this.btndashboard.UseVisualStyleBackColor = false;
            this.btndashboard.Click += new System.EventHandler(this.btndashboard_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.Product,
            this.Description,
            this.Category,
            this.Supplier,
            this.Quantity,
            this.Availibility,
            this.PurchasePrice,
            this.SellingPrice,
            this.ThresholdValue,
            this.ExpiryDate,
            this.DateAdded,
            this.LastUpdated});
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.Location = new System.Drawing.Point(220, 256);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1339, 430);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ItemID
            // 
            this.ItemID.HeaderText = "Item ID";
            this.ItemID.MinimumWidth = 6;
            this.ItemID.Name = "ItemID";
            this.ItemID.Width = 125;
            // 
            // Product
            // 
            this.Product.HeaderText = "Product Name";
            this.Product.MinimumWidth = 6;
            this.Product.Name = "Product";
            this.Product.Width = 125;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.Width = 125;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.MinimumWidth = 6;
            this.Category.Name = "Category";
            this.Category.Width = 125;
            // 
            // Supplier
            // 
            this.Supplier.HeaderText = "Supplier";
            this.Supplier.MinimumWidth = 6;
            this.Supplier.Name = "Supplier";
            this.Supplier.Width = 125;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MinimumWidth = 6;
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 125;
            // 
            // Availibility
            // 
            this.Availibility.HeaderText = "Availibility";
            this.Availibility.MinimumWidth = 6;
            this.Availibility.Name = "Availibility";
            this.Availibility.Width = 125;
            // 
            // PurchasePrice
            // 
            this.PurchasePrice.HeaderText = "Purchase Price";
            this.PurchasePrice.MinimumWidth = 6;
            this.PurchasePrice.Name = "PurchasePrice";
            this.PurchasePrice.Width = 125;
            // 
            // SellingPrice
            // 
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.MinimumWidth = 6;
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.Width = 125;
            // 
            // ThresholdValue
            // 
            this.ThresholdValue.HeaderText = "Threshold Value";
            this.ThresholdValue.MinimumWidth = 6;
            this.ThresholdValue.Name = "ThresholdValue";
            this.ThresholdValue.Width = 125;
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.HeaderText = "Expiry Date";
            this.ExpiryDate.MinimumWidth = 6;
            this.ExpiryDate.Name = "ExpiryDate";
            this.ExpiryDate.Width = 125;
            // 
            // DateAdded
            // 
            this.DateAdded.HeaderText = "Date Added";
            this.DateAdded.MinimumWidth = 6;
            this.DateAdded.Name = "DateAdded";
            this.DateAdded.Width = 125;
            // 
            // LastUpdated
            // 
            this.LastUpdated.HeaderText = "Last Updated";
            this.LastUpdated.MinimumWidth = 6;
            this.LastUpdated.Name = "LastUpdated";
            this.LastUpdated.Width = 125;
            // 
            // btnadd
            // 
            this.btnadd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnadd.BackgroundImage = global::BISFT.Properties.Resources.adp;
            this.btnadd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnadd.Location = new System.Drawing.Point(929, 196);
            this.btnadd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(262, 52);
            this.btnadd.TabIndex = 2;
            this.btnadd.UseVisualStyleBackColor = false;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDelete.BackgroundImage = global::BISFT.Properties.Resources.del;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Location = new System.Drawing.Point(1458, 196);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(101, 52);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdateProduct
            // 
            this.btnUpdateProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnUpdateProduct.BackgroundImage = global::BISFT.Properties.Resources.upp;
            this.btnUpdateProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdateProduct.Location = new System.Drawing.Point(1197, 196);
            this.btnUpdateProduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateProduct.Name = "btnUpdateProduct";
            this.btnUpdateProduct.Size = new System.Drawing.Size(262, 52);
            this.btnUpdateProduct.TabIndex = 5;
            this.btnUpdateProduct.UseVisualStyleBackColor = false;
            this.btnUpdateProduct.Click += new System.EventHandler(this.btnUpdateProduct_Click);
            // 
            // txtsearch
            // 
            this.txtsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtsearch.Location = new System.Drawing.Point(433, 221);
            this.txtsearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(295, 26);
            this.txtsearch.TabIndex = 6;
            this.txtsearch.Text = "Search Bar (Enter Item Id)";
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            // 
            // lblTotalProductsCount
            // 
            this.lblTotalProductsCount.AutoSize = true;
            this.lblTotalProductsCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblTotalProductsCount.Location = new System.Drawing.Point(442, 176);
            this.lblTotalProductsCount.Name = "lblTotalProductsCount";
            this.lblTotalProductsCount.Size = new System.Drawing.Size(18, 20);
            this.lblTotalProductsCount.TabIndex = 11;
            this.lblTotalProductsCount.Text = "0";
            this.lblTotalProductsCount.Click += new System.EventHandler(this.lblTotalProductsCount_Click);
            // 
            // lblLowStockC
            // 
            this.lblLowStockC.AutoSize = true;
            this.lblLowStockC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblLowStockC.Location = new System.Drawing.Point(684, 176);
            this.lblLowStockC.Name = "lblLowStockC";
            this.lblLowStockC.Size = new System.Drawing.Size(18, 20);
            this.lblLowStockC.TabIndex = 13;
            this.lblLowStockC.Text = "0";
            this.lblLowStockC.Click += new System.EventHandler(this.lblLowStockC_Click);
            // 
            // btncustomers
            // 
            this.btncustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btncustomers.BackgroundImage = global::BISFT.Properties.Resources.Screenshot_2025_03_29_163939;
            this.btncustomers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncustomers.Location = new System.Drawing.Point(30, 316);
            this.btncustomers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btncustomers.Name = "btncustomers";
            this.btncustomers.Size = new System.Drawing.Size(183, 55);
            this.btncustomers.TabIndex = 14;
            this.btncustomers.UseVisualStyleBackColor = false;
            this.btncustomers.Click += new System.EventHandler(this.btncustomers_Click);
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1728, 788);
            this.Controls.Add(this.btncustomers);
            this.Controls.Add(this.txtsearch);
            this.Controls.Add(this.btndashboard);
            this.Controls.Add(this.btnorders);
            this.Controls.Add(this.lblLowStockC);
            this.Controls.Add(this.lblTotalProductsCount);
            this.Controls.Add(this.btnUpdateProduct);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InventoryForm";
            this.Text = "InventoryForm";
            this.Load += new System.EventHandler(this.InventoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btndashboard;
        private System.Windows.Forms.Button btnorders;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Availibility;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchasePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThresholdValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateAdded;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastUpdated;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdateProduct;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label lblTotalProductsCount;
        private System.Windows.Forms.Label lblLowStockC;
        private System.Windows.Forms.Button btncustomers;
    }
}