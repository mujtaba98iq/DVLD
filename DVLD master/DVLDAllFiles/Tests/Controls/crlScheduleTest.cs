using ApplcatoinTypesBusinessLayer;
using ApplicationsBusinessLayer;
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
using TestTypesBusinessLayer;

namespace DVLD2.Tests.Controls
{
    public partial class crlScheduleTest : UserControl
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplications _LocalDrivingLicensesApplicaton;
        private int _TestAppotmentID;

        private clsTestAppointment _TestAppotment;

        public clsTestAppointment TestAppointment
        {
            get { return _TestAppotment; }
        }
        private enum enMode
        {
            Add=1,Update=2
        }

        private enum enCreatedMode
        {
            FirstTimeSchedule = 0, RetakeTestSchedule = 1
        }
        enCreatedMode _CreatedMode;
       private enMode _Mode = enMode.Add;
        private clsTestTypes.enTestType _TestTypesID=clsTestTypes.enTestType.VisionTest;

        public clsTestTypes.enTestType TestTypesID
        {
            get { return _TestTypesID; }
            set
            {
                _TestTypesID = value;
                switch (_TestTypesID)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        {
                            lblTitle.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }
                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            lblTitle.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestTypes.enTestType.StreetTest:
                        {
                            lblTitle.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }
                }
            }
        }

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public crlScheduleTest()
        {
            InitializeComponent();
        }

        private void crlScheduleTest_Load(object sender, EventArgs e)
        {
        }

        public void LoadLocalApplicationInfo(int LocalDrivingLicenseApplicationID, int AppitmentID = -1)
        {
            if (AppitmentID == -1)
                _Mode = enMode.Add;
            else
                _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            MessageBox.Show("Here" + _LocalDrivingLicenseApplicationID);
            _TestAppotmentID = AppitmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicensesApplicaton = clsLocalDrivingLicenseApplications.Find(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicensesApplicaton == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            //decide if the createion mode is retake test or not based if the person attended this test before
            if (_LocalDrivingLicensesApplicaton.DoesAttendTestType(_TestTypesID))

                _CreatedMode = enCreatedMode.RetakeTestSchedule;
            else
                _CreatedMode = enCreatedMode.FirstTimeSchedule;


            if (_CreatedMode == enCreatedMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationTypes.Find((int)clsApplcations.enApplicationType.RetakeTest).ApplicatoinFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicensesApplicaton.LocalLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicensesApplicaton.LicensesClassesInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicensesApplicaton.PersonFullName;

            //this will show the trials for this test before  
            lblTrial.Text = _LocalDrivingLicensesApplicaton.DoesPassTestType(_TestTypesID).ToString();


            if (_Mode == enMode.Add)
            {
                lblFees.Text = clsTestTypes.Find(_TestTypesID).TestFees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppotment = new clsTestAppointment();
            }

            else
            {

                if (!_LoadTestAppointmentData())
                    return;
            }


            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();


            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePrviousTestConstraint())
                return;

        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppotment = clsTestAppointment.Find(_TestAppotmentID);
            if(_TestAppotment == null )
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppotmentID.ToString(),
           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }


            MessageBox.Show(_TestAppotment.TestID.ToString());
            MessageBox.Show(_TestAppotment.AppointmentDate.ToString());
            lblFees.Text=_TestAppotment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppotment.AppointmentDate)<0)
             dtpTestDate.MinDate= DateTime.Now;
            else
                dtpTestDate.MinDate= _TestAppotment.AppointmentDate;

            dtpTestDate.Value = _TestAppotment.AppointmentDate;
            if (_TestAppotment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                MessageBox.Show(_TestAppotment.RetakeTestApplicationID.ToString());

                lblRetakeAppFees.Text = _TestAppotment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppotment.RetakeTestApplicationID.ToString();

            }
        
  
            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            //if appointment is locked that means the person already sat for this test
            //we cannot update locked appointment
            if (_TestAppotment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;

            }
            else
                lblUserMessage.Visible = false;

            return true;
        }
        private bool _HandlePrviousTestConstraint()
        {
            //we need to make sure that this person passed the prvious required test before apply to the new test.
            //person cannno apply for written test unless s/he passes the vision test.
            //person cannot apply for street test unless s/he passes the written test.

            switch (TestTypesID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    lblUserMessage.Visible = false;

                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.
                    if (!_LocalDrivingLicensesApplicaton.DoesPassTestType(clsTestTypes.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

                case clsTestTypes.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    if (!_LocalDrivingLicensesApplicaton.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

            }
            return true;

        }
        private bool _HandleActiveTestAppointmentConstraint()
        {
            //this will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will linkit to the appoinment.
            if (_Mode == enMode.Add && _CreatedMode == enCreatedMode.RetakeTestSchedule)
            {
                //incase the mode is add new and creation mode is retake test we should create a seperate application for it.
                //then we linke it with the appointment.

                //First Create Applicaiton 
                clsApplcations Application = new clsApplcations();

                Application.ApplicantPersonID = _LocalDrivingLicensesApplicaton.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplcations.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplcations.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = (float)clsApplicationTypes.Find((int)clsApplcations.enApplicationType.RetakeTest).ApplicatoinFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppotment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppotment.RetakeTestApplicationID = Application.ApplicationID;

            }
            return true;
        }

        private void FillLocalApplicationInfo()
        {
            lblDrivingClass.Text = _LocalDrivingLicensesApplicaton.LicensesClassesInfo.ClassName;
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblFullName.Text = _LocalDrivingLicensesApplicaton.PersonFullName;
            lblFees.Text = clsTestTypes.Find(_TestTypesID).TestFees.ToString();
            dtpTestDate.MinDate = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleActiveTestAppointmentConstraint())
                return;
           
            _TestAppotment.TestTypeID= _TestTypesID;
            _TestAppotment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppotment.AppointmentDate = dtpTestDate.Value;
            _TestAppotment.PaidFees =Convert.ToDecimal(lblFees.Text);
            _TestAppotment.CreatedByUserID=clsGlobal.CurrentUser.UserID;
            _TestAppotment.RetakeTestApplicationID=-1;
            if (_TestAppotment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }

    

