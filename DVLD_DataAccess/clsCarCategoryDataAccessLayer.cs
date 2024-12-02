using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data.SqlClient;

namespace DVLD_with_CarP_Rental_DataAccessLayer
{
public class VehicleCategoriesDTO
{
public int CategoryID {get; set;}
public string CategoryName {get; set;}


public VehicleCategoriesDTO ( int CategoryID,  string CategoryName)
{
this.CategoryID = CategoryID;
this.CategoryName = CategoryName;


}


}

 public static class clsVehicleCategoriesDataAccess

{
public static VehicleCategoriesDTO GetVehicleCategoriesInfoByID(int CategoryID)
{
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"])) 
{
	string query = "[SP_Car_GetVehicleCategoriesByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
		command.CommandType = CommandType.StoredProcedure;
		command.Parameters.AddWithValue("@CategoryID", CategoryID);
		 connection.Open();
		using(SqlDataReader reader =  command.ExecuteReader())
{

			if (reader.Read())
			{
				 return new VehicleCategoriesDTO

(
			reader.GetInt32(reader.GetOrdinal("CategoryID")),			reader.GetString(reader.GetOrdinal("CategoryName"))
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
public static async Task<int> AddVehicleCategoriesAsync(VehicleCategoriesDTO VehicleCategoriesDTO)
{
    int ID = -1;
    try
    {
        using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
        {
            string query = "[SP_Car_AddVehicleCategories]";
            using(	SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
	command.Parameters.AddWithValue("@CategoryName", VehicleCategoriesDTO.CategoryName );

                var outputIdParam = new SqlParameter("@NewVehicleCategoriesId", SqlDbType.Int)
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

public static async Task<bool> UpdateVehicleCategoriesAsync(VehicleCategoriesDTO VehicleCategoriesDTO)
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))

{
	string query = "[SP_Car_UpdateVehicleCategories]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@CategoryID", VehicleCategoriesDTO.CategoryID );

	command.Parameters.AddWithValue("@CategoryName", VehicleCategoriesDTO.CategoryName );

		await connection.OpenAsync(); // Asynchronously open the connection
		rowsAffected =await command.ExecuteNonQueryAsync(); // Asynchronously execute the command
		}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static async Task<bool> DeleteVehicleCategories(int CategoryID)
{
	int rowsAffected = 0;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_DeleteVehicleCategories]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
		command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@CategoryID", CategoryID );

		 await connection.OpenAsync();
		rowsAffected = await command.ExecuteNonQueryAsync();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);
}

public static async Task<bool> IsVehicleCategoriesExist(int CategoryID)
{
	bool isFound = false;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_IsVehicleCategoriesExist]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
				command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@CategoryID", CategoryID );

			await connection.OpenAsync();;
			 var result = await command.ExecuteScalarAsync();
				 isFound = (result != null && (int)result > 0);
			}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;
}
        public static async Task<List<VehicleCategoriesDTO>> GetAllVehicleCategoriesAsync()
        {
            var vehicleCategoriesList = new List<VehicleCategoriesDTO>();

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
            using (var command = new SqlCommand("[SP_Car_GetAllVehicleCategories]", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            vehicleCategoriesList.Add(new VehicleCategoriesDTO(
                                reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                reader.GetString(reader.GetOrdinal("CategoryName"))
                            ));
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // تسجيل الأخطاء الخاصة بـ SQL
                    clsErrorHandling.HandleError($"SQL Error in GetAllVehicleCategoriesAsync: {sqlEx.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    // تسجيل الأخطاء العامة
                    clsErrorHandling.HandleError($"Error in GetAllVehicleCategoriesAsync: {ex.Message}");
                    return null;
                }
            }

            return vehicleCategoriesList;
        }




    }

}