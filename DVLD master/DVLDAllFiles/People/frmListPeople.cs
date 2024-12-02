using DevExpress.Export.Xl;
using DVLD2.People.Controls;
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

namespace DVLD2.People
{
    public partial class frmListPeople : Form
    {
        // استدعاء دالة GetAllPeople للحصول على بيانات الأشخاص
        private static DataTable _dtListPeople = clsPeople.GetAllPeople();

        // إنشاء DataTable جديد من _dtListPeople، مع تحديد الأعمدة المطلوبة
        private DataTable _dtPeople = _dtListPeople.DefaultView.ToTable(
            false,
            "PersonID",
            "NationalNo",
            "FirstName",
            "SecondName",
            "ThirdName",
            "LastName",
            "Gendor", // تأكد من استخدام Gendor بدلاً من GendorCaption
            "DateOfBirth",
            "CountryName",
            "Phone",
            "Email"
        );

        public frmListPeople()
        {
            InitializeComponent();
        }
        public void LoadPeople()
        {
            dgvPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
            if (dgvPeople.Rows.Count > 0)
            {

                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;


                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;
            }

        }
        private void _RefreshPeople()
        {
            _dtListPeople = clsPeople.GetAllPeople();
            _dtPeople = _dtListPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gendor", "DateOfBirth", "CountryName",
                                                       "Phone", "Email"  );
            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            LoadPeople();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           _RefreshPeople();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible =(cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                 txtFilterValue.Text = "";
                 txtFilterValue.Focus();
            }
   
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value);
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
        }

        private void uPdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value);
            frmAddUpdatePerson frm = new frmAddUpdatePerson(PersonID);
            frm.ShowDialog();
            _RefreshPeople();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                //Perform Delele and refresh
                if (clsPeople.DeleteclsPeople((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeople();
                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmShowPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string column = "";
            string cbValue = cbFilterBy.Text;
            switch (cbValue)
            {
                case "Person ID":
                    column = "PersonID";
                    break;
                case "National No.":
                    column = "NationalNo";
                    break;
                case "First Name":
                    column = "FirstName";
                    break;
                case "Second Name":
                    column = "SecondName";
                    break;
                case "Third Name":
                    column = "ThirdName";
                    break;
                case "Last Name":
                    column = "LastName";
                    break;
                case "Gendor":
                    column = "Gendor";
                    break;
                case "Phone":
                    column = "Phone";
                    break;
                case "Email":
                    column = "Email";
                    break;
                default:
                    column = "None";
                    break;
            }

            // إذا كان الإدخال فارغًا أو العمود غير صحيح، قم بإعادة تعيين التصفية
            if (txtFilterValue.Text.Trim() == "" || column == "None")
            {
                _dtListPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtListPeople.Rows.Count.ToString(); // عرض عدد السجلات
                return;
            }

            // بناء التصفية بناءً على نوع العمود
            if (column == "PersonID")
            {
                // تأكد من أن قيمة PersonID صحيحة
                if (int.TryParse(txtFilterValue.Text.Trim(), out int personId))
                {
                    _dtListPeople.DefaultView.RowFilter = string.Format("[{0}] ={1}", column, personId);
                }
                else
                {
                    // إذا كانت القيمة غير صالحة، قم بإزالة التصفية
                    _dtListPeople.DefaultView.RowFilter = "";
                }
            }
            else
            {
                // تصفية الأعمدة الأخرى باستخدام LIKE
                _dtListPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", column, txtFilterValue.Text.Trim());
            }

            // تحديث عدد السجلات المصفاة
            lblRecordsCount.Text = _dtListPeople.DefaultView.Count.ToString();
        }

    }
}
