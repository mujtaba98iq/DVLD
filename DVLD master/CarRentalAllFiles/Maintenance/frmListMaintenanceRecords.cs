using MaintenanceBusinessLayer;
using ReservationsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.CarRentalAllFiles.Maintenance
{
    public partial class frmListMaintenanceRecords : Form
    {
        DataTable _dtMaintenance;
        public frmListMaintenanceRecords()
        {
            InitializeComponent();
        }

        private int maintenanceId
        {
            get {return Convert.ToInt32(dgvPrescriptions.CurrentRow.Cells[0].Value);}   
        }

        private async void frmListMaintenanceRecords_Load(object sender, EventArgs e)
        {
            _dtMaintenance = await clsMaintenance.GetAllMaintenanceWithDataTable();
            dgvPrescriptions.DataSource = _dtMaintenance;
            lblRecordsCount.Text = dgvPrescriptions.Columns.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            dgvPrescriptions.Columns[0].HeaderText = "Maintenance ID";
            dgvPrescriptions.Columns[0].Width = 100;

            dgvPrescriptions.Columns[1].HeaderText = "Vehicle ID";
            dgvPrescriptions.Columns[1].Width = 100;

            dgvPrescriptions.Columns[2].HeaderText = "Description";
            dgvPrescriptions.Columns[2].Width = 100;

            dgvPrescriptions.Columns[3].HeaderText = "Maintenance Date";
            dgvPrescriptions.Columns[3].Width = 150;

            dgvPrescriptions.Columns[4].HeaderText = "Cost";
            dgvPrescriptions.Columns[4].Width = 120;

            dgvPrescriptions.Columns[5].HeaderText = "Created By User ID";
            dgvPrescriptions.Columns[5].Width = 120;

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            int vehicleId=Convert.ToInt32(dgvPrescriptions.CurrentRow.Cells[1].Value);
            frmAddUpdateMaintenance frm=new frmAddUpdateMaintenance(vehicleId);
            frm.ShowDialog();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string ColumnValue = "";
            string FillterValue = cbFilterBy.Text;
            switch (FillterValue)
            {
                case "Maintenance ID":
                    ColumnValue = "MaintenanceID";
                    break;

                case "Vehicle ID":
                    ColumnValue = "VehicleID";
                    break;

                case "Description":
                    ColumnValue = "Description";
                    break;

                case "Maintenance Date":
                    ColumnValue = "MaintenanceDate";
                    break;

                case "Cost":
                    ColumnValue = "Cost";
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
                _dtMaintenance.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtMaintenance.Columns.Count.ToString();
                return;
            }

            if (ColumnValue == "Description")
                _dtMaintenance.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", ColumnValue, txtFilterValue.Text.Trim());
            else
                _dtMaintenance.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnValue, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = _dtMaintenance.Columns.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowMaintenanceDetals frm = new frmShowMaintenanceDetals(maintenanceId);
            frm.ShowDialog();
        }
    }
}
