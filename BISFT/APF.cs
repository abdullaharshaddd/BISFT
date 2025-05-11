using BISFT;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BISFT
{
    public partial class APF : Form
    {
        public APF()
        {
            InitializeComponent();
            this.Load += APF_Load;
            cmbProductName.SelectedIndexChanged += cmbProductName_SelectedIndexChanged;
        }

        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal PricePerUnit { get; private set; }

        private Dictionary<string, (decimal price, int stock)> inventoryData = new Dictionary<string, (decimal, int)>();

        private void APF_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = "select product, sellingprice, quantity from inventoryitems";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string product = reader["product"].ToString();
                        decimal price = Convert.ToDecimal(reader["sellingprice"]);
                        int stock = Convert.ToInt32(reader["quantity"]);

                        cmbProductName.Items.Add(product);
                        inventoryData[product] = (price, stock);
                    }
                }
            }
        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProduct = cmbProductName.SelectedItem.ToString();
            if (inventoryData.ContainsKey(selectedProduct))
            {
                PricePerUnit = inventoryData[selectedProduct].price;
                lblPricePerUnit.Text = $"Rs. {PricePerUnit:N2}";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbProductName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            ProductName = cmbProductName.SelectedItem.ToString();
            Quantity = (int)numQuantity.Value;

            if (!inventoryData.ContainsKey(ProductName))
            {
                MessageBox.Show("Product not found in inventory.");
                return;
            }

            int availableStock = inventoryData[ProductName].stock;

            if (Quantity > availableStock)
            {
                MessageBox.Show($"Not enough stock available.\nAvailable: {availableStock}\nYou requested: {Quantity}",
                                "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
