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

namespace DVLD2.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {
      private  int _UserID = -1;
      private  clsUsers _User;

        public int UserID
        {
            get { return _UserID; }
        }
        public clsUsers User
        {
            get { return _User; }
        }

        public void LoadUserInfo(int UserID)
        {
            _User =  clsUsers.Find(UserID);
            if(_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillUserInfo();
        }

        private void _ResetPersonInfo()
        {
            ctrlPersonCard21.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

        private void FillUserInfo()
        {
            _UserID=_User.UserID;
            ctrlPersonCard21.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void ctrlPersonCard21_Load(object sender, EventArgs e)
        {
            
        }
    }
}
