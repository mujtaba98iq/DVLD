using DVLD_with_Car_Rental_DataAccessLayer;
using DVLD_with_CarP_Rental_DataAccessLayer;
using DVLD2.CarRentalAllFiles.Vehicles.Controls;
using PeopleBusinessLayer;
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
using VehicleCategoriesBusinessLayer;
using VehiclesBusinessLayer;

namespace DVLD2.CarRentalAllFiles.Vehicles
{
    public partial class frmListVehiclesRecords : Form
    {

        public frmListVehiclesRecords()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private  async void frmListVehicles_Load(object sender, EventArgs e)
        {

            clsReservations reservations = await clsReservations.Find(2);
            if (reservations != null)
            {
                reservations.CreatedByUserID = 2;
                reservations.VehicleID = 2;
                reservations.RentalPricePerDay = 200;
                reservations.DropoffLocation = "2Mujtaba";
                reservations.PickupLocation = "2Ibrahim";
                reservations.RentalStartDate = DateTime.Now.AddDays(2);
                reservations.RentalEndDate = DateTime.Now.AddDays(12);
                reservations.InitialCheckNotes = "2Notes";
                reservations.InitialTotalDueAmount = 250;
                reservations.CustomerID = 2;
                reservations.InitialRentalDays = 2;
                if (!await reservations.Save())
                    MessageBox.Show("Null");
                else
                    MessageBox.Show("Has rows");
            }
            else
                MessageBox.Show("Null");


            MessageBox.Show(reservations.BookingID.ToString());
        }
    }
}
