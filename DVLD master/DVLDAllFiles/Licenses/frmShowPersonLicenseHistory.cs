using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonID = -1;
        public frmShowPersonLicenseHistory( )
        {
            InitializeComponent();
        }
        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFiltter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFiltter1.FiltterEnabled = false;
                ctrlDriverLicenses1.LoadByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFiltter1.FiltterEnabled = true;
                ctrlPersonCardWithFiltter1.Focus();
            }
        }

        private void ctrlPersonCardWithFiltter1_OnPersonSelected(int obj)
        {
            _PersonID =obj ;
            if (_PersonID == -1)
            {
                ctrlDriverLicenses1.Clear();
            }
            ctrlDriverLicenses1.LoadByPersonID(_PersonID);

        }

        private void ctrlDriverLicenses1_Load(object sender, EventArgs e)
        {

        }
    }
}
