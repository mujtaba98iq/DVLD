using ApplcatoinTypesBusinessLayer;
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
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtApplicationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtApplicationTypes = clsApplicationTypes.GetAllApplcatoinTypes();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;
            lblRecordsCount.Text = dgvApplicationTypes.Columns.Count.ToString();

            if (dgvApplicationTypes.Columns.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 100;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppllicationTypeID = (int)dgvApplicationTypes.CurrentRow.Cells[0].Value;
            frmEditApplicationType frm = new frmEditApplicationType(AppllicationTypeID);
            frm.ShowDialog();
            frmListApplicationTypes_Load(null, null);

        }
    }

  
}
