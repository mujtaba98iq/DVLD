using DVLD_with_Car_Rental_DataAccessLayer;
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

namespace DVLD2.CarRentalAllFiles.Reservations
{
    public partial class frmfrmListReservationRecords : Form
    {
        List<ReservationsDTO> _reservations;
        DataTable _dtReservations;
        public frmfrmListReservationRecords()
        {
            InitializeComponent();
        }
        public int currentReservationId
        {
            get { return Convert.ToInt32(dgvPrescriptions.CurrentRow.Cells[0].Value); }
            }
        public int currentVehicleId
        {
            get { return Convert.ToInt32(dgvPrescriptions.CurrentRow.Cells[2].Value); }
        }

        private async void frmfrmListReservationRecords_Load(object sender, EventArgs e)
        {
            _dtReservations = await clsReservations.GetAllReservationsWithDataTable();
            dgvPrescriptions.DataSource = _dtReservations;
           lblRecordsCount.Text=dgvPrescriptions.Columns.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            dgvPrescriptions.Columns[0].HeaderText = "Reservation ID";
            dgvPrescriptions.Columns[0].Width = 60;

            dgvPrescriptions.Columns[1].HeaderText = "Customer ID";
            dgvPrescriptions.Columns[1].Width = 60;

            dgvPrescriptions.Columns[2].HeaderText = "Vehicle ID";
            dgvPrescriptions.Columns[2].Width = 60;

            dgvPrescriptions.Columns[3].HeaderText = "Rental Start Date";
            dgvPrescriptions.Columns[3].Width = 100;

            dgvPrescriptions.Columns[4].HeaderText = "Rental End Date";
            dgvPrescriptions.Columns[4].Width = 100;

            dgvPrescriptions.Columns[5].HeaderText = "Pickup Location";
            dgvPrescriptions.Columns[5].Width = 100;

            dgvPrescriptions.Columns[6].HeaderText = "Dropoff Location";
            dgvPrescriptions.Columns[6].Width = 100;

            dgvPrescriptions.Columns[7].HeaderText = "Initial Rental Days";
            dgvPrescriptions.Columns[7].Width = 80;

            dgvPrescriptions.Columns[8].HeaderText = "Rental Price Per Day";
            dgvPrescriptions.Columns[8].Width = 80;

            dgvPrescriptions.Columns[9].HeaderText = "Initial Total Due Amount";
            dgvPrescriptions.Columns[9].Width = 80;

            dgvPrescriptions.Columns[10].HeaderText = "Initial Check Notes";
            dgvPrescriptions.Columns[10].Width = 120;

            dgvPrescriptions.Columns[11].HeaderText = "Created By User ID";
            dgvPrescriptions.Columns[11].Width = 60;

        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if(cbFilterBy.Text=="None")
                txtFilterValue.Enabled = false;
            else 
                txtFilterValue.Enabled = true;
            
            txtFilterValue.Focus();
            txtFilterValue.Text = "";
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string ColumnValue = "";
            string FillterValue = cbFilterBy.Text;
            switch (FillterValue)
            {
                case "Booking ID":
                    ColumnValue = "BookingID";
                    break;

                case "Customer ID":
                    ColumnValue = "CustomerID";
                    break;

                case "Vehicle ID":
                    ColumnValue = "VehicleID";
                    break;

                case "Pickup Location":
                    ColumnValue = "PickupLocation";
                    break;

                case "Dropoff Location":
                    ColumnValue = "DropoffLocation";
                    break;

                case "Initial Rental Days":
                    ColumnValue = "InitialRentalDays";
                    break;
                case "Rental Price Per Day":
                    ColumnValue = "RentalPricePerDay";
                    break;
                case "Initial Total Due Amount":
                    ColumnValue = "InitialTotalDueAmount";
                    break;
                case "Initial Check Notes":
                    ColumnValue = "InitialCheckNotes";
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
                _dtReservations.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtReservations.Columns.Count.ToString();
                return;
            }

            if (ColumnValue == "PickupLocation" || ColumnValue == "DropoffLocation")
                _dtReservations.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", ColumnValue, txtFilterValue.Text.Trim());
            else
                _dtReservations.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnValue, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = _dtReservations.Columns.Count.ToString();

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            int VehicleID=Convert.ToInt32( dgvPrescriptions.CurrentRow.Cells[2].Value);
            frmAddReservation frm = new frmAddReservation(VehicleID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            frmShowReservationDetails frm = new frmShowReservationDetails(currentReservationId);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddReservation frm = new frmAddReservation(currentReservationId, currentVehicleId);
            frm.ShowDialog();
            frmfrmListReservationRecords_Load(sender, e);
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (clsGlobal.MessageBoxAddDeleteConfirmation("Reservation"))
                return;
            if (await clsReservations.DeleteReservations(currentReservationId))
                clsGlobal.MessageBoxDeleteSuccessfully();
            else
                clsGlobal.MessageBoxDeleteFailed();
            frmfrmListReservationRecords_Load(null,null);
        }
    }
}
