using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace HotelBookingManagement.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        // Gọi chuỗi kết nối từ cấu hình chung của team
        private readonly string _connectionString = DatabaseConnection.ConnectionString;

        public Customers GetCustomerByPhoneOrIdentity(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT CustomerId, FullName, Phone, Email, IdentityNumber, Address, DateOfBirth, CreatedAt 
                                 FROM dbo.Customers 
                                 WHERE Phone = @Keyword OR IdentityNumber = @Keyword";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", keyword);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customers 
                            {
                                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                FullName = reader["FullName"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                IdentityNumber = reader["IdentityNumber"].ToString(),

                                // Ép kiểu an toàn cho các trường cho phép NULL
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : string.Empty,
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? (DateTime?)reader["DateOfBirth"] : null,
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}