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
    public partial class frmShowMaintenanceDetals : Form
    {
        public frmShowMaintenanceDetals(int MaintenanceID)
        {
            InitializeComponent();
            ctrlMaintenanceInfoCard1.LoadInfo(MaintenanceID);
        }

        private void frmShowMaintenanceDetals_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
