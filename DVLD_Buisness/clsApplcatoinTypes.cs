using System;
using System.Data;
using ApplcatoinTypesDataAccessLayer;
namespace ApplcatoinTypesBusinessLayer
{

    public class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicatoinFees { get; set; }


        public clsApplicationTypes()
        {
            this.ApplicationTypeID = default;
            this.ApplicationTypeTitle = default;
            this.ApplicatoinFees = default;


            Mode = enMode.AddNew;

        }

        private clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicatoinFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicatoinFees = ApplicatoinFees;


            Mode = enMode.Update;

        }

        private bool _AddNewApplicationTypes()
        {
            //call DataAccess Layer 

            this.ApplicationTypeID = clsApplcatoinTypesDataAccess.AddNewApplicationTypes(this.ApplicationTypeTitle, this.ApplicatoinFees);

            return (this.ApplicationTypeID != -1);

        }

        private bool _UpdateApplicationTypes()
        {
            //call DataAccess Layer 

            return clsApplcatoinTypesDataAccess.UpdateApplicationTypes(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicatoinFees);

        }

        public static clsApplicationTypes Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = default;
            decimal ApplicatoinFees = default;


            if (clsApplcatoinTypesDataAccess.GetApplicationTypesInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicatoinFees))
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicatoinFees);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationTypes())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationTypes();

            }




            return false;
        }

        public static DataTable GetAllApplcatoinTypes() { return clsApplcatoinTypesDataAccess.GetAllApplcatoinTypes(); }

        public static bool DeleteApplicationTypes(int ApplicationTypeID) { return clsApplcatoinTypesDataAccess.DeleteApplicationTypes(ApplicationTypeID); }

        public static bool isApplicationTypesExist(int ApplicationTypeID) { return clsApplcatoinTypesDataAccess.IsApplicationTypesExist(ApplicationTypeID); }


    }

}