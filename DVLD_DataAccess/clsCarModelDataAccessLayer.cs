using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data.SqlClient;

namespace DVLD_with_Car_Rental_DataAccessLayer
{

public class MakeModelsDTO
{
public int ModelID {get; set;}
public int MakeID {get; set;}
public string ModelName {get; set;}


public MakeModelsDTO ( int ModelID,  int MakeID,  string ModelName)
{
this.ModelID = ModelID;
this.MakeID = MakeID;
this.ModelName = ModelName;


}


}

 public static class clsMakeModelsDataAccess

{
public static async Task< MakeModelsDTO> GetMakeModelsInfoByID(int ModelID)
{
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"])) 
{
	string query = "[SP_Car_GetMakeModelsByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
		command.CommandType = CommandType.StoredProcedure;
		command.Parameters.AddWithValue("@ModelID", ModelID);
		await connection.OpenAsync();
		using(SqlDataReader reader = await command.ExecuteReaderAsync())
{

			if (reader.Read())
			{
				 return new MakeModelsDTO

(
			reader.GetInt32(reader.GetOrdinal("ModelID")),			reader.GetInt32(reader.GetOrdinal("MakeID")),			reader.GetString(reader.GetOrdinal("ModelName"))
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
public static async Task<int> AddMakeModelsAsync(MakeModelsDTO MakeModelsDTO)
{
    int ID = -1;
    try
    {
        using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
        {
            string query = "[SP_Car_AddMakeModels]";
            using(	SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
	command.Parameters.AddWithValue("@MakeID", MakeModelsDTO.MakeID );

	command.Parameters.AddWithValue("@ModelName", MakeModelsDTO.ModelName );

                var outputIdParam = new SqlParameter("@NewMakeModelsId", SqlDbType.Int)
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

public static async Task<bool> UpdateMakeModelsAsync(MakeModelsDTO MakeModelsDTO)
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))

{
	string query = "[SP_Car_UpdateMakeModels]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@ModelID", MakeModelsDTO.ModelID );

	command.Parameters.AddWithValue("@MakeID", MakeModelsDTO.MakeID );

	command.Parameters.AddWithValue("@ModelName", MakeModelsDTO.ModelName );

		await connection.OpenAsync(); // Asynchronously open the connection
		rowsAffected =await command.ExecuteNonQueryAsync(); // Asynchronously execute the command
		}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static async Task<bool> DeleteMakeModels(int ModelID)
{
	int rowsAffected = 0;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_DeleteMakeModels]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
		command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@ModelID", ModelID );

		 await connection.OpenAsync();
		rowsAffected = await command.ExecuteNonQueryAsync();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);
}

public static async Task<bool> IsMakeModelsExist(int ModelID)
{
	bool isFound = false;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_IsMakeModelsExist]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
				command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@ModelID", ModelID );

			await connection.OpenAsync();;
			 var result = await command.ExecuteScalarAsync();
				 isFound = (result != null && (int)result > 0);
			}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;
}

public static async Task<List<MakeModelsDTO>> GetAllMakeModels()
{
	var MakeModelsList = new List<MakeModelsDTO>();
	try {
		using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
		{
			string query = "[SP_Car_GetAllMakeModels]";
			using (	SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();
				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						MakeModelsList.Add(new MakeModelsDTO(
			reader.GetInt32(reader.GetOrdinal("ModelID")),
			reader.GetInt32(reader.GetOrdinal("MakeID")),	
			reader.GetString(reader.GetOrdinal("ModelName"))
						));
					}
				}
			}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}
	return MakeModelsList;
}


}

}