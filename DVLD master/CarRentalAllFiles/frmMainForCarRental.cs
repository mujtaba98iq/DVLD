using DVLD2.CarRentalAllFiles.Customers;
using DVLD2.CarRentalAllFiles.GlobalForm;
using DVLD2.CarRentalAllFiles.Maintenance;
using DVLD2.CarRentalAllFiles.Reservations;
using DVLD2.CarRentalAllFiles.VehicleReturns;
using DVLD2.CarRentalAllFiles.Vehicles;
using DVLD2.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.CarRentalAllFiles
{
    public partial class frmMainForCarRental : Form
    {
        public frmMainForCarRental()
        {
            InitializeComponent();
        }

        private void btnPorfile_Click(object sender, EventArgs e)
        {
            int userId=clsGlobal.CurrentUser.UserID;
            using (frmWaitForm frmWait = new frmWaitForm(clsGlobal.ShortWait))
            {
                frmWait.ShowDialog(this);
            }

            frmChangePassword frm = new frmChangePassword(1);
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            pnlHome.Controls.Clear();
            pnlHome.Controls.Add(frm);
            frm.Show();
        }

        private void btnLgoOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            int UserID = clsGlobal.CurrentUser.UserID;
            frmShowUserInfo frm = new frmShowUserInfo(UserID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            int UserID = clsGlobal.CurrentUser.UserID;
            frmChangePassword frm = new frmChangePassword(UserID);
            frm.ShowDialog();
        }

      

        private void btnFrmApplications_Click(object sender, EventArgs e)
        {
            using (frmWaitForm frmWait = new frmWaitForm(clsGlobal.ShortWait))
            {
                frmWait.ShowDialog(this);
            }

            frmListCusomersRecords frm = new frmListCusomersRecords();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            pnlHome.Controls.Clear();
            pnlHome.Controls.Add(frm);
            frm.Show();
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            using (frmWaitForm frmWait = new frmWaitForm(clsGlobal.ShortWait))
            {
                frmWait.ShowDialog(this);
            }

            frmListReservation frm = new frmListReservation();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            pnlHome.Controls.Clear();
            pnlHome.Controls.Add(frm);
            frm.Show();
        }

        private void btnReservationRecords_Click(object sender, EventArgs e)
        {
            using (frmWaitForm frmWait = new frmWaitForm(clsGlobal.ShortWait))
            {
                frmWait.ShowDialog(this);
            }

            frmfrmListReservationRecords frm = new frmfrmListReservationRecords();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            pnlHome.Controls.Clear();
            pnlHome.Controls.Add(frm);
            frm.Show();
        }

        private void btnReturns_Click(object sender, EventArgs e)
        {
            using (frmWaitForm frmWait = new frmWaitForm(clsGlobal.ShortWait))
            {
                frmWait.ShowDialog(this);
            }

            frmListVehicleReturnsRecords frm = new frmListVehicleReturnsRecords();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            pnlHome.Controls.Clear();
            pnlHome.Controls.Add(frm);
            frm.Show();
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            using (frmWaitForm frmWait = new frmWaitForm(clsGlobal.ShortWait))
            {
                frmWait.ShowDialog(this);
            }

            frmListMaintenanceRecords frm = new frmListMaintenanceRecords();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            pnlHome.Controls.Clear();
            pnlHome.Controls.Add(frm);
            frm.Show();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReloadVehicles_Click(object sender, EventArgs e)
        {
            btnVehicles_Click(sender, e);   
        }

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            using (frmWaitForm frmWait = new frmWaitForm(clsGlobal.ShortWait))
            {
                frmWait.ShowDialog(this);
            }

            frmListVehicles frm = new frmListVehicles();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            pnlHome.Controls.Clear();
            pnlHome.Controls.Add(frm);
            frm.Show();
        }
    }
}
