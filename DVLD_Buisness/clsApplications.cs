using System;
using System.Data;
using ApplcatoinTypesBusinessLayer;
using ApplicationsDataAccessLayer;
using PeopleBusinessLayer;
using UsersBusinessLayer;

namespace ApplicationsBusinessLayer
{

    public class clsApplcations
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType
        {
            NewLocalDrivingLicense=1, RenewDrivingLicense=2,ReplacementLostDrivingLicense=3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        }
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public float PaidFees { get; set; }
        public DateTime LastStatusDate { get; set; }
        public int CreatedByUserID { get; set; }

        public clsPeople PersonInfo;
        public clsApplicationTypes ApplicationTypeInfo;
        public clsUsers CreatedByUserInfo;

       public enum enApplicationStatus
        {
            New=1 ,
            Cancelled =2,
            Completed=3
        }
       

        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "UnKnown";
                }
            }
        }
        public clsApplcations()
        {
            this.ApplicationID = default;
            this.ApplicantPersonID = default;
            this.ApplicationDate = default;
            this.ApplicationTypeID = default;
            this.ApplicationStatus = default;
            this.PaidFees = default;
            this.LastStatusDate = default;
            this.CreatedByUserID = default;
            this.PersonInfo = default;
            this.ApplicationTypeInfo = default;

            Mode = enMode.AddNew;

        }

        private clsApplcations(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus, float PaidFees, DateTime LastStatusDate, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.PaidFees = PaidFees;
            this.LastStatusDate = LastStatusDate;
            this.CreatedByUserID = CreatedByUserID;
            this.PersonInfo = clsPeople.Find(ApplicantPersonID);
            this.ApplicationTypeInfo = clsApplicationTypes.Find(ApplicationTypeID);
     
            this.CreatedByUserInfo = clsUsers.Find(CreatedByUserID);


            Mode = enMode.Update;

        }

        private bool _AddNewApplications()
        {
            //call DataAccess Layer 

            this.ApplicationID = clsApplicationsDataAccess.AddNewApplications(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte)this.ApplicationStatus, this.PaidFees, this.LastStatusDate, this.CreatedByUserID);

            return (this.ApplicationID != -1);

        }

        private bool _UpdateApplications()
        {
            //call DataAccess Layer 

            return clsApplicationsDataAccess.UpdateApplications(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte)this.ApplicationStatus, this.PaidFees, this.LastStatusDate, this.CreatedByUserID);

        }

        public static clsApplcations Find(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = 20;

            bool IsFound = clsApplicationsDataAccess.GetApplicationsInfoByID
                                (
                                    ApplicationID, ref ApplicantPersonID,
                                    ref ApplicationDate, ref ApplicationTypeID,
                                    ref ApplicationStatus, ref PaidFees, ref LastStatusDate
                                    , ref CreatedByUserID
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsApplcations(ApplicationID, ApplicantPersonID,
                                     ApplicationDate, ApplicationTypeID,
                                    (enApplicationStatus)ApplicationStatus, PaidFees, LastStatusDate,
                                      CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplications())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplications();

            }




            return false;
        }

        protected bool SetCompleted()
        {
            return clsApplicationsDataAccess.UpdateStatus(ApplicationID, 3);
        }
        public static DataTable GetAllApplicSations() { return clsApplicationsDataAccess.GetAllApplications(); }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplcations.enApplicationType ApplicationTypeID, int LicenseClassID) 
        {
            return clsApplicationsDataAccess.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID); 
        }
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID) 
        {
            return clsApplicationsDataAccess.GetActiveInternationalLicenseIDByDriverID(DriverID); 
        }

        public static bool DeleteApplications(int ApplicationID) { return clsApplicationsDataAccess.DeleteApplications(ApplicationID); }
        public  bool CenselApplication() { return clsApplicationsDataAccess.UpdateStatus(ApplicationID,2); }

        public static bool isApplicationsExist(int ApplicationID) { return clsApplicationsDataAccess.IsApplicationsExist(ApplicationID); }
       


    }

}