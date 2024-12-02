using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LocalDrivingLicenseApplicationsDataAccessLayer
{
	public static class clsLocalDrivingLicenseApplicationsDataAccess
	{
		public static bool GetLocalDrivingLicenseApplicationsInfoByID(int LocalLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
		{
			bool isFound = false;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalLicenseApplicationID = @LocalLicenseApplicationID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					LocalLicenseApplicationID = (int)reader["LocalLicenseApplicationID"];
					ApplicationID = (int)reader["ApplicationID"];
					LicenseClassID = (int)reader["LicenseClassID"];

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
		public static int AddNewLocalDrivingLicenseApplications(int ApplicationID, int LicenseClassID)
		{

			//this function will return the new person id if succeeded and -1 if not.
			int LocalDrivingLicenseApplicationID = -1;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"INSERT INTO LocalDrivingLicenseApplications ( 
                            ApplicationID,LicenseClassID)
                             VALUES (@ApplicationID,@LicenseClassID);
                             SELECT SCOPE_IDENTITY();";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

			try
			{
				connection.Open();

				object result = command.ExecuteScalar();

				if (result != null && int.TryParse(result.ToString(), out int insertedID))
				{
					LocalDrivingLicenseApplicationID = insertedID;
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


			return LocalDrivingLicenseApplicationID;

		}
		public static bool DoesAttendTestType(int LocalLicenseApplicationID, int TestTypeID)
		{

			bool IsFound = false;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"SELECT top 1  Found=1     
                          FROM            LocalDrivingLicenseApplications INNER JOIN
                         TestAppointment ON TestAppointment.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalLicenseApplicationID INNER JOIN
                         Tests ON TestAppointment.TestAppointmentID = Tests.TestAppointmentID where
						    (LocalDrivingLicenseApplications.LocalLicenseApplicationID = @LocalLicenseApplicationID) 
                            AND(TestAppointment.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointment.TestAppointmentID desc";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);

			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


			try
			{
				connection.Open();

				object result = command.ExecuteScalar();


				if (result != null)
				{
					IsFound = true;
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


			return IsFound;

		}
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            byte TotalTrialsPerTest = 0;

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);


            string query = @" SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointment ON LocalDrivingLicenseApplications.LocalLicenseApplicationID = TestAppointment.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointment.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointment.TestTypeID = @TestTypeID)
                       ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte Trials))
                {
                    TotalTrialsPerTest = Trials;
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

            return TotalTrialsPerTest;

        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
		{

			bool Result = false;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointment ON LocalDrivingLicenseApplications.LocalLicenseApplicationID = TestAppointment.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointment.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalLicenseApplicationID = @LocalLicenseApplicationID) 
                            AND(TestAppointment.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointment.TestAppointmentID desc";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
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

            return Result;
        }
		public static int GetTriles(int LocalLicenseApplicationID, int TestTypeID)
		{

			int CountTrils = -1;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"SELECT     TotalTrialsPerTest=  Count(Tests.TestID)
                     FROM            Tests INNER JOIN
                                              TestAppointment ON Tests.TestAppointmentID = TestAppointment.TestAppointmentID INNER JOIN
                     LocalDrivingLicenseApplications ON 
                     TestAppointment.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalLicenseApplicationID
                     where LocalDrivingLicenseApplications.LocalLicenseApplicationID=@LocalLicenseApplicationID and TestTypeID=@TestTypeID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);

			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


			try
			{
				connection.Open();

				object result = command.ExecuteScalar();


				if (result != null && int.TryParse(result.ToString(), out int insertedID))
				{
					CountTrils = insertedID;
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


			return CountTrils;

		}
		public static bool UpdateLocalDrivingLicenseApplications(int LocalLicenseApplicationID, int ApplicationID, int LicenseClassID)
		{
			int rowsAffected = 0;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE LocalDrivingLicenseApplications
	SET	ApplicationID = @ApplicationID,
	LicenseClassID = @LicenseClassID	WHERE LocalLicenseApplicationID = @LocalLicenseApplicationID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteLocalDrivingLicenseApplications(int LocalLicenseApplicationID)
		{
			int rowsAffected = 0;
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE LocalDrivingLicenseApplications WHERE LocalLicenseApplicationID = @LocalLicenseApplicationID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}
		
		public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

		{

			bool Result = false;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointment ON LocalDrivingLicenseApplications.LocalLicenseApplicationID = TestAppointment.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointment.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointment.TestAppointmentID desc";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

			try
			{
				connection.Open();

				object result = command.ExecuteScalar();


				if (result != null)
				{
					Result = true;
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

			return Result;

		}
		public static bool IsLocalDrivingLicenseApplicationsExist(int LocalLicenseApplicationID)
		{
			bool isFound = false;
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM LocalDrivingLicenseApplications WHERE LocalLicenseApplicationID= @LocalLicenseApplicationID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);

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

		public static DataTable GetAllLocalDrivingLicenseApplications()
		{

			DataTable dt = new DataTable();
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = @"SELECT *
                              FROM LocalDrivingLicenseApplications_View
                              order by ApplicationDate Desc";
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