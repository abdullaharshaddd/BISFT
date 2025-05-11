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

namespace BISFT
{
    public partial class UpdateProductForm: Form
    {
        private int _itemId;

        public UpdateProductForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            LoadProductDetails();
        }

        private void LoadProductDetails()
        {
            using (SqlConnection connection = DataBaseAccess.GetConnection())
            {
                string query = "SELECT Product, Description, Quantity, Availability, Category, PurchasePrice, SellingPrice, Supplier, ThresholdValue, ExpiryDate FROM InventoryItems WHERE ItemID = @ItemID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemID", _itemId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtUProduct.Text = reader["Product"] != DBNull.Value ? reader["Product"].ToString() : "";
                        txtUDescription.Text = reader["Description"].ToString();
                        txtUQuantity.Text = reader["Quantity"].ToString();
                        cmbUAvailability.SelectedItem = reader["Availability"].ToString();
                        txtUCategory.Text = reader["Category"].ToString();
                        txtUPurchasePrice.Text = reader["PurchasePrice"].ToString();
                        txtUSellingPrice.Text = reader["SellingPrice"].ToString();
                        txtUSupplier.Text = reader["Supplier"].ToString();
                        txtUThresholdValue.Text = reader["ThresholdValue"].ToString();
                        if (reader["ExpiryDate"] != DBNull.Value)
                            dtpUExpiryDate.Value = Convert.ToDateTime(reader["ExpiryDate"]);
                        else
                            dtpUExpiryDate.Checked = false;
                    }
                }
            }
        }



        private void lblUProduct_Click(object sender, EventArgs e)
        {

        }

        private void txtUProduct_TextChanged(object sender, EventArgs e)
        {
            txtUProduct.BackColor = string.IsNullOrWhiteSpace(txtUProduct.Text) ? Color.Salmon : Color.White;
        }


        private void lblUDescription_Click(object sender, EventArgs e)
        {

        }

        private void txtUDescription_TextChanged(object sender, EventArgs e)
        {
            txtUDescription.BackColor = string.IsNullOrWhiteSpace(txtUDescription.Text) ? Color.Salmon : Color.White;
        }

        private void lblUQuantity_Click(object sender, EventArgs e)
        {

        }

        private void txtUQuantity_TextChanged(object sender, EventArgs e)
        {
            bool isValid = int.TryParse(txtUQuantity.Text, out int quantity) && quantity > 0;
            txtUQuantity.BackColor = isValid ? Color.White : Color.Salmon;
        }


        private void lblUAvailability_Click(object sender, EventArgs e)
        {

        }

        private void cmbUAvailability_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblUCategory_Click(object sender, EventArgs e)
        {

        }

        private void txtUCategory_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUPurchasePrice_Click(object sender, EventArgs e)
        {

        }

        private void txtUPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            txtUPurchasePrice.BackColor = decimal.TryParse(txtUPurchasePrice.Text, out _) ? Color.White : Color.Salmon;
        }


        private void lblUSellingPrice_Click(object sender, EventArgs e)
        {

        }

        private void txtÚSellingPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUSupplier_Click(object sender, EventArgs e)
        {

        }

        private void txtUSupplier_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUThresholdValue_Click(object sender, EventArgs e)
        {

        }

        private void txtUThresholdValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUDate_Click(object sender, EventArgs e)
        {

        }

        private void dtpUExpiryDate_ValueChanged(object sender, EventArgs e)
        {

        }
        private void UpdateProduct()
        {
            using (SqlConnection connection = DataBaseAccess.GetConnection())
            {
                string query = @"
            UPDATE InventoryItems
            SET Product = @Product, Description = @Description, Quantity = @Quantity,
                Availability = @Availability, Category = @Category, PurchasePrice = @PurchasePrice,
                SellingPrice = @SellingPrice, Supplier = @Supplier, ThresholdValue = @ThresholdValue,
                ExpiryDate = @ExpiryDate
            WHERE ItemID = @ItemID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemID", _itemId);
                command.Parameters.AddWithValue("@Product", txtUProduct.Text);
                command.Parameters.AddWithValue("@Description", txtUDescription.Text);
                command.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtUQuantity.Text));
                command.Parameters.AddWithValue("@Availability",
                      cmbUAvailability.SelectedItem != null ? cmbUAvailability.SelectedItem.ToString() : "Unknown"
                    );

                command.Parameters.AddWithValue("@Category", txtUCategory.Text);
                command.Parameters.AddWithValue("@PurchasePrice", Convert.ToDecimal(txtUPurchasePrice.Text));
                command.Parameters.AddWithValue("@SellingPrice", Convert.ToDecimal(txtUSellingPrice.Text));
                command.Parameters.AddWithValue("@Supplier", txtUSupplier.Text);
                command.Parameters.AddWithValue("@ThresholdValue", Convert.ToInt32(txtUThresholdValue.Text));
                command.Parameters.AddWithValue("@ExpiryDate", dtpUExpiryDate.Checked ? (object)dtpUExpiryDate.Value : DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    UpdateProduct();

                    // 💡 Low Stock Alert Logic
                    int quantity = Convert.ToInt32(txtUQuantity.Text);
                    int threshold = Convert.ToInt32(txtUThresholdValue.Text);
                    string productName = txtUProduct.Text;

                    if (quantity <= threshold)
                    {
                        MessageBox.Show(
                            $"⚠️ Product '{productName}' is low in stock.\nCurrent Quantity: {quantity}\nThreshold: {threshold}",
                            "Low Stock Alert",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }

                    MessageBox.Show("Product updated successfully.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please correct the highlighted errors.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(txtUProduct.Text)) isValid = false;
            if (!decimal.TryParse(txtUPurchasePrice.Text, out _)) isValid = false;
            if (!decimal.TryParse(txtUSellingPrice.Text, out _)) isValid = false;

            return isValid;
        }

     
        



        private void btnUCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
