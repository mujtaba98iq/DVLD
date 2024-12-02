using DVLD_with_Car_Rental_DataAccessLayer;
using PaymentsBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VehiclesBusinessLayer;
namespace ReservationsBusinessLayer
{

public class clsReservations
{
 public ReservationsDTO RDTOForAdd
  {get { return (new ReservationsDTO(this.BookingID,this.CustomerID,this.VehicleID,this.RentalStartDate,this.RentalEndDate,this.PickupLocation,this.DropoffLocation, this.InitialRentalDays,this.RentalPricePerDay,this.InitialTotalDueAmount, this.InitialCheckNotes,this.CreatedByUserID,this.paymentInfoDTO)); }}

        public ReservationsDTO RDTOForUpdate
        { get { return (new ReservationsDTO(this.BookingID, this.CustomerID, this.VehicleID, this.RentalStartDate, this.RentalEndDate, this.PickupLocation, this.DropoffLocation, this.InitialRentalDays, this.RentalPricePerDay, this.InitialTotalDueAmount, this.InitialCheckNotes, this.CreatedByUserID)); } }

        public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int BookingID {get; set;}
public int CustomerID {get; set;}
public int VehicleID {get; set;}
public DateTime RentalStartDate {get; set;}
public DateTime RentalEndDate {get; set;}
public string PickupLocation {get; set;}
public string DropoffLocation {get; set;}
public byte InitialRentalDays {get; set;}
public decimal RentalPricePerDay {get; set;}
public decimal InitialTotalDueAmount {get; set;}
public string InitialCheckNotes {get; set;}
public int CreatedByUserID {get; set;}
        public PaymentInfoDTO paymentInfoDTO {get; set;}
        public clsVehicles VehicleInfo { get; set;}
        public clsPayments PaymentInfo { get; set;}

public clsReservations()
{
this.BookingID = default;
this.CustomerID = default;
this.VehicleID = default;
this.RentalStartDate = default;
this.RentalEndDate = default;
this.PickupLocation = default;
this.DropoffLocation = default;
this.InitialRentalDays = default;
this.RentalPricePerDay = default;
this.InitialTotalDueAmount = default;
this.InitialCheckNotes = default;
this.CreatedByUserID = default;
            this.VehicleInfo = default;
            this.PaymentInfo = default;
            this.paymentInfoDTO =default;

Mode = enMode.AddNew;

}

public clsReservations(ReservationsDTO RDTO, enMode cMode =enMode.AddNew) 
{
this.BookingID = RDTO.BookingID;
this.CustomerID = RDTO.CustomerID;
this.VehicleID = RDTO.VehicleID;
this.RentalStartDate = RDTO.RentalStartDate;
this.RentalEndDate = RDTO.RentalEndDate;
this.PickupLocation = RDTO.PickupLocation;
this.DropoffLocation = RDTO.DropoffLocation;
this.InitialCheckNotes = RDTO.InitialCheckNotes;
            this.RentalPricePerDay= RDTO.RentalPricePerDay;
            this.InitialRentalDays= RDTO.InitialRentalDays;
            this.InitialTotalDueAmount= RDTO.InitialTotalDueAmount;
this.CreatedByUserID = RDTO.CreatedByUserID;
            this.VehicleInfo =  clsVehicles.FindWithOutAsync(VehicleID);
            this.PaymentInfo = clsPayments.FindByReservationID(BookingID);


Mode = cMode;

}

private async Task<bool> _AddNewReservationsAsync()
{
this.BookingID =await clsReservationsDataAccess.AddReservationsAsync(RDTOForAdd);
return (this.BookingID != -1);

}

private async Task<bool> _UpdateReservationsAsync()
{
          
return await clsReservationsDataAccess.UpdateReservationsAsync(RDTOForUpdate);
}

public static async Task<clsReservations> Find(int Id)
{
ReservationsDTO RDTO =await clsReservationsDataAccess.GetReservationsInfoByID(Id);

if(RDTO != null)
return new clsReservations(RDTO,enMode.Update);
else
return null;

}

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewReservationsAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdateReservationsAsync();

            }




            return false;
        }

public static async Task<List<ReservationsDTO>> GetAllReservations(){return await clsReservationsDataAccess.GetAllReservations();}
public static async Task<DataTable> GetAllReservationsWithDataTable(){return await clsReservationsDataAccess.GetAllReservationsWithDataTable();}

public static async Task<bool> DeleteReservations(int BookingID){return await clsReservationsDataAccess.DeleteReservations(BookingID);}

public static async Task<bool> IsReservationsExist(int BookingID){return await clsReservationsDataAccess.IsReservationsExist(BookingID);}
public static async Task<bool> IsVehicleReservedByUser(int VehicleID, int UserID){return await clsReservationsDataAccess.IsVehicleReservedByUser(VehicleID,UserID);}


}

}