using System;
using System.Data;
using ApplicationsBusinessLayer;
using DetainedLicensesDataAccessLayer;
using UsersBusinessLayer;

namespace DetainedLicensesBusinessLayer
{

    public class clsDetainedLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }

    
        public ApplicationsBusinessLayer.clsApplcations ApplicationInfo;
        

        public clsDetainedLicenses()
        {
            this.DetainID = default;
            this.LicenseID = default;
            this.DetainDate = default;
            this.FineFees = default;
            this.CreatedByUserID = default;
            this.IsReleased = default;
            this.ReleaseDate = default;
            this.ReleasedByUserID = default;
            this.ReleaseApplicationID = default;

            this.ApplicationInfo = default;

            Mode = enMode.AddNew;

        }

        private clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;

            this.ApplicationInfo = clsApplcations.Find(ReleaseApplicationID);


            Mode = enMode.Update;

        }

        private bool _AddNewDetainedLicenses()
        {
            //call DataAccess Layer 

            this.DetainID = clsDetainedLicensesDataAccess.AddNewDetainedLicenses(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);

            return (this.DetainID != -1);

        }

        private bool _UpdateDetainedLicenses()
        {
            //call DataAccess Layer 

            return clsDetainedLicensesDataAccess.UpdateDetainedLicenses(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);

        }

        public static clsDetainedLicenses Find(int DetainID)
        {
            int LicenseID = default;
            DateTime DetainDate = default;
            decimal FineFees = default;
            int CreatedByUserID = default;
            bool IsReleased = default;
            DateTime ReleaseDate = default;
            int ReleasedByUserID = default;
            int ReleaseApplicationID = default;


            if (clsDetainedLicensesDataAccess.GetDetainedLicensesInfoByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }
        public static clsDetainedLicenses FindByLicenseID(int LicenseID)
        {
            int DetainID = default;
            DateTime DetainDate = default;
            decimal FineFees = default;
            int CreatedByUserID = default;
            bool IsReleased = default;
            DateTime ReleaseDate = default;
            int ReleasedByUserID = default;
            int ReleaseApplicationID = default;


            if (clsDetainedLicensesDataAccess.GetDetainedLicensesInfoByLicenseID(ref DetainID,  LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicenses())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetainedLicenses();

            }




            return false;
        }

        public static DataTable GetAllDetainedLicenses() { return clsDetainedLicensesDataAccess.GetAllDetainedLicenses(); }

        public static bool DeleteDetainedLicenses(int DetainID) { return clsDetainedLicensesDataAccess.DeleteDetainedLicenses(DetainID); }

        public static bool isDetainedLicensesExist(int DetainID) { return clsDetainedLicensesDataAccess.IsDetainedLicensesExist(DetainID); }
        public static bool IsLicenseDetained(int LicenseID) { return clsDetainedLicensesDataAccess.IsLicenseDetained(LicenseID); }
        public  bool ReleaseDetainedLicesense(int ReleasedByUserID,  int ReleaseApplicationID)
        {
            return clsDetainedLicensesDataAccess.ReleaseDetainedLicenses(this.DetainID,ReleasedByUserID,ReleaseApplicationID);
        }

    }

}