using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LicensesDataAccessLayer
{
	public static class clsLicensesDataAccess
	{
		public static bool GetLicensesInfoByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
		{
			bool isFound = false;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@LicenseID", LicenseID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					LicenseID = (int)reader["LicenseID"];
					ApplicationID = (int)reader["ApplicationID"];
					DriverID = (int)reader["DriverID"];
					LicenseClass = (int)reader["LicenseClass"];
					IssueDate = (DateTime)reader["IssueDate"];
					ExpirationDate = (DateTime)reader["ExpirationDate"];
					Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : Notes = default;
					PaidFees = (decimal)reader["PaidFees"];
					IsActive = (bool)reader["IsActive"];
					IssueReason = (byte)reader["IssueReason"];
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
		public static int AddNewLicenses(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
		{

			int ID = -1;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @" INSERT INTO Licenses
      (ApplicationID,
       DriverID,
       LicenseClass,
       IssueDate,
       ExpirationDate,
       Notes,
       PaidFees,
       IsActive,IssueReason,
       CreatedByUserID)
VALUES
      (
      @ApplicationID,
      @DriverID,
      @LicenseClass,
      @IssueDate,
      @ExpirationDate,
      @Notes,
      @PaidFees,
      @IsActive,@IssueReason, 
      @CreatedByUserID);
   SELECT SCOPE_IDENTITY();";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

			command.Parameters.AddWithValue("@DriverID", DriverID);

			command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

			command.Parameters.AddWithValue("@IssueDate", IssueDate);

			command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

			if (string.IsNullOrEmpty(Notes))
				command.Parameters.AddWithValue("@Notes", DBNull.Value);
			else
				command.Parameters.AddWithValue("@Notes", Notes);
			command.Parameters.AddWithValue("@PaidFees", PaidFees);

			command.Parameters.AddWithValue("@IsActive", IsActive);

			command.Parameters.AddWithValue("@IssueReason", IssueReason);

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
		public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
		{

			int ID = -1;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"	 SELECT Licenses.LicenseID
                      FROM  Licenses INNER JOIN
                         Applications ON Licenses.ApplicationID = Applications.ApplicationID
						 where ApplicantPersonID=@ApplicantPersonID and IsActive=1
						 and LicenseClass=@LicenseClass";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
			command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

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
	
		public static bool UpdateLicenses(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
		{
			int rowsAffected = 0;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE Licenses
	SET	ApplicationID = @ApplicationID,
	DriverID = @DriverID,
	LicenseClass = @LicenseClass,
	IssueDate = @IssueDate,
	ExpirationDate = @ExpirationDate,
	Notes = @Notes,
	PaidFees = @PaidFees,
	IsActive = @IsActive,
	IssueReason = @IssueReason,
	CreatedByUserID = @CreatedByUserID	WHERE LicenseID = @LicenseID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@LicenseID", LicenseID);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

			command.Parameters.AddWithValue("@DriverID", DriverID);

			command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

			command.Parameters.AddWithValue("@IssueDate", IssueDate);

			command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

			if (string.IsNullOrEmpty(Notes))
				command.Parameters.AddWithValue("@Notes", DBNull.Value);
			else
				command.Parameters.AddWithValue("@Notes", Notes);
			command.Parameters.AddWithValue("@PaidFees", PaidFees);

			command.Parameters.AddWithValue("@IsActive", IsActive);

			command.Parameters.AddWithValue("@IssueReason", IssueReason);

			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeactivateLicense(int LicenseID)	{
			int rowsAffected = 0;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE Licenses
	SET	
	IsActive = 0
	WHERE LicenseID = @LicenseID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@LicenseID", LicenseID);
			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();

			}
			catch (Exception ex)
			{
				//Console.WriteLine("Error: " + ex.Message);
				return false;
			}

			finally
			{
				connection.Close();
			}

			return (rowsAffected > 0);

		}
		public static bool DeleteLicenses(int LicenseID)
		{
			int rowsAffected = 0;
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE Licenses WHERE LicenseID = @LicenseID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LicenseID", LicenseID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsLicensesExist(int LicenseID)
		{
			bool isFound = false;
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM Licenses WHERE LicenseID= @LicenseID";
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
		
	

		public static DataTable GetAllLicenses()
		{

			DataTable dt = new DataTable();
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Licenses";
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
		public static DataTable GetAllLicenses(int DriverID)
		{

			DataTable dt = new DataTable();
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = @"  SELECT Licenses.LicenseID, 
                 Licenses.ApplicationID, 
                 LincnseClasses.ClassName, 
                 Licenses.IssueDate,
                 Licenses.ExpirationDate,
                 Licenses.IsActive
                 FROM Licenses INNER JOIN
                 LincnseClasses ON Licenses.LicenseClass = LincnseClasses.LicenseClassID
                 where DriverID=@DriverID Order by IsActive desc , ExpirationDate desc";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("DriverID", DriverID);
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