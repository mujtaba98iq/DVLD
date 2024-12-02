using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Success_or_fail_Custom
{
    public partial class frmSuccessed : Form
    {
        public frmSuccessed()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modren11_OnDrawingClosed(bool obj)
        {
            label1.Visible = true;
            button1.Visible = true;
        }
    }
}
