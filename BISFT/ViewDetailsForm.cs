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
    public partial class ViewDetailsForm: Form
    {
        private int saleId;

        public ViewDetailsForm(int saleId)
        {
            InitializeComponent();
            this.saleId = saleId;
            this.Load += ViewDetailsForm_Load;
        }
        private void ViewDetailsForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = DataBaseAccess.GetConnection())
            {
                con.Open();

                string saleQuery = "SELECT CustomerName, SaleDate, TotalAmount FROM Sales WHERE SaleID = @id";
                using (SqlCommand cmd = new SqlCommand(saleQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", saleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblSaleID.Text = saleId.ToString();
                            lblCustomer.Text = reader["CustomerName"].ToString();
                            lblDate.Text = Convert.ToDateTime(reader["SaleDate"]).ToString("dd MMM yyyy");
                            lblTotal.Text = "Rs. " + Convert.ToDecimal(reader["TotalAmount"]).ToString("N2");
                        }
                    }
                }

                string itemQuery = "SELECT ProductName, Quantity, PricePerUnit, (Quantity * PricePerUnit) AS Total FROM SaleItems WHERE SaleID = @id";
                using (SqlCommand cmd = new SqlCommand(itemQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", saleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dgvProducts.Rows.Clear();
                        while (reader.Read())
                        {
                            dgvProducts.Rows.Add(
                                reader["ProductName"].ToString(),
                                reader["Quantity"].ToString(),
                                Convert.ToDecimal(reader["PricePerUnit"]).ToString("N2"),
                                Convert.ToDecimal(reader["Total"]).ToString("N2")
                            );
                        }
                    }
                }
            }
        }




    }
}
