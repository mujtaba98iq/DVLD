using System;
using System.Data;
using TestAppointmentBusinessLayer;
using TestsDataAccessLayer;
using TestTypesBusinessLayer;
using UsersBusinessLayer;

namespace TestsBusinessLayer
{

    public class clsTests
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTestAppointment TestAppointmentInfo;
        public clsUsers UserInfo;
        public clsTests()
        {
            this.TestID = default;
            this.TestAppointmentID = default;
            this.TestResult = default;
            this.Notes = default;
            this.CreatedByUserID = default;


            Mode = enMode.AddNew;

        }

        private clsTests(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.UserInfo = clsUsers.Find(CreatedByUserID);


            Mode = enMode.Update;

        }

        private bool _AddNewTests()
        {
            //call DataAccess Layer 

            this.TestID = clsTestsDataAccess.AddNewTests(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);

        }

        private bool _UpdateTests()
        {
            //call DataAccess Layer 

            return clsTestsDataAccess.UpdateTests(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);

        }
        public static clsTests FindLastTestPerPersonAndLicenseClass
     (int PersonID, int LicenseClassID, clsTestTypes.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestsDataAccess.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTests(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }
        public static clsTests Find(int TestID)
        {
            int TestAppointmentID = default;
            bool TestResult = default;
            string Notes = default;
            int CreatedByUserID = default;


            if (clsTestsDataAccess.GetTestsInfoByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new clsTests(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTests())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTests();

            }




            return false;
        }

        public static DataTable GetAllTests() { return clsTestsDataAccess.GetAllTests(); }

        public static bool DeleteTests(int TestID) { return clsTestsDataAccess.DeleteTests(TestID); }

      
        public static bool isTestsExist(int TestID) { return clsTestsDataAccess.IsTestsExist(TestID); }
        public static bool PassAllTests(int LocalLicenseApplicationID) { return GetPassedTestCount(LocalLicenseApplicationID)==3; }
        public static byte GetPassedTestCount(int LocalLicenseApplicationID)
        {
            return clsTestsDataAccess.GetPassedTestCount(LocalLicenseApplicationID);
        }

    }

}