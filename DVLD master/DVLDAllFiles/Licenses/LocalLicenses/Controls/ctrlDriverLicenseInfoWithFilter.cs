using ApplicationsBusinessLayer;
using LicensesBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Licenses.LocalLicenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        int _LicenseID = -1;
        bool _FilterEnabled = false;
        clsLicenses _License;

        public event Action<int> OnLicenseSelected;

        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> hander = OnLicenseSelected;
            if (hander != null)
            {
                hander(LicenseID);
            }
        }

        private bool _ShowAddLicenses=true;

        public bool ShowAddLicenses
        {
            get { return _ShowAddLicenses; }
            set
            {
                _ShowAddLicenses = value;
            }
        }
        
        public bool FilterEnabled
        {
            get { return txtLicenseID.Enabled; }
            set
            {
                _FilterEnabled = value;
                txtLicenseID.Enabled = value;
            }
        }

        private bool _ShowFiltterLicense;

        public bool ShowFiltterLicense
        {
            get { return (_ShowFiltterLicense); }
            set
            {
                _ShowFiltterLicense = value;
            }
        }
        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }
        public clsApplcations Application
        {
            get { return  _License.ApplicationInfo; }
        }
        public clsLicenses SelectedLicenseInfo
        { get { return ctrlDriverLicenseInfo1.LicenseInfo; } }

        public clsLicenses LicenseInfo
        {
            get { return ctrlDriverLicenseInfo1.LicenseInfo; }
        }
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {

        }

     
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);
        

    }
        public void LoadLicenseInfo(int LicenseID)
        {


            txtLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadLicensesInfo(LicenseID);
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);


        }

        


        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
    
        }

        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {

        }
        public void FiltterFoucs()
        {
            
            txtLicenseID.Focus();
        }
        public void btnFindDisable()
        {
           btnFind.Enabled = false;
        }
 

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This Fild Required");
            }
                else
                {
                    errorProvider1.SetError(txtLicenseID, null);
                }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

        }
    }
}
