using CountriesBusinessLayer;
using DVLD2.Properties;
using PeopleBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.People.Controls
{
    public partial class ctrlPersonCard1 : UserControl
    {
        private int _PersonID;
        private clsPeople _Person;

       public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPeople SelectedPersonInfo
        {
            get { return _Person; }
        }
     
        public ctrlPersonCard1(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        public ctrlPersonCard1()
        {
        }

        public void LoadPersonInfo(int PersonID)
        {

            _Person = clsPeople.Find(PersonID);
            if (_Person == null)
            {
                RestPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FillPersonInfo();



        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPeople.Find(NationalNo);
            if (_Person == null)
            {
                RestPersonInfo();
                MessageBox.Show("No Person with NationalNo = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FillPersonInfo();
        }

        private void FillPersonInfo()
        {
           // llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblFullName.Text = _Person.FullName;
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = clsCountries.Find(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;
            LoadPersonImage();

        }

        private void LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.ImageLocation = "C:\\Users\\mujta\\source\\repos\\C#_Disctop\\LibraryFullProject\\BookLibrary\\Icons\\DarkMode\\Man 128.png";
            else
                pbPersonImage.ImageLocation = "C: \\Users\\mujta\\source\\repos\\C#_Disctop\\LibraryFullProject\\BookLibrary\\Icons\\DarkMode\\Woman 128.png";
            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;

                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void RestPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;
        }

        private void linklbEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
            LoadPersonInfo(_PersonID);
        }
    }
}
