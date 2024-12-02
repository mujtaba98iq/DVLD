using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LincnseClassesDataAccessLayer
{
	public static class clsLincnseClassesDataAccess
	{
		public static bool GetLicensesClassesInfoByID(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref decimal ClassFees)
		{
			bool isFound = false;

		SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM LincnseClasses WHERE LicenseClassID = @LicenseClassID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					LicenseClassID = (int)reader["LicenseClassID"];
					ClassName = (string)reader["ClassName"];
					ClassDescription = (string)reader["ClassDescription"];
					MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
					DefaultValidityLength = (byte)reader["DefaultValidityLength"];
					ClassFees = (decimal)reader["ClassFees"];

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
		public static bool GetLicensesClassesInfoByLicenseName(ref int LicenseClassID,  string ClassName, ref string ClassDescription, ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref decimal ClassFees)
		{
			bool isFound = false;

		SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM LincnseClasses WHERE ClassName = @ClassName";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ClassName", ClassName);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					LicenseClassID = (int)reader["LicenseClassID"];
					ClassName = (string)reader["ClassName"];
					ClassDescription = (string)reader["ClassDescription"];
					MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
					DefaultValidityLength = (byte)reader["DefaultValidityLength"];
					ClassFees = (decimal)reader["ClassFees"];

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
		public static int AddNewLicensesClasses(string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees)
		{

			int ID = -1;

		SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"INSERT INTO LincnseClasses VALUES (@ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees)
        SELECT SCOPE_IDENTITY()";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@ClassName", ClassName);

			command.Parameters.AddWithValue("@ClassDescription", ClassDescription);

			command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);

			command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);

			command.Parameters.AddWithValue("@ClassFees", ClassFees);


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
		public static bool UpdateLicensesClasses(int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees)
		{
			int rowsAffected = 0;

		SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE LincnseClasses
	SET	ClassName = @ClassName,
	ClassDescription = @ClassDescription,
	MinimumAllowedAge = @MinimumAllowedAge,
	DefaultValidityLength = @DefaultValidityLength,
	ClassFees = @ClassFees	WHERE LicenseClassID = @LicenseClassID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

			command.Parameters.AddWithValue("@ClassName", ClassName);

			command.Parameters.AddWithValue("@ClassDescription", ClassDescription);

			command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);

			command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);

			command.Parameters.AddWithValue("@ClassFees", ClassFees);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteLicensesClasses(int LicenseClassID)
		{
			int rowsAffected = 0;
		SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE LincnseClasses WHERE LicenseClassID = @LicenseClassID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsLicensesClassesExist(int LicenseClassID)
		{
			bool isFound = false;
		SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM LincnseClasses WHERE LicenseClassID= @LicenseClassID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

		public static DataTable GetAllLincnseClasses()
		{

			DataTable dt = new DataTable();
		SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM LincnseClasses";
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