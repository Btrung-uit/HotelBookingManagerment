using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace HotelBookingManagement.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly string _connectionString = DatabaseConnection.ConnectionString;

        public DataTable GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Câu lệnh SQL lọc ra các phòng TRỐNG (Không nằm trong các Booking bị trùng lịch)
                string query = @"
                    SELECT r.RoomId, r.RoomNumber, rt.TypeName, rt.PricePerNight, r.Status
                    FROM dbo.Rooms r
                    JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                    WHERE r.Status = 'AVAILABLE' 
                      AND r.RoomId NOT IN (
                          SELECT bd.RoomId
                          FROM dbo.BookingDetails bd
                          JOIN dbo.Bookings b ON bd.BookingId = b.BookingId
                          WHERE b.Status != 'CANCELLED'
                            AND (b.CheckInDate < @CheckOut AND b.CheckOutDate > @CheckIn)
                      )";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CheckIn", checkInDate);
                    cmd.Parameters.AddWithValue("@CheckOut", checkOutDate);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}