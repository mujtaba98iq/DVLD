using System;
using System.Collections.Generic;
using System.Data;
using DVLD_DataAccess;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DVLDWithCarRentalDataAccessLayer
{
    public class VehicleCopiesDTO
    {
        public int CopyID { get; set; }
        public int VehicleID { get; set; }
        public bool AvailabilityStatus { get; set; }
        public int CreateByUserID { get; set; }


        public VehicleCopiesDTO(int CopyID, int VehicleID, bool AvailabilityStatus, int CreateByUserID)
        {
            this.CopyID = CopyID;
            this.VehicleID = VehicleID;
            this.AvailabilityStatus = AvailabilityStatus;
            this.CreateByUserID = CreateByUserID;


        }


    }

    public static class clsVehicleCopiesDataAccess

    {
        public static async Task<VehicleCopiesDTO> GetVehicleCopiesInfoByID(int CopyID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetVehicleCopiesByID]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CopyID", CopyID);
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {

                            if (reader.Read())
                            {
                                return new VehicleCopiesDTO

               (
                           reader.GetInt32(reader.GetOrdinal("CopyID")), 
                           reader.GetInt32(reader.GetOrdinal("VehicleID")),
                           reader.GetBoolean(reader.GetOrdinal("AvailabilityStatus")), 
                           reader.GetInt32(reader.GetOrdinal("CreateByUserID"))
               );
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }

            return null;

        }
        public static async Task<int> AddVehicleCopiesAsync(VehicleCopiesDTO VehicleCopiesDTO)
        {
            int ID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_AddVehicleCopies]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@VehicleID", VehicleCopiesDTO.VehicleID);

                        command.Parameters.AddWithValue("@AvailabilityStatus", VehicleCopiesDTO.AvailabilityStatus);

                        command.Parameters.AddWithValue("@CreateByUserID", VehicleCopiesDTO.CreateByUserID);

                        var outputIdParam = new SqlParameter("@NewVehicleCopiesId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        ID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }
            return ID;
        }

        public static async Task<bool> UpdateVehicleCopiesAsync(VehicleCopiesDTO VehicleCopiesDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))

                {
                    string query = "[SP_Car_UpdateVehicleCopies]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@CopyID", VehicleCopiesDTO.CopyID);

                        command.Parameters.AddWithValue("@VehicleID", VehicleCopiesDTO.VehicleID);

                        command.Parameters.AddWithValue("@AvailabilityStatus", VehicleCopiesDTO.AvailabilityStatus);

                        command.Parameters.AddWithValue("@CreateByUserID", VehicleCopiesDTO.CreateByUserID);

                        await connection.OpenAsync(); // Asynchronously open the connection
                        rowsAffected = await command.ExecuteNonQueryAsync(); // Asynchronously execute the command
                    }
                }
            }

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }

            return (rowsAffected > 0);

        }
        public static async Task<bool> DeleteVehicleCopies(int CopyID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_DeleteVehicleCopies]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CopyID", CopyID);

                        await connection.OpenAsync();
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }

            return (rowsAffected == 1);
        }

        public static async Task<bool> IsVehicleCopiesExist(int CopyID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_IsVehicleCopiesExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CopyID", CopyID);

                        await connection.OpenAsync(); ;
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0);
                    }
                }
            }

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }

            return isFound;
        }
        public static bool IsVehicleAvalbel(int VehicleID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_IsVehicleAvailable]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@VehicleID", VehicleID);

                         connection.Open(); ;
                        var result =  command.ExecuteScalar();
                        isFound = (result != null && (int)result > 0);
                    }
                }
            }

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }

            return isFound;
        }

        public static async Task<List<VehicleCopiesDTO>> GetAllVehicleCopies()
        {
            var VehicleCopiesList = new List<VehicleCopiesDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetAllVehicleCopies]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                VehicleCopiesList.Add(new VehicleCopiesDTO(
                    reader.GetInt32(reader.GetOrdinal("CopyID")), reader.GetInt32(reader.GetOrdinal("VehicleID")), reader.GetBoolean(reader.GetOrdinal("AvailabilityStatus")), reader.GetInt32(reader.GetOrdinal("CreateByUserID"))
                                ));
                            }
                        }
                    }
                }
            }

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }
            return VehicleCopiesList;
        }


    }

}