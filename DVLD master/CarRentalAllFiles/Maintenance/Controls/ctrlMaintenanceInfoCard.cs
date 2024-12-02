using MaintenanceBusinessLayer;
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

namespace DVLD2.CarRentalAllFiles.Maintenance.Controls
{
    public partial class ctrlMaintenanceInfoCard : UserControl
    {
        int _MaintenanceID;
        clsMaintenance _Maintenance;
        public ctrlMaintenanceInfoCard()
        {
            InitializeComponent();
        }
        public  void AddUpdateMode()
        {
            txtDescription.ReadOnly = false;
            dtpMaintenanceDate.Enabled = true;
            nudCost.ReadOnly = false;
        }
        public async void LaodVehiclInfo(int VehicleID)
        {
            if (VehicleID <= 0) {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            clsVehicles vehicle =  await clsVehicles.Find(VehicleID);
            if (vehicle == null)
            {
                clsGlobal.MessageBoxListOrObjectIsEmpty();
                return;
            }
            txtVehicleName.Text = vehicle.VehicleName;
            pbVehicle.ImageLocation = vehicle.VehicleImage;
        }

        public void GetMaintenanceInfo(ref clsMaintenance maintenance)
        {
            maintenance.MaintenanceDate = dtpMaintenanceDate.Value;
            maintenance.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            maintenance.Description= txtDescription.Text;
            maintenance.Cost = nudCost.Value;
        }
        public void RestInfo()
        {
            txtDescription.Text = "???";
            txtCreatedByUserID.Text ="???";
            txtMaintenanceID.Text ="???";
            txtVehicleName.Text ="???";
            nudCost.Value = 0;
            dtpMaintenanceDate.Value =DateTime.Now;
            pbVehicle.ImageLocation = "???";
        }
        public async void LoadInfo(int MaintenanceID)
        {
            if (MaintenanceID <= 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            _Maintenance =await clsMaintenance.Find(MaintenanceID);
            if (_Maintenance == null)
            {
                clsGlobal.MessageBoxListOrObjectIsEmpty();
                return;
            }
            FillInfo();

        }

        private void FillInfo()
        {
            txtDescription.Text=_Maintenance.Description;
            txtCreatedByUserID.Text = _Maintenance.CreatedByUserID.ToString();
            txtMaintenanceID.Text= _Maintenance.MaintenanceID.ToString();
            txtVehicleName.Text = _Maintenance.VehicleInfo.VehicleName;
            nudCost.Value = _Maintenance.Cost;
            dtpMaintenanceDate.Value = _Maintenance.MaintenanceDate;
            pbVehicle.ImageLocation = _Maintenance.VehicleInfo.VehicleImage;
        }

        private void lblLicenseID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
