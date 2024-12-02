using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data.SqlClient;

namespace DVLD_with_Car_Rental_DataAccessLayer
{
public class FuleTypesDTO
{
public int FuleTypeID {get; set;}
public string FuleType {get; set;}


public FuleTypesDTO ( int FuleTypeID,  string FuleType)
{
this.FuleTypeID = FuleTypeID;
this.FuleType = FuleType;


}


}

 public static class clsFuleTypesDataAccess

{
public static FuleTypesDTO GetFuleTypesInfoByID(int FuleTypeID)
{
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"])) 
{
	string query = "[SP_Car_GetFuleTypesByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
		command.CommandType = CommandType.StoredProcedure;
		command.Parameters.AddWithValue("@FuleTypeID", FuleTypeID);
		 connection.Open();
		using(SqlDataReader reader =  command.ExecuteReader())
{

			if (reader.Read())
			{
				 return new FuleTypesDTO

(
			reader.GetInt32(reader.GetOrdinal("FuleTypeID")),		
			reader.GetString(reader.GetOrdinal("FuleType"))
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
public static async Task<int> AddFuleTypesAsync(FuleTypesDTO FuleTypesDTO)
{
    int ID = -1;
    try
    {
        using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
        {
            string query = "[SP_Car_AddFuleTypes]";
            using(	SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
	command.Parameters.AddWithValue("@FuleType", FuleTypesDTO.FuleType );

                var outputIdParam = new SqlParameter("@NewFuleTypesId", SqlDbType.Int)
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

public static async Task<bool> UpdateFuleTypesAsync(FuleTypesDTO FuleTypesDTO)
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))

{
	string query = "[SP_Car_UpdateFuleTypes]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@FuleTypeID", FuleTypesDTO.FuleTypeID );

	command.Parameters.AddWithValue("@FuleType", FuleTypesDTO.FuleType );

		await connection.OpenAsync(); // Asynchronously open the connection
		rowsAffected =await command.ExecuteNonQueryAsync(); // Asynchronously execute the command
		}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static async Task<bool> DeleteFuleTypes(int FuleTypeID)
{
	int rowsAffected = 0;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_DeleteFuleTypes]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
		command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@FuleTypeID", FuleTypeID );

		 await connection.OpenAsync();
		rowsAffected = await command.ExecuteNonQueryAsync();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);
}

public static async Task<bool> IsFuleTypesExist(int FuleTypeID)
{
	bool isFound = false;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_IsFuleTypesExist]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
				command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@FuleTypeID", FuleTypeID );

			await connection.OpenAsync();;
			 var result = await command.ExecuteScalarAsync();
				 isFound = (result != null && (int)result > 0);
			}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;
}

public static async Task<List<FuleTypesDTO>> GetAllFuleTypes()
{
	var FuleTypesList = new List<FuleTypesDTO>();
	try {
		using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
		{
			string query = "[SP_Car_GetAllFuleTypes]";
			using (	SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();
				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						FuleTypesList.Add(new FuleTypesDTO(
			reader.GetInt32(reader.GetOrdinal("FuleTypeID")),		
			reader.GetString(reader.GetOrdinal("FuleType"))
						));
					}
				}
			}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}
	return FuleTypesList;
}


}

}