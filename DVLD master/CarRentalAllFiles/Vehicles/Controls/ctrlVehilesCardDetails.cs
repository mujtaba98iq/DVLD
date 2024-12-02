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
    public partial class ctrlVehilesCardDetails : UserControl
    {
        clsVehicles _vehicle;
        int _vehicleID = -1;
      
        public ctrlVehilesCardDetails()
        {
            InitializeComponent();
        }
        public void RestForm()
        {
            lblVehicleID.Text ="???";
            lblVehicleName.Text ="???";
            lblCarCategoryID.Text = "???";
            lblCreatedByUserID.Text = "???";
            lblMakeID.Text = "???";
            lblModelID.Text = "???";
            lblPlateNumber.Text = "???";
            lblRentalPricePerDay.Text = "???";
            lblYear.Text = "???";
            pbVehicle.ImageLocation = "???";
            lblFuelTypeID.Text = "???";
            lblMileage.Text = "???";
        }
        public async void LoadInfo(int vehicleID)
        {
            if (vehicleID < 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            _vehicle = await clsVehicles.Find(vehicleID);
            if (_vehicle == null)
            {
                clsGlobal.MessageBoxNotFound("Vehicle");
                return;
            }
            _vehicleID = vehicleID;

            lblVehicleID.Text = _vehicleID.ToString();
            lblVehicleName.Text = _vehicle.VehicleName.ToString();
            lblCarCategoryID.Text = _vehicle.CarCategoryID.ToString();
            lblCreatedByUserID.Text = _vehicle.CreatedByUserID.ToString();
            lblMakeID.Text = _vehicle.MakeID.ToString();
            lblModelID.Text = _vehicle.ModelID.ToString();
            lblPlateNumber.Text = _vehicle.PlateNumber.ToString();
            lblRentalPricePerDay.Text=_vehicle.RentalPricePerDay.ToString();
            lblYear.Text = _vehicle.Year.ToString();
            pbVehicle.ImageLocation = _vehicle.VehicleImage;          
            lblFuelTypeID.Text= _vehicle.FuelTypeID.ToString();
            lblMileage.Text = _vehicle.Mileage.ToString();
        }
    }
}
