using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLDWithCarRentalDataAccessLayer;
namespace VehicleCopiesBusinessLayer
{

    public class clsVehicleCopies
    {
        public VehicleCopiesDTO VDTO
        { get { return (new VehicleCopiesDTO(this.CopyID, this.VehicleID, this.AvailabilityStatus, this.CreateByUserID)); } }
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int CopyID { get; set; }
        public int VehicleID { get; set; }
        public bool AvailabilityStatus { get; set; }
        public int CreateByUserID { get; set; }


        public clsVehicleCopies()
        {
            this.CopyID = default;
            this.VehicleID = default;
            this.AvailabilityStatus = default;
            this.CreateByUserID = default;


            Mode = enMode.AddNew;

        }

        public clsVehicleCopies(VehicleCopiesDTO VDTO, enMode cMode = enMode.AddNew)
        {
            this.CopyID = VDTO.CopyID;
            this.VehicleID = VDTO.VehicleID;
            this.AvailabilityStatus = VDTO.AvailabilityStatus;
            this.CreateByUserID = VDTO.CreateByUserID;


            Mode = cMode;

        }

        private async Task<bool> _AddNewVehicleCopiesAsync()
        {
            this.CopyID = await clsVehicleCopiesDataAccess.AddVehicleCopiesAsync(VDTO);
            return (this.CopyID != -1);

        }

        private async Task<bool> _UpdateVehicleCopiesAsync()
        {
            return await clsVehicleCopiesDataAccess.UpdateVehicleCopiesAsync(VDTO);
        }

        public static async Task<clsVehicleCopies> Find(int Id)
        {
            VehicleCopiesDTO VDTO = await clsVehicleCopiesDataAccess.GetVehicleCopiesInfoByID(Id);

            if (VDTO != null)
                return new clsVehicleCopies(VDTO, enMode.Update);
            else
                return null;

        }

        public async Task<bool> Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewVehicleCopiesAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateVehicleCopiesAsync();

            }




            return false;
        }

        public static async Task<List<VehicleCopiesDTO>> GetAllVehicleCopies() { return await clsVehicleCopiesDataAccess.GetAllVehicleCopies(); }

        public static async Task<bool> DeleteVehicleCopies(int CopyID) { return await clsVehicleCopiesDataAccess.DeleteVehicleCopies(CopyID); }

        public static async Task<bool> IsVehicleCopiesExist(int CopyID) { return await clsVehicleCopiesDataAccess.IsVehicleCopiesExist(CopyID); }
        public static bool IsVehicleAvalbel(int VehicleID) { return clsVehicleCopiesDataAccess.IsVehicleAvalbel(VehicleID); }


    }

}