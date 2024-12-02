using ApplcatoinTypesBusinessLayer;
using ApplicationsBusinessLayer;
using LicensesBusinessLayer;
using LincnseClassesBusinessLayer;
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

namespace DVLD2.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        private int _SelectedPerson;
        enum enMode
        {
            Add=1,
            Update=2
        }
        enMode _Mode = enMode.Add;
        private int _LocalApplicationID;
        private clsLocalDrivingLicenseApplications _LocalDriverLicenseApplication;

        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _Mode = enMode.Add;
        }
        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalApplicationID)
        {
            InitializeComponent();
            _LocalApplicationID = LocalApplicationID;
            _Mode = enMode.Update;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _RestInfo();
            if (_Mode == enMode.Update)
                FillLocalApplicationInfo();
        }

        private void FillLocalApplicationInfo()
        {
            _LocalDriverLicenseApplication = clsLocalDrivingLicenseApplications.Find(_LocalApplicationID);
            if (_LocalDriverLicenseApplication == null)
            {
                return;
            }
            ctrlPersonCardWithFiltter1.LoadPersonInfo(_LocalDriverLicenseApplication.ApplicantPersonID);
            lblApplicationDate.Text = _LocalDriverLicenseApplication.ApplicationDate.ToShortDateString();
            lblFees.Text = _LocalDriverLicenseApplication.PaidFees.ToString();
            lblLocalDrivingLicebseApplicationID.Text = _LocalDriverLicenseApplication.LocalLicenseApplicationID.ToString();
        }

        private void _RestInfo()
        {   //this will initialize the reset the defaule values
            _FillLicenseComboBox();


            if (_Mode == enMode.Add)
            {

                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDriverLicenseApplication = new clsLocalDrivingLicenseApplications();
                ctrlPersonCardWithFiltter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                cbLicenseClass.SelectedIndex = 2;
                lblFees.Text = clsApplicationTypes.Find((int)clsApplcations.enApplicationType.NewLocalDrivingLicense).ApplicatoinFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;


            }
        }

        private void _FillLicenseComboBox()
        {
            DataTable dt = clsLicensesClasses.GetAllLincnseClasses();
            foreach(DataRow Row in dt.Rows)
            {
                cbLicenseClass.Items.Add(Row["ClassName"]);
            }
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {


            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }


            //incase of add new mode.
            if (ctrlPersonCardWithFiltter1.PersonID != -1)
            {

                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];

            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFiltter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            int LicenseClassID = clsLicensesClasses.Find(cbLicenseClass.Text).LicenseClassID;


            int ActiveApplicationID = clsApplcations.GetActiveApplicationIDForLicenseClass(_SelectedPerson, clsApplcations.enApplicationType.NewLocalDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }


            //check if user already have issued license of the same driving  class.
            if (clsLicenses.isLicensesExistByPersonID(ctrlPersonCardWithFiltter1.PersonID, LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDriverLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFiltter1.PersonID; 
            _LocalDriverLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDriverLicenseApplication.ApplicationTypeID = 1;
            _LocalDriverLicenseApplication.ApplicationStatus = clsApplcations.enApplicationStatus.New;
            _LocalDriverLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDriverLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDriverLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDriverLicenseApplication.LicenseClassID = LicenseClassID;

        
            if (_LocalDriverLicenseApplication.Save())
            {
     
                lblLocalDrivingLicebseApplicationID.Text = _LocalDriverLicenseApplication.LocalLicenseApplicationID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpPersonalInfo"];
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }
        private void DataBackEvent(object sender,int PersonID)
        {
            _SelectedPerson = PersonID;
            ctrlPersonCardWithFiltter1.LoadPersonInfo(PersonID);
        }
        private void ctrlPersonCardWithFiltter1_OnPersonSelected(int obj)
        {
            _SelectedPerson = obj;
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFiltter1.FilterFocus();
        }
    }
}
