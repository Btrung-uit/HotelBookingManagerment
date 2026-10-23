using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace HotelBookingManagement.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly string _connectionString = DatabaseConnection.ConnectionString;

        public int Add(Booking booking)
        {
            throw new NotImplementedException();
        }

        public int AddWithDetails(Booking booking, IReadOnlyCollection<BookingDetail> details)
        {
            throw new NotImplementedException();
        }

        public Booking CreateBooking(Booking newBooking, List<int> selectedRoomIds)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Thêm vào bảng Bookings
                        string queryBooking = @"
                            INSERT INTO dbo.Bookings (CustomerId, CheckInDate, CheckOutDate, TotalPrice, Status, CreatedAt)
                            OUTPUT INSERTED.BookingId
                            VALUES (@CustomerId, @CheckIn, @CheckOut, @TotalPrice, 'PENDING', GETDATE());";

                        using (SqlCommand cmd = new SqlCommand(queryBooking, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@CustomerId", newBooking.CustomerId);
                            cmd.Parameters.AddWithValue("@CheckIn", newBooking.CheckInDate);
                            cmd.Parameters.AddWithValue("@CheckOut", newBooking.CheckOutDate);
                            cmd.Parameters.AddWithValue("@TotalPrice", newBooking.TotalAmount);

                            newBooking.BookingId = (int)cmd.ExecuteScalar();
                        }

                        // 2. Thêm vào bảng BookingDetails (Cho danh sách nhiều phòng)
                        string queryDetail = @"
                            INSERT INTO dbo.BookingDetails (BookingId, RoomId, PriceAtBooking)
                            VALUES (@BookingId, @RoomId, @PriceAtBooking);";

                        // Tạm tính giá trung bình mỗi phòng để lưu vào chi tiết
                        decimal pricePerRoom = newBooking.TotalAmount / selectedRoomIds.Count;

                        foreach (int roomId in selectedRoomIds)
                        {
                            using (SqlCommand cmdDetail = new SqlCommand(queryDetail, conn, transaction))
                            {
                                cmdDetail.Parameters.AddWithValue("@BookingId", newBooking.BookingId);
                                cmdDetail.Parameters.AddWithValue("@RoomId", roomId);
                                cmdDetail.Parameters.AddWithValue("@PriceAtBooking", pricePerRoom);
                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return newBooking; // Trả về object Booking kèm ID vừa tạo thành công
                    }
                    catch
                    {
                        transaction.Rollback(); // Có lỗi sẽ tự động hủy thao tác bảo vệ DB
                        return null;
                    }
                }
            }
        }

        public IEnumerable<Booking> GetAll()
        {
            var bookings = new List<Booking>();
            string query = "SELECT * FROM dbo.Bookings ORDER BY BookingId DESC";

            using var connection = DatabaseConnection.GetConnection();
            using var command = new SqlCommand(query, connection);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var booking = new Booking
                {
                    BookingId = Convert.ToInt32(reader["BookingId"]),
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                    CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                    CheckOutDate = Convert.ToDateTime(reader["CheckOutDate"]),
                    Status = reader["Status"].ToString(),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                };
                bookings.Add(booking);
            }

            return bookings;
        }

        public IEnumerable<int> GetBookedRoomIdsByDateRange(DateTime checkIn, DateTime checkOut, int? excludeBookingId = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public Booking? GetById(int bookingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingDetail> GetDetailsByBookingId(int bookingId)
        {
            throw new NotImplementedException();
        }

        public bool HasOverlappingBooking(int roomId, DateTime checkIn, DateTime checkOut, int? excludeBookingId = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(Booking booking)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(int bookingId, string status)
        {
            const string query = @"
        UPDATE dbo.Bookings
        SET Status = @Status, 
            UpdatedAt = GETDATE()
        WHERE BookingId = @BookingId;";

            using var connection = DatabaseConnection.GetConnection();
            using var command = new SqlCommand(query, connection);

            command.Parameters.Add("@BookingId", SqlDbType.Int).Value = bookingId;
            command.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = status.Trim();

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }
}