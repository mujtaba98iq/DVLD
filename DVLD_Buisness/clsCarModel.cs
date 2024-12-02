using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLD_with_Car_Rental_DataAccessLayer;
namespace MakeModelsBusinessLayer
{

public class clsMakeModels
{
 public MakeModelsDTO MDTO
  {get { return (new MakeModelsDTO(this.ModelID,this.MakeID,this.ModelName)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int ModelID {get; set;}
public int MakeID {get; set;}
public string ModelName {get; set;}


public clsMakeModels()
{
this.ModelID = default;
this.MakeID = default;
this.ModelName = default;


Mode = enMode.AddNew;

}

public clsMakeModels(MakeModelsDTO MDTO, enMode cMode =enMode.AddNew) 
{
this.ModelID = MDTO.ModelID;
this.MakeID = MDTO.MakeID;
this.ModelName = MDTO.ModelName;


Mode = cMode;

}

private async Task<bool> _AddNewMakeModelsAsync()
{
this.ModelID =await clsMakeModelsDataAccess.AddMakeModelsAsync(MDTO);
return (this.ModelID != -1);

}

private async Task<bool> _UpdateMakeModelsAsync()
{
return await clsMakeModelsDataAccess.UpdateMakeModelsAsync(MDTO);
}

public static async Task<clsMakeModels> Find(int Id)
{
MakeModelsDTO MDTO =await clsMakeModelsDataAccess.GetMakeModelsInfoByID(Id);

if(MDTO != null)
return new clsMakeModels(MDTO,enMode.Update);
else
return null;

}

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewMakeModelsAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateMakeModelsAsync();

            }




            return false;
        }

public static async Task<List<MakeModelsDTO>> GetAllMakeModels(){return await clsMakeModelsDataAccess.GetAllMakeModels();}

public static async Task<bool> DeleteMakeModels(int ModelID){return await clsMakeModelsDataAccess.DeleteMakeModels(ModelID);}

public static async Task<bool> IsMakeModelsExist(int ModelID){return await clsMakeModelsDataAccess.IsMakeModelsExist(ModelID);}


}

}