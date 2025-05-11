namespace BISFT
{
    partial class UpdateProductForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateProductForm));
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnUCancel = new System.Windows.Forms.Button();
            this.dtpUExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.txtUSupplier = new System.Windows.Forms.TextBox();
            this.cmbUAvailability = new System.Windows.Forms.ComboBox();
            this.txtUCategory = new System.Windows.Forms.TextBox();
            this.txtUThresholdValue = new System.Windows.Forms.TextBox();
            this.txtUPurchasePrice = new System.Windows.Forms.TextBox();
            this.txtUSellingPrice = new System.Windows.Forms.TextBox();
            this.txtUQuantity = new System.Windows.Forms.TextBox();
            this.txtUDescription = new System.Windows.Forms.TextBox();
            this.txtUProduct = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackgroundImage = global::BISFT.Properties.Resources.updatebtn;
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Location = new System.Drawing.Point(473, 366);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(117, 48);
            this.btnUpdate.TabIndex = 46;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnUCancel
            // 
            this.btnUCancel.BackgroundImage = global::BISFT.Properties.Resources.canel;
            this.btnUCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUCancel.Location = new System.Drawing.Point(596, 366);
            this.btnUCancel.Name = "btnUCancel";
            this.btnUCancel.Size = new System.Drawing.Size(102, 48);
            this.btnUCancel.TabIndex = 45;
            this.btnUCancel.UseVisualStyleBackColor = true;
            this.btnUCancel.Click += new System.EventHandler(this.btnUCancel_Click);
            // 
            // dtpUExpiryDate
            // 
            this.dtpUExpiryDate.Location = new System.Drawing.Point(504, 301);
            this.dtpUExpiryDate.Name = "dtpUExpiryDate";
            this.dtpUExpiryDate.ShowCheckBox = true;
            this.dtpUExpiryDate.Size = new System.Drawing.Size(194, 22);
            this.dtpUExpiryDate.TabIndex = 44;
            this.dtpUExpiryDate.ValueChanged += new System.EventHandler(this.dtpUExpiryDate_ValueChanged);
            // 
            // txtUSupplier
            // 
            this.txtUSupplier.Location = new System.Drawing.Point(541, 221);
            this.txtUSupplier.Name = "txtUSupplier";
            this.txtUSupplier.Size = new System.Drawing.Size(105, 22);
            this.txtUSupplier.TabIndex = 43;
            this.txtUSupplier.TextChanged += new System.EventHandler(this.txtUSupplier_TextChanged);
            // 
            // cmbUAvailability
            // 
            this.cmbUAvailability.FormattingEnabled = true;
            this.cmbUAvailability.Items.AddRange(new object[] {
            "In Stock",
            "Low Stock",
            "Out of Stock"});
            this.cmbUAvailability.Location = new System.Drawing.Point(241, 264);
            this.cmbUAvailability.Name = "cmbUAvailability";
            this.cmbUAvailability.Size = new System.Drawing.Size(154, 24);
            this.cmbUAvailability.TabIndex = 42;
            this.cmbUAvailability.SelectedIndexChanged += new System.EventHandler(this.cmbUAvailability_SelectedIndexChanged);
            // 
            // txtUCategory
            // 
            this.txtUCategory.Location = new System.Drawing.Point(241, 301);
            this.txtUCategory.Name = "txtUCategory";
            this.txtUCategory.Size = new System.Drawing.Size(166, 22);
            this.txtUCategory.TabIndex = 33;
            this.txtUCategory.TextChanged += new System.EventHandler(this.txtUCategory_TextChanged);
            // 
            // txtUThresholdValue
            // 
            this.txtUThresholdValue.Location = new System.Drawing.Point(541, 260);
            this.txtUThresholdValue.Name = "txtUThresholdValue";
            this.txtUThresholdValue.Size = new System.Drawing.Size(89, 22);
            this.txtUThresholdValue.TabIndex = 32;
            this.txtUThresholdValue.TextChanged += new System.EventHandler(this.txtUThresholdValue_TextChanged);
            // 
            // txtUPurchasePrice
            // 
            this.txtUPurchasePrice.Location = new System.Drawing.Point(541, 148);
            this.txtUPurchasePrice.Name = "txtUPurchasePrice";
            this.txtUPurchasePrice.Size = new System.Drawing.Size(82, 22);
            this.txtUPurchasePrice.TabIndex = 31;
            this.txtUPurchasePrice.TextChanged += new System.EventHandler(this.txtUPurchasePrice_TextChanged);
            // 
            // txtUSellingPrice
            // 
            this.txtUSellingPrice.Location = new System.Drawing.Point(541, 184);
            this.txtUSellingPrice.Name = "txtUSellingPrice";
            this.txtUSellingPrice.Size = new System.Drawing.Size(82, 22);
            this.txtUSellingPrice.TabIndex = 30;
            this.txtUSellingPrice.TextChanged += new System.EventHandler(this.txtÚSellingPrice_TextChanged);
            // 
            // txtUQuantity
            // 
            this.txtUQuantity.Location = new System.Drawing.Point(241, 230);
            this.txtUQuantity.Name = "txtUQuantity";
            this.txtUQuantity.Size = new System.Drawing.Size(154, 22);
            this.txtUQuantity.TabIndex = 29;
            this.txtUQuantity.TextChanged += new System.EventHandler(this.txtUQuantity_TextChanged);
            // 
            // txtUDescription
            // 
            this.txtUDescription.Location = new System.Drawing.Point(241, 184);
            this.txtUDescription.Name = "txtUDescription";
            this.txtUDescription.Size = new System.Drawing.Size(155, 22);
            this.txtUDescription.TabIndex = 28;
            this.txtUDescription.TextChanged += new System.EventHandler(this.txtUDescription_TextChanged);
            // 
            // txtUProduct
            // 
            this.txtUProduct.Location = new System.Drawing.Point(241, 148);
            this.txtUProduct.Name = "txtUProduct";
            this.txtUProduct.Size = new System.Drawing.Size(155, 22);
            this.txtUProduct.TabIndex = 26;
            this.txtUProduct.TextChanged += new System.EventHandler(this.txtUProduct_TextChanged);
            // 
            // UpdateProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(822, 482);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnUCancel);
            this.Controls.Add(this.dtpUExpiryDate);
            this.Controls.Add(this.txtUSupplier);
            this.Controls.Add(this.cmbUAvailability);
            this.Controls.Add(this.txtUCategory);
            this.Controls.Add(this.txtUThresholdValue);
            this.Controls.Add(this.txtUPurchasePrice);
            this.Controls.Add(this.txtUSellingPrice);
            this.Controls.Add(this.txtUQuantity);
            this.Controls.Add(this.txtUDescription);
            this.Controls.Add(this.txtUProduct);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateProductForm";
            this.Text = "UpdateProductForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnUCancel;
        private System.Windows.Forms.DateTimePicker dtpUExpiryDate;
        private System.Windows.Forms.TextBox txtUSupplier;
        private System.Windows.Forms.ComboBox cmbUAvailability;
        private System.Windows.Forms.TextBox txtUCategory;
        private System.Windows.Forms.TextBox txtUThresholdValue;
        private System.Windows.Forms.TextBox txtUPurchasePrice;
        private System.Windows.Forms.TextBox txtUSellingPrice;
        private System.Windows.Forms.TextBox txtUQuantity;
        private System.Windows.Forms.TextBox txtUDescription;
        private System.Windows.Forms.TextBox txtUProduct;
    }
}