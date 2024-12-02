using DVLD2.CarRentalAllFiles.VehicleReturns;
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

namespace DVLD2.CarRentalAllFiles.Reservations.Controls
{

    public partial class ctrlReservationVehicleCard : UserControl
    {
        int _reservationID = -1;
        clsReservations _reservations;
        FlowLayoutPanel _PanelForReservaionList;
        public ctrlReservationVehicleCard()
        {
            InitializeComponent();
        }
        public async void LoadInfo(int ReservationID)
        {

            if (ReservationID <= 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            try
            {
                _reservationID = ReservationID;
                _reservations =await clsReservations.Find(_reservationID);

                if (_reservations == null)
                {
                    clsGlobal.MessageBoxNotFound("reservation");
                    return;
                }
                lblReservationID.Text=_reservationID.ToString();
                lblVehicleID.Text=_reservations.VehicleID.ToString();
                lblDropoffLocation.Text = _reservations.DropoffLocation;
                lblInitialRentalDays.Text = _reservations.InitialRentalDays.ToString();
                lblPickupLocation.Text= _reservations.PickupLocation;
                lblPricePerDay.Text=_reservations.RentalPricePerDay.ToString();
                lblVehicleName.Text=_reservations.VehicleInfo.VehicleName;
                lblYear.Text = _reservations.VehicleInfo.Year.ToString();
                if (_reservations.VehicleInfo.IsAvailabe)
                {
                    lblIsAvalibel.Text ="Is Availabe";
                    lblIsAvalibel.BackColor = Color.LightBlue;                    
                }
                else
                {
                    lblIsAvalibel.Text = "Not Availabe";
                    lblIsAvalibel.BackColor = Color.Red;
                }
                pbVehicle.ImageLocation = _reservations.VehicleInfo.VehicleImage;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }


        }

        private void cmsbtnReturnVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmAddVehicleReturn frm = new frmAddVehicleReturn(_reservationID);
            frm.ShowDialog();
        }

        private void cmsbtnVehicleDetails_Click(object sender, EventArgs e)
        {
            frmShowReservationDetails frm = new frmShowReservationDetails(_reservationID);
            frm.ShowDialog();
        }

        private void ctrlReservationVehicleCard_Load(object sender, EventArgs e)
        {

        }
    }
}
