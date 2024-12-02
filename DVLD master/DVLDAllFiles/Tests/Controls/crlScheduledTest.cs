using DVLD2.GlobalClasses;
using DVLD2.Properties;
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
using TestAppointmentBusinessLayer;
using TestsBusinessLayer;
using TestTypesBusinessLayer;

namespace DVLD2.Tests.Controls
{
    public partial class crlScheduledTest : UserControl
    {
        private int _TestID = -1;
        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointment;
        private clsTestTypes.enTestType _TestTypeID;
        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }
        public clsTestTypes.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }    
                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestTypes.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }

                }
            }
        }


        public int TestID
        {
            get { return _TestID; }
        }
     

        public crlScheduledTest()
        {
            InitializeComponent();
        }
        public void LoadTestInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.GetTestID();
            if (_TestAppointment.LocalLicensesInfo == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _TestAppointment.LocalLicensesInfo.LocalLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillTestAppointmentInfo();
        }

        private void FillTestAppointmentInfo()
        {
            lblTestID.Text =(_TestAppointment.TestID==-1)? "Not Taken Yet" :_TestAppointment.TestID.ToString();
            lblTrial.Text = _TestAppointment.LocalLicensesInfo.GetTriles(_TestTypeID).ToString();
            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _TestAppointment.LocalLicensesInfo.LicensesClassesInfo.ClassName;
            lblFullName.Text = _TestAppointment.LocalLicensesInfo.PersonFullName;
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblDate.Text =clsFormat.DateToShort(_TestAppointment.AppointmentDate).ToString();
            
        }
    }
}
