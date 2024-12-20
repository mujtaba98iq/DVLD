﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UsersDataAccessLayer
{
	public static class clsUsersDataAccess
	{
		public static bool GetUsersInfoByID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
		{
			bool isFound = false;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Users WHERE UserID = @UserID";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@UserID", UserID);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					UserID = (int)reader["UserID"];
					PersonID = (int)reader["PersonID"];
					UserName = (string)reader["UserName"];
					Password = (string)reader["Password"];
					IsActive = (bool)reader["IsActive"];

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

		}	public static bool GetUsersInfoByUserNameAndPassword(ref int UserID, ref int PersonID,  string UserName,  string Password, ref bool IsActive)
		{
			bool isFound = false;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Users WHERE UserName = @UserName and Password=@Password";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@UserName", UserName);
			command.Parameters.AddWithValue("@Password", Password);

			try
			{

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					isFound = true;

					UserID = (int)reader["UserID"];
					PersonID = (int)reader["PersonID"];
					UserName = (string)reader["UserName"];
					Password = (string)reader["Password"];
					IsActive = (bool)reader["IsActive"];

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
		public static int AddNewUsers(int PersonID, string UserName, string Password, bool IsActive)
		{

			int ID = -1;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"INSERT INTO Users VALUES (@PersonID, @UserName, @Password, @IsActive)
        SELECT SCOPE_IDENTITY()";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@PersonID", PersonID);

			command.Parameters.AddWithValue("@UserName", UserName);

			command.Parameters.AddWithValue("@Password", Password);

			command.Parameters.AddWithValue("@IsActive", IsActive);


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
		public static bool UpdateUsers(int UserID, int PersonID, string UserName, string Password, bool IsActive)
		{
			int rowsAffected = 0;

			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);

			string query = @"UPDATE Users
	SET	PersonID = @PersonID,
	UserName = @UserName,
	Password = @Password,
	IsActive = @IsActive	WHERE UserID = @UserID";

			SqlCommand command = new SqlCommand(query, connection);


			command.Parameters.AddWithValue("@UserID", UserID);

			command.Parameters.AddWithValue("@PersonID", PersonID);

			command.Parameters.AddWithValue("@UserName", UserName);

			command.Parameters.AddWithValue("@Password", Password);

			command.Parameters.AddWithValue("@IsActive", IsActive);


			try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
			catch (Exception ex) { }
			finally { connection.Close(); }

			return (rowsAffected > 0);

		}
		public static bool DeleteUsers(int UserID)
		{
			int rowsAffected = 0;
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "DELETE Users WHERE UserID = @UserID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@UserID", UserID);

			try
			{
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception ex) { }
			finally { connection.Close(); }


			return (rowsAffected > 0);

		}

		public static bool IsUsersExist(int UserID)
		{
			bool isFound = false;
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM Users WHERE UserID= @UserID";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@UserID", UserID);

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
		public static bool IsUsersExist(string UserName)
		{
			bool isFound = false;
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM Users WHERE UserName= @UserName";
			SqlCommand command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@UserName", UserName);

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
		
		public static bool IsUsersExistByPersonID(int PersonID)
		{
			bool isFound = false;
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT Found=1 FROM Users WHERE PersonID= @PersonID";
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

		public static DataTable GetAllUsers()
		{

			DataTable dt = new DataTable();
			SqlConnection connection =new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
			string query = "SELECT * FROM Users";
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