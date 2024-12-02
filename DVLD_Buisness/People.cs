using System;
using System.Data;
using System.Threading.Tasks;
using CountriesBusinessLayer;
using PeopleDataAccessLayer;
namespace PeopleBusinessLayer
{

    public class clsPeople
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }


        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int NationalityCountryID { get; set; }
        private string _ImagePath { get; set; }

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public clsCountries CountryInfo;

        public clsPeople()
        {
            this.PersonID = default;
            this.NationalNo = default;
            this.FirstName = default;
            this.SecondName = default;
            this.ThirdName = default;
            this.LastName = default;
            this.DateOfBirth = default;
            this.Gendor = default;
            this.Address = default;
            this.Email = default;
            this.Phone = default;
            this.NationalityCountryID = default;
            this.ImagePath = default;
            this.CountryInfo = default;


            Mode = enMode.AddNew;

        }

        private clsPeople(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gendor, string Address, string Email, string Phone, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Email = Email;
            this.Phone = Phone;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountries.Find(NationalityCountryID);


            Mode = enMode.Update;

        }

        private bool _AddNewclsPeople()
        {
            //call DataAccess Layer 

            this.PersonID = clsPeopleDataAccess.AddNewclsPeople(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Email, this.Phone, this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);

        }

        private bool _UpdateclsPeople()
        {
            //call DataAccess Layer 

            return clsPeopleDataAccess.UpdateclsPeople(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Email, this.Phone, this.NationalityCountryID, this.ImagePath);

        }

        public static clsPeople Find(int PersonID)
        {
            // تعريف القيم الافتراضية
            string nationalNo = default;
            string firstName = default;
            string secondName = default;
            string thirdName = default;
            string lastName = default;
            DateTime dateOfBirth = default;
            byte gender = default;
            string address = default;
            string email = default;
            string phone = default;
            int nationalityCountryID = default;
            string imagePath = default;

            // استدعاء الدالة لجلب البيانات
            bool isFound = clsPeopleDataAccess.GetclsPeopleInfoByID(
                PersonID,
                ref nationalNo,
                ref firstName,
                ref secondName,
                ref thirdName,
                ref lastName,
                ref dateOfBirth,
                ref gender,
                ref address,
                ref email,
                ref phone,
                ref nationalityCountryID,
                ref imagePath
            );

            // إذا تم العثور على البيانات، أنشئ كائن وأعده، وإلا أعد null
            return isFound
                ? new clsPeople(
                    PersonID,
                    nationalNo,
                    firstName,
                    secondName,
                    thirdName,
                    lastName,
                    dateOfBirth,
                    gender,
                    address,
                    email,
                    phone,
                    nationalityCountryID,
                    imagePath
                )
                : null;
        }

        public static clsPeople Find(string NationalNo)
        {
            int PersonID = default;
            string FirstName = default;
            string SecondName = default;
            string ThirdName = default;
            string LastName = default;
            DateTime DateOfBirth = default;
            byte Gendor = default;
            string Address = default;
            string Email = default;
            string Phone = default;
            int NationalityCountryID = default;
            string ImagePath = default;


            if (clsPeopleDataAccess.GetclsPeopleInfoByNationalNo(ref PersonID,  NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Email, ref Phone, ref NationalityCountryID, ref ImagePath))
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Email, Phone, NationalityCountryID, ImagePath);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewclsPeople())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateclsPeople();

            }




            return false;
        }

        public static   DataTable GetAllPeople() { return  clsPeopleDataAccess.GetAllPeople(); }

        public static bool DeleteclsPeople(int PersonID) { return clsPeopleDataAccess.DeleteclsPeople(PersonID); }

        public static bool isclsPeopleExist(int PersonID) { return clsPeopleDataAccess.IsclsPeopleExist(PersonID); }
        public static bool isclsPeopleExist(string NationalNo) { return clsPeopleDataAccess.IsclsPeopleExist(NationalNo); }


    }

}