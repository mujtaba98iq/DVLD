using System;
using System.Data;
using ApplcatoinTypesBusinessLayer;
using ApplicationsBusinessLayer;
using DetainedLicensesBusinessLayer;
using DriversBusinessLayer;
using LicensesDataAccessLayer;
using LincnseClassesBusinessLayer;
using UsersBusinessLayer;

namespace LicensesBusinessLayer
{

    public class clsLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID { get; set; }
        public ApplicationsBusinessLayer.clsApplcations ApplicationInfo;
        public clsDrivers DriverInfo;
        public clsLicensesClasses LicenseClassInfo;
        public UsersBusinessLayer.clsUsers UserInfo;
        public clsDetainedLicenses DetainedInfo { get; set; }

       public string IssueReasonText
        {
            get { return GetIssueReasonText(this.IssueReason); }
        }
        public bool IsDetained
        {
            get { return clsDetainedLicenses.IsLicenseDetained(this.LicenseID); }
        }
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public clsLicenses()
        {
            this.LicenseID = default;
            this.ApplicationID = default;
            this.DriverID = default;
            this.LicenseClassID = default;
            this.IssueDate = default;
            this.ExpirationDate = default;
            this.Notes = default;
            this.PaidFees = default;
            this.IsActive = default;
            this.IssueReason = default;
            this.CreatedByUserID = default;
            this.ApplicationInfo = default;
            this.DriverInfo = default;
            this.LicenseClassInfo = default;
            this.UserInfo = default;
            
            Mode = enMode.AddNew;

        }

        private clsLicenses(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.ApplicationInfo = clsApplcations.Find(ApplicationID);
            this.DriverInfo = clsDrivers.Find(DriverID);
            this.LicenseClassInfo = clsLicensesClasses.Find(LicenseClassID);
            this.UserInfo = UsersBusinessLayer.clsUsers.Find(CreatedByUserID);
            this.DetainedInfo=clsDetainedLicenses.FindByLicenseID(LicenseID);
            Mode = enMode.Update;

        }

        public int Replcament(object getIssueResoun)
        {
            throw new NotImplementedException();
        }

        private bool _AddNewLicenses()
        {
            //call DataAccess Layer 

            this.LicenseID = clsLicensesDataAccess.AddNewLicenses(this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);

        }

        public clsLicenses Replcament( clsLicenses.enIssueReason IssueReason, int CreatedByUserID)
        {
            clsApplcations Applicarion = new ApplicationsBusinessLayer.clsApplcations();
            Applicarion.ApplicantPersonID = this.DriverInfo.PersonID;
            Applicarion.ApplicationDate = DateTime.Now;
            Applicarion.ApplicationStatus = clsApplcations.enApplicationStatus.Completed;
            Applicarion.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                  (int)clsApplcations.enApplicationType.ReplaceDamagedDrivingLicense :
                  (int)clsApplcations.enApplicationType.ReplacementLostDrivingLicense;
            Applicarion.CreatedByUserID = CreatedByUserID;
            Applicarion.LastStatusDate = DateTime.Now;
            Applicarion.PaidFees = (float)clsApplicationTypes.Find((int)clsApplcations.enApplicationType.ReplacementLostDrivingLicense).ApplicatoinFees;
            if (!Applicarion.Save())
            {
                return null;
            }
            clsLicenses NewLicense = new clsLicenses();
            NewLicense.ApplicationID = Applicarion.ApplicationID;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.DriverID = this.DriverID;
            int DefaultValidityLength = this.LicenseClassInfo.DefaultValidityLength;
            NewLicense.ExpirationDate =DateTime.Now.AddYears( DefaultValidityLength);
            NewLicense.IsActive = true;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.IssueReason = IssueReason;
            NewLicense.PaidFees = this.PaidFees;
            NewLicense.Notes = this.Notes;
            NewLicense.LicenseClassID = this.LicenseClassID;
            if (!NewLicense.Save())
            {
                return null;
            }
            DeactivateCurrentLicense();


            return NewLicense;

        }
        public clsLicenses ReNewLicenses(string Notes, int CreatedByUserID)
        {
            clsApplcations Application = new clsApplcations();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationStatus = clsApplcations.enApplicationStatus.Completed;
            Application.ApplicationTypeID =(int)clsApplcations.enApplicationType.RenewDrivingLicense;
            Application.CreatedByUserID = CreatedByUserID;
            Application.PaidFees = (float)clsApplicationTypes.Find((int)clsApplcations.enApplicationType.RenewDrivingLicense).ApplicatoinFees;
            Application.LastStatusDate = DateTime.Now;

            if (!Application.Save())
            {
                return null;
            }
            clsLicenses NewLicense = new clsLicenses();
            NewLicense.ApplicationID = Application.ApplicationID ;
            NewLicense.DriverID = this.DriverID;
            NewLicense.CreatedByUserID = CreatedByUserID;
            int DefaultValidityLength = this.LicenseClassInfo.DefaultValidityLength;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.PaidFees = this.PaidFees;
            NewLicense.Notes = Notes;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicenses.enIssueReason.Renew;
            NewLicense.LicenseClassID = this.LicenseClassID;

            if (!NewLicense.Save())
            {
                return null;
            }
            DeactivateCurrentLicense();
            return NewLicense;
        }

        private bool DeactivateCurrentLicense()
        {
            return clsLicensesDataAccess.DeactivateLicense(this.LicenseID);
        }

        private bool _UpdateLicenses()
        {
            //call DataAccess Layer 

            return clsLicensesDataAccess.UpdateLicenses(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,  (byte)this.IssueReason, this.CreatedByUserID);

        }
        public string GetIssueReasonText(enIssueReason issueReason)
        {
            switch (issueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Tme";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "NotKnown";
            }
        }

        public static clsLicenses Find(int LicenseID)
        {
            int ApplicationID = default;
            int DriverID = default;
            int LicenseClass = default;
            DateTime IssueDate = default;
            DateTime ExpirationDate = default;
            string Notes = default;
            decimal PaidFees = default;
            bool IsActive = default;
            byte IssueReason = default;
            int CreatedByUserID = default;


            if (clsLicensesDataAccess.GetLicensesInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive,ref IssueReason, ref CreatedByUserID))
                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenses())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicenses();

            }




            return false;
        }

        public static DataTable GetAllLicenses() { return clsLicensesDataAccess.GetAllLicenses(); }
        public static DataTable GetAllLicenses(int DriverID) { return clsLicensesDataAccess.GetAllLicenses(DriverID); }

        public static bool DeleteLicenses(int LicenseID) { return clsLicensesDataAccess.DeleteLicenses(LicenseID); }
        public int Detain(float FineFees,int CreatedByUserID)
        {
            clsDetainedLicenses detainedLicenses = new clsDetainedLicenses();
            detainedLicenses.CreatedByUserID = CreatedByUserID;
            detainedLicenses.DetainDate = DateTime.Now;
            detainedLicenses.FineFees = Convert.ToDecimal(PaidFees);
            detainedLicenses.LicenseID = this.LicenseID;
            

            if (!detainedLicenses.Save())
            {
                return -1;
            }
            return detainedLicenses.DetainID;
        }
        public static int GetActiveLicenseIDByPersonID(int PersonID,int LicenseClassID) { return clsLicensesDataAccess.GetActiveLicenseIDByPersonID(PersonID,LicenseClassID); }

        public static bool isLicensesExist(int LicenseID) { return clsLicensesDataAccess.IsLicensesExist(LicenseID); }
        public  Boolean IsLicenseExpired() { return this.ExpirationDate < DateTime.Now; }
        public bool ReleaseDetainedLicense()
        {
            clsApplcations Application = new clsApplcations();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationStatus = clsApplcations.enApplicationStatus.Completed;
            Application.ApplicationTypeID = (int)clsApplcations.enApplicationType.RenewDrivingLicense;
            Application.CreatedByUserID = CreatedByUserID;
            Application.PaidFees = (float)clsApplicationTypes.Find((int)clsApplcations.enApplicationType.RenewDrivingLicense).ApplicatoinFees;
            Application.LastStatusDate = DateTime.Now;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }
           return this.DetainedInfo.ReleaseDetainedLicesense(CreatedByUserID, Application.ApplicationID);
            

        }
        public static bool isLicensesExistByPersonID(int PersonID,int LicenseID) { return (clsLicensesDataAccess.GetActiveLicenseIDByPersonID(PersonID, LicenseID)>-1); }

    }

}