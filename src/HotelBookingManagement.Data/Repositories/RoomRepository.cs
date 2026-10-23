using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace HotelBookingManagement.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly string _connectionString = DatabaseConnection.ConnectionString;

        public int Add(Room room)
        {
            throw new NotImplementedException();
        }

        public int CountByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int roomId)
        {
            throw new NotImplementedException();
        }

        public bool ExistsByRoomNumber(string roomNumber, int? excludeRoomId = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetAll()
        {
            throw new NotImplementedException();
        }

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

        public IEnumerable<Room> GetAvailableRooms(int? roomTypeId = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetAvailableRoomsByDateRange(DateTime checkIn, DateTime checkOut, int? roomTypeId = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetByFilter(int? roomTypeId = null, string? status = null, int? floor = null)
        {
            throw new NotImplementedException();
        }

        public Room? GetById(int roomId)
        {
            throw new NotImplementedException();
        }

        public Room? GetByRoomNumber(string roomNumber)
        {
            throw new NotImplementedException();
        }

        public bool Update(Room room)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(int roomId, string newStatus)
        {
            throw new NotImplementedException();
        }
    }
}