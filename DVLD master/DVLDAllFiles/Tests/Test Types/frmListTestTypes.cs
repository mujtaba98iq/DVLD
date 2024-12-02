using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTypesBusinessLayer;

namespace DVLD2.Tests.Test_Types
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtListTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtListTestTypes = clsTestTypes.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtListTestTypes;
            if (dgvTestTypes.Columns.Count > 0)
            {

                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 120;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 200;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 400;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 100;

            }
            lblRecordsCount.Text = dgvTestTypes.Columns.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType(Convert.ToInt32( dgvTestTypes.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }
    }
}
