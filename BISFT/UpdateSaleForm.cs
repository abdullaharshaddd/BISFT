using BISFT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BISFT
{
    public partial class UpdateSaleForm : Form
    {
        private int saleId;
        private string customerName;
        private DateTime saleDate;
        private decimal totalAmount;

        public UpdateSaleForm(int saleId, string customerName, DateTime saleDate, decimal totalAmount)
        {
            InitializeComponent();

            this.saleId = saleId;
            this.customerName = customerName;
            this.saleDate = saleDate;
            this.totalAmount = totalAmount;

            this.Load += UpdateSaleForm_Load;
            dgvProducts.CellValueChanged += dgvProducts_CellValueChanged;
            dgvProducts.EditingControlShowing += dgvProducts_EditingControlShowing;
            dgvProducts.CellClick += dgvProducts_CellClick;
        }

        private void UpdateSaleForm_Load(object sender, EventArgs e)
        {
            lblCustomerName.Text = customerName;
            dtpSaleDate.Value = saleDate;

            dgvProducts.Columns.Clear();
            dgvProducts.Rows.Clear();
            dgvProducts.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colProduct = new DataGridViewTextBoxColumn();
            colProduct.Name = "ProductName";
            colProduct.HeaderText = "Product";
            dgvProducts.Columns.Add(colProduct);

            DataGridViewTextBoxColumn colQty = new DataGridViewTextBoxColumn();
            colQty.Name = "Quantity";
            colQty.HeaderText = "Quantity";
            dgvProducts.Columns.Add(colQty);

            DataGridViewTextBoxColumn colPrice = new DataGridViewTextBoxColumn();
            colPrice.Name = "PricePerUnit";
            colPrice.HeaderText = "Price Per Unit";
            dgvProducts.Columns.Add(colPrice);

            DataGridViewTextBoxColumn colTotal = new DataGridViewTextBoxColumn();
            colTotal.Name = "Total";
            colTotal.HeaderText = "Total";
            dgvProducts.Columns.Add(colTotal);

            DataGridViewButtonColumn removeBtn = new DataGridViewButtonColumn();
            removeBtn.Name = "RemoveProduct";
            removeBtn.Text = "❌";
            removeBtn.UseColumnTextForButtonValue = true;
            removeBtn.Width = 30;
            dgvProducts.Columns.Add(removeBtn);

            LoadSaleItems();
            UpdateTotalPriceLabel();
        }

        private void LoadSaleItems()
        {
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = "select productname, quantity, priceperunit from saleitems where saleid = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", saleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string product = reader["productname"].ToString();
                            int quantity = Convert.ToInt32(reader["quantity"]);
                            decimal price = Convert.ToDecimal(reader["priceperunit"]);
                            decimal total = quantity * price;

                            dgvProducts.Rows.Add(product, quantity, price, total);
                        }
                    }
                }
            }
        }

        private void UpdateTotalPriceLabel()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }

            lblUpdatedTotal.Text = $"Rs. {total:N2}";
        }


        private void dgvProducts_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvProducts.CurrentCell.ColumnIndex == dgvProducts.Columns["Quantity"].Index)
            {
                e.Control.KeyPress -= OnlyAllowNumbers_KeyPress;
                e.Control.KeyPress += OnlyAllowNumbers_KeyPress;
            }
        }

        private void OnlyAllowNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex >= 0 && dgvProducts.Columns[e.ColumnIndex].Name == "Quantity")
            {
                try
                {
                    int quantity = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells["Quantity"].Value);
                    decimal price = Convert.ToDecimal(dgvProducts.Rows[e.RowIndex].Cells["PricePerUnit"].Value);
                    dgvProducts.Rows[e.RowIndex].Cells["Total"].Value = quantity * price;
                    UpdateTotalPriceLabel();
                }
                catch
                {
                }
            }
        }
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex >= 0 && dgvProducts.Columns[e.ColumnIndex].Name == "RemoveProduct")
            {
                dgvProducts.Rows.RemoveAt(e.RowIndex);
                UpdateTotalPriceLabel();
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            dgvProducts.AllowUserToAddRows = false; 

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    string restoreQuery = "select productname, quantity from saleitems where saleid = @id";
                    using (SqlCommand restoreCmd = new SqlCommand(restoreQuery, con, transaction))
                    {
                        restoreCmd.Parameters.AddWithValue("@id", saleId);
                        using (SqlDataReader reader = restoreCmd.ExecuteReader())
                        {
                            List<(string product, int quantity)> restoreList = new List<(string, int)>();
                            while (reader.Read())
                            {
                                restoreList.Add((reader["productname"].ToString(), Convert.ToInt32(reader["quantity"])));
                            }
                            reader.Close();

                            foreach (var item in restoreList)
                            {
                                string updateStockQuery = "update inventoryitems set quantity = quantity + @qty where product = @prod";
                                using (SqlCommand stockCmd = new SqlCommand(updateStockQuery, con, transaction))
                                {
                                    stockCmd.Parameters.AddWithValue("@qty", item.quantity);
                                    stockCmd.Parameters.AddWithValue("@prod", item.product);
                                    stockCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    string deleteQuery = "delete from saleitems where saleid = @id";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", saleId);
                        cmd.ExecuteNonQuery();
                    }

                    decimal updatedTotal = 0;

                    foreach (DataGridViewRow row in dgvProducts.Rows)
                    {
                        if (row.IsNewRow || row.Cells["ProductName"].Value == null ||
                            row.Cells["Quantity"].Value == null || row.Cells["PricePerUnit"].Value == null)
                        {
                            continue;
                        }

                        string product = row.Cells["ProductName"].Value.ToString();
                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                        decimal price = Convert.ToDecimal(row.Cells["PricePerUnit"].Value);
                        updatedTotal += quantity * price;

                        string checkStockQuery = "select quantity from inventoryitems where product = @prod";
                        using (SqlCommand checkCmd = new SqlCommand(checkStockQuery, con, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@prod", product);
                            int currentStock = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (quantity > currentStock)
                            {
                                throw new Exception($"Not enough stock for {product}.\nAvailable: {currentStock}, Requested: {quantity}");
                            }
                        }

                        string insertQuery = "insert into saleitems (saleid, productname, quantity, priceperunit) values (@sid, @product, @qty, @price)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@sid", saleId);
                            cmd.Parameters.AddWithValue("@product", product);
                            cmd.Parameters.AddWithValue("@qty", quantity);
                            cmd.Parameters.AddWithValue("@price", price);
                            cmd.ExecuteNonQuery();
                        }

                        string reduceStockQuery = "update inventoryitems set quantity = quantity - @qty where product = @prod";
                        using (SqlCommand invCmd = new SqlCommand(reduceStockQuery, con, transaction))
                        {
                            invCmd.Parameters.AddWithValue("@qty", quantity);
                            invCmd.Parameters.AddWithValue("@prod", product);
                            invCmd.ExecuteNonQuery();
                        }
                    }

                    string updateSale = "update sales set totalamount = @total, saledate = @date where saleid = @id";
                    using (SqlCommand cmd = new SqlCommand(updateSale, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@total", updatedTotal);
                        cmd.Parameters.AddWithValue("@date", dtpSaleDate.Value);
                        cmd.Parameters.AddWithValue("@id", saleId);
                        cmd.ExecuteNonQuery();
                    }

                    string updateCustomer = @"
                update customers 
                set AmountRemaining = AmountRemaining - @oldAmount + @newAmount
                where CustomerName = @name";

                    using (SqlCommand cmd = new SqlCommand(updateCustomer, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@oldAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@newAmount", updatedTotal);
                        cmd.Parameters.AddWithValue("@name", customerName);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Sale and customer amounts updated successfully!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private int GetAvailableStock(string productName)
        {
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = "select quantity from inventoryitems where product = @product";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@product", productName);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            APF form = new APF();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string productName = form.ProductName;
                int addedQty = form.Quantity;
                decimal unitPrice = form.PricePerUnit;

                int existingQtyInGrid = 0;

                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.Cells["ProductName"].Value?.ToString() == productName)
                    {
                        existingQtyInGrid += Convert.ToInt32(row.Cells["Quantity"].Value);
                    }
                }

                int inventoryQty = GetAvailableStock(productName);

                if (existingQtyInGrid + addedQty > inventoryQty)
                {
                    MessageBox.Show($"Not enough stock!\nAvailable: {inventoryQty}\nIn Cart Already: {existingQtyInGrid}\nTrying to Add: {addedQty}", "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool found = false;

                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.Cells["ProductName"].Value?.ToString() == productName)
                    {
                        int updatedQty = Convert.ToInt32(row.Cells["Quantity"].Value) + addedQty;
                        row.Cells["Quantity"].Value = updatedQty;
                        row.Cells["Total"].Value = updatedQty * unitPrice;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    decimal total = addedQty * unitPrice;
                    dgvProducts.Rows.Add(productName, addedQty, unitPrice, total);
                }

                UpdateTotalPriceLabel();
            }
        }
    }
}
