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
using System.Configuration;  



namespace BISFT
{
    public partial class CustomerPage : Form
    {
        public CustomerPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide(); 
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text)) 
            {
                LoadCustomers();
            }
        }


        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search Customer")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black; 
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search Customer";
                txtSearch.ForeColor = Color.Gray; 
            }
        }

      
    private void LoadCustomers()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString
;

            using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string query = @"
            SELECT CustomerID, CustomerName, Email, Phone, Address, CustomerType, AmountPaid, AmountRemaining,
                   CASE
                       WHEN AmountRemaining > 0 THEN 'Unpaid'
                       ELSE 'Paid'
                   END AS Status
            FROM Customers";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt != null && dt.Rows.Count > 0)
            {
                dgvCustomers.DataSource = dt; 
                lblTotalCustomers.Text = $"Total Customers: {dt.Rows.Count}";

                foreach (DataGridViewRow row in dgvCustomers.Rows)
                {
                    string status = row.Cells["Status"]?.Value?.ToString();
                    if (status == "Paid")
                    {
                        row.Cells["Status"].Style.BackColor = Color.LightGreen;
                        row.Cells["Status"].Style.ForeColor = Color.DarkGreen;
                    }
                    else if (status == "Unpaid")
                    {
                        row.Cells["Status"].Style.BackColor = Color.MistyRose;
                        row.Cells["Status"].Style.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                MessageBox.Show("No data found.");
            }
        }
    }



    private void CustomerPage_Load(object sender, EventArgs e)
        {
            LoadCustomers(); 
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim(); 

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Please enter a customer name to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))

            {
                con.Open();
                string query = "SELECT * FROM Customers WHERE CustomerName LIKE @search"; 
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%"); 
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvCustomers.DataSource = dt; 
                }
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            using (AddCustomerForm addCustomerForm = new AddCustomerForm())
            {
                if (addCustomerForm.ShowDialog() == DialogResult.OK) 
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))

                    {
                        con.Open();
                        string query = "INSERT INTO Customers (CustomerName, Email, Phone, Address) VALUES (@name, @email, @phone, @address)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@name", addCustomerForm.CustomerName);
                            cmd.Parameters.AddWithValue("@email", addCustomerForm.Email);
                            cmd.Parameters.AddWithValue("@phone", addCustomerForm.Phone);
                            cmd.Parameters.AddWithValue("@address", addCustomerForm.Address);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCustomers(); 
                        }
                    }
                }
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvCustomers.SelectedRows[0];
            int customerID = Convert.ToInt32(selectedRow.Cells["CustomerID"].Value);
            string customerName = selectedRow.Cells["CustomerName"].Value.ToString();
            string email = selectedRow.Cells["Email"].Value.ToString();
            string phone = selectedRow.Cells["Phone"].Value.ToString();
            string address = selectedRow.Cells["Address"].Value.ToString();
            string CustomerType = selectedRow.Cells["CustomerType"].Value.ToString();
            decimal amountPaid = Convert.ToDecimal(selectedRow.Cells["AmountPaid"].Value);
            decimal amountRemaining = Convert.ToDecimal(selectedRow.Cells["AmountRemaining"].Value);

            using (EditCustomerForm editForm = new EditCustomerForm(customerID, customerName, email, phone, address,CustomerType, amountPaid, amountRemaining))
            {
                if (editForm.ShowDialog() == DialogResult.OK) 
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))

                    {
                        con.Open();
                        string query = "UPDATE Customers SET CustomerName = @name, Email = @email, Phone = @phone, Address = @address,CustomerType=@CustomerType, AmountPaid = @paid, AmountRemaining = @remaining WHERE CustomerID = @id";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@id", customerID);
                            cmd.Parameters.AddWithValue("@name", editForm.CustomerName);
                            cmd.Parameters.AddWithValue("@email", editForm.Email);
                            cmd.Parameters.AddWithValue("@phone", editForm.Phone);
                            cmd.Parameters.AddWithValue("@address", editForm.Address);
                            cmd.Parameters.AddWithValue("@CustomerType", editForm.CustomerType);
                            cmd.Parameters.AddWithValue("@paid", editForm.AmountPaid);
                            cmd.Parameters.AddWithValue("@remaining", editForm.AmountRemaining);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadCustomers(); 
                    MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvCustomers.SelectedRows[0];
            int customerID = Convert.ToInt32(selectedRow.Cells["CustomerID"].Value);
            string customerName = selectedRow.Cells["CustomerName"].Value.ToString();

            DialogResult confirm = MessageBox.Show($"Are you sure you want to delete {customerName}?",
                                                   "Confirm Deletion",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))

                {
                    con.Open();
                    string query = "DELETE FROM Customers WHERE CustomerID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", customerID);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadCustomers();

                MessageBox.Show($"Customer '{customerName}' has been deleted successfully!", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Deletion cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sortQuery = "";

            switch (cmbSort.SelectedItem.ToString())
            {
                case "By ID(Asc)":
                    sortQuery = "SELECT * FROM Customers ORDER BY CustomerID ASC";
                    break;
                case "By ID(Desc)":
                    sortQuery = "SELECT * FROM Customers ORDER BY CustomerID DESC";
                    break;
                case "By Name(Asc)":
                    sortQuery = "SELECT * FROM Customers ORDER BY CustomerName ASC";
                    break;
                case "By Name(Desc)":
                    sortQuery = "SELECT * FROM Customers ORDER BY CustomerName DESC";
                    break;
                default:
                    return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))

            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sortQuery, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvCustomers.DataSource = dt; 
                }
            }
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
