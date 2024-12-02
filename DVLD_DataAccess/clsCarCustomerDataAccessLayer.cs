using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data.SqlClient;

namespace DVLD_with_Car_Rental_DataAccessLayer
{
public class CustomersDTO
{
public int CustomerID {get; set;}
public int PersonID {get; set;}
public int LicenseID {get; set;}
public int CreatedByUserID {get; set;}


public CustomersDTO ( int CustomerID,  int PersonID,int LicenseID,  int CreatedByUserID)
{
this.CustomerID = CustomerID;
this.PersonID = PersonID;
this.LicenseID = LicenseID;
this.CreatedByUserID = CreatedByUserID;


}


}

 public static class clsCustomersDataAccess

{
public static async Task< CustomersDTO> GetCustomersInfoByID(int CustomerID)
{
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"])) 
{
	string query = "[SP_Car_GetCustomersByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
		command.CommandType = CommandType.StoredProcedure;
		command.Parameters.AddWithValue("@CustomerID", CustomerID);
		await connection.OpenAsync();
		using(SqlDataReader reader = await command.ExecuteReaderAsync())
{

			if (reader.Read())
			{
				 return new CustomersDTO

(
			reader.GetInt32(reader.GetOrdinal("CustomerID")),		
			reader.GetInt32(reader.GetOrdinal("PsersonID")),		
			reader.GetInt32(reader.GetOrdinal("LicenseID")),		
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


        public static async Task<CustomersDTO> GetCustomersInfoByPersonID(int PersonID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetCustomersByPsersonID]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PsersonID", PersonID);
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {

                            if (reader.Read())
                            {
                                return new CustomersDTO

               (
                           reader.GetInt32(reader.GetOrdinal("CustomerID")),
                           reader.GetInt32(reader.GetOrdinal("PsersonID")),
                           reader.GetInt32(reader.GetOrdinal("LicenseID")),
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

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }

            return null;

        }
        public static async Task<int> AddCustomersAsync(CustomersDTO CustomersDTO)
{
    int ID = -1;
    try
    {
        using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
        {
            string query = "[SP_Car_AddCustomers]";
            using(	SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
	command.Parameters.AddWithValue("@PersonID", CustomersDTO.PersonID );

	command.Parameters.AddWithValue("@LicenseID", CustomersDTO.LicenseID );

	command.Parameters.AddWithValue("@CreatedByUserID", CustomersDTO.CreatedByUserID );

                var outputIdParam = new SqlParameter("@NewCustomersId", SqlDbType.Int)
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

public static async Task<bool> UpdateCustomersAsync(CustomersDTO CustomersDTO)
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))

{
	string query = "[SP_Car_UpdateCustomers]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@CustomerID", CustomersDTO.CustomerID );

	command.Parameters.AddWithValue("@PersonID", CustomersDTO.PersonID );


	command.Parameters.AddWithValue("@LicenseID", CustomersDTO.LicenseID );

	command.Parameters.AddWithValue("@CreatedByUserID", CustomersDTO.CreatedByUserID );

		await connection.OpenAsync(); // Asynchronously open the connection
		rowsAffected =await command.ExecuteNonQueryAsync(); // Asynchronously execute the command
		}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static async Task<bool> DeleteCustomers(int CustomerID)
{
	int rowsAffected = 0;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_Car_DeleteCustomers]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
		command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@CustomerID", CustomerID );

		 await connection.OpenAsync();
		rowsAffected = await command.ExecuteNonQueryAsync();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);
}

public static async Task<bool> IsCustomersExist(int CustomerID)
{
	bool isFound = false;
try {
using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
{
		string query = "[SP_IsCustomersExist]";
using (	SqlCommand command = new SqlCommand(query, connection))
{
				command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@CustomerID", CustomerID );

			await connection.OpenAsync();;
			 var result = await command.ExecuteScalarAsync();
				 isFound = (result != null && (int)result > 0);
			}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;
}
        public static async Task<List<CustomersDTO>> GetAllCustomers()
        {
            List<CustomersDTO> customers = new List<CustomersDTO>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_GetAllCustomers]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var customer = new CustomersDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetInt32(reader.GetOrdinal("LicenseID")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                                );

                                customers.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            //  ÕÊÌ· List<CustomersDTO> ≈·Ï DataTable
            dt.Columns.Add("CustomerID", typeof(int));
            dt.Columns.Add("PersonID", typeof(int));
            dt.Columns.Add("LicenseID", typeof(int));
            dt.Columns.Add("CreatedByUserID", typeof(int));

            foreach (var customer in customers)
            {
                dt.Rows.Add(customer.CustomerID, customer.PersonID, customer.LicenseID, customer.CreatedByUserID);
            }

            return customers;
        }

        public static async Task<DataTable> GetAllCustomersWithDataTable()
{
			DataTable dt = new DataTable();
	try {
		using (	SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
		{
			string query = "[SP_GetAllCustomers]";
			using (	SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();
				using (SqlDataReader reader = await command.ExecuteReaderAsync())
				{
							if (reader.HasRows) dt.Load(reader);

				}
			}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}
	return dt;
}


}

}