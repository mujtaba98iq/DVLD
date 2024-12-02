using DVLD2.Properties;
using InternationalLicensesBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Licenses.International_Licenses.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        private int _InternationalLicenseID = -1;
        private clsInternationalLicenses _InternationalLicense;

        public int InternationalLicense
        {
            get { return _InternationalLicenseID; }
        }
        public clsInternationalLicenses InternationalLicenses
        {
            get { return _InternationalLicense; }
        }

        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        public void LoadInternationalLicensesInfo(int InternationalLicensesID)
        {
            _InternationalLicenseID=InternationalLicensesID;
            _InternationalLicense = clsInternationalLicenses.Find(InternationalLicensesID);

            if(_InternationalLicense == null )
            {
                RestInternationalLicenseInfo();
                MessageBox.Show("No Licnese with InternationalLicensesID = " + InternationalLicensesID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }
            FIllInternationalLicenseWithInfo();
        }

        private void FIllInternationalLicenseWithInfo()
        {
            lblInternationalLicenseID.Text = _InternationalLicenseID.ToString();
            lblLocalLicenseID.Text=_InternationalLicense.LocalDriverApplicationID.ToString();
            lblApplicationID.Text=_InternationalLicense.ApplicationID.ToString();
            lblExpirationDate.Text=_InternationalLicense.ExpirationDate.ToString();
            lblDriverID.Text=_InternationalLicense.DirverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToString();    
            lblFullName.Text=_InternationalLicense.DriverInfo.PersonInfo.FullName.ToString();
            lblGendor.Text = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Femail";
            lblIsActive.Text= _InternationalLicense.IsActive.ToString();
            lblIssueDate.Text= _InternationalLicense.IssueDate.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo.ToString();
            LoadPersonImage();
        }

        private void LoadPersonImage()
        {
            if (InternationalLicenses.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image= Resources.Female_512;

            string ImagePath = InternationalLicenses.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;

                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RestInternationalLicenseInfo()
        {

        }
    }
}
