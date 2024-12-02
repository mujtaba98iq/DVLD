using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLD_with_Car_Rental_DataAccessLayer;
using FuleTypesBusinessLayer;
using VehicleCategoriesBusinessLayer;
using VehicleCopiesBusinessLayer;
namespace VehiclesBusinessLayer
{

public class clsVehicles
{
 public VehiclesDTO VDTO
  {get { return (new VehiclesDTO(this.VehicleID,this.MakeID,this.ModelID,this.Year,this.Mileage,this.FuelTypeID,this.PlateNumber,this.CarCategoryID,this.RentalPricePerDay,this.CreatedByUserID,this.VehicleName,this.VehicleImage)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int VehicleID {get; set;}
public int MakeID {get; set;}
public int ModelID {get; set;}
public int Year {get; set;}
public int Mileage {get; set;}
public int FuelTypeID {get; set;}
public string PlateNumber {get; set;}
public int CarCategoryID {get; set;}
public decimal RentalPricePerDay {get; set;}
public int CreatedByUserID {get; set;}
public string VehicleName {get; set;}
public string VehicleImage {get; set;}
        public bool IsAvailabe {get;set; }
      public  clsVehicleCategories CategorieInfo {get; set;}
        public clsFuleTypes fuleTypeInfo {get; set;}


public clsVehicles()
{
this.VehicleID = default;
this.MakeID = default;
this.ModelID = default;
this.Year = default;
this.Mileage = default;
this.FuelTypeID = default;
this.PlateNumber = default;
this.CarCategoryID = default;
this.RentalPricePerDay = default;
this.CreatedByUserID = default;
this.VehicleName = default;
this.VehicleImage = default;
            this.IsAvailabe = default;
            this.CategorieInfo = default;
            this.fuleTypeInfo = default;


Mode = enMode.AddNew;

}

public   clsVehicles(VehiclesDTO VDTO, enMode cMode =enMode.AddNew) 
{
this.VehicleID = VDTO.VehicleID;
this.MakeID = VDTO.MakeID;
this.ModelID = VDTO.ModelID;
this.Year = VDTO.Year;
this.Mileage = VDTO.Mileage;
this.FuelTypeID = VDTO.FuelTypeID;
this.PlateNumber = VDTO.PlateNumber;
this.CarCategoryID = VDTO.CarCategoryID;
this.RentalPricePerDay = VDTO.RentalPricePerDay;
this.CreatedByUserID = VDTO.CreatedByUserID;
this.VehicleName = VDTO.VehicleName;
this.VehicleImage = VDTO.VehicleImage;
this.IsAvailabe =  clsVehicleCopies.IsVehicleAvalbel(VehicleID);
            this.CategorieInfo = clsVehicleCategories.Find(CarCategoryID);
            this.fuleTypeInfo = clsFuleTypes.Find(FuelTypeID);
            Mode = cMode;

}

private async Task<bool> _AddNewVehiclesAsync()
{
this.VehicleID =await clsVehiclesDataAccess.AddVehicleAsync(VDTO);
return (this.VehicleID != -1);

}

private async Task<bool> _UpdateVehiclesAsync()
{
return await clsVehiclesDataAccess.UpdateVehiclesAsync(VDTO);
}
        public static clsVehicles FindWithOutAsync(int Id)
        {
            VehiclesDTO VDTO =  clsVehiclesDataAccess.GetVehiclesInfoByIDWithOutAsync(Id);

            if (VDTO != null)
                return new clsVehicles(VDTO, enMode.Update);
            else
                return null;

        }

        public static async Task<clsVehicles> Find(int Id)
{
VehiclesDTO VDTO =await clsVehiclesDataAccess.GetVehiclesInfoByID(Id);

if(VDTO != null)
return new clsVehicles(VDTO,enMode.Update);
else
return null;

}
        public static  clsVehicles FindWithoutAsyinc(int Id)
        {
            VehiclesDTO VDTO =  clsVehiclesDataAccess.GetVehiclesInfoByIDWithOutAsync(Id);

            if (VDTO != null)
                return new clsVehicles(VDTO, enMode.Update);
            else
                return null;

        }

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewVehiclesAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateVehiclesAsync();

            }




            return false;
        }

public static async Task<List<VehiclesDTO>> GetAllVehicles(){return await clsVehiclesDataAccess.GetAllVehicles();}
public static async Task<DataTable> GetAllVehiclesCarCategory(){return await clsVehiclesDataAccess.GetAllVehiclesCarCategory();}
public static async Task<DataTable> GetAllVehiclesFuleTypes(){return await clsVehiclesDataAccess.GetAllVehiclesFuleTypes();}
public static async Task<DataTable> GetAllVehiclesMakes(){return await clsVehiclesDataAccess.GetAllVehiclesMakes();}
public static async Task<clsComboBoxInfo> GetComboBoxInfoForVehicleAsync(clsComboBoxInfo comboBoxParameters) {return await clsVehiclesDataAccess.GetComboBoxInfoForVehicleAsync(comboBoxParameters);}

public static async Task<bool> DeleteVehicles(int VehicleID){return await clsVehiclesDataAccess.DeleteVehicles(VehicleID);}

public static async Task<bool> IsVehiclesExist(int VehicleID){return await clsVehiclesDataAccess.IsVehiclesExist(VehicleID);}


}

}