using DevExpress.Utils;
using DevExpress.Utils.CommonDialogs;
using DVLD_with_Car_Rental_DataAccessLayer;
using DVLD2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehiclesBusinessLayer;

namespace DVLD2.CarRentalAllFiles.Vehicles
{
    public partial class frmAddUpdateVehicle : Form
    {
        int _vehicleID = -1;
        clsVehicles _vehicle;
      public  enum enMode
        {
            Add,Update
        }
        enMode _Mode = enMode.Add;
        public enMode Mode { get { return _Mode;

               }
            set
            {
                switch (value)
                {
                    case enMode.Add:
                        lblTitle.Text = "Add Vehicles";
                        break;
                    case enMode.Update:
                        lblTitle.Text = "Update Vehicles";
                        break;
                    default:
                        lblTitle.Text = "Add Vehicles";
                        break;
                }
            }
            } 
        public frmAddUpdateVehicle()
        {
            InitializeComponent();
            _Mode = enMode.Add;
            _vehicle= new clsVehicles();    
        }
        public frmAddUpdateVehicle(int vehicleID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _vehicleID = vehicleID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateVehicle_Load(object sender, EventArgs e)
        {
            LoadComboBox();

            if (_Mode == enMode.Update)
            {
                LoadInfo(_vehicleID);
            }

        }

        private async void LoadInfo(int vehicleId)
        {
            if(vehicleId <= 0)
            {
                clsGlobal.MessageBoxNotValid();
                return;
            }
           _vehicle=await clsVehicles.Find(vehicleId);
            if (_vehicle == null)
            {
                clsGlobal.MessageBoxListOrObjectIsEmpty();
                return;
            }
            lblVehicleID.Text = vehicleId.ToString();
            txtMakeID.Text = _vehicle.MakeID.ToString();
            txtModelID.Text = _vehicle.ModelID.ToString();
            txtPlateNumber.Text = _vehicle.PlateNumber.ToString();
            txtRentalPricePerDay.Text = _vehicle.RentalPricePerDay.ToString();
            txtVehicleName.Text = _vehicle.VehicleName.ToString();
            nudMileage.Value = _vehicle.Mileage;
            dtpYear.Value = new DateTime(_vehicle.Year, 1, 1); 
            cbCarCategory.Text=_vehicle.CategorieInfo.CategoryName;
            cbFuelType.Text=_vehicle.fuleTypeInfo.FuleType;
            lblCreatedByUserID.Text = _vehicle.CreatedByUserID.ToString();
            pbVehicle.ImageLocation = _vehicle.VehicleImage;

        }

        private void LoadComboBox()
        {
            FillComboBoxFuleTypes();
            FillComboBoxCarCategory();
            lblCreatedByUserID.Text=clsGlobal.CurrentUser.UserID.ToString();
        }

        private async void FillComboBoxFuleTypes()
        {
            DataTable dt = await clsVehicles.GetAllVehiclesFuleTypes();

            if (dt == null)
            {
                clsGlobal.MessageBoxListOrObjectIsEmpty();
                return;
            }
            cbFuelType.Controls.Clear();

            foreach (DataRow dr in dt.Rows) {

                cbFuelType.Items.Add(dr["FuleType"]);
            }
            cbFuelType.SelectedIndex = 0;


        }
        private async void FillComboBoxCarCategory()
        {
            DataTable dt = await clsVehicles.GetAllVehiclesCarCategory();

            if (dt == null)
            {
                clsGlobal.MessageBoxListOrObjectIsEmpty();
                return;
            }
            cbCarCategory.Controls.Clear();

            foreach (DataRow dr in dt.Rows)
            {

                cbCarCategory.Items.Add(dr["CategoryName"]);
            }
            cbCarCategory.SelectedIndex = 0;

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Add)
            {
                if (clsGlobal.MessageBoxAddSaveConfirmation("Vehicle"))
                
                    return;                
            }
            else
            {
                if (clsGlobal.MessageBoxUpdateSaveConfirmation("Vehicle"))
                
                    return;   
            }
            if (!this.ValidateChildren())
                return;
           clsComboBoxInfo comboBoxParameters = new clsComboBoxInfo();
           comboBoxParameters.categoryName= cbCarCategory.Text;
           comboBoxParameters.fuleType= cbFuelType.Text;
           comboBoxParameters.Make= txtMakeID.Text;
           comboBoxParameters.ModelName= txtModelID.Text;

            clsComboBoxInfo ComboBoxInfo =await clsVehicles.GetComboBoxInfoForVehicleAsync(comboBoxParameters);
            if(ComboBoxInfo == null )
            {
                MessageBox.Show("CarCategoryName and FuelType Not found, Enter correct values");
                return;
            }


            _vehicle.CarCategoryID =ComboBoxInfo.CarCategoryID;
            _vehicle.FuelTypeID=ComboBoxInfo.FuelTypeID;
            _vehicle.RentalPricePerDay =Convert.ToDecimal( txtRentalPricePerDay.Text);
            _vehicle.PlateNumber =txtPlateNumber.Text;
            _vehicle.VehicleName =txtVehicleName.Text;
            _vehicle.ModelID = ComboBoxInfo.ModelID;
            _vehicle.MakeID = ComboBoxInfo.MakeID;
            _vehicle.CreatedByUserID=clsGlobal.CurrentUser.UserID;
            _vehicle.VehicleImage=pbVehicle.ImageLocation;
            _vehicle.Year=dtpYear.Value.Year;
            _vehicle.Mileage =(int) nudMileage.Value;
            if (await _vehicle.Save())
            {
                clsGlobal.MessageBoxSavedSuccessfully();
            }
            else
            {
                clsGlobal.MessageBoxSavedFailed();
            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbVehicle.ImageLocation = null;
            pbVehicle.Image = Resources.car;
            llRemoveImage.Visible = false;
            
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = clsGlobal.OpenFileDialogFilter();
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbVehicle.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                // ...
            }
        }

        private void txtVehicleName_Validating(object sender, CancelEventArgs e)
        {
            CheckValidation((TextBox)sender, e);
        }

        private void CheckValidation(TextBox sender, CancelEventArgs e)
        {
            clsGlobal.ValidatingtextBoxNull(sender,errorProvider1, e);
        }

        private void cbFuelType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
