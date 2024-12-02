using System;
using System.Data;
using ApplicationsBusinessLayer;
using DriversBusinessLayer;
using InternationalLicensesDataAccessLayer;
using UsersBusinessLayer;

namespace InternationalLicensesBusinessLayer
{

    public class clsInternationalLicenses : ApplicationsBusinessLayer.clsApplcations
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int LocalDriverApplicationID { get; set; }
        public int DirverID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public clsDrivers DriverInfo;

        public clsInternationalLicenses()
        {
            this.ApplicationTypeID = (int)ApplicationsBusinessLayer.clsApplcations.enApplicationType.NewInternationalLicense;
            this.InternationalLicenseID = default;
            this.LocalDriverApplicationID = default;
            this.DirverID = default;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;

            this.IsActive = true;
            Mode = enMode.AddNew;

        }

        private clsInternationalLicenses(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
            int InternationalLicenseID, int LocalDriverApplicationID, int DirverID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID =(int)ApplicationsBusinessLayer.clsApplcations.enApplicationType.NewInternationalLicense ;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.LocalDriverApplicationID = LocalDriverApplicationID;
            this.DirverID = DirverID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
          
            this.DriverInfo = clsDrivers.Find(this.DirverID);

            Mode = enMode.Update;

        }

        private bool _AddNewInternationalLicenses()
        {
            //call DataAccess Layer 

            this.InternationalLicenseID = clsInternationalLicensesDataAccess.AddNewInternationalLicenses(this.LocalDriverApplicationID, this.ApplicationID, this.DirverID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);

        }

        private bool _UpdateInternationalLicenses()
        {
            //call DataAccess Layer 

            return clsInternationalLicensesDataAccess.UpdateInternationalLicenses(this.InternationalLicenseID, this.LocalDriverApplicationID, this.ApplicationID, this.DirverID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

        }

        public static clsInternationalLicenses Find(int InternationalLicenseID)
        {
            int LocalDriverApplicationID = default;
            int ApplicatoinID = default;
            int DirverID = default;
            DateTime IssueDate = default;
            DateTime ExpirationDate = default;
            bool IsActive = default;
            int CreatedByUserID = default;


            if (clsInternationalLicensesDataAccess.GetInternationalLicensesInfoByID(InternationalLicenseID, ref LocalDriverApplicationID, ref ApplicatoinID, ref DirverID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                ApplicationsBusinessLayer.clsApplcations applications = ApplicationsBusinessLayer.clsApplcations.Find(ApplicatoinID);

                return new clsInternationalLicenses(applications.ApplicationID,applications.ApplicantPersonID,
                    applications.ApplicationDate,applications.ApplicationStatus,
                    applications.LastStatusDate,applications.PaidFees,applications.CreatedByUserID,
                    InternationalLicenseID, LocalDriverApplicationID, DirverID, IssueDate, ExpirationDate, IsActive);
            }
            else
                return null;

        }

        public bool Save()
        {

            base.Mode = (clsApplcations.enMode)Mode;
            if (!base.Save())
                return true;
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicenses())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateInternationalLicenses();

            }




            return false;
        }

        public static DataTable GetAllInternationalLicenses() { return clsInternationalLicensesDataAccess.GetAllInternationalLicenses(); }

        public static bool DeleteInternationalLicenses(int InternationalLicenseID) { return clsInternationalLicensesDataAccess.DeleteInternationalLicenses(InternationalLicenseID); }

        public static bool isInternationalLicensesExist(int InternationalLicenseID) { return clsInternationalLicensesDataAccess.IsInternationalLicensesExist(InternationalLicenseID); }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicensesDataAccess.GetDriverInternationalLicenses(DriverID);
        }

    }

}