using DVLD2.CarRentalAllFiles.GlobalForm;
using DVLD2.People.Controls;
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

namespace DVLD2.CarRentalAllFiles.Maintenance
{
    public partial class frmAddUpdateMaintenance : Form
    {
        int _MaintenanceID;
        int _vehicleId;

        clsMaintenance _Maintenance;
        public frmAddUpdateMaintenance(int vehicleId)
        {
            InitializeComponent();
            _Mode = enMode.Add;
            _Maintenance = new clsMaintenance();
            _vehicleId = vehicleId;
        }
        public frmAddUpdateMaintenance(int MaintenanceID,int vehicleId)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _MaintenanceID = MaintenanceID;
            _vehicleId = vehicleId;
        }
        enum enMode
        {
            Add,Update
        }
      enMode  _Mode= enMode.Add;
        private  void frmAddUpdateMaintenance_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                LoadMaintenanceInfo(_MaintenanceID);
            }
            else
            {
                ctrlVehicleCardWithFiltter1.LoadInfo(_vehicleId);
            }
        }

        private async void LoadMaintenanceInfo(int MaintenanceID)
        {
            if (MaintenanceID <= 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            _Maintenance=await clsMaintenance.Find(MaintenanceID);
            if(_Maintenance == null)
            {
                clsGlobal.MessageBoxListOrObjectIsEmpty();
                return;
            }
            ctrlMaintenanceInfoCard1.LoadInfo(MaintenanceID);
            ctrlVehicleCardWithFiltter1.LoadInfo(_Maintenance.VehicleID);
            btnSave.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (ctrlVehicleCardWithFiltter1.VehicleID <= 0)
            {
                MessageBox.Show(ctrlVehicleCardWithFiltter1.VehicleID.ToString());
                MessageBox.Show("chose vehicle first!");
                ctrlVehicleCardWithFiltter1.FilterFucos();
                return;
            }
            using (frmWaitForm frm = new frmWaitForm(clsGlobal.ShortWait))
            {
                frm.ShowDialog();
                ctrlMaintenanceInfoCard1.AddUpdateMode();
                ctrlMaintenanceInfoCard1.LaodVehiclInfo(ctrlVehicleCardWithFiltter1.VehicleID);
                tcMaintenanceInfo.SelectedTab = tcMaintenanceInfo.TabPages["tpMaintenanceInfo"];
                btnSave.Enabled = true;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                clsGlobal.MessageBoxValidateChildren();
                return;
            }
            if((_Mode==enMode.Add && clsGlobal.MessageBoxAddSaveConfirmation("Maintenance"))||
                (_Mode==enMode.Update && clsGlobal.MessageBoxUpdateSaveConfirmation("Maintenance")))         
            {
                return;
            }

            ctrlMaintenanceInfoCard1.GetMaintenanceInfo(ref _Maintenance);

            if (_Mode == enMode.Update)
            {
                _Maintenance.MaintenanceID = _MaintenanceID;
            }
            _Maintenance.VehicleID = ctrlVehicleCardWithFiltter1.VehicleID;
            if (await _Maintenance.Save())
            {
                clsGlobal.MessageBoxSavedSuccessfully();
                RestForm();
            }
            else
            {
                clsGlobal.MessageBoxSavedFailed();
            }
        }

        private void RestForm()
        {
            ctrlMaintenanceInfoCard1.RestInfo();
            ctrlVehicleCardWithFiltter1.RestForm();
        }


    }
}
