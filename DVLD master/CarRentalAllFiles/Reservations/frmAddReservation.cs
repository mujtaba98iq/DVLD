using CustomersBusinessLayer;
using DevExpress.Utils;
using DriversBusinessLayer;
using DVLD_with_Car_Rental_DataAccessLayer;
using DVLD2.CarRentalAllFiles.GlobalForm;
using LicensesBusinessLayer;
using ReservationsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.CarRentalAllFiles.Reservations
{
    public partial class frmAddReservation : Form
    {
        clsReservations _reservations=null;
        int _vehicleID = -1;
        int _ReservationID = -1;
        enum enMode
        {
            Add,Update
        }
        enMode _Mode=enMode.Add;
        public frmAddReservation(int VehicleID)
        {
            InitializeComponent();
            _Mode = enMode.Add;
            _vehicleID = VehicleID;
        }
        public frmAddReservation(int ReservationID, int VehicleID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _vehicleID = VehicleID;

            _ReservationID = ReservationID;
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if(!clsDrivers.isDriversExist(clsGlobal.CurrentUser.PersonInfo.PersonID))
            {
                clsGlobal.MessageBoxDontHaveALicense();
                return;
            }
            if ((_Mode == enMode.Add && clsGlobal.MessageBoxAddSaveConfirmation("reservation")) ||
                (_Mode == enMode.Update && clsGlobal.MessageBoxUpdateSaveConfirmation("reservation")))
            {
                return;
            }

            if (!this.ValidateChildren())
            {
                clsGlobal.MessageBoxValidateChildren();
                return;
            }

            try
            {
                clsReservations reservation;

                if (_Mode == enMode.Add)
                {
                    reservation = new clsReservations();
                    _reservations = FillInfo(reservation);

                }
                else
                {
                    reservation = await clsReservations.Find(_ReservationID) ?? throw new InvalidOperationException("Reservation not found.");
                    _reservations = FillInfoForUpdate(reservation);

                }

                bool isSaved = await _reservations.Save();

                if (isSaved)
                {
                    _Mode = enMode.Update;
                    lblReservationID.Text = _reservations.BookingID.ToString();
                    clsGlobal.MessageBoxSavedSuccessfully();
                    restForm();
                }
                else
                {
                    clsGlobal.MessageBoxSavedFailed();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please check the entered values.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Operation error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void restForm()
        {
            ctrlCustomerCardWithFiltter1.restInfo();
            lblCustomerID.Text = "???";
            lblVehicleID.Text = "???";
            lblReservationID.Text = "???";
            ctrlVehilesCard1.restInfo();
            txtDropoffLocation.Text = "";
            txtInitialCheckNotes.Text = "";
            txtPaymentDetails.Text = "";
            txtPickupLocation.Text = "";
            nudPaidInitialTotalDueAmount.Value = 0;

        }

        private clsReservations FillInfo(clsReservations reservation)
        {
                   reservation.CustomerID = Convert.ToInt32(lblCustomerID.Text);
                   reservation.DropoffLocation = txtDropoffLocation.Text;
                   reservation.PickupLocation = txtPickupLocation.Text;
                   reservation.VehicleID = Convert.ToInt32(lblVehicleID.Text);
                   reservation.InitialCheckNotes = txtInitialCheckNotes.Text;
                   reservation.RentalStartDate = dtpRentalStartDate.Value;
                   reservation.RentalEndDate = dtpRentalEndDate.Value;
                   reservation.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            reservation.paymentInfoDTO = new PaymentInfoDTO( txtPaymentDetails.Text,nudPaidInitialTotalDueAmount.Value);
            reservation.paymentInfoDTO.PaidInitialTotalDueAmount = nudPaidInitialTotalDueAmount.Value;
            reservation.paymentInfoDTO.PaymentDetails = txtPaymentDetails.Text;
            return reservation;
        }
        private clsReservations FillInfoForUpdate(clsReservations reservation)
        {
            reservation.CustomerID = Convert.ToInt32(lblCustomerID.Text);
            reservation.DropoffLocation = txtDropoffLocation.Text;
            reservation.PickupLocation = txtPickupLocation.Text;
            reservation.VehicleID = Convert.ToInt32(lblVehicleID.Text);
            reservation.InitialCheckNotes = txtInitialCheckNotes.Text;
            reservation.RentalStartDate = dtpRentalStartDate.Value;
            reservation.RentalEndDate = dtpRentalEndDate.Value;
            reservation.CreatedByUserID = clsGlobal.CurrentUser.UserID;
    
            return reservation;
        }

        private async void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (ctrlCustomerCardWithFiltter1.CustomerID == -1)
            {
                clsGlobal.MessageBoxNextFailed("Customer ID is required.");
                return;
            }

            using (frmWaitForm frm = new frmWaitForm(clsGlobal.ShortWait))
            {
                frm.ShowDialog(); 
                                          
                await Task.Run(() => LoadInfoForAddOperation(_vehicleID)); 

            }
            tcReservationInfo.SelectedTab = tcReservationInfo.TabPages["tpReservationinfo"];
        }



        private void LoadInfoForAddOperation(int vehicleID)
        {
            if (lblCustomerID.InvokeRequired)
            {
                lblCustomerID.Invoke(new MethodInvoker(() =>
                {
                    lblCustomerID.Text = ctrlCustomerCardWithFiltter1.CustomerID.ToString();
                }));
            }
            else
            {
                lblCustomerID.Text = ctrlCustomerCardWithFiltter1.CustomerID.ToString();
            }

            lblVehicleID.Text = vehicleID.ToString();
            ctrlVehilesCard1.LoadVelhileInfo(vehicleID);
        }

        private async void LoadInfoForUpdateOperation(int reservationID)
        {
            if(reservationID<=0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            try
            {
                _ReservationID = reservationID;
                _reservations = await clsReservations.Find(reservationID);
                if (_reservations == null)
                {
                    clsGlobal.MessageBoxListOrObjectIsEmpty();
                    return;
                }
                pnlPaymentInfo.Visible = false;
                pnlPaymentInfo.Enabled = false;

                ctrlCustomerCardWithFiltter1.LoadInfo(_reservations.CustomerID);

                LoadInfoForAddOperation(_reservations.VehicleID);

                lblReservationID.Text = _ReservationID.ToString();
                dtpRentalStartDate.Value = _reservations.RentalStartDate;
                dtpRentalEndDate.Value = _reservations.RentalEndDate;
                txtDropoffLocation.Text = _reservations.DropoffLocation;
                txtPickupLocation.Text = _reservations.PickupLocation;
                txtInitialCheckNotes.Text = _reservations.InitialCheckNotes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading reservation info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


      
        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            GenralTextBoxValidating((TextBox)sender, e);
        }
        void GenralTextBoxValidating(TextBox textBox,CancelEventArgs e)
        {
            if (txtPickupLocation.Text == "")
            {
                clsGlobal.ValidatingtextBoxNull(textBox,errorProvider1,e);
                return;
            }
        }

      
       

        private void ctrlVehilesCard1_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                LoadInfoForUpdateOperation(_ReservationID);
            }
        }

        private void tpCustomerInfo_Click(object sender, EventArgs e)
        {

        }

        private  void btnReservationNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                btnSave_Click(sender, e);
                return;
            }

            if (ctrlCustomerCardWithFiltter1.CustomerID == -1)
            {
                clsGlobal.MessageBoxNextFailed("Customer ID is required.");
                return;
            }
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fild is required you must fill them.");
                return;
            }

            using (frmWaitForm frm = new frmWaitForm(clsGlobal.ShortWait))
            {
                frm.ShowDialog();
                nudPaidInitialTotalDueAmount.Focus();
                btnSave.Enabled = true;
            }
            tcReservationInfo.SelectedTab = tcReservationInfo.TabPages["tpPaymentInfo"];
        }

        private void frmAddReservation_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                LoadInfoForUpdateOperation(_ReservationID);
            }
            else
            {
                LoadInfoForAddOperation(_vehicleID);
            }
        }
    }
}
