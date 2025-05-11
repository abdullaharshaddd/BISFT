using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;


namespace BISFT
{
    public partial class EditCustomerForm : Form
    {
        public int CustomerID { get; private set; }
        public string CustomerName => txtCustomerName.Text;
        public string Email => txtEmail.Text;
        public string Phone => txtPhone.Text;
        public string Address => txtAddress.Text;
        public decimal AmountPaid => decimal.Parse(txtAmountPaid.Text);
        public decimal AmountRemaining => decimal.Parse(txtAmountRemaining.Text);

        public string CustomerType => cmbCustomerType.Text;


        public EditCustomerForm(int customerID, string customerName, string email, string phone, string address,string CustomerType,  decimal amountPaid, decimal amountRemaining)
        {
            InitializeComponent();

            CustomerID = customerID;
            txtCustomerID.Text = customerID.ToString();
            txtCustomerName.Text = customerName;
            txtEmail.Text = email;
            txtPhone.Text = phone;
            txtAddress.Text = address;
            txtAmountPaid.Text = amountPaid.ToString();
            txtAmountRemaining.Text = amountRemaining.ToString();

            cmbCustomerType.Items.Clear(); 
            cmbCustomerType.Items.Add("Retail");
            cmbCustomerType.Items.Add("Wholesale");

            if (CustomerType == "Retail")
                cmbCustomerType.SelectedIndex = 0; 
            else if (CustomerType == "Wholesale")
                cmbCustomerType.SelectedIndex = 1;  
            else
                cmbCustomerType.SelectedIndex = -1; 

            txtCustomerID.ReadOnly = true;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.KeyDown += TextBox_ArrowKeyNavigation;
                }
            }

            this.cmbCustomerType.SelectedIndexChanged += new System.EventHandler(this.cmbCustomerType_SelectedIndexChanged);
        }



        string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;

        private void UpdateCustomerType(string customerType)
        {
            try
            {
                if (customerType != cmbCustomerType.SelectedItem.ToString())
                {
                    string query = "UPDATE Customers SET CustomerType = @CustomerType WHERE CustomerID = @CustomerID";
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@CustomerType", customerType);
                        cmd.Parameters.AddWithValue("@CustomerID", this.CustomerID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Customer type updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void TextBox_ArrowKeyNavigation(object sender, KeyEventArgs e)
        {
            TextBox currentBox = sender as TextBox;

            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl(currentBox, true, true, true, true);
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl(currentBox, false, true, true, true); 
            }
            else if (e.KeyCode == Keys.Enter && sender == txtAmountRemaining)
            {
                e.SuppressKeyPress = true;
                btnUpdate.PerformClick();
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtAmountPaid.Text) ||
                string.IsNullOrWhiteSpace(txtAmountRemaining.Text))
            {
                MessageBox.Show("All fields are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"^\d{4}-\d{7}$"))
            {
                MessageBox.Show("Phone number must be in the format 1234-5678901.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmountPaid.Text, out decimal amountPaid) ||
                !decimal.TryParse(txtAmountRemaining.Text, out decimal amountRemaining))
            {
                MessageBox.Show("Amount Paid and Amount Remaining must be numeric.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["BISFTDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))

            {
                con.Open();
                string query = @"UPDATE Customers 
                         SET CustomerName = @name, Email = @email, Phone = @phone, Address = @address, 
                             AmountPaid = @paid, AmountRemaining = @remaining 
                         WHERE CustomerID = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", txtCustomerID.Text);
                    cmd.Parameters.AddWithValue("@name", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@paid", amountPaid);
                    cmd.Parameters.AddWithValue("@remaining", amountRemaining);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtCustomerID_TextChanged(object sender, EventArgs e) { }
        private void txtCustomerName_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtPhone_TextChanged(object sender, EventArgs e) { }
        private void txtAddress_TextChanged(object sender, EventArgs e) { }
        private void lblCustomerID_Click(object sender, EventArgs e) { }
        private void lblCustomerName_Click(object sender, EventArgs e) { }
        private void lblEmail_Click(object sender, EventArgs e) { }
        private void lblPhone_Click(object sender, EventArgs e) { }
        private void lblAddress_Click(object sender, EventArgs e) { }

        private void EditCustomerForm_Load(object sender, EventArgs e)
        {

        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cmbCustomerType.SelectedItem.ToString();
            if (selectedType != CustomerType) 
            {
                UpdateCustomerType(selectedType);
            }
        }


    }
}
