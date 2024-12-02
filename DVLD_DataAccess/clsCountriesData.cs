using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CountriesDataAccessLayer
{
	public static class clsCountriesDataAccess
	{
		public static bool GetCountriesInfoByID(int CountryID, ref string CountryName)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@CountryID", CountryID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					CountryID = (int)reader["CountryID"];
					CountryName = (string)reader["CountryName"];

				}
				else
				{
					isFound = false;
				}

				reader.Close();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }

			return isFound;

		}
		public static bool GetCountriesInfoByName(ref int CountryID,  string CountryName)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@CountryName", CountryName);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					CountryID = (int)reader["CountryID"];
					CountryName = (string)reader["CountryName"];

				}
				else
				{
					isFound = false;
				}

				reader.Close();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }

			return isFound;

		}
		public static int AddNewCountries(string CountryName)
		{

			int ID = -1;

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

            string query = @"INSERT INTO Countries VALUES (@CountryName)
        SELECT SCOPE_IDENTITY()";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@CountryName", CountryName);


			try
			{
				connection.Open();

				object result = command.ExecuteScalar();


				if (result != null && int.TryParse(result.ToString(), out int insertedID))
				{
					ID = insertedID;
				}
			}

			catch (Exception ex)
			{
				//Console.WriteLine(Error:  + ex.Message);

			}

			finally
			{
				connection.Close();
			}


			return ID;

		}
		public static bool UpdateCountries(int CountryID, string CountryName)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE Countries
	SET	CountryName = @CountryName	WHERE CountryID = @CountryID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@CountryID", CountryID);

			command.Parameters.AddWithValue("@CountryName", CountryName);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteCountries(int CountryID)
		{
			int rowsAffected = 0;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE Countries WHERE CountryID = @CountryID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@CountryID", CountryID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsCountriesExist(int CountryID)
		{
			bool isFound = false;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM Countries WHERE CountryID= @CountryID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@CountryID", CountryID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				isFound = reader.HasRows;
				reader.Close();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return isFound;

		}

		public static DataTable GetAllCountries()
		{

			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Countries";
			SqlCommand command = new SqlCommand(query, connection);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				if (reader.HasRows) dt.Load(reader);
				reader.Close();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return dt;
		}


	}

}