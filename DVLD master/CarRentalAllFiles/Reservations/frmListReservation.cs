using DVLD_with_Car_Rental_DataAccessLayer;
using DVLD2.CarRentalAllFiles.GlobalForm;
using DVLD2.CarRentalAllFiles.Reservations.Controls;
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
    public partial class frmListReservation : Form
    {
        public frmListReservation()
        {
            InitializeComponent();
        }
        async void LoadInfListReservation()
        {
            List<ReservationsDTO> listReservations = await clsReservations.GetAllReservations();
            if (listReservations == null)
            {
                clsGlobal.MessageBoxListOrObjectIsEmpty();
                return;
            }

            using (frmWaitForm frm = new frmWaitForm(clsGlobal.ShortWait))
            {
                frm.ShowDialog();

                foreach (var reservation in listReservations)
                {
                    ctrlReservationVehicleCard reservatoinCard = new ctrlReservationVehicleCard();
                    reservatoinCard.LoadInfo(reservation.BookingID);
                    pnlReservations.Controls.Add(reservatoinCard);
                }
            }
               

        }

        private void frmListReservation_Load(object sender, EventArgs e)
        {
            LoadInfListReservation();

        }
    }
}
