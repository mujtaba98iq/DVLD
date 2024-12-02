using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLD_with_Car_Rental_DataAccessLayer;
namespace FuleTypesBusinessLayer
{

public class clsFuleTypes
{
 public FuleTypesDTO FDTO
  {get { return (new FuleTypesDTO(this.FuleTypeID,this.FuleType)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int FuleTypeID {get; set;}
public string FuleType {get; set;}


public clsFuleTypes()
{
this.FuleTypeID = default;
this.FuleType = default;


Mode = enMode.AddNew;

}

public clsFuleTypes(FuleTypesDTO FDTO, enMode cMode =enMode.AddNew) 
{
this.FuleTypeID = FDTO.FuleTypeID;
this.FuleType = FDTO.FuleType;


Mode = cMode;

}

private async Task<bool> _AddNewFuleTypesAsync()
{
this.FuleTypeID =await clsFuleTypesDataAccess.AddFuleTypesAsync(FDTO);
return (this.FuleTypeID != -1);

}

private async Task<bool> _UpdateFuleTypesAsync()
{
return await clsFuleTypesDataAccess.UpdateFuleTypesAsync(FDTO);
}

public static clsFuleTypes Find(int Id)
{
FuleTypesDTO FDTO = clsFuleTypesDataAccess.GetFuleTypesInfoByID(Id);

if(FDTO != null)
return new clsFuleTypes(FDTO,enMode.Update);
else
return null;

}

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewFuleTypesAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateFuleTypesAsync();

            }




            return false;
        }

public static async Task<List<FuleTypesDTO>> GetAllFuleTypes(){return await clsFuleTypesDataAccess.GetAllFuleTypes();}

public static async Task<bool> DeleteFuleTypes(int FuleTypeID){return await clsFuleTypesDataAccess.DeleteFuleTypes(FuleTypeID);}

public static async Task<bool> IsFuleTypesExist(int FuleTypeID){return await clsFuleTypesDataAccess.IsFuleTypesExist(FuleTypeID);}


}

}