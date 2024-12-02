using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TestsDataAccessLayer
{
	public static class clsTestsDataAccess
	{
		public static bool GetTestsInfoByID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
		{
			bool isFound = false;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Tests WHERE TestID = @TestID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@TestID", TestID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					TestID = (int)reader["TestID"];
					TestAppointmentID = (int)reader["TestAppointmentID"];
					TestResult = (bool)reader["TestResult"];
					Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : Notes = default;
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
		public static int AddNewTests(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
		{

			int ID = -1;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"INSERT INTO Tests VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID)
                                 UPDATE TestAppointment 
                                SET IsLocked=1 where TestAppointmentID = TestAppointmentID;

                                SELECT SCOPE_IDENTITY();";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

			command.Parameters.AddWithValue("@TestResult", TestResult);

			if (string.IsNullOrEmpty(Notes))
				command.Parameters.AddWithValue("@Notes", DBNull.Value);
			else
				command.Parameters.AddWithValue("@Notes", Notes);
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
		public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
		{

			byte PassedTestsCount = 0;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @" SELECT PassedTestCount = count(TestTypeID)
                         FROM Tests INNER JOIN
                         TestAppointment ON Tests.TestAppointmentID = TestAppointment.TestAppointmentID
						 where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID  and TestResult=1";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


			try
			{
				connection.Open();

				object result = command.ExecuteScalar();


				if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
				{
					PassedTestsCount = ptCount;
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


			return PassedTestsCount;

		}

		public static bool UpdateTests(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
		{
			int rowsAffected = 0;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE Tests
	SET	TestAppointmentID = @TestAppointmentID,
	TestResult = @TestResult,
	Notes = @Notes,
	CreatedByUserID = @CreatedByUserID	WHERE TestID = @TestID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@TestID", TestID);

			command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

			command.Parameters.AddWithValue("@TestResult", TestResult);

			if (string.IsNullOrEmpty(Notes))
				command.Parameters.AddWithValue("@Notes", DBNull.Value);
			else
				command.Parameters.AddWithValue("@Notes", Notes);
			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteTests(int TestID)
		{
			int rowsAffected = 0;
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE Tests WHERE TestID = @TestID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@TestID", TestID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsTestsExist(int TestID)
		{
			bool isFound = false;
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM Tests WHERE TestID= @TestID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@TestID", TestID);

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
		public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
				  (int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
					ref int TestAppointmentID, ref bool TestResult,
					ref string Notes, ref int CreatedByUserID)
		{
			bool isFound = false;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointment ON Tests.TestAppointmentID = TestAppointment.TestAppointmentID ON LocalDrivingLicenseApplications.LocalLicenseApplicationID = TestAppointment.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.ApplicantPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointment.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@PersonID", PersonID);
			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

			try
			{
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{

					// The record was found
					isFound = true;
					TestID = (int)reader["TestID"];
					TestAppointmentID = (int)reader["TestAppointmentID"];
					TestResult = (bool)reader["TestResult"];
					if (reader["Notes"] == DBNull.Value)

						Notes = "";
					else
						Notes = (string)reader["Notes"];

					CreatedByUserID = (int)reader["CreatedByUserID"];

				}
				else
				{
					// The record was not found
					isFound = false;
				}

				reader.Close();


			}
			catch (Exception ex)
			{
				//Console.WriteLine("Error: " + ex.Message);
				isFound = false;
			}
			finally
			{
				connection.Close();
			}

			return isFound;
		}
		public static DataTable GetAllTests()
		{

			DataTable dt = new DataTable();
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Tests";
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