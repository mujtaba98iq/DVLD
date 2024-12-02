using System;
using System.Data;
using CountriesDataAccessLayer;
namespace CountriesBusinessLayer
{

    public class clsCountries
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int CountryID { get; set; }
        public string CountryName { get; set; }


        public clsCountries()
        {
            this.CountryID = default;
            this.CountryName = default;


            Mode = enMode.AddNew;

        }

        private clsCountries(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;


            Mode = enMode.Update;

        }

        private bool _AddNewCountries()
        {
            //call DataAccess Layer 

            this.CountryID = clsCountriesDataAccess.AddNewCountries(this.CountryName);

            return (this.CountryID != -1);

        }

        private bool _UpdateCountries()
        {
            //call DataAccess Layer 

            return clsCountriesDataAccess.UpdateCountries(this.CountryID, this.CountryName);

        }

        public static clsCountries Find(int CountryID)
        {
            string CountryName = default;


            if (clsCountriesDataAccess.GetCountriesInfoByID(CountryID, ref CountryName))
                return new clsCountries(CountryID, CountryName);
            else
                return null;

        }
        public static clsCountries Find(string CountryName)
        {
            int CountryID= default;


            if (clsCountriesDataAccess.GetCountriesInfoByName(ref CountryID,  CountryName))
                return new clsCountries(CountryID, CountryName);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountries())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCountries();

            }




            return false;
        }

        public static DataTable GetAllCountries() { return clsCountriesDataAccess.GetAllCountries(); }

        public static bool DeleteCountries(int CountryID) { return clsCountriesDataAccess.DeleteCountries(CountryID); }

        public static bool isCountriesExist(int CountryID) { return clsCountriesDataAccess.IsCountriesExist(CountryID); }


    }

}