using CustomersBusinessLayer;
using DevExpress.Utils;
using DVLD2.CarRentalAllFiles.GlobalForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.CarRentalAllFiles.Customers.Controls
{
    public partial class frmAddUpdateCustomer : Form
    {
        int _customerID = -1;
        clsCustomers _customer;
       public  enum enMode
        {
            Add,Update
        }
        enMode _Mode= enMode.Add;
     public enMode Mode
        {
            get { return _Mode; }
            set
            {
                switch (value)
                {
                    case enMode.Add:
                        _Mode = value;
                        lblTitle.Text = "Add Customer";
                        break;
                    case enMode.Update:
                        _Mode= value;
                        lblTitle.Text = "Update Customer";
                        break;
                }
            }
        }

        public frmAddUpdateCustomer()
        {
            InitializeComponent();
            Mode = enMode.Add;
            _customer = new clsCustomers();
        }

        public frmAddUpdateCustomer(int CustomerID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _customerID = CustomerID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void CheckIsPersonSelected()
        {
            if (ctrlPersonCardWithFiltter1.PersonID <= 0)
            {
                ctrlPersonCardWithFiltter1.FilterFocus();
                return;
            }
        }
        void CheckIsLicenseSelected()
        {
            if (ctrlDriverLicenseInfoWithFilter1.LicenseID <= 0)
            {
                clsGlobal.MessageBoxFillterCardChooseOne("License");
                ctrlDriverLicenseInfoWithFilter1.FiltterFoucs();
                return;
            }
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            CheckIsPersonSelected();
            tcCustomerInfo.SelectedTab = tcCustomerInfo.TabPages["tpLicenseInfo"];
            
        }

        private void btnNextLicenseInfo_Click(object sender, EventArgs e)
        {
            CheckIsPersonSelected();
            CheckIsLicenseSelected();
            LoadChoosedInfo();
            btnSave.Enabled = true;
            tcCustomerInfo.SelectedTab = tcCustomerInfo.TabPages["tpCustomerInfo"];
        }

        private void LoadChoosedInfo()
        {
            lblLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseID.ToString();
            lblPsersonID.Text = ctrlPersonCardWithFiltter1.PersonID.ToString();
            lblCreatedByUserID.Text = clsGlobal.CurrentUser.UserID.ToString();

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if(_Mode==enMode.Add)
            {
                if (clsGlobal.MessageBoxAddSaveConfirmation("Customer"))
                    return;
            }
            else
                 if (clsGlobal.MessageBoxUpdateSaveConfirmation("Customer"))
                return;

            using (frmWaitForm frm = new frmWaitForm(clsGlobal.ShortWait))
            {
                CheckIsPersonSelected();
                CheckIsLicenseSelected();
                frm.ShowDialog();
            }

            _customer.PersonID = ctrlPersonCardWithFiltter1.PersonID;
            _customer.LicenseID = ctrlDriverLicenseInfoWithFilter1.LicenseID;
            _customer.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            using (frmWaitForm frm = new frmWaitForm(clsGlobal.ShortWait))
            {
                try
                {
                    if (await _customer.Save())
                    {
                        Mode = enMode.Update;
                        btnSave.Enabled = false;
                        lblCustomerID.Text = _customer.CustomerID.ToString();
                        clsGlobal.MessageBoxSavedSuccessfully();
                    }
                    else
                    {
                        clsGlobal.MessageBoxSavedFailed();
                        return;
                    }
                }
                catch(Exception ex) { MessageBox.Show(ex.ToString()); }
              
            }  

        }

        private void frmAddUpdateCustomer_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                LoadInfo(_customerID);
            }
        }

        private async void LoadInfo(int CustomerID)
        {
            if(CustomerID <= 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            
             _customer=await clsCustomers.Find(CustomerID);
            if(_customer == null)
            {
                clsGlobal.MessageBoxNotFound("customer");
                return;
            }

            using(frmWaitForm frm =new frmWaitForm(clsGlobal.ShortWait))
            {
                _customerID = CustomerID;
                ctrlPersonCardWithFiltter1.LoadPersonInfo(_customer.PersonID);
                ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_customer.LicenseID);

                lblPsersonID.Text = _customer.PersonID.ToString();
                lblCustomerID.Text = _customer.CustomerID.ToString();
                lblLicenseID.Text = _customer.LicenseID.ToString();
                lblCreatedByUserID.Text = _customer.CreatedByUserID.ToString();
                tcCustomerInfo.SelectedTab = tcCustomerInfo.TabPages["tpCustomerInfo"];
                frm.ShowDialog();
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
