using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data.SqlClient;

namespace DVLD_with_Car_Rental_DataAccessLayer
{

  public  class clsComboBoxInfo
    {
        public int FuelTypeID { get; set; }
        public int CarCategoryID { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
      public  string categoryName,  fuleType,  Make,  ModelName;
    }
    public class VehiclesDTO
    {
        public int VehicleID { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int FuelTypeID { get; set; }
        public string PlateNumber { get; set; }
        public int CarCategoryID { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public int CreatedByUserID { get; set; }
        public string VehicleName { get; set; }
        public string VehicleImage { get; set; }


        public VehiclesDTO(int VehicleID, int MakeID, int ModelID, int Year, int Mileage, int FuelTypeID, string PlateNumber, int CarCategoryID, decimal RentalPricePerDay, int CreatedByUserID, string VehicleName, string VehicleImage)
        {
            this.VehicleID = VehicleID;
            this.MakeID = MakeID;
            this.ModelID = ModelID;
            this.Year = Year;
            this.Mileage = Mileage;
            this.FuelTypeID = FuelTypeID;
            this.PlateNumber = PlateNumber;
            this.CarCategoryID = CarCategoryID;
            this.RentalPricePerDay = RentalPricePerDay;
            this.CreatedByUserID = CreatedByUserID;
            this.VehicleName = VehicleName;
            this.VehicleImage = VehicleImage;


        }


    }

    public static class clsVehiclesDataAccess

    {
        public static async Task<VehiclesDTO> GetVehiclesInfoByID(int VehicleID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetVehiclesByID]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@VehicleID", VehicleID);

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return new VehiclesDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("VehicleID")),
                                    reader.GetInt32(reader.GetOrdinal("MakeID")),
                                    reader.GetInt32(reader.GetOrdinal("ModelID")),
                                    reader.GetInt32(reader.GetOrdinal("Year")),
                                    reader.GetInt32(reader.GetOrdinal("Mileage")),
                                    reader.GetInt32(reader.GetOrdinal("FuelTypeID")),
                                    reader.GetString(reader.GetOrdinal("PlateNumber")),
                                    reader.GetInt32(reader.GetOrdinal("CarCategoryID")),
                                    reader.GetDecimal(reader.GetOrdinal("RentalPricePerDay")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                    reader.GetString(reader.GetOrdinal("VehicleName")),
                                    reader.IsDBNull(reader.GetOrdinal("VehicleImage"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("VehicleImage"))
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
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return null;
        }

        public static VehiclesDTO GetVehiclesInfoByIDWithoutAsync(int VehicleID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetVehiclesByID]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@VehicleID", VehicleID);

                         connection.Open();

                        using (SqlDataReader reader =  command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new VehiclesDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("VehicleID")),
                                    reader.GetInt32(reader.GetOrdinal("MakeID")),
                                    reader.GetInt32(reader.GetOrdinal("ModelID")),
                                    reader.GetInt32(reader.GetOrdinal("Year")),
                                    reader.GetInt32(reader.GetOrdinal("Mileage")),
                                    reader.GetInt32(reader.GetOrdinal("FuelTypeID")),
                                    reader.GetString(reader.GetOrdinal("PlateNumber")),
                                    reader.GetInt32(reader.GetOrdinal("CarCategoryID")),
                                    reader.GetDecimal(reader.GetOrdinal("RentalPricePerDay")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                    reader.GetString(reader.GetOrdinal("VehicleName")),
                                    reader.IsDBNull(reader.GetOrdinal("VehicleImage"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("VehicleImage"))
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
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return null;
        }

        public static VehiclesDTO GetVehiclesInfoByIDWithOutAsync(int VehicleID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetVehiclesByID]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@VehicleID", VehicleID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new VehiclesDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("VehicleID")),
                                    reader.GetInt32(reader.GetOrdinal("MakeID")),
                                    reader.GetInt32(reader.GetOrdinal("ModelID")),
                                    reader.GetInt32(reader.GetOrdinal("Year")),
                                    reader.GetInt32(reader.GetOrdinal("Mileage")),
                                    reader.GetInt32(reader.GetOrdinal("FuelTypeID")),
                                    reader.GetString(reader.GetOrdinal("PlateNumber")),
                                    reader.GetInt32(reader.GetOrdinal("CarCategoryID")),
                                    reader.GetDecimal(reader.GetOrdinal("RentalPricePerDay")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                    reader.GetString(reader.GetOrdinal("VehicleName")),
                                    reader.IsDBNull(reader.GetOrdinal("VehicleImage"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("VehicleImage"))
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
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return null;
        }

        public static async Task<int> AddVehicleAsync(VehiclesDTO vehicleDTO)
        {
            int newVehicleId = -1; // القيمة الافتراضية للمعرف الجديد في حالة فشل الإدخال

            try
            {
                // إنشاء اتصال بقاعدة البيانات باستخدام سلسلة الاتصال من الإعدادات
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_AddVehicles]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MakeID", vehicleDTO.MakeID);
                        command.Parameters.AddWithValue("@ModelID", vehicleDTO.ModelID);
                        command.Parameters.AddWithValue("@Year", vehicleDTO.Year);
                        command.Parameters.AddWithValue("@Mileage", vehicleDTO.Mileage);
                        command.Parameters.AddWithValue("@FuelTypeID", vehicleDTO.FuelTypeID);
                        command.Parameters.AddWithValue("@PlateNumber", vehicleDTO.PlateNumber);
                        command.Parameters.AddWithValue("@CarCategoryID", vehicleDTO.CarCategoryID);
                        command.Parameters.AddWithValue("@RentalPricePerDay", vehicleDTO.RentalPricePerDay);
                        command.Parameters.AddWithValue("@CreatedByUserID", vehicleDTO.CreatedByUserID);
                        command.Parameters.AddWithValue("@VehicleName", vehicleDTO.VehicleName);

                        if (!string.IsNullOrEmpty(vehicleDTO.VehicleImage))
                        {
                            command.Parameters.AddWithValue("@VehicleImage", vehicleDTO.VehicleImage);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@VehicleImage", DBNull.Value);
                        }

                        // إعداد معامل الإخراج لتلقي المعرف الجديد
                        var outputIdParam = new SqlParameter("@NewVehiclesId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        // فتح الاتصال بقاعدة البيانات وتنفيذ الإجراء المخزن
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        // تعيين المعرف الجديد من معامل الإخراج
                        newVehicleId = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // معالجة الخطأ (يمكنك تخصيص المعالجة لتناسب احتياجاتك)
                clsErrorHandling.HandleError(ex.ToString());
            }

            return newVehicleId;
        }



        public static async Task<bool> UpdateVehiclesAsync(VehiclesDTO VehiclesDTO)
        {
            int returnVal = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_UpdateVehicles]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@VehicleID", VehiclesDTO.VehicleID);
                        command.Parameters.AddWithValue("@MakeID", VehiclesDTO.MakeID);
                        command.Parameters.AddWithValue("@ModelID", VehiclesDTO.ModelID);
                        command.Parameters.AddWithValue("@Year", VehiclesDTO.Year);
                        command.Parameters.AddWithValue("@Mileage", VehiclesDTO.Mileage);
                        command.Parameters.AddWithValue("@FuelTypeID", VehiclesDTO.FuelTypeID);
                        command.Parameters.AddWithValue("@PlateNumber", VehiclesDTO.PlateNumber);
                        command.Parameters.AddWithValue("@CarCategoryID", VehiclesDTO.CarCategoryID);
                        command.Parameters.AddWithValue("@RentalPricePerDay", VehiclesDTO.RentalPricePerDay);
                        command.Parameters.AddWithValue("@CreatedByUserID", VehiclesDTO.CreatedByUserID);
                        command.Parameters.AddWithValue("@VehicleName", VehiclesDTO.VehicleName);

                        if (VehiclesDTO.VehicleImage != null)
                        {
                            command.Parameters.AddWithValue("@VehicleImage", VehiclesDTO.VehicleImage);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@VehicleImage", DBNull.Value);
                        }

                        SqlParameter returnValue = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnValue);

                        await connection.OpenAsync(); 
                        try
                        {
                            await command.ExecuteNonQueryAsync(); 
                            returnVal = (int)returnValue.Value;
                        }
                        finally
                        {
                            connection.Close(); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return (returnVal > 0);
        }

        public static async Task<bool> DeleteVehicles(int VehicleID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_DeleteVehicles]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@VehicleID", VehicleID);

                        await connection.OpenAsync();
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }

            return (rowsAffected == 1);
        }

        public static async Task<bool> IsVehiclesExist(int VehicleID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_IsVehiclesExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@VehicleID", VehicleID);

                        await connection.OpenAsync(); ;
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0);
                    }
                }
            }

            catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }

            return isFound;
        }

        public static async Task<List<VehiclesDTO>> GetAllVehicles()
        {
            var vehiclesList = new List<VehiclesDTO>();
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetAllVehicles]";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                vehiclesList.Add(new VehiclesDTO(
                                    reader.GetInt32(reader.GetOrdinal("VehicleID")),
                                    reader.GetInt32(reader.GetOrdinal("MakeID")),
                                    reader.GetInt32(reader.GetOrdinal("ModelID")),
                                    reader.GetInt32(reader.GetOrdinal("Year")),
                                    reader.GetInt32(reader.GetOrdinal("Mileage")),
                                    reader.GetInt32(reader.GetOrdinal("FuelTypeID")),
                                    reader.GetString(reader.GetOrdinal("PlateNumber")),
                                    reader.GetInt32(reader.GetOrdinal("CarCategoryID")),
                                    reader.GetDecimal(reader.GetOrdinal("RentalPricePerDay")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                    reader.GetString(reader.GetOrdinal("VehicleName")),
                                    reader.IsDBNull(reader.GetOrdinal("VehicleImage"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("VehicleImage"))
                                        ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
                return null;
            }

            return vehiclesList.Count > 0 ? vehiclesList : null;
        }

      
        public static async Task<DataTable> GetAllVehiclesCarCategory()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetAllVehicleCategories]";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
                return null;
            }

            return dt;
        }
        public static async Task<DataTable> GetAllVehiclesFuleTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetAllFuleTypes]";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
                return null;
            }

            return dt;
        }
       
        public static async Task<DataTable> GetAllVehiclesMakes()
        {
            DataTable dt=new DataTable();
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetAllMakes]";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
                return null;
            }

            return dt;

        }
        public static async Task<clsComboBoxInfo> GetComboBoxInfoForVehicleAsync(clsComboBoxInfo comboBoxInfoParameters)
        {
            clsComboBoxInfo comboBoxInfo = new clsComboBoxInfo();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                {
                    string query = "[SP_Car_GetComboBoxInfoForVehicle]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CategoryName", comboBoxInfoParameters.categoryName);
                        command.Parameters.AddWithValue("@FuleType", comboBoxInfoParameters.fuleType);
                        command.Parameters.AddWithValue("@Make", comboBoxInfoParameters.Make);
                        command.Parameters.AddWithValue("@ModelName", comboBoxInfoParameters.ModelName);

                        var isExistParam = new SqlParameter("@IsExist", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isExistParam);

                        var categoryIdParam = new SqlParameter("@CategoryID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(categoryIdParam);

                        var fuleTypeIdParam = new SqlParameter("@FuleTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(fuleTypeIdParam);

                        var MakeIdParam = new SqlParameter("@MakeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(MakeIdParam);


                        var ModelIdParam = new SqlParameter("@ModelID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(ModelIdParam);
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        if ((bool)isExistParam.Value)
                        {
                            comboBoxInfo.CarCategoryID = Convert.ToInt32(categoryIdParam.Value);
                            comboBoxInfo.FuelTypeID = Convert.ToInt32(fuleTypeIdParam.Value);
                            comboBoxInfo.MakeID = Convert.ToInt32(MakeIdParam.Value);
                            comboBoxInfo.ModelID = Convert.ToInt32(ModelIdParam.Value);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return comboBoxInfo;
        }



    }
}