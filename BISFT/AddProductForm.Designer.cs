namespace BISFT
{
    partial class AddProductForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProductForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtThresholdValue = new System.Windows.Forms.TextBox();
            this.txtPurchasePrice = new System.Windows.Forms.TextBox();
            this.cmbAvailability = new System.Windows.Forms.ComboBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.txtSellingPrice = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.Location = new System.Drawing.Point(474, 367);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 52);
            this.btnSave.TabIndex = 24;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(250, 319);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(133, 22);
            this.txtCategory.TabIndex = 10;
            this.txtCategory.TextChanged += new System.EventHandler(this.txtCategory_TextChanged);
            // 
            // txtThresholdValue
            // 
            this.txtThresholdValue.Location = new System.Drawing.Point(523, 241);
            this.txtThresholdValue.Name = "txtThresholdValue";
            this.txtThresholdValue.Size = new System.Drawing.Size(95, 22);
            this.txtThresholdValue.TabIndex = 8;
            this.txtThresholdValue.TextChanged += new System.EventHandler(this.txtThresholdValue_TextChanged);
            // 
            // txtPurchasePrice
            // 
            this.txtPurchasePrice.Location = new System.Drawing.Point(523, 160);
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.txtPurchasePrice.Size = new System.Drawing.Size(77, 22);
            this.txtPurchasePrice.TabIndex = 7;
            this.txtPurchasePrice.TextChanged += new System.EventHandler(this.txtPurchasePrice_TextChanged);
            // 
            // cmbAvailability
            // 
            this.cmbAvailability.FormattingEnabled = true;
            this.cmbAvailability.Items.AddRange(new object[] {
            "In Stock",
            "Low Stock",
            "Out of Stock"});
            this.cmbAvailability.Location = new System.Drawing.Point(250, 277);
            this.cmbAvailability.Name = "cmbAvailability";
            this.cmbAvailability.Size = new System.Drawing.Size(133, 24);
            this.cmbAvailability.TabIndex = 20;
            this.cmbAvailability.SelectedIndexChanged += new System.EventHandler(this.cmbAvailability_SelectedIndexChanged);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(250, 241);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(133, 22);
            this.txtQuantity.TabIndex = 4;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(523, 277);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(77, 22);
            this.txtSupplier.TabIndex = 21;
            this.txtSupplier.TextChanged += new System.EventHandler(this.txtSupplier_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(250, 200);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(133, 22);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Location = new System.Drawing.Point(495, 317);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.ShowCheckBox = true;
            this.dtpExpiryDate.Size = new System.Drawing.Size(172, 22);
            this.dtpExpiryDate.TabIndex = 22;
            this.dtpExpiryDate.ValueChanged += new System.EventHandler(this.dtpExpiryDate_ValueChanged);
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(250, 160);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(133, 22);
            this.txtProduct.TabIndex = 1;
            this.txtProduct.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.Location = new System.Drawing.Point(523, 200);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(77, 22);
            this.txtSellingPrice.TabIndex = 26;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::BISFT.Properties.Resources.cancelbtn;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Location = new System.Drawing.Point(588, 367);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 52);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(811, 529);
            this.Controls.Add(this.txtSellingPrice);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.txtProduct);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpExpiryDate);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtThresholdValue);
            this.Controls.Add(this.txtPurchasePrice);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.cmbAvailability);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddProductForm";
            this.Text = "AddProductForm";
            this.Load += new System.EventHandler(this.AddProductForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtThresholdValue;
        private System.Windows.Forms.TextBox txtPurchasePrice;
        private System.Windows.Forms.ComboBox cmbAvailability;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.TextBox txtSellingPrice;
        private System.Windows.Forms.Button btnCancel;
    }
}