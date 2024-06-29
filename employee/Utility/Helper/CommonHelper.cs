using employee.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Helper
{
    public class CommonHelper
    {
        private IConfiguration _config;
        public CommonHelper(IConfiguration config)
        {
            _config = config;
        }

        public int DMLTransaction(string query) {
            int Result;
            string connectionString = _config["ConnectionStrings:DefaultConnection"];

            using (SqlConnection connection = new SqlConnection(connectionString)) { 
                connection.Open();

                string sql = query;
                SqlCommand command = new SqlCommand(sql, connection);
                Result = command.ExecuteNonQuery();
                connection.Close();
            }
            return Result;
        }

        public EmployeeVM GetUserByEmail(string query, string email, string password)
        {
            EmployeeVM user = null;
            string connectionString = _config["ConnectionStrings:DefaultConnection"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new EmployeeVM
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"])
                        };
                    }
                }
                connection.Close();
            }

            return user;
        }

        // Modified to accept parameters
        public SingleEntity GetEntityById(string query, int roleId)
        {
            SingleEntity singleEntity = null;
            string connectionString = _config["ConnectionStrings:DefaultConnection"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoleId", roleId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        singleEntity = new SingleEntity
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                        };
                    }
                }
                connection.Close();
            }

            return singleEntity;
        }
        public List<Training> GetTrainings()
        {
            List<Training> trainings = new List<Training>();

            string connectionString = _config["ConnectionStrings:DefaultConnection"];
            string query = "SELECT TrainingId, TrainingName FROM Trainings"; // Adjust query as per your database schema

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Training training = new Training
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("TrainingId")),
                        Name = reader.GetString(reader.GetOrdinal("TrainingName"))
                        // Map other properties as needed
                    };

                    trainings.Add(training);
                }

                reader.Close();
            }

            return trainings;
        }

        public bool UserAlreadyExist(string query) { 
                bool flag = false;

                string connectionString = _config["ConnectionStrings:DefaultConnection"];

                using (SqlConnection connection = new SqlConnection(connectionString)) { 
                    connection.Open();

                    string sql = query;
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) { 
                        flag = true;
                    }
                    connection.Close();
                }
                return flag;
            }
        }
}
