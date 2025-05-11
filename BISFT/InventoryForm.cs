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
    public partial class InventoryForm : Form
    {
        public InventoryForm()
        {
            InitializeComponent();
            DataBaseAccess.InventoryUpdated += InventoryUpdatedHandler; 
            LoadInventoryItems();
            LoadInventorySummary();
        }


        private void InventoryUpdatedHandler(object sender, EventArgs e)
        {
            LoadInventoryItems();  
            LoadInventorySummary();
            int lowStockCount = Convert.ToInt32(DataBaseAccess.GetLowStockCount());
            if (lowStockCount > 0)
            {
                MessageBox.Show($"⚠️ There are {lowStockCount} products low in stock!", "Low Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DataBaseAccess.InventoryUpdated -= InventoryUpdatedHandler;  
            base.OnFormClosed(e);
        }

        private void LoadInventoryItems()
        {
            DataTable inventoryItems = DataBaseAccess.GetAllInventoryItems();
            dataGridView1.DataSource = null;  
            dataGridView1.Columns.Clear();    
            dataGridView1.DataSource = inventoryItems;  
        }


        private void LoadInventorySummary()
        {
            lblTotalProductsCount.Text = DataBaseAccess.GetTotalProductsCount().ToString();
            lblLowStockC.Text = DataBaseAccess.GetLowStockCount().ToString();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["quantity"].Value != DBNull.Value && row.Cells["thresholdvalue"].Value != DBNull.Value)
                {
                    int quantity = Convert.ToInt32(row.Cells["quantity"].Value);
                    int threshold = Convert.ToInt32(row.Cells["thresholdvalue"].Value);

                    if (quantity <= threshold)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;  // red highlight
                    }
                }
            }


        }

        private void pnlnavi_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnorders_Click(object sender, EventArgs e)
        {
            SalesForm salesForm = new SalesForm();
            salesForm.Show();
            this.Hide(); 
        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            using (AddProductForm addForm = new AddProductForm())
            {
                var result = addForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadInventoryItems();
                }
            }
        }
   

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.", "Select Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this product?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int itemId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ItemID"].Value);

                    DataBaseAccess.DeleteInventoryItem(itemId);

                    LoadInventoryItems();

                    MessageBox.Show("Product deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a product to update.", "Select Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int itemId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ItemID"].Value);
            using (UpdateProductForm updateForm = new UpdateProductForm(itemId))
            {
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    LoadInventoryItems(); 
                }

            }


        }


        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtsearch.Text))
            {
                LoadInventoryItems();  // Load all items if the search box is cleared
            }
            else
            {
                int itemId;
                if (int.TryParse(txtsearch.Text, out itemId))
                {
                    DataTable results = DataBaseAccess.SearchInventoryItemsById(itemId);
                    if (results.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = results;
                    }
                    else
                    {
                        dataGridView1.DataSource = null; // Clear existing results
                    }
                }
                else
                {
                    dataGridView1.DataSource = null; // Clear existing results if input is not a valid integer
                }
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            int itemId;
            if (int.TryParse(txtsearch.Text, out itemId))  // Checks if the input is a valid integer
            {
                DataTable results = DataBaseAccess.SearchInventoryItemsById(itemId);
                dataGridView1.DataSource = results;  // Assigns results, will be empty if no results are found
            }
            else
            {
                dataGridView1.DataSource = null;  // Clears the grid if input is not a valid integer
            }
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            LoadInventorySummary();
        }
      





        private void lblTotalProductsCount_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalProducts_Click(object sender, EventArgs e)
        {

        }

        private void lblLowStock_Click(object sender, EventArgs e)
        {

        }

        private void lblLowStockC_Click(object sender, EventArgs e)
        {

        }

    

        private void btncustomers_Click(object sender, EventArgs e)
        {
            CustomerPage customerPage = new CustomerPage();

            // When the CustomerPage is closed, show Inventory again (optional)
            customerPage.FormClosed += (s, args) => this.Show();

            customerPage.Show();
            this.Hide();  // Hide the Inventory Form
        }

    }
}
