using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace HotelBookingManagement.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly string _connectionString = DatabaseConnection.ConnectionString;

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
    }
}