using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data.SqlClient;

namespace DVLD_with_Car_Rental_DataAccessLayer
{
public class VehicleReturnsDTO
{
public int ReturenID {get; set;}
public DateTime ActualReturnDate {get; set;}
public byte ActualRentalDays {get; set;}
public short Mileage {get; set;}
public short ConsumedMilaeage {get; set;}
public string FinalCheckNotes {get; set;}
public decimal AdditionalCharges {get; set;}
public decimal ActualTotalDueAmount {get; set;}
public int CreatedByUserID {get; set;}


public VehicleReturnsDTO ( int ReturenID,  DateTime ActualReturnDate,  byte ActualRentalDays,  short Mileage,  short ConsumedMilaeage,  string FinalCheckNotes,  decimal AdditionalCharges,  decimal ActualTotalDueAmount,  int CreatedByUserID)
{
this.ReturenID = ReturenID;
this.ActualReturnDate = ActualReturnDate;
this.ActualRentalDays = ActualRentalDays;
this.Mileage = Mileage;
this.ConsumedMilaeage = ConsumedMilaeage;
this.FinalCheckNotes = FinalCheckNotes;
this.AdditionalCharges = AdditionalCharges;
this.ActualTotalDueAmount = ActualTotalDueAmount;
this.CreatedByUserID = CreatedByUserID;


}


}

 public static class clsVehicleReturnsDataAccess

{
        public static async Task<VehicleReturnsDTO> GetVehicleReturnsInfoByID(int ReturenID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetVehicleReturnsByID]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReturenID", ReturenID);
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return new VehicleReturnsDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("ReturenID")),
                                    reader.GetDateTime(reader.GetOrdinal("ActualReturnDate")),
                                    reader.GetByte(reader.GetOrdinal("ActualRentalDays")),
                                    reader.GetInt16(reader.GetOrdinal("Mileage")),
                                    reader.GetInt16(reader.GetOrdinal("ConsumedMilaeage")),
                                    // Handle null value for FinalCheckNotes
                                    reader.IsDBNull(reader.GetOrdinal("FinalCheckNotes")) ? null : reader.GetString(reader.GetOrdinal("FinalCheckNotes")),
                                    reader.GetDecimal(reader.GetOrdinal("AdditionalCharges")),
                                    reader.GetDecimal(reader.GetOrdinal("ActualTotalDueAmount")),
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

        public static async Task<int> AddVehicleReturnsAsync(VehicleReturnsDTO VehicleReturnsDTO)
{
    int ID = -1;
    try
    {
        using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
        {
            string query = "[SP_Car_AddVehicleReturns]";
            using(	SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
	command.Parameters.AddWithValue("@ActualReturnDate", VehicleReturnsDTO.ActualReturnDate );

	command.Parameters.AddWithValue("@ActualRentalDays", VehicleReturnsDTO.ActualRentalDays );

	command.Parameters.AddWithValue("@Mileage", VehicleReturnsDTO.Mileage );

	command.Parameters.AddWithValue("@ConsumedMilaeage", VehicleReturnsDTO.ConsumedMilaeage );
						if(VehicleReturnsDTO.FinalCheckNotes==null)
                            command.Parameters.AddWithValue("@FinalCheckNotes", DBNull.Value);
						else

                        command.Parameters.AddWithValue("@FinalCheckNotes", VehicleReturnsDTO.FinalCheckNotes );

	command.Parameters.AddWithValue("@AdditionalCharges", VehicleReturnsDTO.AdditionalCharges );

	command.Parameters.AddWithValue("@ActualTotalDueAmount", VehicleReturnsDTO.ActualTotalDueAmount );

	command.Parameters.AddWithValue("@CreatedByUserID", VehicleReturnsDTO.CreatedByUserID );

                var outputIdParam = new SqlParameter("@NewVehicleReturnsId", SqlDbType.Int)
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

public static async Task<bool> UpdateVehicleReturnsAsync(VehicleReturnsDTO VehicleReturnsDTO)
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))

{
	string query = @"[SP_Car_UpdateVehicleReturns]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@ReturenID", VehicleReturnsDTO.ReturenID );

	command.Parameters.AddWithValue("@ActualReturnDate", VehicleReturnsDTO.ActualReturnDate );

	command.Parameters.AddWithValue("@ActualRentalDays", VehicleReturnsDTO.ActualRentalDays );

	command.Parameters.AddWithValue("@Mileage", VehicleReturnsDTO.Mileage );

	command.Parameters.AddWithValue("@ConsumedMilaeage", VehicleReturnsDTO.ConsumedMilaeage );

                        if (VehicleReturnsDTO.FinalCheckNotes == null)
                            command.Parameters.AddWithValue("@FinalCheckNotes", DBNull.Value);
                        else

                            command.Parameters.AddWithValue("@FinalCheckNotes", VehicleReturnsDTO.FinalCheckNotes);

                        command.Parameters.AddWithValue("@AdditionalCharges", VehicleReturnsDTO.AdditionalCharges );

	command.Parameters.AddWithValue("@ActualTotalDueAmount", VehicleReturnsDTO.ActualTotalDueAmount );

	command.Parameters.AddWithValue("@CreatedByUserID", VehicleReturnsDTO.CreatedByUserID );

		await connection.OpenAsync(); // Asynchronously open the connection
		rowsAffected =await command.ExecuteNonQueryAsync(); // Asynchronously execute the command
		}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static async Task<bool> DeleteVehicleReturns(int ReturenID)
{
	int rowsAffected = 0;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_DeleteVehicleReturns]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
		command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@ReturenID", ReturenID );

		 await connection.OpenAsync();
		rowsAffected = await command.ExecuteNonQueryAsync();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);
}

public static async Task<bool> IsVehicleReturnsExist(int ReturenID)
{
	bool isFound = false;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_IsVehicleReturnsExist]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
				command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@ReturenID", ReturenID );

			await connection.OpenAsync();;
			 var result = await command.ExecuteScalarAsync();
				 isFound = (result != null && (int)result > 0);
			}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;
}

public static async Task<List<VehicleReturnsDTO>> GetAllVehicleReturns()
{
	var VehicleReturnsList = new List<VehicleReturnsDTO>();
	try {
		using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
		{
			string query = "[SP_Car_GetAllVehicleReturns]";
			using (	SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();
				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						VehicleReturnsList.Add(new VehicleReturnsDTO(
			reader.GetInt32(reader.GetOrdinal("ReturenID")),
            reader.GetDateTime(reader.GetOrdinal("ActualReturnDate")),		
            reader.GetByte(reader.GetOrdinal("ActualRentalDays")),		
            reader.GetInt16(reader.GetOrdinal("Mileage")),		
            reader.GetInt16(reader.GetOrdinal("ConsumedMilaeage")),		
            reader.IsDBNull(reader.GetOrdinal("FinalCheckNotes"))?null : reader.GetString(reader.GetOrdinal("FinalCheckNotes")), 		
            reader.GetDecimal(reader.GetOrdinal("AdditionalCharges")),	
            reader.GetDecimal(reader.GetOrdinal("ActualTotalDueAmount")),	
            reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
						));
					}
				}
			}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}
	return VehicleReturnsList;
}

public static async Task<DataTable> GetAllVehicleReturnsWithDataTable()
{
			DataTable dt=new DataTable();
	try {
		using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
		{
			string query = "[SP_Car_GetAllVehicleReturns]";
			using (	SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();
				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{
                            if (reader.HasRows) { dt.Load(reader); }
			
					}
				}
			}
		
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}
	return dt;
}


}

}