using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace InternationalLicensesDataAccessLayer
{
	public static class clsInternationalLicensesDataAccess
	{
		public static bool GetInternationalLicensesInfoByID(int InternationalAppID, ref int LocalDriverApplicationID, ref int ApplicatoinID, ref int DirverID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM InternationalLicenses WHERE InternationalAppID = @InternationalAppID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@InternationalAppID", InternationalAppID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					InternationalAppID = (int)reader["InternationalAppID"];
					LocalDriverApplicationID = (int)reader["LocalDriverApplicationID"];
					ApplicatoinID = (int)reader["ApplicatoinID"];
					DirverID = (int)reader["DirverID"];
					IssueDate = (DateTime)reader["IssueDate"];
					ExpirationDate = (DateTime)reader["ExpirationDate"];
					IsActive = (bool)reader["IsActive"];
					CreatedByUserID = (int)reader["CreatedByUserID"];

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
		public static int AddNewInternationalLicenses(int LocalDriverApplicationID, int ApplicatoinID, int DirverID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
		{

			int ID = -1;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"
                               Update InternationalLicenses 
                               set IsActive=0
                               where DirverID=@DirverID;

                             INSERT INTO InternationalLicenses
                               (
                                ApplicatoinID,
                                DirverID,
                                LocalDriverApplicationID,
                                IssueDate,
                                ExpirationDate,
                                IsActive,
                                CreatedByUserID)
                         VALUES
                               (@ApplicatoinID,
                                @DirverID,
                                @LocalDriverApplicationID,
                                @IssueDate,
                                @ExpirationDate,
                                @IsActive,
                                @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@LocalDriverApplicationID", LocalDriverApplicationID);

			command.Parameters.AddWithValue("@ApplicatoinID", ApplicatoinID);

			command.Parameters.AddWithValue("@DirverID", DirverID);

			command.Parameters.AddWithValue("@IssueDate", IssueDate);

			command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

			command.Parameters.AddWithValue("@IsActive", IsActive);

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
		public static bool UpdateInternationalLicenses(int InternationalAppID, int LocalDriverApplicationID, int ApplicatoinID, int DirverID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE InternationalLicenses
	SET	LocalDriverApplicationID = @LocalDriverApplicationID,
	ApplicatoinID = @ApplicatoinID,
	DirverID = @DirverID,
	IssueDate = @IssueDate,
	ExpirationDate = @ExpirationDate,
	IsActive = @IsActive,
	CreatedByUserID = @CreatedByUserID	WHERE InternationalAppID = @InternationalAppID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@InternationalAppID", InternationalAppID);

			command.Parameters.AddWithValue("@LocalDriverApplicationID", LocalDriverApplicationID);

			command.Parameters.AddWithValue("@ApplicatoinID", ApplicatoinID);

			command.Parameters.AddWithValue("@DirverID", DirverID);

			command.Parameters.AddWithValue("@IssueDate", IssueDate);

			command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

			command.Parameters.AddWithValue("@IsActive", IsActive);

			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteInternationalLicenses(int InternationalAppID)
		{
			int rowsAffected = 0;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE InternationalLicenses WHERE InternationalAppID = @InternationalAppID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@InternationalAppID", InternationalAppID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}
		public static DataTable GetDriverInternationalLicenses(int DriverID)
		{

			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"
                SELECT    InternationalAppID, ApplicatoinID,
		                LocalDriverApplicationID , IssueDate, 
                        ExpirationDate, IsActive
		    from InternationalLicenses where DirverID=@DirverID
                order by ExpirationDate desc";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@DirverID", DriverID);

			try
			{
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)

				{
					dt.Load(reader);
				}

				reader.Close();


			}

			catch (Exception ex)
			{
				// Console.WriteLine("Error: " + ex.Message);
			}
			finally
			{
				connection.Close();
			}

			return dt;

		}

		public static bool IsInternationalLicensesExist(int InternationalAppID)
		{
			bool isFound = false;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM InternationalLicenses WHERE InternationalAppID= @InternationalAppID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@InternationalAppID", InternationalAppID);

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

		public static DataTable GetAllInternationalLicenses()
		{

			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = @"Select InternationalAppID,ApplicatoinID,DirverID,
                                               LocalDriverApplicationID,IssueDate,ExpirationDate,IsActive
                                                from InternationalLicenses order by IsActive ,ExpirationDate Desc";
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