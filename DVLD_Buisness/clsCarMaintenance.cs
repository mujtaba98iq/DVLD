using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLD_with_Car_Rental_DataAccessLayer;
using VehiclesBusinessLayer;
namespace MaintenanceBusinessLayer
{

public class clsMaintenance
{
 public MaintenanceDTO MDTO
  {get { return (new MaintenanceDTO(this.MaintenanceID,this.VehicleID,this.Description,this.MaintenanceDate,this.Cost,this.CreatedByUserID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int MaintenanceID {get; set;}
public int VehicleID {get; set;}
public string Description {get; set;}
public DateTime MaintenanceDate {get; set;}
public decimal Cost {get; set;}
public int CreatedByUserID {get; set;}
public clsVehicles VehicleInfo {get; set;}


public clsMaintenance()
{
this.MaintenanceID = default;
this.VehicleID = default;
this.Description = default;
this.MaintenanceDate = default;
this.Cost = default;
this.CreatedByUserID = default;
            this.VehicleInfo = default;

Mode = enMode.AddNew;

}

public clsMaintenance(MaintenanceDTO MDTO, enMode cMode =enMode.AddNew) 
{
this.MaintenanceID = MDTO.MaintenanceID;
this.VehicleID = MDTO.VehicleID;
this.Description = MDTO.Description;
this.MaintenanceDate = MDTO.MaintenanceDate;
this.Cost = MDTO.Cost;
this.CreatedByUserID = MDTO.CreatedByUserID;
            this.VehicleInfo=clsVehicles.FindWithoutAsyinc(VehicleID);


Mode = cMode;

}

private async Task<bool> _AddNewMaintenanceAsync()
{
this.MaintenanceID =await clsMaintenanceDataAccess.AddMaintenanceAsync(MDTO);
return (this.MaintenanceID != -1);

}

private async Task<bool> _UpdateMaintenanceAsync()
{
return await clsMaintenanceDataAccess.UpdateMaintenanceAsync(MDTO);
}

public static async Task<clsMaintenance> Find(int Id)
{
MaintenanceDTO MDTO =await clsMaintenanceDataAccess.GetMaintenanceInfoByID(Id);

if(MDTO != null)
return new clsMaintenance(MDTO,enMode.Update);
else
return null;

}

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewMaintenanceAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateMaintenanceAsync();

            }




            return false;
        }

public static async Task<List<MaintenanceDTO>> GetAllMaintenance(){return await clsMaintenanceDataAccess.GetAllMaintenance();}
public static async Task<DataTable> GetAllMaintenanceWithDataTable(){return await clsMaintenanceDataAccess.GetAllMaintenanceWithDataTable();}

public static async Task<bool> DeleteMaintenance(int MaintenanceID){return await clsMaintenanceDataAccess.DeleteMaintenance(MaintenanceID);}

public static async Task<bool> IsMaintenanceExist(int MaintenanceID){return await clsMaintenanceDataAccess.IsMaintenanceExist(MaintenanceID);}


}

}