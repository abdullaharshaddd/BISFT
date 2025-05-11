using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BISFT
{
    public partial class AddProductForm: Form
    {
        public AddProductForm()
        {
            InitializeComponent();
        }

        private void lblProduct_Click(object sender, EventArgs e)
        {

        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtQuantity.Text, out _))
            {
                txtQuantity.BackColor = Color.Salmon; 
            }
            else
            {
                txtQuantity.BackColor = Color.White; 
            }
        }


        private void lblQuantity_Click(object sender, EventArgs e)
        {

        }

        private void cmbAvailability_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAvailability.SelectedItem.ToString() == "Out of Stock")
            {
                txtQuantity.Text = "0"; 
                txtQuantity.Enabled = false; 
            }
            else
            {
                txtQuantity.Enabled = true; 
            }
        }

        private void lblAvailability_Click(object sender, EventArgs e)
        {

        }

        private void lblCategory_Click(object sender, EventArgs e)
        {

        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPurchasePrice.Text, out _))
            {
                txtPurchasePrice.BackColor = Color.Salmon; 
            }
            else
            {
                txtPurchasePrice.BackColor = Color.White; 
            }
        }


        private void lblPurchasePrice_Click(object sender, EventArgs e)
        {

        }

        private void txtSellingPrice_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtSellingPrice.Text, out _))
            {
                txtSellingPrice.BackColor = Color.Salmon; 
            }
            else
            {
                txtSellingPrice.BackColor = Color.White; 
            }
        }


        private void lblSellingPrice_Click(object sender, EventArgs e)
        {

        }

        private void txtSupplier_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSupplier_Click(object sender, EventArgs e)
        {

        }

        private void txtThresholdValue_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtThresholdValue.Text, out _))
            {
                txtThresholdValue.BackColor = Color.Salmon; 
            }
            else
            {
                txtThresholdValue.BackColor = Color.White; 
            }
        }


        private void lblThresholdValue_Click(object sender, EventArgs e)
        {

        }

        private void dtpExpiryDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpExpiryDate.Value < DateTime.Today)
            {
                MessageBox.Show("Expiry date cannot be in the past.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpExpiryDate.Value = DateTime.Today; 
            }
        }


        private void lblDate_Click(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProduct.Text))
                {
                    MessageBox.Show("Product name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity))
                {
                    MessageBox.Show("Quantity must be a valid integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.Focus();
                    return;
                }

                if (!decimal.TryParse(txtPurchasePrice.Text, out decimal purchasePrice))
                {
                    MessageBox.Show("Purchase Price must be a valid decimal number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPurchasePrice.Focus();
                    return;
                }

                if (!decimal.TryParse(txtSellingPrice.Text, out decimal sellingPrice))
                {
                    MessageBox.Show("Selling Price must be a valid decimal number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSellingPrice.Focus();
                    return;
                }

                if (!int.TryParse(txtThresholdValue.Text, out int thresholdValue))
                {
                    MessageBox.Show("Threshold Value must be a valid integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtThresholdValue.Focus();
                    return;
                }


                string product = txtProduct.Text;
                string description = txtDescription.Text;
                string availability = cmbAvailability.SelectedItem?.ToString() ?? "";
                string category = txtCategory.Text;
                string supplier = txtSupplier.Text;
                DateTime? expiryDate = dtpExpiryDate.Checked ? (DateTime?)dtpExpiryDate.Value : null;

                DataBaseAccess.InsertInventoryItem(product, description, quantity, availability,
                                                   category, purchasePrice, sellingPrice, supplier,
                                                   thresholdValue, expiryDate);

                MessageBox.Show("Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving the product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnUSellingPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10F);
                    txt.ForeColor = Color.Black;
                    txt.BackColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }

                if (ctrl is ComboBox cmb)
                {
                    cmb.Font = new Font("Segoe UI", 10F);
                    cmb.FlatStyle = FlatStyle.Flat;
                    cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }
       




    }
}
