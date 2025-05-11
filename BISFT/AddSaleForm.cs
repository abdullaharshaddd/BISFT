using BISFT;
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
    public partial class AddSaleForm: Form
    {
        public AddSaleForm()
        {
            InitializeComponent();
            this.Load += AddSaleForm_Load; 
            dgvCart.CellClick += dgvCart_CellClick;

            dgvCart.CellValueChanged += dgvCart_CellValueChanged;
            dgvCart.EditingControlShowing += dgvCart_EditingControlShowing;

        }

        private Dictionary<string, decimal> productPrices = new Dictionary<string, decimal>();

        private Dictionary<string, (decimal price, int stock)> inventoryData = new Dictionary<string, (decimal, int)>();
        private void AddSaleForm_Load(object sender, EventArgs e)
        {
            cmbProduct.Items.Clear();
            inventoryData.Clear(); 

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

                        cmbProduct.Items.Add(product);
                        inventoryData[product] = (price, stock);
                    }
                }
            }

            if (cmbProduct.Items.Count > 0)
                cmbProduct.SelectedIndex = 0;  

            if (dgvCart.Columns.Count == 0)
            {
                DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
                deleteBtn.Name = "Delete";
                deleteBtn.HeaderText = "";
                deleteBtn.Text = "🗑️";
                deleteBtn.UseColumnTextForButtonValue = true;
                deleteBtn.Width = 50;
                dgvCart.Columns.Add(deleteBtn);
            }

            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToResizeRows = false;
        }



        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCart.Columns[e.ColumnIndex].Name == "Delete")
            {
                dgvCart.Rows.RemoveAt(e.RowIndex);
                UpdateTotalLabel();
            }
        }

        private void dgvCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCart.Columns[e.ColumnIndex].Name == "Quantity")
            {
                try
                {
                    int quantity = Convert.ToInt32(dgvCart.Rows[e.RowIndex].Cells["Quantity"].Value);
                    decimal price = Convert.ToDecimal(dgvCart.Rows[e.RowIndex].Cells["Price"].Value);
                    decimal total = quantity * price;

                    dgvCart.Rows[e.RowIndex].Cells["TotalPrice"].Value = total;
                    UpdateTotalLabel();
                }
                catch
                {
                }
            }
        }

        private void dgvCart_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvCart.CurrentCell.ColumnIndex == dgvCart.Columns["Quantity"].Index)
            {
                e.Control.KeyPress -= OnlyAllowNumbers;
                e.Control.KeyPress += OnlyAllowNumbers;
            }
        }

        private void OnlyAllowNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void UpdateTotalLabel()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["TotalPrice"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                }
            }

            lblTotalPrice.Text = $"PKR {total:N2}";
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex == -1) return;

            string product = cmbProduct.SelectedItem.ToString();
            int quantityToAdd = (int)numQuantity.Value;

            if (!inventoryData.ContainsKey(product))
            {
                MessageBox.Show("Product not found in inventory.");
                return;
            }

            var (price, stock) = inventoryData[product];

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["Product"].Value.ToString() == product)
                {
                    int existingQty = Convert.ToInt32(row.Cells["Quantity"].Value);
                    int totalQty = existingQty + quantityToAdd;

                    if (totalQty > stock)
                    {
                        MessageBox.Show($"Not enough stock!\nAvailable: {stock}\nRequested Total: {totalQty}", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    row.Cells["Quantity"].Value = totalQty;
                    row.Cells["TotalPrice"].Value = price * totalQty;
                    UpdateTotalLabel();
                    return;
                }
            }

            if (quantityToAdd > stock)
            {
                MessageBox.Show($"Not enough stock!\nAvailable: {stock}\nRequested: {quantityToAdd}", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvCart.Rows.Add(product, price, quantityToAdd, price * quantityToAdd, "✏️", "🗑️");
            UpdateTotalLabel();
        }

        public List<(string Product, int Quantity, decimal PricePerUnit)> CartItems { get; private set; } = new List<(string, int, decimal)>();
        public decimal OrderTotal { get; private set; }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0)
            {
                MessageBox.Show("Your cart is empty. Please add items before placing the order.");
                return;
            }

            CartItems.Clear();
            OrderTotal = 0;

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.IsNewRow) continue;

                string product = row.Cells["Product"].Value?.ToString();
                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                decimal price = Convert.ToDecimal(row.Cells["PricePerUnit"].Value); 

                CartItems.Add((product, quantity, price));
                OrderTotal += quantity * price;
            }

            PaymentForm paymentForm = new PaymentForm(CartItems, OrderTotal);
            if (paymentForm.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
