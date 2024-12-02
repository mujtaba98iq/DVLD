using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ApplicationsDataAccessLayer
{
	public static class clsApplicationsDataAccess
	{
		public static bool GetApplicationsInfoByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus, ref float PaidFees, ref DateTime LastStatusDate, ref int CreatedByUserID)
		{
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
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
		public static int AddNewApplications(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, float PaidFees, DateTime LastStatusDate, int CreatedByUserID)
		{

			int ID = -1;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"INSERT INTO Applications ( 
                            ApplicantPersonID,ApplicationDate,ApplicationTypeID,
                            ApplicationStatus,LastStatusDate,
                            PaidFees,CreatedByUserID)
                             VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                      @ApplicationStatus,@LastStatusDate,
                                      @PaidFees,   @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

			command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);

			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

			command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

			command.Parameters.AddWithValue("@PaidFees", PaidFees);

			command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);

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
		public static bool UpdateApplications(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, float PaidFees, DateTime LastStatusDate, int CreatedByUserID)
		{
			int rowsAffected = 0;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE Applications
	SET	ApplicantPersonID = @ApplicantPersonID,
	ApplicationDate = @ApplicationDate,
	ApplicationTypeID = @ApplicationTypeID,
	ApplicationStatus = @ApplicationStatus,
	PaidFees = @PaidFees,
	LastStatusDate = @LastStatusDate,
	CreatedByUserID = @CreatedByUserID	WHERE ApplicationID = @ApplicationID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

			command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

			command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);

			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

			command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

			command.Parameters.AddWithValue("@PaidFees", PaidFees);

			command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);

			command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteApplications(int ApplicationID)
		{
			int rowsAffected = 0;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE Applications WHERE ApplicationID = @ApplicationID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}
		

		public static bool IsApplicationsExist(int ApplicationID)
		{
			bool isFound = false;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM Applications WHERE ApplicationID= @ApplicationID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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
		public static bool UpdateStatus(int ApplicationID,int NewStatus)
		{
			int rowsAffected=0;
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = @"UPDATE Applications
              	SET
              	ApplicationStatus = @ApplicationStatus,
                                LastStatusDate =@LastStatusDate

		WHERE ApplicationID = @ApplicationID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
			command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}

		public static DataTable GetAllApplications()
		{

			DataTable dt = new DataTable();
			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Applications";
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


		public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
		{
			int ActiveApplicationID = -1;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
			command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
			command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
			try
			{
				connection.Open();
				object result = command.ExecuteScalar();


				if (result != null && int.TryParse(result.ToString(), out int AppID))
				{
					ActiveApplicationID = AppID;
				}
			}
			catch (Exception ex)
			{
				//Console.WriteLine("Error: " + ex.Message);
				return ActiveApplicationID;
			}
			finally
			{
				connection.Close();
			}

			return ActiveApplicationID;
		}
		public static int GetActiveInternationalLicenseIDByDriverID(int DirverID)
		{
			int ActiveApplicationID = -1;

			SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"Select Top 1 InternationalAppID from InternationalLicenses
          where DirverID=@DirverID and GetDate() between IssueDate and  ExpirationDate order by ExpirationDate Desc;";

			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("DirverID", DirverID);
			try
			{
				connection.Open();
				object result = command.ExecuteScalar();


				if (result != null && int.TryParse(result.ToString(), out int AppID))
				{
					ActiveApplicationID = AppID;
				}
			}
			catch (Exception ex)
			{
				//Console.WriteLine("Error: " + ex.Message);
				return ActiveApplicationID;
			}
			finally
			{
				connection.Close();
			}

			return ActiveApplicationID;
		}

	}


}