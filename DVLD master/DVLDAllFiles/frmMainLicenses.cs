using DVLD2.Applications.Application_Types;
using DVLD2.Applications.International_License;
using DVLD2.Applications.Local_Driving_License;
using DVLD2.Applications.Renew_Local_License;
using DVLD2.Applications.ReplaceLostOrDamagedLicense;
using DVLD2.Applications.Rlease_Detained_License;
using DVLD2.Drivers;
using DVLD2.Licenses.Detain_License;
using DVLD2.People;
using DVLD2.Tests.Test_Types;
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

namespace DVLD2.DVLDAllFiles
{
    public partial class frmMainLicenses : Form
    {
        public frmMainLicenses(frmLogin frmLogin)
        {
            InitializeComponent();
        }
        public frmMainLicenses()
        {
            InitializeComponent();
        }
        string _DashboardClickBotton;
        public string DashboardClickBotton
        {
            get { return _DashboardClickBotton; }
            set
            {
                _DashboardClickBotton = value;
            }
        }
    

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            frmDashboard frm = new frmDashboard();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            pnlHome.Controls.Clear();
            pnlHome.Controls.Add(frm);
            frm.Show();
        }
        public void LoadFormFromDashBoard()
        {
            if (_DashboardClickBotton == "")
            {
                MessageBox.Show("Yes");
                btnDashboard_Click(null, null);
                return;
            }
            else
                switch (_DashboardClickBotton)
                {
                    case "Drivers":
                        btnDrivers_Click(null, null);
                        break;
                    case "Applications":
                        btnFrmApplications_Click(null, null);
                        break;
                    case "People":
                        btnPeople_Click(null, null);
                        break;
                    case "Users":
                        btnUsers_Click(null, null);
                        break;
                    case "Licenses":

                        break;
                }
        }
     

        private void btnPeople_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void btnPorfile_Click(object sender, EventArgs e)
        {
            cmsAccountSettings.Show(btnFrmApplications, btnFrmApplications.Width, btnFrmApplications.Height);

        }

        private void btnFrmApplications_Click(object sender, EventArgs e)
        {
            cmsApplications.Show(btnFrmApplications, btnFrmApplications.Width, btnFrmApplications.Height);
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
            MessageBox.Show(UserID.ToString());

            frmChangePassword frm = new frmChangePassword(UserID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            frmListInternationalLicesnseApplications frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

      
        private void frmMainLicenses1_Load(object sender, EventArgs e)
        {

            btnDashboard_Click(sender, e);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            btnLgoOut_Click(sender, e);
        }
    }
}
