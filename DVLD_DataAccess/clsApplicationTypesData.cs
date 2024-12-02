using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ApplcatoinTypesDataAccessLayer
{
	public static class clsApplcatoinTypesDataAccess
	{
		public static bool GetApplicationTypesInfoByID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref decimal ApplicatoinFees)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM ApplcatoinTypes WHERE ApplicationTypeID = @ApplicationTypeID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					ApplicationTypeID = (int)reader["ApplicationTypeID"];
					ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
					ApplicatoinFees = (decimal)reader["ApplicationFees"];

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
		public static int AddNewApplicationTypes(string ApplicationTypeTitle, decimal ApplicatoinFees)
		{

			int ID = -1;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"INSERT INTO ApplcatoinTypes VALUES (@ApplicationTypeTitle, @ApplicatoinFees)
        SELECT SCOPE_IDENTITY()";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);

			command.Parameters.AddWithValue("@ApplicatoinFees", ApplicatoinFees);


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
		public static bool UpdateApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicatoinFees)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE ApplcatoinTypes
	SET	ApplicationTypeTitle = @ApplicationTypeTitle,
	ApplicatoinFees = @ApplicatoinFees	WHERE ApplicationTypeID = @ApplicationTypeID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

			command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);

			command.Parameters.AddWithValue("@ApplicatoinFees", ApplicatoinFees);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteApplicationTypes(int ApplicationTypeID)
		{
			int rowsAffected = 0;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE ApplcatoinTypes WHERE ApplicationTypeID = @ApplicationTypeID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsApplicationTypesExist(int ApplicationTypeID)
		{
			bool isFound = false;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM ApplcatoinTypes WHERE ApplicationTypeID= @ApplicationTypeID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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

		public static DataTable GetAllApplcatoinTypes()
		{

			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM ApplcatoinTypes";
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