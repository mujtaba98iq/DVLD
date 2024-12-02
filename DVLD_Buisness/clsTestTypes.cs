using System;
using System.Data;
using TestTypesDataAccessLayer;
namespace TestTypesBusinessLayer
{

    public class clsTestTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestFees { get; set; }

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestTypes()
        {
            this.TestTypeID = default;
            this.TestTypeTitle = default;
            this.TestTypeDescription = default;
            this.TestFees = default;


            Mode = enMode.AddNew;

        }

        private clsTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestFees = TestFees;


            Mode = enMode.Update;

        }

        private bool _AddNewTestTypes()
        {
            //call DataAccess Layer 

            this.TestTypeID = clsTestTypesDataAccess.AddNewTestTypes(this.TestTypeTitle, this.TestTypeDescription, this.TestFees);

            return (this.TestTypeID != -1);

        }

        private bool _UpdateTestTypes()
        {
            //call DataAccess Layer 

            return clsTestTypesDataAccess.UpdateTestTypes(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestFees);

        }

        public static clsTestTypes Find(clsTestTypes.enTestType TestTypeID)
        {
            string TestTypeTitle = default;
            string TestTypeDescription = default;
            decimal TestFees = default;


            if (clsTestTypesDataAccess.GetTestTypesInfoByID((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestFees))
                return new clsTestTypes((int)TestTypeID, TestTypeTitle, TestTypeDescription, TestFees);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestTypes())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestTypes();

            }




            return false;
        }

        public static DataTable GetAllTestTypes() { return clsTestTypesDataAccess.GetAllTestTypes(); }

        public static bool DeleteTestTypes(int TestTypeID) { return clsTestTypesDataAccess.DeleteTestTypes(TestTypeID); }

        public static bool isTestTypesExist(int TestTypeID) { return clsTestTypesDataAccess.IsTestTypesExist(TestTypeID); }


    }

}