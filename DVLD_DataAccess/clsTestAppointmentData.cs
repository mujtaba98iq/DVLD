using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TestAppointmentDataAccessLayer
{
public static class clsTestAppointmentDataAccess
{
		public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
		{

			DataTable dt = new DataTable();
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointment
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";


			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
			command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


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

        public static bool GetTestAppointmentInfoByID(
    int TestAppointmentID,
    ref int TestTypeID,
    ref int LocalDrivingLicenseApplicationID,
    ref DateTime AppointmentDate,
    ref decimal PaidFees,
    ref int CreatedByUserID,
    ref bool IsLocked,
    ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            string query = "SELECT * FROM TestAppointment WHERE TestAppointmentID = @TestAppointmentID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            TestTypeID = reader.GetInt32(reader.GetOrdinal("TestTypeID"));
                            LocalDrivingLicenseApplicationID = reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID"));
                            AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                            PaidFees = reader.GetDecimal(reader.GetOrdinal("PaidFees"));
                            CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                            IsLocked = reader.GetBoolean(reader.GetOrdinal("IsLocked"));

                            // التحقق من RetakeTestApplicationID إذا كان يمكن أن يكون NULL
                            if (reader.IsDBNull(reader.GetOrdinal("RetakeTestApplicationID")))
                                RetakeTestApplicationID = -1;
                            else
                                RetakeTestApplicationID = reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // سجل الخطأ أو التعامل مع الاستثناء
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return isFound;
        }

        public static int AddNewTestAppointment( int TestTypeID,  int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate, decimal PaidFees,  int CreatedByUserID,  bool IsLocked,  int RetakeTestApplicationID){

        int ID = -1;

SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

        string query = @"Insert Into TestAppointment (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked,RetakeTestApplicationID)
                            Values (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,0,@RetakeTestApplicationID);
                
                            SELECT SCOPE_IDENTITY();";

	SqlCommand command = new SqlCommand(query, connection);


	command.Parameters.AddWithValue("@TestTypeID", TestTypeID );

	command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID );

	command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate );

	command.Parameters.AddWithValue("@PaidFees", PaidFees );

	command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID );

	command.Parameters.AddWithValue("@IsLocked", IsLocked ); if (RetakeTestApplicationID == -1)

				if (RetakeTestApplicationID == -1)

					command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
				else
					command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID); 
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
		public static int GetTestID( int TestAppointmentID)
		{

        int TestID = -1;

SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

        string query = @"Select TestID from Tests where TestAppointmentID=@TestAppointmentID";

	SqlCommand command = new SqlCommand(query, connection);


	command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                    try
            {
                connection.Open();

                object result = command.ExecuteScalar();
            

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
					TestID = insertedID;
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


            return TestID;

}
public static bool UpdateTestAppointment( int TestAppointmentID,  int TestTypeID,  int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate, decimal PaidFees,  int CreatedByUserID,  bool IsLocked,  int RetakeTestApplicationID)
{
	int rowsAffected=0;

SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

	string query = @"UPDATE TestAppointment
	SET	TestTypeID = @TestTypeID,
	LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
	AppointmentDate = @AppointmentDate,
	PaidFees = @PaidFees,
	CreatedByUserID = @CreatedByUserID,
	IsLocked = @IsLocked,
	RetakeTestApplicationID = @RetakeTestApplicationID	WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);





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
public static bool DeleteTestAppointment(int TestAppointmentID)
{
	int rowsAffected = 0;
SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
	string query = "DELETE TestAppointment WHERE TestAppointmentID = @TestAppointmentID";
	SqlCommand command = new SqlCommand(query, connection);

	command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID );

	try
	{
		connection.Open();
		rowsAffected = command.ExecuteNonQuery();
	}
	catch (Exception ex) {}
	finally{ connection.Close(); }


	return (rowsAffected > 0);

}

public static bool IsTestAppointmentExist(int TestAppointmentID)
{
	bool isFound = false;
SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
	string query = "SELECT Found=1 FROM TestAppointment WHERE TestAppointmentID= @TestAppointmentID"; 
	SqlCommand command = new SqlCommand(query, connection);

	command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID );

	try
	{
		connection.Open();
		SqlDataReader reader = command.ExecuteReader();
		isFound = reader.HasRows;
		reader.Close();
	}
	catch (Exception ex) {}
	finally{ connection.Close(); }


	return isFound;

}

public static DataTable GetAllTestAppointment()
{

	DataTable dt = new DataTable();
SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
	string query = "SELECT * FROM TestAppointment";
	SqlCommand command = new SqlCommand(query, connection);

	try
	{
		connection.Open();
		SqlDataReader reader = command.ExecuteReader();
		if (reader.HasRows)dt.Load(reader);
		reader.Close();
	}
	catch (Exception ex) {}
	finally{ connection.Close(); }


	return dt;
}


}

}