using BISFT;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BISFT
{
    public partial class PaymentForm: Form
    {
        public PaymentForm()
        {
            InitializeComponent();

            this.Load += PaymentForm_Load;

            dgvOrderSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrderSummary.AllowUserToAddRows = false;
            dgvOrderSummary.AllowUserToResizeRows = false;

            txtPhone.Mask = "0000-0000000";
            cmbCustomerType.Items.AddRange(new[] { "Retail", "Wholesale" });
        }

        private List<(string product, int quantity, decimal price)> cartItems;
        private decimal orderTotal;

        public PaymentForm(List<(string product, int quantity, decimal price)> cartItems, decimal orderTotal)
            : this() 
        {
            this.cartItems = cartItems;
            this.orderTotal = orderTotal;

            this.Load += PaymentForm_Load;

            dgvOrderSummary.Rows.Clear(); 

            foreach ((string product, int quantity, decimal _) in cartItems)
            {
                dgvOrderSummary.Rows.Add(product, quantity);
            }

            lblTotalAmount.Text = $"PKR {orderTotal:N2}";
        }
        private void PaymentForm_Load(object sender, EventArgs e)
        {
            cmbCustomerType.SelectedIndex = 0; 

            dgvOrderSummary.Columns.Clear();
            dgvOrderSummary.Columns.Add("Product", "Product");
            dgvOrderSummary.Columns.Add("Quantity", "Quantity");

            foreach (var item in cartItems)
            {
                decimal total = item.quantity * item.price;
                dgvOrderSummary.Rows.Add(item.product, item.quantity);
            }

            lblTotalAmount.Text = $"PKR {orderTotal:N2}";
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            string searchPhone = txtSearchPhone.Text.Trim();

            if (!System.Text.RegularExpressions.Regex.IsMatch(searchPhone, @"^\d{4}-\d{7}$"))
            {
                MessageBox.Show("Phone number must be in format 03xx-xxxxxxx", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = "SELECT CustomerName, Phone, Email, Address, CustomerType FROM Customers WHERE Phone = @phone";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@phone", searchPhone);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["CustomerName"].ToString();
                            txtPhone.Text = reader["Phone"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtAddress.Text = reader["Address"].ToString();

                            string type = reader["CustomerType"].ToString();
                            cmbCustomerType.SelectedItem = cmbCustomerType.Items.Contains(type) ? type : null;
                        }
                        else
                        {
                            MessageBox.Show("Customer not found. You can add their info.", "New Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            txtName.Clear();
                            txtPhone.Text = searchPhone;
                            txtEmail.Clear();
                            txtAddress.Clear();
                            cmbCustomerType.SelectedIndex = 0;
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }


        private decimal GetOrderTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvOrderSummary.Rows)
            {
                if (row.IsNewRow) continue;

                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                string product = row.Cells["Products"].Value.ToString();
                decimal unitPrice = GetUnitPrice(product);

                total += quantity * unitPrice;
            }

            return total;
        }

        private decimal GetUnitPrice(string productName)
        {
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                string query = "SELECT SellingPrice FROM InventoryItems WHERE Product = @product";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@product", productName);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        return Convert.ToDecimal(result);
                    else
                        throw new Exception("Product not found in inventory.");
                }
            }
        }

        private void UpdateTotalLabel()
        {
            lblTotalAmount.Text = $"PKR {GetOrderTotal():N2}";
        }
        private void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            string customerType = cmbCustomerType.SelectedItem?.ToString();
            decimal amountPaid;

            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{4}-\d{7}$"))
            {
                MessageBox.Show("Phone number must be in format 03xx-xxxxxxx.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(email) && !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            if (!decimal.TryParse(txtAmountPaid.Text.Trim(), out amountPaid))
            {
                MessageBox.Show("Enter a valid paid amount.");
                return;
            }

            if (amountPaid > orderTotal)
            {
                MessageBox.Show("Paid amount cannot exceed total.");
                return;
            }

            decimal remaining = orderTotal - amountPaid;

            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    int customerId = -1;

                    string checkQuery = "SELECT CustomerID FROM Customers WHERE Phone = @phone";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            customerId = Convert.ToInt32(result);
                            string update = "UPDATE Customers SET AmountPaid = AmountPaid + @paid, AmountRemaining = AmountRemaining + @remain WHERE CustomerID = @id";
                            using (SqlCommand up = new SqlCommand(update, con, transaction))
                            {
                                up.Parameters.AddWithValue("@paid", amountPaid);
                                up.Parameters.AddWithValue("@remain", remaining);
                                up.Parameters.AddWithValue("@id", customerId);
                                up.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insert = "INSERT INTO Customers (CustomerName, Phone, Email, Address, AmountPaid, AmountRemaining, CustomerType) OUTPUT INSERTED.CustomerID " +
                                            "VALUES (@name, @phone, @email, @address, @paid, @remain, @type)";
                            using (SqlCommand ins = new SqlCommand(insert, con, transaction))
                            {
                                ins.Parameters.AddWithValue("@name", name);
                                ins.Parameters.AddWithValue("@phone", phone);
                                ins.Parameters.AddWithValue("@email", email);
                                ins.Parameters.AddWithValue("@address", address);
                                ins.Parameters.AddWithValue("@paid", amountPaid);
                                ins.Parameters.AddWithValue("@remain", remaining);
                                ins.Parameters.AddWithValue("@type", customerType ?? "Retail");

                                customerId = (int)ins.ExecuteScalar();
                            }
                        }
                    }

                    int saleId;
                    string insertSale = "INSERT INTO Sales (CustomerName, TotalAmount) OUTPUT INSERTED.SaleID VALUES (@name, @total)";
                    using (SqlCommand cmd = new SqlCommand(insertSale, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@total", orderTotal);
                        saleId = (int)cmd.ExecuteScalar();
                    }

                    foreach (var item in cartItems)
                    {
                        string insertItem = "INSERT INTO SaleItems (SaleID, ProductName, Quantity, PricePerUnit) VALUES (@sid, @product, @qty, @price)";
                        using (SqlCommand cmd = new SqlCommand(insertItem, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@sid", saleId);
                            cmd.Parameters.AddWithValue("@product", item.product);
                            cmd.Parameters.AddWithValue("@qty", item.quantity);
                            cmd.Parameters.AddWithValue("@price", item.price);
                            cmd.ExecuteNonQuery();
                        }

                        string updateInventory = "UPDATE InventoryItems SET Quantity = Quantity - @qty WHERE Product = @product";
                        using (SqlCommand cmd = new SqlCommand(updateInventory, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@qty", item.quantity);
                            cmd.Parameters.AddWithValue("@product", item.product);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show("Payment confirmed and data saved successfully!");
                    GenerateReceiptPdf(name, amountPaid, remaining, cartItems, orderTotal);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void GenerateReceiptPdf(string customerName, decimal amountPaid, decimal remaining, List<(string product, int quantity, decimal price)> cartItems, decimal total)
        {
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            // ✅ Sanitize customer name for safe filename
            string safeName = string.Join("_", customerName.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
            string fileName = $"{safeName}_Receipt_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            string fullPath = Path.Combine(downloadsPath, fileName);

            using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                var regularFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                doc.Add(new Paragraph("🧾 SALES RECEIPT", titleFont));
                doc.Add(new Paragraph($"Date: {DateTime.Now:dd-MM-yyyy HH:mm:ss}", regularFont));
                doc.Add(new Paragraph($"Customer: {customerName}", regularFont));
                doc.Add(new Paragraph(" "));

                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.AddCell(new Phrase("Product", boldFont));
                table.AddCell(new Phrase("Quantity", boldFont));
                table.AddCell(new Phrase("Price/Unit", boldFont));
                table.AddCell(new Phrase("Total", boldFont));

                foreach (var item in cartItems)
                {
                    table.AddCell(new Phrase(item.product, regularFont));
                    table.AddCell(new Phrase(item.quantity.ToString(), regularFont));
                    table.AddCell(new Phrase($"₨ {item.price:N2}", regularFont));
                    table.AddCell(new Phrase($"₨ {(item.quantity * item.price):N2}", regularFont));
                }

                doc.Add(table);
                doc.Add(new Paragraph(" "));

                doc.Add(new Paragraph($"Total: ₨ {total:N2}", boldFont));
                doc.Add(new Paragraph($"Paid: ₨ {amountPaid:N2}", boldFont));
                doc.Add(new Paragraph($"Remaining: ₨ {remaining:N2}", boldFont));

                doc.Close();
                writer.Close();
            }

            MessageBox.Show($"🧾 Receipt saved at:\n{fullPath}", "Receipt Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // ✅ Auto-open the file
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = fullPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open receipt automatically.\n{ex.Message}", "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
