using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
            const string query = "SELECT COUNT(1) FROM dbo.Rooms WHERE Status = @Status;";
            using var conn = DatabaseConnection.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = status.Trim().ToUpperInvariant();
            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Delete(int roomId)
        {
            throw new NotImplementedException();
        }

        public bool ExistsByRoomNumber(string roomNumber, int? excludeRoomId = null)
        {
            const string query = @"
                SELECT COUNT(1) FROM dbo.Rooms 
                WHERE RoomNumber = @RoomNumber 
                  AND (@ExcludeId IS NULL OR RoomId != @ExcludeId);";
            using var conn = DatabaseConnection.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@RoomNumber", SqlDbType.VarChar, 10).Value = roomNumber.Trim();
            cmd.Parameters.Add("@ExcludeId", SqlDbType.Int).Value = (object?)excludeRoomId ?? DBNull.Value;
            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public IEnumerable<Room> GetAll()
        {
            return GetByFilter(null, null, null);
        }

        public DataTable GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();

                // Đã sửa rt.TypeName -> rt.Name AS TypeName khớp 100% với schema.sql
                string query = @"
                    SELECT r.RoomId, 
                           r.RoomNumber, 
                           rt.Name AS TypeName, 
                           rt.Capacity,
                           rt.PricePerNight, 
                           r.Floor,
                           r.Status
                    FROM dbo.Rooms r
                    INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                    WHERE r.Status = 'AVAILABLE' 
                      AND r.RoomId NOT IN (
                          SELECT bd.RoomId
                          FROM dbo.BookingDetails bd
                          INNER JOIN dbo.Bookings b ON bd.BookingId = b.BookingId
                          WHERE b.Status != 'CANCELLED'
                            AND (b.CheckInDate < @CheckOut AND b.CheckOutDate > @CheckIn)
                      )
                    ORDER BY r.RoomNumber ASC;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@CheckIn", SqlDbType.DateTime2).Value = checkInDate;
                    cmd.Parameters.Add("@CheckOut", SqlDbType.DateTime2).Value = checkOutDate;

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
            return GetByFilter(roomTypeId, "AVAILABLE", null);
        }

        public IEnumerable<Room> GetAvailableRoomsByDateRange(DateTime checkIn, DateTime checkOut, int? roomTypeId = null)
        {
            var list = new List<Room>();
            const string query = @"
                SELECT r.RoomId, r.RoomNumber, r.RoomTypeId, r.Floor, r.Status, r.Description,
                       rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight, rt.Description AS RoomTypeDescription
                FROM dbo.Rooms r
                INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                WHERE r.Status = 'AVAILABLE'
                  AND (@RoomTypeId IS NULL OR r.RoomTypeId = @RoomTypeId)
                  AND r.RoomId NOT IN (
                      SELECT bd.RoomId
                      FROM dbo.BookingDetails bd
                      INNER JOIN dbo.Bookings b ON bd.BookingId = b.BookingId
                      WHERE b.Status != 'CANCELLED'
                        AND (b.CheckInDate < @CheckOut AND b.CheckOutDate > @CheckIn)
                  )
                ORDER BY r.RoomNumber ASC;";

            using var conn = DatabaseConnection.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@CheckIn", SqlDbType.DateTime2).Value = checkIn;
            cmd.Parameters.Add("@CheckOut", SqlDbType.DateTime2).Value = checkOut;
            cmd.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = (object?)roomTypeId ?? DBNull.Value;

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReaderToRoom(reader));
            }
            return list;
        }

        public IEnumerable<Room> GetByFilter(int? roomTypeId = null, string? status = null, int? floor = null)
        {
            var list = new List<Room>();
            const string query = @"
                SELECT r.RoomId, r.RoomNumber, r.RoomTypeId, r.Floor, r.Status, r.Description,
                       rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight, rt.Description AS RoomTypeDescription
                FROM dbo.Rooms r
                INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                WHERE (@RoomTypeId IS NULL OR r.RoomTypeId = @RoomTypeId)
                  AND (@Status IS NULL OR r.Status = @Status)
                  AND (@Floor IS NULL OR r.Floor = @Floor)
                ORDER BY r.RoomNumber ASC;";

            using var conn = DatabaseConnection.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = (object?)roomTypeId ?? DBNull.Value;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(status) ? DBNull.Value : status.Trim().ToUpperInvariant();
            cmd.Parameters.Add("@Floor", SqlDbType.Int).Value = (object?)floor ?? DBNull.Value;

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReaderToRoom(reader));
            }
            return list;
        }

        public Room? GetById(int roomId)
        {
            const string query = @"
                SELECT r.RoomId, r.RoomNumber, r.RoomTypeId, r.Floor, r.Status, r.Description,
                       rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight, rt.Description AS RoomTypeDescription
                FROM dbo.Rooms r
                INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                WHERE r.RoomId = @RoomId;";

            using var conn = DatabaseConnection.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;

            conn.Open();
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapReaderToRoom(reader) : null;
        }

        public Room? GetByRoomNumber(string roomNumber)
        {
            const string query = @"
                SELECT r.RoomId, r.RoomNumber, r.RoomTypeId, r.Floor, r.Status, r.Description,
                       rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight, rt.Description AS RoomTypeDescription
                FROM dbo.Rooms r
                INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                WHERE r.RoomNumber = @RoomNumber;";

            using var conn = DatabaseConnection.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@RoomNumber", SqlDbType.VarChar, 10).Value = roomNumber.Trim();

            conn.Open();
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapReaderToRoom(reader) : null;
        }

        public bool Update(Room room)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(int roomId, string newStatus)
        {
            const string query = "UPDATE dbo.Rooms SET Status = @Status WHERE RoomId = @RoomId;";
            using var conn = DatabaseConnection.GetConnection();
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = newStatus.Trim().ToUpperInvariant();
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        private static Room MapReaderToRoom(SqlDataReader reader)
        {
            var roomType = new RoomType
            {
                RoomTypeId = reader.GetInt32(reader.GetOrdinal("RoomTypeId")),
                Name = reader.GetString(reader.GetOrdinal("RoomTypeName")),
                Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                PricePerNight = reader.GetDecimal(reader.GetOrdinal("PricePerNight")),
                Description = reader.IsDBNull(reader.GetOrdinal("RoomTypeDescription")) ? null : reader.GetString(reader.GetOrdinal("RoomTypeDescription"))
            };

            return new Room
            {
                RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),
                RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                RoomTypeId = roomType.RoomTypeId,
                Floor = reader.GetInt32(reader.GetOrdinal("Floor")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                RoomType = roomType
            };
        }
    }
}