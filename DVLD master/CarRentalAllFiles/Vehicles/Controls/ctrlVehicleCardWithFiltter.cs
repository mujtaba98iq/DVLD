using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.CarRentalAllFiles.Vehicles.Controls
{
    public partial class ctrlVehicleCardWithFiltter : UserControl
    {
        int _vehicleID=-1;
        public int VehicleID
        {
            get { return _vehicleID; }
           
                
        }
        public ctrlVehicleCardWithFiltter()
        {
            InitializeComponent();
        }
        public void LoadInfo(int vehicleID)
        {
            if (vehicleID <= 0)
            {
                clsGlobal.MessageBoxNotValid();
            }
            txtFilterValue.Text= vehicleID.ToString();
            btnFind_Click(null,null);
        }
        public void RestForm()
        {
            txtFilterValue.Text = "";
            ctrlVehilesCardDetails1.RestForm(); 
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            int vehicleID =Convert.ToInt32(txtFilterValue.Text);

            if (vehicleID <= 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            ctrlVehilesCardDetails1.LoadInfo(vehicleID);
           
                _vehicleID= vehicleID;   
        }

        public void FilterFucos()
        {
            txtFilterValue.Focus();
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateVehicle frm =new frmAddUpdateVehicle();
            frm.ShowDialog();
        }

        private void ctrlVehicleCardWithFiltter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
        }
    }
}
