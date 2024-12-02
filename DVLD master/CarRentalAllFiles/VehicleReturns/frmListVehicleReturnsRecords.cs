using DVLD2.CarRentalAllFiles.GlobalForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleReturnsBusinessLayer;

namespace DVLD2.CarRentalAllFiles.VehicleReturns
{
    public partial class frmListVehicleReturnsRecords : Form
    {
        DataTable _dtVehicleReturns;

        public frmListVehicleReturnsRecords()
        {
            InitializeComponent();
        }

        private async void frmListVehicleReturnsRecords_Load(object sender, EventArgs e)
        {
            using(frmWaitForm frm =new frmWaitForm(clsGlobal.ShortWait))
            {
                _dtVehicleReturns = await clsVehicleReturns.GetAllVehicleReturnsWithDataTable();
                frm.ShowDialog();
            }

            dgvVehicleReturns.DataSource = _dtVehicleReturns;
            lblRecordsCount.Text = dgvVehicleReturns.Columns.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            dgvVehicleReturns.Columns[0].HeaderText = "Returen ID";
            dgvVehicleReturns.Columns[0].Width = 60;

            dgvVehicleReturns.Columns[1].HeaderText = "Actual Return Date";
            dgvVehicleReturns.Columns[1].Width = 120;

            dgvVehicleReturns.Columns[2].HeaderText = "Actual Rental Days";
            dgvVehicleReturns.Columns[2].Width = 60;

            dgvVehicleReturns.Columns[3].HeaderText = "Mileage";
            dgvVehicleReturns.Columns[3].Width = 100;

            dgvVehicleReturns.Columns[4].HeaderText = "Consumed Milaeage";
            dgvVehicleReturns.Columns[4].Width = 80;

            dgvVehicleReturns.Columns[5].HeaderText = "Final Check Notes";
            dgvVehicleReturns.Columns[5].Width = 140;

            dgvVehicleReturns.Columns[6].HeaderText = "Additional Charges";
            dgvVehicleReturns.Columns[6].Width = 100;

            dgvVehicleReturns.Columns[7].HeaderText = "Actual Total Due Amount";
            dgvVehicleReturns.Columns[7].Width = 120;

            dgvVehicleReturns.Columns[8].HeaderText = "Created By User ID";
            dgvVehicleReturns.Columns[8].Width = 60;

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if(cbFilterBy.Text=="None")
                txtFilterValue.Enabled = false;
            
            else
                txtFilterValue.Enabled=true;
            txtFilterValue.Focus();
            txtFilterValue.Text = "";
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string ColumnValue = "";
            string FillterValue = cbFilterBy.Text;
            switch (FillterValue)
            {
                case "Returen ID":
                    ColumnValue = "ReturenID";
                    break;

                case "Actual Rental Days":
                    ColumnValue = "ActualRentalDays";
                    break;

                case "Mileage":
                    ColumnValue = "Mileage";
                    break;

                case "Consumed Milaeage":
                    ColumnValue = "ConsumedMilaeage";
                    break;

                case "Final Check Notes":
                    ColumnValue = "FinalCheckNotes";
                    break;

                case "Additional Charges":
                    ColumnValue = "AdditionalCharges";
                    break;
                case "Actual Total Due Amount":
                    ColumnValue = "ActualTotalDueAmount";
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
                _dtVehicleReturns.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtVehicleReturns.Columns.Count.ToString();
                return;
            }

            if (ColumnValue == "FinalCheckNotes" )
                _dtVehicleReturns.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", ColumnValue, txtFilterValue.Text.Trim());
            else
                _dtVehicleReturns.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnValue, txtFilterValue.Text.Trim());

            lblRecordsCount.Text=_dtVehicleReturns.Columns.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text!= "Final Check Notes")
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
         

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
