using PeopleBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.People.Controls
{
    public partial class frmShowPersonInfo : Form
    {
   
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard21.LoadPersonInfo(PersonID);
        } 
        public frmShowPersonInfo(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonCard21.LoadPersonInfo(NationalNo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
