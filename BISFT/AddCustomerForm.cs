using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BISFT
{
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();

            
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox textBox)
                {
                    textBox.KeyDown += TextBox_ArrowNavigation;
                }
            }
        }

        
        private void TextBox_ArrowNavigation(object sender, KeyEventArgs e)
        {
            TextBox currentBox = sender as TextBox;

            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl(currentBox, true, true, true, true); 
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl(currentBox, false, true, true, true); 
                e.Handled = true;
            }
        }



        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {

        }

        public string CustomerName => txtCustomerName.Text;
        public string Email => txtEmail.Text;
        public string Phone => txtPhone.Text;
        public string Address => txtAddress.Text;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CustomerName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Phone) ||
                string.IsNullOrWhiteSpace(Address) ||
                string.IsNullOrWhiteSpace(txtAmountPaid.Text) ||
                string.IsNullOrWhiteSpace(txtAmountRemaining.Text))
            {
                MessageBox.Show("All fields are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(CustomerName, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Customer name should only contain letters.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(Phone, @"^\d{4}-\d{7}$"))
            {
                MessageBox.Show("Phone number must be in the format: 1234-5678901", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmountPaid.Text, out _) || !decimal.TryParse(txtAmountRemaining.Text, out _))
            {
                MessageBox.Show("Amount Paid and Amount Remaining must be valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }





        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCustomerName_Click(object sender, EventArgs e)
        {

        }

        private void lblAddress_Click(object sender, EventArgs e)
        {

        }

        private void lblPhone_Click(object sender, EventArgs e)
        {

        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close();
        }

        private void txtAmountRemaining_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmountPaid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
