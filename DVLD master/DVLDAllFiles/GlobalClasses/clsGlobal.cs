using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsersBusinessLayer;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Threading;
using DevExpress.Utils.CommonDialogs;

namespace DVLD2
{
    static public class clsGlobal
    {

        public static clsUsers CurrentUser=clsUsers.Find(1);

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD2";
            string ValueNameForUserName = "UserName";
            string ValueDataForUserName = Username;

            string ValueNameForPassword = "Password";
            string ValueDataForPassword = Password;

            try
            {
                Registry.SetValue(KeyPath, ValueNameForUserName, ValueDataForUserName, RegistryValueKind.String);
                Registry.SetValue(KeyPath, ValueNameForPassword, ValueDataForPassword, RegistryValueKind.String);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD2";
            string ValueNameForUserName = "UserName";
            string ValueNameForPassword = "Password";

            try
            {
                string UserNameVlaue = Registry.GetValue(KeyPath, ValueNameForUserName, null) as string;
                string PasswordVlaue = Registry.GetValue(KeyPath, ValueNameForPassword, null) as string;

                if (UserNameVlaue != null && PasswordVlaue !=null)
                {
                    Username=UserNameVlaue;
                    Password=PasswordVlaue;
                  return true;
                }
                else
                {
                   return false;

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
            return false;
        }


       public static string CumputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] beyteHash=sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(beyteHash).Replace("_","").ToLower();
            }

        }

       static public void MessageBoxNotFound(string column)
        {
            MessageBox.Show($"{column} is not found! Please check the column name and try again.", "Column Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        static public void MessageBoxNotValid()
        {
            MessageBox.Show("Input is not valid! Please enter another value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static public void MessageBoxListOrObjectIsEmpty()
        {
            MessageBox.Show("The list or object is empty. Please add items to proceed.", "Empty Informaion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
          static public void MessageBoxDontHaveALicense()
        {
            MessageBox.Show("You do not have a license. Please issue a driver’s license first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static public void MessageBoxValidateChildren()
        {
            MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                     "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        static public void MessageBoxSavedSuccessfully()
        {
          
                MessageBox.Show("Saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        static public void MessageBoxDeleteSuccessfully()
        {

            MessageBox.Show("Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        static public void MessageBoxFillterCardChooseOne(string ChooseOneFrom)
        {

            MessageBox.Show($"Choose one {ChooseOneFrom} first");
        }
        static public string OpenFileDialogFilter()
        {

            return "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
        }
        static public bool MessageBoxAddSaveConfirmation(string ConfirmationTo)
        {

            if (MessageBox.Show($"Are you shor to add {ConfirmationTo} ?", "Confirmaion", MessageBoxButtons.YesNo) == DialogResult.OK)
            {
                return true;
            }
            else
                return false;

        }
        static public bool MessageBoxAddDeleteConfirmation(string ConfirmationTo)
        {

            if (MessageBox.Show($"Are you shor to Delete {ConfirmationTo} ?", "Confirmaion", MessageBoxButtons.YesNo) == DialogResult.OK)
            {
                return true;
            }
            else
                return false;

        }

        static public bool MessageBoxUpdateSaveConfirmation(string ConfirmationTo)
        {

            if (MessageBox.Show($"Are you shor to Update {ConfirmationTo} ?", "Confirmaion", MessageBoxButtons.YesNo) == DialogResult.OK)
            {
                return true;
            }
            else
                return false;

        }
        static public void MessageBoxSavedFailed()
        {
            MessageBox.Show("An error occurred while saving: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        static public void MessageBoxDeleteFailed()
        {
            MessageBox.Show("An error occurred while Delete: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        static public void MessageBoxNextFailed(string requiredMessage)
        {
            MessageBox.Show($"Please complete the requirements for this page, {requiredMessage} is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        static public void OnlyNumbers(KeyPressEventArgs e)
        {

            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }
        static public void OnlyLetters(KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar);
        }

        static public void OnlyLettersAndNumbers(KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar)&&!char.IsDigit(e.KeyChar);
        }
        static public void ValidatingtextBoxNull(TextBox txtBox, ErrorProvider errorProvider1, CancelEventArgs e,string Massage="")
        {
            if (string.IsNullOrEmpty(txtBox.Text))
            {
                errorProvider1.SetError(txtBox, Massage==""? "This field is required":Massage);
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBox, null);
            }
        }

        static public void ValidatingNumericUpDownNull(NumericUpDown txtBox, ErrorProvider errorProvider1, CancelEventArgs e, string Massage = "")
        {
            if (txtBox.Value <= 0)
            {
                errorProvider1.SetError(txtBox, Massage == "" ? "This field is required, must be greater than 0" : Massage);
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtBox, null);
            }
        }

        public static void ShortWait()
        {
            Thread.Sleep(300); // الانتظار لمدة 100 مللي ثانية
        }


    }
}