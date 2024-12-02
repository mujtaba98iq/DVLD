using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLD_with_Car_Rental_DataAccessLayer;
namespace VehicleReturnsBusinessLayer
{

public class clsVehicleReturns
{
 public VehicleReturnsDTO VDTO
  {get { return (new VehicleReturnsDTO(this.ReturenID,this.ActualReturnDate,this.ActualRentalDays,this.Mileage,this.ConsumedMilaeage,this.FinalCheckNotes,this.AdditionalCharges,this.ActualTotalDueAmount,this.CreatedByUserID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int ReturenID {get; set;}
public DateTime ActualReturnDate {get; set;}
public byte ActualRentalDays {get; set;}
public short Mileage {get; set;}
public short ConsumedMilaeage {get; set;}
public string FinalCheckNotes {get; set;}
public decimal AdditionalCharges {get; set;}
public decimal ActualTotalDueAmount {get; set;}
public int CreatedByUserID {get; set;}


public clsVehicleReturns()
{
this.ReturenID = default;
this.ActualReturnDate = default;
this.ActualRentalDays = default;
this.Mileage = default;
this.ConsumedMilaeage = default;
this.FinalCheckNotes = default;
this.AdditionalCharges = default;
this.ActualTotalDueAmount = default;
this.CreatedByUserID = default;


Mode = enMode.AddNew;

}

public clsVehicleReturns(VehicleReturnsDTO VDTO, enMode cMode =enMode.AddNew) 
{
this.ReturenID = VDTO.ReturenID;
this.ActualReturnDate = VDTO.ActualReturnDate;
this.ActualRentalDays = VDTO.ActualRentalDays;
this.Mileage = VDTO.Mileage;
this.ConsumedMilaeage = VDTO.ConsumedMilaeage;
this.FinalCheckNotes = VDTO.FinalCheckNotes;
this.AdditionalCharges = VDTO.AdditionalCharges;
this.ActualTotalDueAmount = VDTO.ActualTotalDueAmount;
this.CreatedByUserID = VDTO.CreatedByUserID;


Mode = cMode;

}

private async Task<bool> _AddNewVehicleReturnsAsync()
{
this.ReturenID =await clsVehicleReturnsDataAccess.AddVehicleReturnsAsync(VDTO);
return (this.ReturenID != -1);

}

private async Task<bool> _UpdateVehicleReturnsAsync()
{
return await clsVehicleReturnsDataAccess.UpdateVehicleReturnsAsync(VDTO);
}

public static async Task<clsVehicleReturns> Find(int Id)
{
VehicleReturnsDTO VDTO =await clsVehicleReturnsDataAccess.GetVehicleReturnsInfoByID(Id);

if(VDTO != null)
return new clsVehicleReturns(VDTO,enMode.Update);
else
return null;

}

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewVehicleReturnsAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateVehicleReturnsAsync();

            }




            return false;
        }

public static async Task<List<VehicleReturnsDTO>> GetAllVehicleReturns(){return await clsVehicleReturnsDataAccess.GetAllVehicleReturns();}
public static async Task<DataTable> GetAllVehicleReturnsWithDataTable(){return await clsVehicleReturnsDataAccess.GetAllVehicleReturnsWithDataTable();}

public static async Task<bool> DeleteVehicleReturns(int ReturenID){return await clsVehicleReturnsDataAccess.DeleteVehicleReturns(ReturenID);}

public static async Task<bool> IsVehicleReturnsExist(int ReturenID){return await clsVehicleReturnsDataAccess.IsVehicleReturnsExist(ReturenID);}


}

}