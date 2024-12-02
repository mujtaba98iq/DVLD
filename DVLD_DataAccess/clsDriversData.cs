using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DriversDataAccessLayer
{
	public static class clsDriversDataAccess
	{
		public static bool GetDriversInfoByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@DriverID", DriverID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					DriverID = (int)reader["DriverID"];
					PersonID = (int)reader["PersonID"];
					CreatedByUserID = (int)reader["CreatedByUserID"];
					CreatedDate = (DateTime)reader["CreatedDate"];

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
		public static bool GetDriversInfoByPersonID(ref int DriverID,  int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@PersonID", PersonID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					DriverID = (int)reader["DriverID"];
					PersonID = (int)reader["PersonID"];
					CreatedByUserID = (int)reader["CreatedByUserID"];
					CreatedDate = (DateTime)reader["CreatedDate"];

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
		public static int AddNewDrivers(int PersonID, int CreatedByUserID, DateTime CreatedDate)
		{

            int DriverID = -1;
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

            string query = @"Insert Into Drivers (PersonID,CreatedByUserID,CreatedDate)
                            Values (@PersonID,@CreatedByUserID,@CreatedDate);
                          
                            SELECT SCOPE_IDENTITY();";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return DriverID;

        }
		public static bool UpdateDrivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE Drivers
	SET	PersonID = @PersonID,
	CreatedByUserID = @CreatedByUserID,
	CreatedDate = @CreatedDate	WHERE DriverID = @DriverID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@DriverID", DriverID);

			command.Parameters.AddWithValue("@PersonID", PersonID);

			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

			command.Parameters.AddWithValue("@CreatedDate", CreatedDate);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteDrivers(int DriverID)
		{
			int rowsAffected = 0;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE Drivers WHERE DriverID = @DriverID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@DriverID", DriverID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsDriversExist(int PersonID)
		{
			bool isFound = false;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM Drivers WHERE PersonID= @PersonID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@PersonID", PersonID);

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

		public static DataTable GetAllDrivers()
		{

			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Drivers_View order by FullName";
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