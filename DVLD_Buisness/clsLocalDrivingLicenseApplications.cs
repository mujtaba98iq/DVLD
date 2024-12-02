using System;
using System.Data;
using ApplicationsBusinessLayer;
using DriversBusinessLayer;
using LicensesBusinessLayer;
using LincnseClassesBusinessLayer;
using LocalDrivingLicenseApplicationsDataAccessLayer;
using PeopleBusinessLayer;
using TestsBusinessLayer;
using TestTypesBusinessLayer;

namespace LocalDrivingLicenseApplicationsBusinessLayer
{

    public class clsLocalDrivingLicenseApplications:clsApplcations
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int LocalLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsApplcations ApplicationInfo;
        public clsLicensesClasses LicensesClassesInfo;
        public string PersonFullName
        {
            get
            {
                return clsPeople.Find(ApplicantPersonID).FullName;
            }

        }
        public clsLocalDrivingLicenseApplications()

        {
            this.LocalLicenseApplicationID = -1;
            this.LicenseClassID = -1;


            Mode = enMode.AddNew;

        }

        private clsLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            this.LocalLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            this.LicensesClassesInfo = clsLicensesClasses.Find(LicenseClassID);
            this.ApplicationInfo = clsApplcations.Find(ApplicationID);
            Mode = enMode.Update;
        }


        private bool _AddNewLocalDrivingLicenseApplications()
        {
            //call DataAccess Layer 

            this.LocalLicenseApplicationID = clsLocalDrivingLicenseApplicationsDataAccess.AddNewLocalDrivingLicenseApplications(
                this.ApplicationID, this.LicenseClassID);

            return (this.LocalLicenseApplicationID != -1);

        }

        private bool _UpdateLocalDrivingLicenseApplications()
        {
            //call DataAccess Layer 

            return clsLocalDrivingLicenseApplicationsDataAccess.UpdateLocalDrivingLicenseApplications(this.LocalLicenseApplicationID, this.ApplicationID, this.LicenseClassID);

        }

        public static clsLocalDrivingLicenseApplications Find(int LocalLicenseApplicationID)
        {
            int ApplicationID = default;
            int LicenseClassID = default;



            if (clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingLicenseApplicationsInfoByID(LocalLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                clsApplcations application = clsApplcations.Find(ApplicationID);

                return new clsLocalDrivingLicenseApplications(LocalLicenseApplicationID, application.ApplicationID,
                    application.ApplicantPersonID,application.ApplicationDate, application.ApplicationTypeID,
                 (enApplicationStatus)application.ApplicationStatus,application.LastStatusDate,application.PaidFees,application.CreatedByUserID,
                     LicenseClassID);
            }
            else
                return null;

        }

        public bool Save()
        {
            base.Mode = (clsApplcations.enMode)Mode;
            if (!base.Save())
                return false;
            
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplications())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplications();

            }
            return false;
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDrivers Driver = clsDrivers.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDrivers();
                
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicenses License = new clsLicenses();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicensesClassesInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicensesClassesInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicenses.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetCompleted();

                return License.LicenseID;
            }

            else
                return -1;
        }

     

        public static DataTable GetAllLocalDrivingLicenseApplications() { return clsLocalDrivingLicenseApplicationsDataAccess.GetAllLocalDrivingLicenseApplications(); }

        public  bool DeleteLocalDrivingLicenseApplications(int LocalLicenseApplicationID) { return clsLocalDrivingLicenseApplicationsDataAccess.DeleteLocalDrivingLicenseApplications(LocalLicenseApplicationID); }

        public static bool isLocalDrivingLicenseApplicationsExist(int LocalLicenseApplicationID) { return clsLocalDrivingLicenseApplicationsDataAccess.IsLocalDrivingLicenseApplicationsExist(LocalLicenseApplicationID); }

        public  byte GetPassedTestCount()
        {
             return clsTests.GetPassedTestCount(this.LocalLicenseApplicationID);
        }
        public int GetActiveLicenseID()
        {
            return clsLicenses.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        } 
        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID()!=-1);
            
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationsDataAccess.IsThereAnActiveScheduledTest(this.LocalLicenseApplicationID, (int)TestTypeID);
        }
        public clsTests GetLastTestPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTests.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }

        public bool DoesAttendTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DoesAttendTestType(this.LocalLicenseApplicationID,(int) TestTypeID);
        }
        public byte TotalTrialsPerTest(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.TotalTrialsPerTest(this.LocalLicenseApplicationID, (int)TestTypeID);
        }
        public bool DoesPassTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DoesPassTestType(this.LocalLicenseApplicationID,(int) TestTypeID);
        }
        public bool PassAllTests()
        {
          return  clsTests.PassAllTests(this.LocalLicenseApplicationID);
        }
        public int GetTriles(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.GetTriles(this.LocalLicenseApplicationID,(int)TestTypeID);
        }
    }

}