using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.CarRentalAllFiles.Vehicles
{
    public partial class frmShowVehicleDetails : Form
    {
        public frmShowVehicleDetails(int VehicleID)
        {
            InitializeComponent();
            ctrlVehilesCardDetails1.LoadInfo(VehicleID);
        }

        private void frmShowVehicleDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
