using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data.SqlClient;

namespace DVLD_with_Car_Rental_DataAccessLayer
{
public class PaymentsDTO
{
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


public PaymentsDTO ( int PaymentID,  int ReservationID,  int? ReturnID,  string PaymentDetails,  decimal PaidInitialTotalDueAmount,  decimal? ActualTotalDueAmount,  decimal? TotalRemaining,  decimal? TotalRefundedAmount,  DateTime TransactionDate,  DateTime? UpdatedTransactionDate,  int CreatedByUserID)
{
this.PaymentID = PaymentID;
this.ReservationID = ReservationID;
this.ReturnID = ReturnID;
this.PaymentDetails = PaymentDetails;
this.PaidInitialTotalDueAmount = PaidInitialTotalDueAmount;
this.ActualTotalDueAmount = ActualTotalDueAmount;
this.TotalRemaining = TotalRemaining;
this.TotalRefundedAmount = TotalRefundedAmount;
this.TransactionDate = TransactionDate;
this.UpdatedTransactionDate = UpdatedTransactionDate;
this.CreatedByUserID = CreatedByUserID;


}


}


    public class ReturnCarDTO
    {
        public int PaymentID { get; set; }
        public int ReservationID { get; set; }
        public string PaymentDetails { get; set; }
        public decimal PaidInitialTotalDueAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CreatedByUserID { get; set; }
        public string FinalCheckNotes { get; set; }
        public decimal AdditionalCharges { get; set; }
        public int Mileage { get; set; }



        public ReturnCarDTO()
        {
           
        }
        public ReturnCarDTO(int PaymentID, int ReservationID, string PaymentDetails, decimal PaidInitialTotalDueAmount,  int CreatedByUserID, string FinalCheckNotes, decimal AdditionalCharges, int Mileage)
        {
            this.PaymentID = PaymentID;
            this.ReservationID = ReservationID;
            this.PaymentDetails = PaymentDetails;
            this.PaidInitialTotalDueAmount = PaidInitialTotalDueAmount;
            this.TransactionDate = TransactionDate;
            this.CreatedByUserID = CreatedByUserID;
            this.FinalCheckNotes = FinalCheckNotes;
            this.AdditionalCharges = AdditionalCharges;
            this.Mileage = Mileage;
        }


    }

    public static class clsPaymentsDataAccess

{
        public static PaymentsDTO GetPaymentsInfoByID(int PaymentID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetPaymentsByID]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentID", PaymentID);
                         connection.Open();
                        using (SqlDataReader reader =  command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new PaymentsDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("PaymentID")),
                                    reader.GetInt32(reader.GetOrdinal("BookingID")),
                                    reader.IsDBNull(reader.GetOrdinal("ReturnID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ReturnID")),
                                    reader.GetString(reader.GetOrdinal("PaymentDetails")),
                                    reader.GetDecimal(reader.GetOrdinal("PaidInitialTotalDueAmount")),
                                    reader.IsDBNull(reader.GetOrdinal("ActualTotalDueAmount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("ActualTotalDueAmount")),
                                    reader.IsDBNull(reader.GetOrdinal("TotalRemaining")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalRemaining")),
                                    reader.IsDBNull(reader.GetOrdinal("TotalRefundedAmount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalRefundedAmount")),
                                    reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                                    reader.IsDBNull(reader.GetOrdinal("UpdatedTransactionDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedTransactionDate")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                                );
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }
            return null;
        }

        public static PaymentsDTO GetPaymentsInfoByReservationID(int ReservationID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetPaymentsByReservationID]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@BookingID", SqlDbType.Int) { Value = ReservationID });

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new PaymentsDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("PaymentID")),
                                    reader.GetInt32(reader.GetOrdinal("BookingID")),
                                    reader.IsDBNull(reader.GetOrdinal("ReturnID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ReturnID")),
                                    reader.GetString(reader.GetOrdinal("PaymentDetails")),
                                    reader.GetDecimal(reader.GetOrdinal("PaidInitialTotalDueAmount")),
                                    reader.IsDBNull(reader.GetOrdinal("ActualTotalDueAmount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("ActualTotalDueAmount")),
                                    reader.IsDBNull(reader.GetOrdinal("TotalRemaining")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalRemaining")),
                                    reader.IsDBNull(reader.GetOrdinal("TotalRefundedAmount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalRefundedAmount")),
                                    reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                                    reader.IsDBNull(reader.GetOrdinal("UpdatedTransactionDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedTransactionDate")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                                );
                            }
                            else
                            {
                                return null; // ·«  ÊÃœ ”Ã·« 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }
            return null; // ›Ì Õ«·… ÕœÊÀ «” À‰«¡
        }

        public static async Task<int> AddPaymentsAsync(PaymentsDTO PaymentsDTO)
        {
            int ID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_AddPayments]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ReservationID", PaymentsDTO.ReservationID);

                        command.Parameters.AddWithValue("@ReturnID", PaymentsDTO.ReturnID.HasValue ?
                            PaymentsDTO.ReturnID.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@PaymentDetails", PaymentsDTO.PaymentDetails);

                        command.Parameters.AddWithValue("@PaidInitialTotalDueAmount", PaymentsDTO.PaidInitialTotalDueAmount);

                        command.Parameters.AddWithValue("@ActualTotalDueAmount", PaymentsDTO.ActualTotalDueAmount.HasValue ?
                            PaymentsDTO.ActualTotalDueAmount.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@TotalRemaining", PaymentsDTO.TotalRemaining.HasValue ?
                            PaymentsDTO.TotalRemaining.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@TotalRefundedAmount", PaymentsDTO.TotalRefundedAmount.HasValue ?
                            PaymentsDTO.TotalRefundedAmount.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@TransactionDate", PaymentsDTO.TransactionDate);

                        command.Parameters.AddWithValue("@UpdatedTransactionDate", PaymentsDTO.UpdatedTransactionDate.HasValue ?
                            PaymentsDTO.UpdatedTransactionDate.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@CreatedByUserID", PaymentsDTO.CreatedByUserID);

                        var outputIdParam = new SqlParameter("@NewPaymentsId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        ID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }
            return ID;
        }

        public static async Task<bool> UpdatePaymentsAsync(PaymentsDTO PaymentsDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_UpdatePayments]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PaymentID", PaymentsDTO.PaymentID);
                        command.Parameters.AddWithValue("@ReservationID", PaymentsDTO.ReservationID);

                        command.Parameters.AddWithValue("@ReturnID", PaymentsDTO.ReturnID.HasValue ?
                            PaymentsDTO.ReturnID.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@PaymentDetails", PaymentsDTO.PaymentDetails);
                        command.Parameters.AddWithValue("@PaidInitialTotalDueAmount", PaymentsDTO.PaidInitialTotalDueAmount);

                        command.Parameters.AddWithValue("@ActualTotalDueAmount", PaymentsDTO.ActualTotalDueAmount.HasValue ?
                            PaymentsDTO.ActualTotalDueAmount.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@TotalRemaining", PaymentsDTO.TotalRemaining.HasValue ?
                            PaymentsDTO.TotalRemaining.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@TotalRefundedAmount", PaymentsDTO.TotalRefundedAmount.HasValue ?
                            PaymentsDTO.TotalRefundedAmount.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@TransactionDate", PaymentsDTO.TransactionDate);

                        command.Parameters.AddWithValue("@UpdatedTransactionDate", PaymentsDTO.UpdatedTransactionDate.HasValue ?
                            PaymentsDTO.UpdatedTransactionDate.Value : (object)DBNull.Value);

                        command.Parameters.AddWithValue("@CreatedByUserID", PaymentsDTO.CreatedByUserID);

                        await connection.OpenAsync(); 
                        rowsAffected = await command.ExecuteNonQueryAsync(); 
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return (rowsAffected > 0);
        }

        public static async Task<bool> ReturnCar(ReturnCarDTO returnCarDTO)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_ReturnCar]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@PaymentID", SqlDbType.Int) { Value = returnCarDTO.PaymentID });
                        command.Parameters.Add(new SqlParameter("@ReservationID", SqlDbType.Int) { Value = returnCarDTO.ReservationID });
                        command.Parameters.Add(new SqlParameter("@ReturnID", SqlDbType.Int){Value= 0}); 
                        command.Parameters.Add(new SqlParameter("@PaymentDetails", SqlDbType.NVarChar, 100) { Value = returnCarDTO.PaymentDetails });
                        command.Parameters.Add(new SqlParameter("@PaidInitialTotalDueAmount", SqlDbType.SmallMoney) { Value = returnCarDTO.PaidInitialTotalDueAmount });
                        command.Parameters.Add(new SqlParameter("@TransactionDate", SqlDbType.DateTime) { Value = returnCarDTO.TransactionDate });
                        command.Parameters.Add(new SqlParameter("@FinalCheckNotes", SqlDbType.NVarChar, 500) { Value = returnCarDTO.FinalCheckNotes ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@AdditionalCharges", SqlDbType.SmallMoney) { Value = returnCarDTO.AdditionalCharges });
                        command.Parameters.Add(new SqlParameter("@Mileage", SqlDbType.SmallInt) { Value = returnCarDTO.Mileage });
                        command.Parameters.Add(new SqlParameter("@CreatedByUserID", SqlDbType.Int) { Value = returnCarDTO.CreatedByUserID });

                        command.Parameters.Add(new SqlParameter("@RowsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output });

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        int rowsAffected = (int)command.Parameters["@RowsAffected"].Value;

                        isSuccess = rowsAffected > 0;

                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError($"Error in ReturnCar: {ex.Message}");
            }

            return isSuccess;
        }

        public static async Task<bool> DebtAllPayment(int PaymentID)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_DebtAllPayment]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@PaymentID", SqlDbType.Int) { Value = PaymentID });
                      
                        command.Parameters.Add(new SqlParameter("@RowsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output });

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        int rowsAffected = (int)command.Parameters["@RowsAffected"].Value;

                        isSuccess = rowsAffected > 0;

                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError($"Error in ReturnCar: {ex.Message}");
            }

            return isSuccess;
        }


        public static async Task<bool> DeletePayments(int PaymentID)
{
	int rowsAffected = 0;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_DeletePayments]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
		command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@PaymentID", PaymentID );

		 await connection.OpenAsync();
		rowsAffected = await command.ExecuteNonQueryAsync();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);
}

public static async Task<bool> IsPaymentsExist(int PaymentID)
{
	bool isFound = false;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_IsPaymentsExist]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
				command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@PaymentID", PaymentID );

			await connection.OpenAsync();;
			 var result = await command.ExecuteScalarAsync();
				 isFound = (result != null && (int)result > 0);
			}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;
}

        public static async Task<List<PaymentsDTO>> GetAllPayments()
        {
            var PaymentsList = new List<PaymentsDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetAllPayments]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                PaymentsList.Add(new PaymentsDTO(
                                    reader.GetInt32(reader.GetOrdinal("PaymentID")),
                                    reader.GetInt32(reader.GetOrdinal("ReservationID")),
                                    reader.IsDBNull(reader.GetOrdinal("ReturnID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ReturnID")),
                                    reader.GetString(reader.GetOrdinal("PaymentDetails")),
                                    reader.GetDecimal(reader.GetOrdinal("PaidInitialTotalDueAmount")),
                                    reader.IsDBNull(reader.GetOrdinal("ActualTotalDueAmount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("ActualTotalDueAmount")),
                                    reader.IsDBNull(reader.GetOrdinal("TotalRemaining")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalRemaining")),
                                    reader.IsDBNull(reader.GetOrdinal("TotalRefundedAmount")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalRefundedAmount")),
                                    reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                                    reader.IsDBNull(reader.GetOrdinal("UpdatedTransactionDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedTransactionDate")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }
            return PaymentsList;
        }



    }

}