using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLD_with_Car_Rental_DataAccessLayer;
using DVLD_with_CarP_Rental_DataAccessLayer;
namespace VehicleCategoriesBusinessLayer
{

public class clsVehicleCategories
{
 public VehicleCategoriesDTO VDTO
  {get { return (new VehicleCategoriesDTO(this.CategoryID,this.CategoryName)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int CategoryID {get; set;}
public string CategoryName {get; set;}


public clsVehicleCategories()
{
this.CategoryID = default;
this.CategoryName = default;


Mode = enMode.AddNew;

}

public clsVehicleCategories(VehicleCategoriesDTO VDTO, enMode cMode =enMode.AddNew) 
{
this.CategoryID = VDTO.CategoryID;
this.CategoryName = VDTO.CategoryName;


Mode = cMode;

}

private async Task<bool> _AddNewVehicleCategoriesAsync()
{
this.CategoryID =await clsVehicleCategoriesDataAccess.AddVehicleCategoriesAsync(VDTO);
return (this.CategoryID != -1);

}

private async Task<bool> _UpdateVehicleCategoriesAsync()
{
return await clsVehicleCategoriesDataAccess.UpdateVehicleCategoriesAsync(VDTO);
}

public static  clsVehicleCategories Find(int Id)
{
VehicleCategoriesDTO VDTO = clsVehicleCategoriesDataAccess.GetVehicleCategoriesInfoByID(Id);

if(VDTO != null)
return new clsVehicleCategories(VDTO,enMode.Update);
else
return null;

}

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewVehicleCategoriesAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateVehicleCategoriesAsync();

            }




            return false;
        }

public static async  Task<List<VehicleCategoriesDTO>> GetAllVehicleCategories(){return await clsVehicleCategoriesDataAccess.GetAllVehicleCategoriesAsync();}

public static async Task<bool> DeleteVehicleCategories(int CategoryID){return await clsVehicleCategoriesDataAccess.DeleteVehicleCategories(CategoryID);}

public static async Task<bool> IsVehicleCategoriesExist(int CategoryID){return await clsVehicleCategoriesDataAccess.IsVehicleCategoriesExist(CategoryID);}


}

}