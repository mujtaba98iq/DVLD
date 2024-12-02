using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLD_with_Car_Rental_DataAccessLayer;
using PeopleBusinessLayer;
namespace CustomersBusinessLayer
{

public class clsCustomers
{
 public CustomersDTO CDTO
  {get { return (new CustomersDTO(this.CustomerID,this.PersonID, this.LicenseID, this.CreatedByUserID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int CustomerID {get; set;}
public int PersonID { get; set;}
public int LicenseID { get; set;}
public int CreatedByUserID {get; set;}
public clsPeople PersonInfo {get; set;}


public clsCustomers()
{
this.CustomerID = default;
this.PersonID = default;
this.LicenseID = default;
this.CreatedByUserID = default;
            this.PersonInfo = default;

Mode = enMode.AddNew;

}

public clsCustomers(CustomersDTO CDTO, enMode cMode =enMode.AddNew) 
{
this.CustomerID = CDTO.CustomerID;
this.PersonID = CDTO.PersonID;
this.LicenseID = CDTO.LicenseID;
this.CreatedByUserID = CDTO.CreatedByUserID;
            this.PersonInfo=clsPeople.Find(CDTO.PersonID);

Mode = cMode;

}

private async Task<bool> _AddNewCustomersAsync()
{
this.CustomerID =await clsCustomersDataAccess.AddCustomersAsync(CDTO);
return (this.CustomerID != -1);

}

private async Task<bool> _UpdateCustomersAsync()
{
return await clsCustomersDataAccess.UpdateCustomersAsync(CDTO);
}

public static async Task<clsCustomers> Find(int Id)
{
CustomersDTO CDTO =await clsCustomersDataAccess.GetCustomersInfoByID(Id);

if(CDTO != null)
return new clsCustomers(CDTO,enMode.Update);
else
return null;

}


        public static async Task<clsCustomers> FindByPersonID(int PersonID)
        {
            CustomersDTO CDTO = await clsCustomersDataAccess.GetCustomersInfoByPersonID(PersonID);

            if (CDTO != null)
                return new clsCustomers(CDTO, enMode.Update);
            else
                return null;

        }

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewCustomersAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateCustomersAsync();

            }




            return false;
        }

public static async Task<List<CustomersDTO>> GetAllCustomers(){return await clsCustomersDataAccess.GetAllCustomers();}
public static async Task<DataTable> GetAllCustomersWithDataTable(){return await clsCustomersDataAccess.GetAllCustomersWithDataTable();}

public static async Task<bool> DeleteCustomers(int CustomerID){return await clsCustomersDataAccess.DeleteCustomers(CustomerID);}

public static async Task<bool> IsCustomersExist(int CustomerID){return await clsCustomersDataAccess.IsCustomersExist(CustomerID);}


}

}