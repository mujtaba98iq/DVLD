using System;
using System.Data;
using LincnseClassesDataAccessLayer;
namespace LincnseClassesBusinessLayer
{

    public class clsLicensesClasses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }


        public clsLicensesClasses()
        {
            this.LicenseClassID = default;
            this.ClassName = default;
            this.ClassDescription = default;
            this.MinimumAllowedAge = default;
            this.DefaultValidityLength = default;
            this.ClassFees = default;


            Mode = enMode.AddNew;

        }

        private clsLicensesClasses(int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;


            Mode = enMode.Update;

        }

        private bool _AddNewLicensesClasses()
        {
            //call DataAccess Layer 

            this.LicenseClassID = clsLincnseClassesDataAccess.AddNewLicensesClasses(this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);

            return (this.LicenseClassID != -1);

        }

        private bool _UpdateLicensesClasses()
        {
            //call DataAccess Layer 

            return clsLincnseClassesDataAccess.UpdateLicensesClasses(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);

        }

        public static clsLicensesClasses Find(int LicenseClassID)
        {
            string ClassName = default;
            string ClassDescription = default;
            byte MinimumAllowedAge = default;
            byte DefaultValidityLength = default;
            decimal ClassFees = default;


            if (clsLincnseClassesDataAccess.GetLicensesClassesInfoByID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicensesClasses(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;

        }
        public static clsLicensesClasses Find(string ClassName)
        {
            int LicenseClassID = default;
            string ClassDescription = default;
            byte MinimumAllowedAge = default;
            byte DefaultValidityLength = default;
            decimal ClassFees = default;


            if (clsLincnseClassesDataAccess.GetLicensesClassesInfoByLicenseName(ref LicenseClassID,  ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new clsLicensesClasses(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicensesClasses())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicensesClasses();

            }




            return false;
        }

        public static DataTable GetAllLincnseClasses() { return clsLincnseClassesDataAccess.GetAllLincnseClasses(); }

        public static bool DeleteLicensesClasses(int LicenseClassID) { return clsLincnseClassesDataAccess.DeleteLicensesClasses(LicenseClassID); }

        public static bool isLicensesClassesExist(int LicenseClassID) { return clsLincnseClassesDataAccess.IsLicensesClassesExist(LicenseClassID); }


    }

}