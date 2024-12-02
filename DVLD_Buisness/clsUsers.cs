using PeopleBusinessLayer;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using UsersDataAccessLayer;
namespace UsersBusinessLayer
{

    public class clsUsers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPeople PersonInfo;


        public clsUsers()
        {
            this.UserID = default;
            this.PersonID = default;
            this.UserName = default;
            this.Password = default;
            this.IsActive = default;
            this.PersonInfo = default;

            Mode = enMode.AddNew;

        }

        private clsUsers(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.PersonInfo = clsPeople.Find(PersonID);

            Mode = enMode.Update;

        }

        private bool _AddNewUsers()
        {
            //call DataAccess Layer 
            this.Password = CumputeHash(Password);
            this.UserID = clsUsersDataAccess.AddNewUsers(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);

        }

        private bool _UpdateUsers()
        {
            //call DataAccess Layer 
            this.Password = CumputeHash(Password);
            return clsUsersDataAccess.UpdateUsers(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);

        }

        public static clsUsers Find(int UserID)
        {
            int PersonID = default;
            string UserName = default;
            string Password = default;
            bool IsActive = default;


            if (clsUsersDataAccess.GetUsersInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }  public static clsUsers FindByUsernameAndPassword(string UserName,string Password)
        {
            int PersonID = default;
            int UserID = -1;
           
            bool IsActive = default;

            Password = CumputeHash(Password);


            if (clsUsersDataAccess.GetUsersInfoByUserNameAndPassword(ref UserID, ref PersonID,  UserName,  Password, ref IsActive))
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);

            }
            else
                return null;

        }
        public static string CumputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] beyteHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(beyteHash).Replace("_", "").ToLower();
            }

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUsers())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUsers();

            }




            return false;
        }

        public static DataTable GetAllUsers() { return clsUsersDataAccess.GetAllUsers(); }

        public static bool DeleteUsers(int UserID) { return clsUsersDataAccess.DeleteUsers(UserID); }

        public static bool isUsersExist(int UserID) { return clsUsersDataAccess.IsUsersExist(UserID); }
        public static bool isUsersExist(string UserName) { return clsUsersDataAccess.IsUsersExist(UserName); }
        public static bool isUsersExistByPersonID(int PersonID) { return clsUsersDataAccess.IsUsersExistByPersonID(PersonID); }


    }

}