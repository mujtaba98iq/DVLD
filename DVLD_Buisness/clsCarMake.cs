using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLD_with_Car_Rental_DataAccessLayer;
namespace MakesBusinessLayer
{

public class clsMakes
{
 public MakesDTO MDTO
  {get { return (new MakesDTO(this.MakeID,this.Make)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int MakeID {get; set;}
public string Make {get; set;}


public clsMakes()
{
this.MakeID = default;
this.Make = default;


Mode = enMode.AddNew;

}

public clsMakes(MakesDTO MDTO, enMode cMode =enMode.AddNew) 
{
this.MakeID = MDTO.MakeID;
this.Make = MDTO.Make;


Mode = cMode;

}

private async Task<bool> _AddNewMakesAsync()
{
this.MakeID =await clsMakesDataAccess.AddMakesAsync(MDTO);
return (this.MakeID != -1);

}

private async Task<bool> _UpdateMakesAsync()
{
return await clsMakesDataAccess.UpdateMakesAsync(MDTO);
}

public static async Task<clsMakes> Find(int Id)
{
MakesDTO MDTO =await clsMakesDataAccess.GetMakesInfoByID(Id);

if(MDTO != null)
return new clsMakes(MDTO,enMode.Update);
else
return null;

}

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewMakesAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateMakesAsync();

            }




            return false;
        }

public static async Task<List<MakesDTO>> GetAllMakes(){return await clsMakesDataAccess.GetAllMakes();}

public static async Task<bool> DeleteMakes(int MakeID){return await clsMakesDataAccess.DeleteMakes(MakeID);}

public static async Task<bool> IsMakesExist(int MakeID){return await clsMakesDataAccess.IsMakesExist(MakeID);}


}

}