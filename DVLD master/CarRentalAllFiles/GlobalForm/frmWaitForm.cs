using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.CarRentalAllFiles.GlobalForm
{
    public partial class frmWaitForm : Form
    {
        public Action Artan { get; set;}

        public frmWaitForm(Action Artan)
        {
            InitializeComponent();
            if (Artan == null)
            {
                throw new ArgumentNullException();
            }
            this.Artan = Artan;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(Artan).ContinueWith(s => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
