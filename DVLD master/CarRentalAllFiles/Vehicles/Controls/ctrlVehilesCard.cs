using DVLD2.CarRentalAllFiles.Maintenance;
using DVLD2.CarRentalAllFiles.Reservations;
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
using VehiclesBusinessLayer;

namespace DVLD2.CarRentalAllFiles.Vehicles.Controls
{
    public partial class ctrlVehilesCard : UserControl
    {
        int _VehicleID = -1;
        clsVehicles _vehicle;
        public ctrlVehilesCard()
        {
            InitializeComponent();
        }

        public int VehicleID
        {
            get { return _VehicleID; }
        }

        public async void LoadVelhileInfo(int VehicleID)
        {
            if(VehicleID<1)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }   

             _vehicle= await clsVehicles.Find(VehicleID);
            if (_vehicle == null)
            {
                clsGlobal.MessageBoxNotFound("Vehicle");
                return;
            }
            _VehicleID = VehicleID;
            lblVehicleName.Text = _vehicle.VehicleName;
            lblYear.Text = _vehicle.Year.ToString();
            lblVehicleID.Text = _vehicle.VehicleID.ToString();
            lblPricePerDay.Text=$"{_vehicle.RentalPricePerDay.ToString()} $";
            pbVehicle.ImageLocation = _vehicle.VehicleImage;
            if (_vehicle.IsAvailabe)
            {
                lblIsAvalibel.Text = "Available";
                lblIsAvalibel.BackColor = Color.LightBlue;
            }
            else
            {
                lblIsAvalibel.Text = "Not Available";
                lblIsAvalibel.BackColor = Color.Red;
            }
                


        }
        public void restInfo()
        {
            lblVehicleName.Text = "???";
            lblYear.Text = "???";
            lblVehicleID.Text = "???";
            lblPricePerDay.Text = "???";
            pbVehicle.ImageLocation = "???";
        }
        private void vehicleDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cmsbtnVehicleDetails_Click(object sender, EventArgs e)
        {
            frmShowVehicleDetails frm = new frmShowVehicleDetails(_VehicleID);
            frm.ShowDialog();   
        }

        private void cmsbtnVehicleReservation_Click(object sender, EventArgs e)
        {
            if (!_vehicle.IsAvailabe)
            {
                MessageBox.Show("Not Avaiabel");
                return;
            }
            frmAddReservation frm = new frmAddReservation(_VehicleID);
            frm.ShowDialog();
        }

       

        private void makeNotAvailabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Emplemeted Yet !");
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateVehicle frm =new frmAddUpdateVehicle(_VehicleID);
            frm.ShowDialog();
        }

        private void vehicleManToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_vehicle.IsAvailabe)
            {
                MessageBox.Show("Not Avaiabel");
                return;
            }

            frmAddUpdateMaintenance frm =new frmAddUpdateMaintenance(_VehicleID);
            frm.ShowDialog();
        }
    }
}
