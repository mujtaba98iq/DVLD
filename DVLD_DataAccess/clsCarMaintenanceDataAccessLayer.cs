using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data.SqlClient;

namespace DVLD_with_Car_Rental_DataAccessLayer
{
public class MaintenanceDTO
{
public int MaintenanceID {get; set;}
public int VehicleID {get; set;}
public string Description {get; set;}
public DateTime MaintenanceDate {get; set;}
public decimal Cost {get; set;}
public int CreatedByUserID {get; set;}


public MaintenanceDTO ( int MaintenanceID,  int VehicleID,  string Description,  DateTime MaintenanceDate,  decimal Cost,  int CreatedByUserID)
{
this.MaintenanceID = MaintenanceID;
this.VehicleID = VehicleID;
this.Description = Description;
this.MaintenanceDate = MaintenanceDate;
this.Cost = Cost;
this.CreatedByUserID = CreatedByUserID;


}


}

 public static class clsMaintenanceDataAccess

{
public static async Task< MaintenanceDTO> GetMaintenanceInfoByID(int MaintenanceID)
{
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"])) 
{
	string query = "[SP_Car_GetMaintenanceByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
		command.CommandType = CommandType.StoredProcedure;
		command.Parameters.AddWithValue("@MaintenanceID", MaintenanceID);
		await connection.OpenAsync();
		using(SqlDataReader reader = await command.ExecuteReaderAsync())
{

			if (reader.Read())
			{
				 return new MaintenanceDTO

(
			reader.GetInt32(reader.GetOrdinal("MaintenanceID")),		
			reader.GetInt32(reader.GetOrdinal("VehicleID")),		
			reader.GetString(reader.GetOrdinal("Description")),	
			reader.GetDateTime(reader.GetOrdinal("MaintenanceDate")),	
			reader.GetDecimal(reader.GetOrdinal("Cost")),		
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
public static async Task<int> AddMaintenanceAsync(MaintenanceDTO MaintenanceDTO)
{
    int ID = -1;
    try
    {
        using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
        {
            string query = "[SP_Car_AddMaintenance]";
            using(	SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
	command.Parameters.AddWithValue("@VehicleID", MaintenanceDTO.VehicleID );

	command.Parameters.AddWithValue("@Description", MaintenanceDTO.Description );

	command.Parameters.AddWithValue("@MaintenanceDate", MaintenanceDTO.MaintenanceDate );

	command.Parameters.AddWithValue("@Cost", MaintenanceDTO.Cost );

	command.Parameters.AddWithValue("@CreatedByUserID", MaintenanceDTO.CreatedByUserID );

                var outputIdParam = new SqlParameter("@NewMaintenanceId", SqlDbType.Int)
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
        public static async Task<bool> UpdateMaintenanceAsync(MaintenanceDTO MaintenanceDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                using (SqlCommand command = new SqlCommand("[SP_Car_UpdateMaintenance]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with null safety
                    command.Parameters.Add("@MaintenanceID", SqlDbType.Int).Value = MaintenanceDTO.MaintenanceID;
                    command.Parameters.Add("@VehicleID", SqlDbType.Int).Value = MaintenanceDTO.VehicleID;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = (object)MaintenanceDTO.Description ?? DBNull.Value;
                    command.Parameters.Add("@MaintenanceDate", SqlDbType.DateTime).Value = MaintenanceDTO.MaintenanceDate;
                    command.Parameters.Add("@Cost", SqlDbType.SmallMoney).Value = MaintenanceDTO.Cost;
                    command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = MaintenanceDTO.CreatedByUserID;

                    // ≈÷«›… „Œ—Ã«  · ·ﬁÌ «·ﬁÌ„… «·„—Ã⁄… „‰ SP
                    var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    // Open the connection and execute the query asynchronously
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // ﬁ—«¡… «·ﬁÌ„… «·„—Ã⁄… „‰ «·≈Ã—«¡ «·„Œ“‰
                    int result = (int)returnValue.Value;

                    return result > 0; // ≈—Ã«⁄ true ≈–«  „  ÕœÌÀ ”Ã·« 
                }
            }
            catch (Exception ex)
            {
                // Log the error with full details
                clsErrorHandling.HandleError($"Error in UpdateMaintenanceAsync: {ex.Message}");
                return false; // ≈—Ã«⁄ false ›Ì Õ«·… ÊÃÊœ Œÿ√
            }
        }


        public static async Task<bool> DeleteMaintenance(int MaintenanceID)
{
	int rowsAffected = 0;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_DeleteMaintenance]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
		command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@MaintenanceID", MaintenanceID );

		 await connection.OpenAsync();
		rowsAffected = await command.ExecuteNonQueryAsync();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);
}

public static async Task<bool> IsMaintenanceExist(int MaintenanceID)
{
	bool isFound = false;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_IsMaintenanceExist]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
				command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@MaintenanceID", MaintenanceID );

			await connection.OpenAsync();;
			 var result = await command.ExecuteScalarAsync();
				 isFound = (result != null && (int)result > 0);
			}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;
}

public static async Task<List<MaintenanceDTO>> GetAllMaintenance()
{
	var MaintenanceList = new List<MaintenanceDTO>();
	try {
		using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
		{
			string query = "[SP_Car_GetAllMaintenance]";
			using (	SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();
				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						MaintenanceList.Add(new MaintenanceDTO(
			reader.GetInt32(reader.GetOrdinal("MaintenanceID")),		
			reader.GetInt32(reader.GetOrdinal("VehicleID")),		
			reader.GetString(reader.GetOrdinal("Description")),		
			reader.GetDateTime(reader.GetOrdinal("MaintenanceDate")),	
			reader.GetDecimal(reader.GetOrdinal("Cost")),			
			reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
						));
					}
				}
			}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}
	return MaintenanceList;
}


        public static async Task<DataTable> GetAllMaintenanceWithDataTable()
        {
            DataTable dt= new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetAllMaintenance]";
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

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }
            return dt;
        }


    }

}