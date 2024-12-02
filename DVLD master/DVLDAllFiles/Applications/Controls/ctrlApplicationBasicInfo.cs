using ApplicationsBusinessLayer;
using DVLD2.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsersBusinessLayer;

namespace DVLD2.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private int _ApplicationID;
        private int _PersonID = -1;
        private ApplicationsBusinessLayer.clsApplcations _Application;

        public int ApplicationID
        {
            get { return _ApplicationID; }
        }
        public ApplicationsBusinessLayer.clsApplcations Application
        {
            get { return _Application; }
        }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public void LoadApplicationInfo(int ApplicaitonID)
        {
            _ApplicationID= ApplicaitonID;
            _Application = ApplicationsBusinessLayer.clsApplcations.Find(_ApplicationID);
            _PersonID = _Application.ApplicantPersonID;
           if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            FillApplcationInfo();
        }

        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;

            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";

        }

        private void FillApplcationInfo()
        {
            _ApplicationID = _Application.ApplicationID;
            lblApplicant.Text=_Application.ApplicantPersonID.ToString();
            lblApplicationID.Text = _ApplicationID.ToString();
          
            lblCreatedByUser.Text = _Application.CreatedByUserInfo.UserName;
            lblDate.Text = _Application.ApplicationDate.ToString();
            lblFees.Text=_Application.PaidFees.ToString();
            lblStatus.Text = _Application.StatusText;
             lblStatusDate.Text=_Application.LastStatusDate.ToString();
            lblType.Text = _Application.ApplicationTypeInfo.ApplicationTypeTitle;
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_PersonID);
            frm.ShowDialog();
        }
    }
}
