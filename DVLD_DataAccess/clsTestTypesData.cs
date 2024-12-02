using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TestTypesDataAccessLayer
{
	public static class clsTestTypesDataAccess
	{
		public static bool GetTestTypesInfoByID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref decimal TestFees)
		{
			bool isFound = false;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					TestTypeID = reader["TestTypeID"] != DBNull.Value ? (int)reader["TestTypeID"] : TestTypeID = default;
					TestTypeTitle = reader["TestTypeTitle"] != DBNull.Value ? (string)reader["TestTypeTitle"] : TestTypeTitle = default;
					TestTypeDescription = reader["TestTypeDescription"] != DBNull.Value ? (string)reader["TestTypeDescription"] : TestTypeDescription = default;
					TestFees = reader["TestFees"] != DBNull.Value ? (decimal)reader["TestFees"] : TestFees = default;

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
		public static int AddNewTestTypes(string TestTypeTitle, string TestTypeDescription, decimal TestFees)
		{

			int ID = -1;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"INSERT INTO TestTypes VALUES (@TestTypeTitle, @TestTypeDescription, @TestFees)
        SELECT SCOPE_IDENTITY()";

			SqlCommand command = new SqlCommand(query, connection);


			if (string.IsNullOrEmpty(TestTypeTitle))
				command.Parameters.AddWithValue("@TestTypeTitle", DBNull.Value);
			else
				command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
			if (string.IsNullOrEmpty(TestTypeDescription))
				command.Parameters.AddWithValue("@TestTypeDescription", DBNull.Value);
			else
				command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
			if (string.IsNullOrEmpty(TestFees.ToString()))
				command.Parameters.AddWithValue("@TestFees", DBNull.Value);
			else
				command.Parameters.AddWithValue("@TestFees", TestFees);

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
		public static bool UpdateTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestFees)
		{
			int rowsAffected = 0;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE TestTypes
	SET	TestTypeTitle = @TestTypeTitle,
	TestTypeDescription = @TestTypeDescription,
	TestFees = @TestFees	WHERE TestTypeID = @TestTypeID";

			SqlCommand command = new SqlCommand(query, connection);


			if (string.IsNullOrEmpty(TestTypeID.ToString()))
				command.Parameters.AddWithValue("@TestTypeID", DBNull.Value);
			else
				command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
			if (string.IsNullOrEmpty(TestTypeTitle))
				command.Parameters.AddWithValue("@TestTypeTitle", DBNull.Value);
			else
				command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
			if (string.IsNullOrEmpty(TestTypeDescription))
				command.Parameters.AddWithValue("@TestTypeDescription", DBNull.Value);
			else
				command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
			if (string.IsNullOrEmpty(TestFees.ToString()))
				command.Parameters.AddWithValue("@TestFees", DBNull.Value);
			else
				command.Parameters.AddWithValue("@TestFees", TestFees);

			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteTestTypes(int TestTypeID)
		{
			int rowsAffected = 0;
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE TestTypes WHERE TestTypeID = @TestTypeID";
			SqlCommand command = new SqlCommand(query, connection);

			if (string.IsNullOrEmpty(TestTypeID.ToString()))
				command.Parameters.AddWithValue("@TestTypeID", DBNull.Value);
			else
				command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsTestTypesExist(int TestTypeID)
		{
			bool isFound = false;
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM TestTypes WHERE TestTypeID= @TestTypeID";
			SqlCommand command = new SqlCommand(query, connection);

			if (string.IsNullOrEmpty(TestTypeID.ToString()))
				command.Parameters.AddWithValue("@TestTypeID", DBNull.Value);
			else
				command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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

		public static DataTable GetAllTestTypes()
		{

			DataTable dt = new DataTable();
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM TestTypes";
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