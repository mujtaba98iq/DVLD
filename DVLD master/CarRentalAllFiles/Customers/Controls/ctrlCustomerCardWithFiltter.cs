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
    public partial class ctrlCustomerCardWithFiltter : UserControl
    {
        public event Action<int> OnPersonSelected;
        public int CustomerID { get { return _customerID; } }

        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
                handler(personID);
        }

        int _customerID = -1;

        public ctrlCustomerCardWithFiltter()
        {
            InitializeComponent();
        }
        public void LoadInfo(int CustomerID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = CustomerID.ToString();
            btnFind_Click(null, null);
        }
        public void restInfo()
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = "";

            ctrlCustomerCard1.restInfo();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            if (txtFilterValue.Text == "")
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }

           if(cbFilterBy.SelectedIndex == 0)
            {
              await  ctrlCustomerCard1.LoadInfoByCustomerID(Convert.ToInt32(txtFilterValue.Text));
                
            }
           else
            {
                ctrlCustomerCard1.LoadInfoByPersonID(Convert.ToInt32(txtFilterValue.Text));
                PersonSelected(ctrlCustomerCard1.CustomerID);
            }
            _customerID = ctrlCustomerCard1.CustomerID;
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGlobal.OnlyNumbers(e);
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            clsGlobal.ValidatingtextBoxNull(txtFilterValue, errorProvider1, e);
        }

        private void ctrlCustomerCard1_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
        }
    }
}
