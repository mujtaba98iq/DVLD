using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data.SqlClient;

namespace DVLD_with_Car_Rental_DataAccessLayer
{
public class MakesDTO
{
public int MakeID {get; set;}
public string Make {get; set;}


public MakesDTO ( int MakeID,  string Make)
{
this.MakeID = MakeID;
this.Make = Make;


}


}

 public static class clsMakesDataAccess

{
public static async Task< MakesDTO> GetMakesInfoByID(int MakeID)
{
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"])) 
{
	string query = "[SP_Car_GetMakesByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
		command.CommandType = CommandType.StoredProcedure;
		command.Parameters.AddWithValue("@MakeID", MakeID);
		await connection.OpenAsync();
		using(SqlDataReader reader = await command.ExecuteReaderAsync())
{

			if (reader.Read())
			{
				 return new MakesDTO

(
			reader.GetInt32(reader.GetOrdinal("MakeID")),	
			reader.GetString(reader.GetOrdinal("Make"))
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
public static async Task<int> AddMakesAsync(MakesDTO MakesDTO)
{
    int ID = -1;
    try
    {
        using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
        {
            string query = "[SP_Car_AddMakes]";
            using(	SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
	command.Parameters.AddWithValue("@Make", MakesDTO.Make );

                var outputIdParam = new SqlParameter("@NewMakesId", SqlDbType.Int)
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

public static async Task<bool> UpdateMakesAsync(MakesDTO MakesDTO)
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))

{
	string query = "[SP_Car_UpdateMakes]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@MakeID", MakesDTO.MakeID );

	command.Parameters.AddWithValue("@Make", MakesDTO.Make );

		await connection.OpenAsync(); // Asynchronously open the connection
		rowsAffected =await command.ExecuteNonQueryAsync(); // Asynchronously execute the command
		}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static async Task<bool> DeleteMakes(int MakeID)
{
	int rowsAffected = 0;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_DeleteMakes]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
		command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@MakeID", MakeID );

		 await connection.OpenAsync();
		rowsAffected = await command.ExecuteNonQueryAsync();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);
}

public static async Task<bool> IsMakesExist(int MakeID)
{
	bool isFound = false;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_IsMakesExist]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
				command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@MakeID", MakeID );

			await connection.OpenAsync();;
			 var result = await command.ExecuteScalarAsync();
				 isFound = (result != null && (int)result > 0);
			}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;
}

public static async Task<List<MakesDTO>> GetAllMakes()
{
	var MakesList = new List<MakesDTO>();
	try {
		using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
		{
			string query = "[SP_Car_GetAllMakes]";
			using (	SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();
				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						MakesList.Add(new MakesDTO(
			reader.GetInt32(reader.GetOrdinal("MakeID")),	
			reader.GetString(reader.GetOrdinal("Make"))
						));
					}
				}
			}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}
	return MakesList;
}


}

}