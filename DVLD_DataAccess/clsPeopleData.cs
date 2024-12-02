using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PeopleDataAccessLayer
{
	public static class clsPeopleDataAccess
	{
        public static bool GetclsPeopleInfoByID(int personID, ref string nationalNo, ref string firstName, ref string secondName, ref string thirdName, ref string lastName, ref DateTime dateOfBirth, ref byte gender, ref string address, ref string email, ref string phone, ref int nationalityCountryID, ref string imagePath)
        {
            bool isFound = false;

            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM People WHERE PersonID = @PersonID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int) { Value = personID });

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            // قراءة القيم من القارئ مع التحقق من القيم null
                            nationalNo = reader["NationalNo"] != DBNull.Value ? (string)reader["NationalNo"] : nationalNo;
                            firstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : firstName;
                            secondName = reader["SecondName"] != DBNull.Value ? (string)reader["SecondName"] : secondName;
                            thirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : thirdName;
                            lastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : lastName;
                            dateOfBirth = reader["DateOfBirth"] != DBNull.Value ? (DateTime)reader["DateOfBirth"] : dateOfBirth;
                            gender = reader["Gendor"] != DBNull.Value ? (byte)reader["Gendor"] : gender;
                            address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : address;
                            email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : email;
                            phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : phone;
                            nationalityCountryID = reader["NationalityCountryID"] != DBNull.Value ? (int)reader["NationalityCountryID"] : nationalityCountryID;
                            imagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : imagePath;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // سجل الاستثناء أو استخدمه حسب الحاجة
                    Console.WriteLine("Error: " + ex.Message);
                    // يمكنك أيضًا تسجيله إلى ملف أو نظام تسجيل آخر
                }
            }

            return isFound;
        }

        public static bool GetclsPeopleInfoByNationalNo(ref int PersonID,  string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gendor, ref string Address, ref string Email, ref string Phone, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

         SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : LastName = default;
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : Email = default;
                    Phone = (string)reader["Phone"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : ImagePath = default;

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
        public static int AddNewclsPeople(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, short Gendor, string Address, string Email, string Phone, int NationalityCountryID, string ImagePath)
		{

			int ID = -1;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"INSERT INTO People VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address, @Email, @Phone, @NationalityCountryID, @ImagePath)
        SELECT SCOPE_IDENTITY()";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@NationalNo", NationalNo);

			command.Parameters.AddWithValue("@FirstName", FirstName);

			command.Parameters.AddWithValue("@SecondName", SecondName);

			command.Parameters.AddWithValue("@ThirdName", ThirdName);

			if (string.IsNullOrEmpty(LastName))
				command.Parameters.AddWithValue("@LastName", DBNull.Value);
			else
				command.Parameters.AddWithValue("@LastName", LastName);
			command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

			command.Parameters.AddWithValue("@Gendor", Gendor);

			command.Parameters.AddWithValue("@Address", Address);

			if (string.IsNullOrEmpty(Email))
				command.Parameters.AddWithValue("@Email", DBNull.Value);
			else
				command.Parameters.AddWithValue("@Email", Email);
			command.Parameters.AddWithValue("@Phone", Phone);

			command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

			if (string.IsNullOrEmpty(ImagePath))
				command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
			else
				command.Parameters.AddWithValue("@ImagePath", ImagePath);

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
		public static bool UpdateclsPeople(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, short Gendor, string Address, string Email, string Phone, int NationalityCountryID, string ImagePath)
		{
			int rowsAffected = 0;

		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE People
	SET	NationalNo = @NationalNo,
	FirstName = @FirstName,
	SecondName = @SecondName,
	ThirdName = @ThirdName,
	LastName = @LastName,
	DateOfBirth = @DateOfBirth,
	Gendor = @Gendor,
	Address = @Address,
	Email = @Email,
	Phone = @Phone,
	NationalityCountryID = @NationalityCountryID,
	ImagePath = @ImagePath	WHERE PersonID = @PersonID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@PersonID", PersonID);

			command.Parameters.AddWithValue("@NationalNo", NationalNo);

			command.Parameters.AddWithValue("@FirstName", FirstName);

			command.Parameters.AddWithValue("@SecondName", SecondName);

			command.Parameters.AddWithValue("@ThirdName", ThirdName);

			if (string.IsNullOrEmpty(LastName))
				command.Parameters.AddWithValue("@LastName", DBNull.Value);
			else
				command.Parameters.AddWithValue("@LastName", LastName);
			command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

			command.Parameters.AddWithValue("@Gendor", Gendor);

			command.Parameters.AddWithValue("@Address", Address);

			if (string.IsNullOrEmpty(Email))
				command.Parameters.AddWithValue("@Email", DBNull.Value);
			else
				command.Parameters.AddWithValue("@Email", Email);
			command.Parameters.AddWithValue("@Phone", Phone);

			command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

			if (string.IsNullOrEmpty(ImagePath))
				command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
			else
				command.Parameters.AddWithValue("@ImagePath", ImagePath);

			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteclsPeople(int PersonID)
		{
			int rowsAffected = 0;
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE People WHERE PersonID = @PersonID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@PersonID", PersonID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsclsPeopleExist(int PersonID)
		{
			bool isFound = false;
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM People WHERE PersonID= @PersonID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@PersonID", PersonID);

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
		public static bool IsclsPeopleExist(string NationalNo)
		{
			bool isFound = false;
		SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM People WHERE NationalNo= @NationalNo";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            string query = @"
        SELECT p.PersonID, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, p.LastName,
            CASE 
                WHEN p.Gendor = 0 THEN 'Male'
                ELSE 'Female'
            END AS Gendor,
            p.DateOfBirth, c.CountryName, p.Phone, p.Email
        FROM People AS p 
        INNER JOIN Countries AS c ON c.CountryID = p.NationalityCountryID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text; // تغيير إلى Text

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // تسجيل الاستثناء (يمكنك استخدام إطار عمل تسجيل أو كتابة في ملف سجل)
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return dt;
        }



    }

}