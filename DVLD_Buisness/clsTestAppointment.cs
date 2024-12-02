using ApplicationsBusinessLayer;
using LocalDrivingLicenseApplicationsBusinessLayer;
using System;
using System.Data;
using TestAppointmentDataAccessLayer;
using TestsBusinessLayer;
using TestTypesBusinessLayer;
using UsersBusinessLayer;

namespace TestAppointmentBusinessLayer
{

    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public clsTestTypes.enTestType TestTypeID { get; set; }

      

        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsTestTypes TestTypesInfo;
        public clsLocalDrivingLicenseApplications LocalLicensesInfo;
        public UsersBusinessLayer.clsUsers UserInfo;
        public ApplicationsBusinessLayer.clsApplcations RetakeTestAppInfo { set; get; }

        public int TestID
        {
            get { return GetTestID(); }
        }
        public clsTestAppointment()
        {
            this.TestAppointmentID = default;
            this.TestTypeID = default;
            this.LocalDrivingLicenseApplicationID = default;
            this.AppointmentDate = default;
            this.PaidFees = default;
            this.CreatedByUserID = default;
            this.IsLocked = default;
            this.RetakeTestApplicationID = -1;
            this.TestTypesInfo = default;
            this.LocalLicensesInfo = default;
            this.UserInfo = default;

            Mode = enMode.AddNew;

        }

        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = (clsTestTypes.enTestType)TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.TestTypesInfo = clsTestTypes.Find((clsTestTypes.enTestType)TestTypeID) ;
            this.LocalLicensesInfo = clsLocalDrivingLicenseApplications.Find(LocalDrivingLicenseApplicationID) ;
            this.UserInfo = UsersBusinessLayer.clsUsers.Find(CreatedByUserID) ;
            

            Mode = enMode.Update;

        }

        private bool _AddNewTestAppointment()
        {
            //call DataAccess Layer 

            this.TestAppointmentID = clsTestAppointmentDataAccess.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);

        }
        public int GetTestID()
        {
            //call DataAccess Layer 
            return clsTestAppointmentDataAccess.GetTestID(this.TestAppointmentID);
        }

        private bool _UpdateTestAppointment()
        {
            //call DataAccess Layer 

            return clsTestAppointmentDataAccess.UpdateTestAppointment(this.TestAppointmentID,(int) this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);

        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.MinValue;
            decimal PaidFees = 0m;  // تغيير النوع إلى decimal
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            bool isFound = clsTestAppointmentDataAccess.GetTestAppointmentInfoByID(
                TestAppointmentID,
                ref TestTypeID,
                ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate,
                ref PaidFees,
                ref CreatedByUserID,
                ref IsLocked,
                ref RetakeTestApplicationID
            );

            if (isFound)
            {
                return new clsTestAppointment(
                    TestAppointmentID,
                    TestTypeID,
                    LocalDrivingLicenseApplicationID,
                    AppointmentDate,
                    PaidFees,
                    CreatedByUserID,
                    IsLocked,
                    RetakeTestApplicationID
                );
            }
            else
            {
                Console.WriteLine($"Appointment with ID {TestAppointmentID} not found.");
                return null;
            }
        }



        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }




            return false;
        }

        public static DataTable GetAllTestAppointment() { return clsTestAppointmentDataAccess.GetAllTestAppointment(); }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentDataAccess.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }
        public static bool DeleteTestAppointment(int TestAppointmentID) { return clsTestAppointmentDataAccess.DeleteTestAppointment(TestAppointmentID); }

        public static bool isTestAppointmentExist(int TestAppointmentID) { return clsTestAppointmentDataAccess.IsTestAppointmentExist(TestAppointmentID); }


    }

}