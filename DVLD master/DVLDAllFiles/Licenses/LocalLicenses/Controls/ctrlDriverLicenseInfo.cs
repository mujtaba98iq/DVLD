using DVLD2.GlobalClasses;
using DVLD2.Properties;
using LicensesBusinessLayer;
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

namespace DVLD2.Licenses.LocalLicenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return _LicenseID; }  
        }
        private clsLicenses _License;

        public clsLicenses LicenseInfo
        {
            get { return _License; }
        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadLicensesInfo(int LicenseID)
        {
            _License = clsLicenses.Find(LicenseID);
            if(_License == null )
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }
            FillLicensesInfo();
        }

        private void FillLicensesInfo()
        {
            _LicenseID = _License.LicenseID;
            lblLicenseID.Text = _LicenseID.ToString();
            lblDriverID.Text = _License.DriverID.ToString();
            lblClass.Text=_License.LicenseClassInfo.ClassName;
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.ApplicationInfo.PersonInfo.DateOfBirth);
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text=_License.IsDetained ? "Yes" : "No";  
            lblExpirationDate.Text= _License.ExpirationDate.ToString();
            lblFullName.Text=_License.ApplicationInfo.PersonInfo.FullName;
            lblGendor.Text = _License.ApplicationInfo.PersonInfo.Gendor == 0 ? "Male" : "Femail";
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblNotes.Text=_License.Notes==""?"No Notes": _License.Notes;
            lblNationalNo.Text = _License.ApplicationInfo.PersonInfo.NationalNo;
            lblIssueReason.Text = _License.IssueReasonText;
            _LoadPersonImage();

        }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
