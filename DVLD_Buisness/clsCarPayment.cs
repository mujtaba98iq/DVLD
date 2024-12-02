using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DVLD_with_Car_Rental_DataAccessLayer;
namespace PaymentsBusinessLayer
{

public class clsPayments
{
 public PaymentsDTO PDTO
  {get { return (new PaymentsDTO(this.PaymentID,this.ReservationID,this.ReturnID,this.PaymentDetails,this.PaidInitialTotalDueAmount,this.ActualTotalDueAmount,this.TotalRemaining,this.TotalRefundedAmount,this.TransactionDate,this.UpdatedTransactionDate,this.CreatedByUserID)); }}


        public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int PaymentID {get; set;}
public int ReservationID {get; set;}
public int? ReturnID {get; set;}
public string PaymentDetails {get; set;}
public decimal PaidInitialTotalDueAmount {get; set;}
public decimal? ActualTotalDueAmount {get; set;}
public decimal? TotalRemaining {get; set;}
public decimal? TotalRefundedAmount {get; set;}
public DateTime TransactionDate {get; set;}
public DateTime? UpdatedTransactionDate {get; set;}

public int CreatedByUserID {get; set;}


public clsPayments()
{
this.PaymentID = default;
this.ReservationID = default;
this.ReturnID = default;
this.PaymentDetails = default;
this.PaidInitialTotalDueAmount = default;
this.ActualTotalDueAmount = default;
this.TotalRemaining = default;
this.TotalRefundedAmount = default;
this.TransactionDate = default;
this.UpdatedTransactionDate = default;
this.CreatedByUserID = default;


Mode = enMode.AddNew;

}

public clsPayments(PaymentsDTO PDTO, enMode cMode =enMode.AddNew) 
{
this.PaymentID = PDTO.PaymentID;
this.ReservationID = PDTO.ReservationID;
this.ReturnID = PDTO.ReturnID;
this.PaymentDetails = PDTO.PaymentDetails;
this.PaidInitialTotalDueAmount = PDTO.PaidInitialTotalDueAmount;
this.ActualTotalDueAmount = PDTO.ActualTotalDueAmount;
this.TotalRemaining = PDTO.TotalRemaining;
this.TotalRefundedAmount = PDTO.TotalRefundedAmount;
this.TransactionDate = PDTO.TransactionDate;
this.UpdatedTransactionDate = PDTO.UpdatedTransactionDate;
this.CreatedByUserID = PDTO.CreatedByUserID;


Mode = cMode;

}

private async Task<bool> _AddNewPaymentsAsync()
{
this.PaymentID =await clsPaymentsDataAccess.AddPaymentsAsync(PDTO);
return (this.PaymentID != -1);

}

private async Task<bool> _UpdatePaymentsAsync()
{
return await clsPaymentsDataAccess.UpdatePaymentsAsync(PDTO);
}

     static public async Task<bool> ReturnCar(ReturnCarDTO RCDTO)
        {
            return await clsPaymentsDataAccess.ReturnCar(RCDTO);
        }

        public static clsPayments Find(int Id)
{
PaymentsDTO PDTO = clsPaymentsDataAccess.GetPaymentsInfoByID(Id);

if(PDTO != null)
return new clsPayments(PDTO,enMode.Update);
else
return null;

}
        public static clsPayments FindByReservationID(int ReservationID)
        {
            PaymentsDTO PDTO =  clsPaymentsDataAccess.GetPaymentsInfoByReservationID(ReservationID);

            if (PDTO != null)
                return new clsPayments(PDTO, enMode.Update);
            else
                return null;

        }

        public async Task<bool> Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewPaymentsAsync())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return await _UpdatePaymentsAsync();

            }




            return false;
        }

public static async Task<List<PaymentsDTO>> GetAllPayments(){return await clsPaymentsDataAccess.GetAllPayments();}

public static async Task<bool> DeletePayments(int PaymentID){return await clsPaymentsDataAccess.DeletePayments(PaymentID);}

public static async Task<bool> IsPaymentsExist(int PaymentID){return await clsPaymentsDataAccess.IsPaymentsExist(PaymentID);}
public async Task<bool> DebtPayment(){return await clsPaymentsDataAccess.DebtAllPayment(this.PaymentID);}


}

}