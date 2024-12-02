using System;
using System.Data;
using DriversDataAccessLayer;
using InternationalLicensesBusinessLayer;
using LicensesBusinessLayer;
using PeopleBusinessLayer;
using UsersBusinessLayer;

namespace DriversBusinessLayer
{

    public class clsDrivers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsPeople PersonInfo;
        public clsDrivers()
        {
            this.DriverID = default;
            this.PersonID = default;
            this.CreatedByUserID = default;
            this.CreatedDate = default;
            this.PersonInfo = default;
            Mode = enMode.AddNew;

        }

        private clsDrivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = clsPeople.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewDrivers()
        {
            //call DataAccess Layer 

            this.DriverID = clsDriversDataAccess.AddNewDrivers(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (this.DriverID != -1);

        }

        private bool _UpdateDrivers()
        {
            //call DataAccess Layer 

            return clsDriversDataAccess.UpdateDrivers(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);

        }

        public static clsDrivers Find(int DriverID)
        {
            int PersonID = default;
            int CreatedByUserID = default;
            DateTime CreatedDate = default;


            if (clsDriversDataAccess.GetDriversInfoByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }
        public static clsDrivers FindByPersonID(int PersonID)
        {
            int DriverID = default;
            int CreatedByUserID = default;
            DateTime CreatedDate = default;


            if (clsDriversDataAccess.GetDriversInfoByPersonID(ref DriverID,  PersonID, ref CreatedByUserID, ref CreatedDate))
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDrivers())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDrivers();

            }

            return false;
        }

        public static DataTable GetAllDrivers() { return clsDriversDataAccess.GetAllDrivers(); }

        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicenses.GetAllLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenses.GetDriverInternationalLicenses(DriverID);
        }
        public static bool DeleteDrivers(int DriverID) { return clsDriversDataAccess.DeleteDrivers(DriverID); }

        public static bool isDriversExist(int DriverID) { return clsDriversDataAccess.IsDriversExist(DriverID); }
        public static bool isThePersonADriver(int personID) { return clsDriversDataAccess.IsDriversExist(personID); }


    }

}