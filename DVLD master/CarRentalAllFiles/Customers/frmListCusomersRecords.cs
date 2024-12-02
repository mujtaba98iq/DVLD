using CustomersBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.CarRentalAllFiles.Customers
{
    public partial class frmListCusomersRecords : Form
    {
        DataTable _dtCousomers;
        public frmListCusomersRecords()
        {
            InitializeComponent();
        }

        private async void frmListCusomersRecords_Load(object sender, EventArgs e)
        {
            _dtCousomers = await clsCustomers.GetAllCustomersWithDataTable();
            dgvCustomers.DataSource = _dtCousomers;
            lblRecordsCount.Text = _dtCousomers.Columns.Count.ToString();

            dgvCustomers.Columns[0].Name = "Customer ID";
            dgvCustomers.Columns[0].Width = 140;

            dgvCustomers.Columns[1].Name = "Pserson ID";
            dgvCustomers.Columns[1].Width = 140;

            dgvCustomers.Columns[2].Name = "License ID";
            dgvCustomers.Columns[2].Width = 140;

            dgvCustomers.Columns[3].Name = "Created By User ID";
            dgvCustomers.Columns[3].Width = 140;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string ColumnValue = "";
            string FillterValue = cbFilterBy.Text;
            switch (FillterValue)
            {
                case "Customer ID":
                    ColumnValue = "CustomerID";
                    break;

                case "Pserson ID":
                    ColumnValue = "PsersonID";
                    break;

                case "License ID":
                    ColumnValue = "LicenseID";
                    break;

                case "Created By User ID":
                    ColumnValue = "CreatedByUserID";
                    break;

                default:
                    ColumnValue = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtCousomers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtCousomers.Columns.Count.ToString();
                return;
            }           
                _dtCousomers.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnValue, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = _dtCousomers.Columns.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (cbFilterBy.Text == "None") { 
                txtFilterValue.Enabled=false;
            }
            else
                txtFilterValue.Enabled = true;
            lblRecordsCount.Text = _dtCousomers.Columns.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text != "None")
                e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;

        }
    }
}