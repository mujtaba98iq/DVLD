using ApplicationsBusinessLayer;
using DriversBusinessLayer;
using DVLD2.DVLDAllFiles;
using LicensesBusinessLayer;
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
using UsersBusinessLayer;

namespace DVLD2
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM yyyy");
            lblClock.Text = DateTime.Now.ToString("hh:mm:ss");
            lblSup.Text = DateTime.Now.ToString("tt"); //AM PM
            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM yyyy");
            lblClock.Text = DateTime.Now.ToString("hh:mm:ss");
            lblSup.Text = DateTime.Now.ToString("tt");
        }

        private void bunifuGradientPanel2_Click(object sender, EventArgs e)
        {
            frmMainLicenses frm = new frmMainLicenses();
            frm.DashboardClickBotton = "People";
            frm.LoadFormFromDashBoard();
          
        }

        private void bunifugradientpanel1_click(object sender, EventArgs e)
        {
            frmMainLicenses frm = new frmMainLicenses();
            frm.DashboardClickBotton = "Drivers";
            frm.LoadFormFromDashBoard();
            
        }

        private void bunifuGradientPanel3_Click(object sender, EventArgs e)
        {
            frmMainLicenses frm = new frmMainLicenses();
            frm.DashboardClickBotton = "Users";
            frm.LoadFormFromDashBoard();
            
        }

        private void bunifuGradientPanel4_Click(object sender, EventArgs e)
        {
            frmMainLicenses frm = new frmMainLicenses();
            frm.DashboardClickBotton= "Applications";
            frm.LoadFormFromDashBoard();
        }

        private void bunifuGradientPanel7_Click(object sender, EventArgs e)
        {
            frmMainLicenses frm = new frmMainLicenses();
            frm.DashboardClickBotton = "Licenses";
            frm.LoadFormFromDashBoard();
          
        }
        void LoadNumberOfRecords()
        {
            lblNumberOfApplications.Text=clsApplcations.GetAllApplicSations().Rows.Count.ToString();
            //lblNumberOfPeople.Text=clsPeople.GetAllPeople().Rows.Count.ToString();
            lblNumberOfDrivers.Text=clsDrivers.GetAllDrivers().Rows.Count.ToString();
            lblNumberOfUsers.Text=clsUsers.GetAllUsers().Rows.Count.ToString();
            lblNumberOFLicenses.Text=clsLicenses.GetAllLicenses().Rows.Count.ToString();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            LoadNumberOfRecords();
        }
    }
}

