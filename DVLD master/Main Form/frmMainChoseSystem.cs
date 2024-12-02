using DVLD2.CarRentalAllFiles;
using DVLD2.CarRentalAllFiles.GlobalForm;
using DVLD2.DVLDAllFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Main_Form
{
    public partial class frmMainChoseSystem : Form
    {
        public frmMainChoseSystem()
        {
            InitializeComponent();
        }

        private void btnCarRentalSystem_Click(object sender, EventArgs e)
        {
            frmMainForCarRental frm =new frmMainForCarRental();
            frm.ShowDialog();
        }

        private void btnLicensesSystem_Click(object sender, EventArgs e)
        {
            using(frmWaitForm frmWaite =new frmWaitForm(clsGlobal.ShortWait))
            {
                frmWaite.ShowDialog();
                frmMainLicenses frm = new frmMainLicenses();
                frm.ShowDialog();
            }

         
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
