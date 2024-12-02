using DVLD_with_Car_Rental_DataAccessLayer;
using DVLD2.CarRentalAllFiles.Vehicles.Controls;
using DVLDWithCarRentalDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleCopiesBusinessLayer;
using VehiclesBusinessLayer;

namespace DVLD2.CarRentalAllFiles.Vehicles
{
    public partial class frmListVehicles : Form
    {
        List<VehiclesDTO> _Vehicles = new List<VehiclesDTO>();
        clsVehicleCopies _Vehicle = new clsVehicleCopies();
        int _VehicleID = -1;

        public frmListVehicles()
        {
            InitializeComponent();
            LoadVehicles();
        }

        private async void LoadVehicles()
        {
            _Vehicles.Clear();
            pnlVehicles.Controls.Clear();
            _Vehicles = await clsVehicles.GetAllVehicles();
            if (_Vehicles == null || _Vehicles.Count < 0)
            {
                clsGlobal.MessageBoxListOrObjectIsEmpty();
                return;
            }

            foreach (VehiclesDTO Vehicle in _Vehicles)
            {
                _VehicleID = Vehicle.VehicleID;
                ctrlVehilesCard vehilesCard = new ctrlVehilesCard();
                vehilesCard.LoadVelhileInfo(Vehicle.VehicleID);
                pnlVehicles.Controls.Add(vehilesCard);
            }

        }
    }
}
