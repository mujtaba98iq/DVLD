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

namespace DVLD2.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _AppoitmentID = -1;
        private clsTestTypes.enTestType _TestType ;
        private clsTests _Test;
        private int _TestID = -1;


        public frmTakeTest(int AppoitmentID,clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _AppoitmentID = AppoitmentID;
            _TestType = TestTypeID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            crlScheduledTest1.TestTypeID = _TestType;

            crlScheduledTest1.LoadTestInfo(_AppoitmentID);

            if (crlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;


             _TestID = crlScheduledTest1.TestID;

            if (_TestID != -1)
            {
                _Test = clsTests.Find(_TestID);

                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNotes.Text = _Test.Notes;

                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                btnSave.Enabled = false;
            }

            else
                _Test = new clsTests();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                     "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No
            )
            {
                return;
            }

            _Test.TestAppointmentID = _AppoitmentID;
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Test.Notes = txtNotes.Text;
            _Test.TestResult = rbPass.Checked;
            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
