using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DVLD_with_Car_Rental_DataAccessLayer
{
public class ReservationsDTO
{
public int BookingID {get; set;}
public int CustomerID {get; set;}
public int VehicleID {get; set;}
public DateTime RentalStartDate {get; set;}
public DateTime RentalEndDate {get; set;}
public string PickupLocation {get; set;}
public string DropoffLocation {get; set;}
public byte InitialRentalDays { get; set;}
public decimal RentalPricePerDay { get; set;}
public decimal InitialTotalDueAmount { get; set;}
public string InitialCheckNotes {get; set;}
public int CreatedByUserID {get; set;}
        public PaymentInfoDTO paymentInfoDTO { get; set; }


        public ReservationsDTO(int BookingID, int CustomerID, int VehicleID, DateTime RentalStartDate, DateTime RentalEndDate, string PickupLocation, string DropoffLocation, byte InitialRentalDays, decimal RentalPricePerDay, decimal InitialTotalDueAmount, string InitialCheckNotes, int CreatedByUserID, PaymentInfoDTO paymentInfoDTO=  null)
{
this.BookingID = BookingID;
this.CustomerID = CustomerID;
this.VehicleID = VehicleID;
this.RentalStartDate = RentalStartDate;
this.RentalEndDate = RentalEndDate;
this.PickupLocation = PickupLocation;
this.DropoffLocation = DropoffLocation;
            this.RentalPricePerDay = RentalPricePerDay;
            this.InitialRentalDays = InitialRentalDays;
            this.InitialTotalDueAmount=InitialTotalDueAmount;
this.InitialCheckNotes = InitialCheckNotes;
this.CreatedByUserID = CreatedByUserID;
            if(paymentInfoDTO!= null )
            this.paymentInfoDTO= paymentInfoDTO;


}


}
    public class PaymentInfoDTO
    {

        public decimal PaidInitialTotalDueAmount { get; set; }
        public string PaymentDetails { get; set; }


        public PaymentInfoDTO(string PaymentDetails, decimal PaidInitialTotalDueAmount)
        {
        this.PaidInitialTotalDueAmount = PaidInitialTotalDueAmount;
            this.PaymentDetails=  PaymentDetails;

        }


    }

    public static class clsReservationsDataAccess

{
public static async Task<ReservationsDTO> GetReservationsInfoByID(int BookingID)
{
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"])) 
{
	string query = "[SP_Car_GetReservationsByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
		command.CommandType = CommandType.StoredProcedure;
		command.Parameters.AddWithValue("@BookingID", BookingID);
		await connection.OpenAsync();
		using(SqlDataReader reader = await command.ExecuteReaderAsync())
{

			if (reader.Read())
			{
                                return new ReservationsDTO

               (
                           reader.GetInt32(reader.GetOrdinal("BookingID")),
                           reader.GetInt32(reader.GetOrdinal("CustomerID")),
                           reader.GetInt32(reader.GetOrdinal("VehicleID")),
                           reader.GetDateTime(reader.GetOrdinal("RentalStartDate")),
                           reader.GetDateTime(reader.GetOrdinal("RentalEndDate")),
                           reader.GetString(reader.GetOrdinal("PickupLocation")),
                           reader.GetString(reader.GetOrdinal("DropoffLocation")),
                           reader.GetByte(reader.GetOrdinal("InitialRentalDays")),
                           reader.GetDecimal(reader.GetOrdinal("RentalPricePerDay")),
                           reader.GetDecimal(reader.GetOrdinal("InitialTotalDueAmount")),
                           reader.IsDBNull(reader.GetOrdinal("InitialCheckNotes")) ?null: reader.GetString(reader.GetOrdinal("InitialCheckNotes")),
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

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

 return null;

}
    public static async Task<int> AddReservationsAsync(ReservationsDTO ReservationsDTO)
    {
        int ID = -1;
        try
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
            {
                string query = "[SP_Car_AddReservations]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CustomerID", ReservationsDTO.CustomerID);
                    command.Parameters.AddWithValue("@VehicleID", ReservationsDTO.VehicleID);
                    command.Parameters.AddWithValue("@RentalStartDate", ReservationsDTO.RentalStartDate);
                    command.Parameters.AddWithValue("@RentalEndDate", ReservationsDTO.RentalEndDate);
                    command.Parameters.AddWithValue("@PickupLocation", ReservationsDTO.PickupLocation);
                    command.Parameters.AddWithValue("@DropoffLocation", ReservationsDTO.DropoffLocation);
               
                    command.Parameters.AddWithValue("@InitialCheckNotes", ReservationsDTO.InitialCheckNotes ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@CreatedByUserID", ReservationsDTO.CreatedByUserID);
                    command.Parameters.AddWithValue("@PaymentDetails", ReservationsDTO.paymentInfoDTO.PaymentDetails);
                    command.Parameters.AddWithValue("@PaidInitialTotalDueAmount", ReservationsDTO.paymentInfoDTO.PaidInitialTotalDueAmount);

                    var outputIdParam = new SqlParameter("@NewReservationsId", SqlDbType.Int)
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


        public static async Task<bool> UpdateReservationsAsync(ReservationsDTO ReservationsDTO)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_UpdateReservations]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // ÅÖÇÝÉ ÇáãÚÇãáÇÊ
                        command.Parameters.AddWithValue("@BookingID", ReservationsDTO.BookingID);
                        command.Parameters.AddWithValue("@CustomerID", ReservationsDTO.CustomerID);
                        command.Parameters.AddWithValue("@VehicleID", ReservationsDTO.VehicleID);
                        command.Parameters.AddWithValue("@RentalStartDate", ReservationsDTO.RentalStartDate);
                        command.Parameters.AddWithValue("@RentalEndDate", ReservationsDTO.RentalEndDate);
                        command.Parameters.AddWithValue("@PickupLocation", ReservationsDTO.PickupLocation);
                        command.Parameters.AddWithValue("@DropoffLocation", ReservationsDTO.DropoffLocation);
                        command.Parameters.AddWithValue("@InitialRentalDays", ReservationsDTO.InitialRentalDays);
                        command.Parameters.AddWithValue("@RentalPricePerDay", ReservationsDTO.RentalPricePerDay);
                        command.Parameters.AddWithValue("@InitialTotalDueAmount", ReservationsDTO.InitialTotalDueAmount);
                        command.Parameters.AddWithValue("@InitialCheckNotes", ReservationsDTO.InitialCheckNotes ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", ReservationsDTO.CreatedByUserID);

                        // ÅÖÇÝÉ ãÚÇãá ÇáÅÎÑÇÌ áÚÏÏ ÇáÕÝæÝ ÇáãÊÃËÑÉ
                        SqlParameter affectedRowsParam = new SqlParameter("@AffectedRows", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(affectedRowsParam);

                        // ÝÊÍ ÇáÇÊÕÇá æÊäÝíÐ ÇáÇÓÊÚáÇã ÈÔßá ÛíÑ ãÊÒÇãä
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        // ÇáÍÕæá Úáì ÚÏÏ ÇáÕÝæÝ ÇáãÊÃËÑÉ
                        rowsAffected = (int)affectedRowsParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return rowsAffected > 0;
        }

        public static async Task<bool> DeleteReservations(int BookingID)
{
	int rowsAffected = 0;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_DeleteReservations]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
		command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@BookingID", BookingID );

		 await connection.OpenAsync();
		rowsAffected = await command.ExecuteNonQueryAsync();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);
}

public static async Task<bool> IsReservationsExist(int BookingID)
{
	bool isFound = false;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_IsReservationsExist]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
				command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@BookingID", BookingID );

			await connection.OpenAsync();;
			 var result = await command.ExecuteScalarAsync();
				 isFound = (result != null && (int)result > 0);
			}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;
}

        public static async Task<bool> IsVehicleReservedByUser(int VehicleID,int UserID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_IsVehicleReservedByUser]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@VehicleID", VehicleID);
                        command.Parameters.AddWithValue("@UserID", UserID);

                        await connection.OpenAsync(); ;
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0);
                    }
                }
            }

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }

            return isFound;
        }

        public static async Task<List<ReservationsDTO>> GetAllReservations()
    {
        var ReservationsList = new List<ReservationsDTO>();
        try
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
            {
                string query = "[SP_Car_GetAllReservations]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ReservationsList.Add(new ReservationsDTO(
                                reader.GetInt32(reader.GetOrdinal("BookingID")),
                                reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                reader.GetInt32(reader.GetOrdinal("VehicleID")),
                                reader.GetDateTime(reader.GetOrdinal("RentalStartDate")),
                                reader.GetDateTime(reader.GetOrdinal("RentalEndDate")),
                                reader.GetString(reader.GetOrdinal("PickupLocation")),
                                reader.GetString(reader.GetOrdinal("DropoffLocation")),
                                 reader.GetByte(reader.GetOrdinal("InitialRentalDays")),
                           reader.GetDecimal(reader.GetOrdinal("RentalPricePerDay")),
                           reader.GetDecimal(reader.GetOrdinal("InitialTotalDueAmount")),
                                reader.IsDBNull(reader.GetOrdinal("InitialCheckNotes")) ? null : reader.GetString(reader.GetOrdinal("InitialCheckNotes")),
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
        return ReservationsList;
    }
        public static async Task<DataTable> GetAllReservationsWithDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetAllReservations]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows) { dt.Load(reader); }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }
            return dt;
        }




    }

}