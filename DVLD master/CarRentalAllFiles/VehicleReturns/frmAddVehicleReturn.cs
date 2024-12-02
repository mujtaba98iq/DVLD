using DVLD_with_Car_Rental_DataAccessLayer;
using DVLD2.CarRentalAllFiles.GlobalForm;
using PaymentsBusinessLayer;
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
using VehicleReturnsBusinessLayer;

namespace DVLD2.CarRentalAllFiles.VehicleReturns
{
    public partial class frmAddVehicleReturn : Form
    {
        int _ReservationID = -1;
        clsReservations _reservations;
        clsPayments _payments;
        int _paymentID;
        public frmAddVehicleReturn(int ReservationID)
        {
            InitializeComponent();
            LoadInfo(ReservationID);
        }

       
        private async void LoadInfo(int ReservationID)
        {
            if (ReservationID <= 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            try
            {
                _ReservationID = ReservationID;
                _reservations = await clsReservations.Find(_ReservationID);
                if (_reservations==null)
                {
                    clsGlobal.MessageBoxListOrObjectIsEmpty();
                    return;
                }

                lblReservationID.Text=_ReservationID.ToString();
                lblVehicleID.Text=_reservations.VehicleInfo.VehicleID.ToString();
                ctrlVehilesCard1.LoadVelhileInfo(_reservations.VehicleInfo.VehicleID);
                btnSave.Enabled = true;

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        



        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (clsGlobal.MessageBoxAddSaveConfirmation("Return Vehicle"))
                return;

            try
            {


                ReturnCarDTO returnCarDTO = new ReturnCarDTO();
                returnCarDTO.FinalCheckNotes = txtFinalCheckNotes.Text;
                returnCarDTO.AdditionalCharges = nudAdditionalCharges.Value;
                returnCarDTO.TransactionDate = _reservations.PaymentInfo.TransactionDate;
                returnCarDTO.ReservationID = _ReservationID;
                returnCarDTO.PaymentID = _reservations.PaymentInfo.PaymentID;
                returnCarDTO.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                returnCarDTO.Mileage = (int)nudMileage.Value;
                returnCarDTO.PaidInitialTotalDueAmount = _reservations.PaymentInfo.PaidInitialTotalDueAmount;
                returnCarDTO.PaymentDetails = _reservations.PaymentInfo.PaymentDetails;
                _paymentID = _reservations.PaymentInfo.PaymentID;

                bool isSaved = await clsPayments.ReturnCar(returnCarDTO);
                MessageBox.Show(isSaved.ToString());
                if (isSaved)
                {
                    clsGlobal.MessageBoxSavedSuccessfully();
                    DeptPaymentPage();
                }
                else
                {
                    clsGlobal.MessageBoxSavedFailed();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeptPaymentPage()
        {
            using (frmWaitForm frm = new frmWaitForm(clsGlobal.ShortWait))
            {
                frm.ShowDialog();

                await Task.Run(() => LoadInfoForDeptPaymentPage(_paymentID));

            }

        }

        private   void LoadInfoForDeptPaymentPage(int PaymentID)
        {
            if (PaymentID <= 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            _paymentID=PaymentID;
            _payments =  clsPayments.Find(_paymentID);
            if (_payments == null)
            {
                clsGlobal.MessageBoxNotFound("Payment");
                return;
            }



            if (_payments.TotalRemaining > 0)
            {
                lblToSystemOrToCustomer.Text = "To Customer";
                lblMoneyForCreditorOrDebtor.Text = _payments.TotalRemaining.ToString();
                lblMoneyForCreditorOrDebtor.BackColor = Color.Red;
            }
            else
            {
                lblToSystemOrToCustomer.Text = "To System";
                lblMoneyForCreditorOrDebtor.Text = (_payments.TotalRemaining * -1).ToString();
                lblMoneyForCreditorOrDebtor.BackColor = Color.LightGreen;
            }
            if (tcReservationInfo.InvokeRequired) {

                tcReservationInfo.Invoke(new Action(() =>
                    {
                        tcReservationInfo.SelectedTab = tcReservationInfo.TabPages["tpCompletePayment"];
                        btnDebtPayment.Enabled = true;

                    }
                    ));

            }
            else
            {
                tcReservationInfo.SelectedTab = tcReservationInfo.TabPages["tpCompletePayment"];

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnDebtPayment_Click(object sender, EventArgs e)
        {
            if (clsGlobal.MessageBoxUpdateSaveConfirmation("And all Debt Payment"))
                return;
           if(await _payments.DebtPayment())
            {
                clsGlobal.MessageBoxSavedSuccessfully();
                btnDebtPayment.Enabled = false;
                btnSave.Enabled = false;
            }
           else
            {
                clsGlobal.MessageBoxSavedFailed();
            }

        }
    }
}
