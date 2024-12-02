using CustomersBusinessLayer;
using DVLD_DataAccess;
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
    public partial class ctrlCustomerCard : UserControl
    {

        clsCustomers _customer;
        int _customerID = -1;

        public int CustomerID { get { return _customerID; } }
        public ctrlCustomerCard()
        {
            InitializeComponent();
        }

        public async Task LoadInfoByCustomerID(int customerID)
        {
            if (customerID < 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }

            try
            {
                _customer = await clsCustomers.Find(customerID);
                if (_customer == null)
                {
                    clsGlobal.MessageBoxNotFound("customer");
                    return;
                }

                _customerID = customerID;
                FillInfo();
            }
            catch (Exception ex)
            {
                

            }
        }
        public void restInfo()
        {
            lblCustomerID.Text = "???";
            lblLicenseID.Text = "???";
            lblPsersonID.Text = "???";
            lblCreatedByUserID.Text = "???";
            pbCustomer.ImageLocation = null;
            lblCustomerName.Text = "???";
        }


        public async void LoadInfoByPersonID(int PersonID)
        {
            if (PersonID < 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
            _customer = await clsCustomers.FindByPersonID(PersonID);
            if (_customer == null)
            {
                clsGlobal.MessageBoxNotFound("customer");
                return;
            }
            _customerID = _customer.CustomerID;
            FillInfo();
        }
        void FillInfo()
        {
            lblCustomerID.Text = _customerID.ToString();
            lblLicenseID.Text = _customer.LicenseID.ToString();
            lblPsersonID.Text = _customer.PersonID.ToString();
            lblCreatedByUserID.Text = _customer.CreatedByUserID.ToString();
            pbCustomer.ImageLocation = _customer.PersonInfo.ImagePath;
            lblCustomerName.Text = _customer.PersonInfo.FullName;
        }
    }
}
