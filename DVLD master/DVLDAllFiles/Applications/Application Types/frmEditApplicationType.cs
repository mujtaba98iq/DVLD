using ApplcatoinTypesBusinessLayer;
using DVLD2.GlobalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Applications.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID;
        private clsApplicationTypes _ApplicatonTypes;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _RestApplicationInfo();
        }

        private void _RestApplicationInfo()
        {
            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();
            _ApplicatonTypes = clsApplicationTypes.Find(_ApplicationTypeID);
            if (_ApplicatonTypes != null)
            {
                txtFees.Text = _ApplicatonTypes.ApplicatoinFees.ToString();
                txtTitle.Text = _ApplicatonTypes.ApplicationTypeTitle;
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
            _ApplicatonTypes.ApplicatoinFees = Convert.ToDecimal(txtFees.Text);
            _ApplicatonTypes.ApplicationTypeTitle = txtTitle.Text.Trim();

            if (_ApplicatonTypes.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");

            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;

            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
            if (clsValidatoin.IsNumber(txtFees.Text))
                {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);

            }

        }
    }
}
