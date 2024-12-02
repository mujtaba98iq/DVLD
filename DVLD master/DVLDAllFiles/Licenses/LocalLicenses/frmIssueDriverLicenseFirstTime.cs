using DVLD2.Applications.Local_Driving_License;
using LicensesBusinessLayer;
using LocalDrivingLicenseApplicationsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Licenses.LocalLicenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplications _localDrivingLicenseApplications;
        public frmIssueDriverLicenseFirstTime(int  LocalDriverLicenseApplicatoinID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDriverLicenseApplicatoinID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            
            int LicenseID = _localDrivingLicenseApplications.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(),clsGlobal.CurrentUser.UserID);
            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
             "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.Find(_LocalDrivingLicenseApplicationID);
            if (_localDrivingLicenseApplications == null)
            {
                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_localDrivingLicenseApplications.PassAllTests())
            {
                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _localDrivingLicenseApplications.GetActiveLicenseID();
            if (LicenseID != -1)
            {
                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlDrivingLicenseApplicationInfo1.LoadDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplicationID);

        }
    }
}
