using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DetainedLicensesDataAccessLayer
{
	public static class clsDetainedLicensesDataAccess
	{
		public static bool GetDetainedLicensesInfoByID(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@DetainID", DetainID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					DetainID = (int)reader["DetainID"];
					LicenseID = (int)reader["LicenseID"];
					DetainDate = (DateTime)reader["DetainDate"];
					FineFees = (decimal)reader["FineFees"];
					CreatedByUserID = (int)reader["CreatedByUserID"];
					IsReleased = (bool)reader["IsReleased"];
					ReleaseDate = reader["ReleaseDate"] != DBNull.Value ? (DateTime)reader["ReleaseDate"] : ReleaseDate = default;
					ReleasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? (int)reader["ReleasedByUserID"] : ReleasedByUserID = default;
					ReleaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? (int)reader["ReleaseApplicationID"] : ReleaseApplicationID = default;

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

		}public static bool GetDetainedLicensesInfoByLicenseID(ref int DetainID,  int LicenseID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@LicenseID", LicenseID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					DetainID = (int)reader["DetainID"];
					LicenseID = (int)reader["LicenseID"];
					DetainDate = (DateTime)reader["DetainDate"];
					FineFees = (decimal)reader["FineFees"];
					CreatedByUserID = (int)reader["CreatedByUserID"];
					IsReleased = (bool)reader["IsReleased"];
					ReleaseDate = reader["ReleaseDate"] != DBNull.Value ? (DateTime)reader["ReleaseDate"] : ReleaseDate = default;
					ReleasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? (int)reader["ReleasedByUserID"] : ReleasedByUserID = default;
					ReleaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? (int)reader["ReleaseApplicationID"] : ReleaseApplicationID = default;

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
		public static int AddNewDetainedLicenses(int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
		{

			int ID = -1;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"INSERT INTO dbo.DetainedLicenses
                               (LicenseID,
                               DetainDate,
                               FineFees,
                               CreatedByUserID,
                               IsReleased
                               )
                            VALUES
                               (@LicenseID,
                               @DetainDate, 
                               @FineFees, 
                               @CreatedByUserID,
                               0
                             );
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
		public static bool UpdateDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE DetainedLicenses
	SET	LicenseID = @LicenseID,
	DetainDate = @DetainDate,
	FineFees = @FineFees,
	CreatedByUserID = @CreatedByUserID,
	IsReleased = @IsReleased,
	ReleaseDate = @ReleaseDate,
	ReleasedByUserID = @ReleasedByUserID,
	ReleaseApplicationID = @ReleaseApplicationID	WHERE DetainID = @DetainID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@DetainID", DetainID);

			command.Parameters.AddWithValue("@LicenseID", LicenseID);

			command.Parameters.AddWithValue("@DetainDate", DetainDate);

			command.Parameters.AddWithValue("@FineFees", FineFees);

			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

			command.Parameters.AddWithValue("@IsReleased", IsReleased);

			if (string.IsNullOrEmpty(ReleaseDate.ToString()))
				command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
			else
				command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
			if (string.IsNullOrEmpty(ReleasedByUserID.ToString()))
				command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
			else
				command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
			if (string.IsNullOrEmpty(ReleaseApplicationID.ToString()))
				command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
			else
				command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteDetainedLicenses(int DetainID)
		{
			int rowsAffected = 0;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE DetainedLicenses WHERE DetainID = @DetainID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@DetainID", DetainID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}
		public static bool ReleaseDetainedLicenses(int DetainID,int ReleasedByUserID, int ReleaseApplicationID)
		{
			int rowsAffected = 0;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = @"UPDATE DetainedLicenses 
                 	SET	
                 	IsReleased =1
                 	,ReleaseApplicationID=@ReleaseApplicationID,
                 	ReleasedByUserID=@ReleasedByUserID,
                 	ReleaseDate=@ReleaseDate
                  WHERE DetainID =@DetainID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@DetainID", DetainID);
			command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
			command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
			command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsLicenseDetained(int LicenseID)
		{
			bool isFound = false;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM DetainedLicenses WHERE LicenseID= @LicenseID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
		public static bool IsDetainedLicensesExist(int DetainID)
		{
			bool isFound = false;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM DetainedLicenses WHERE DetainID= @DetainID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@DetainID", DetainID);

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

		public static DataTable GetAllDetainedLicenses()
		{

			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM DetainedLicenses_View Order by IsReleased ,DetainID";
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