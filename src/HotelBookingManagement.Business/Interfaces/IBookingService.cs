using System;
using System.Collections.Generic;
using HotelBookingManagement.Models; 

namespace HotelBookingManagement.Business.Interfaces
{
    public interface IBookingService 
    {
        // 1.  Task BOOK04: Truy vấn phòng trống
        List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int roomTypeId);

        // 2. Task BOOK03, BOOK05: Kiểm tra ngày và chặn trùng lịch
        bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate);

        // 3.  Task BOOK08: Ước tính giá
        decimal EstimateBookingCost(List<int> roomIds, DateTime checkInDate, DateTime checkOutDate);

        // 4.  Task BOOK06: Tạo và lưu booking 
        Booking CreateBooking(Booking newBooking, List<int> selectedRoomIds);

        // 5. Task BOOK07: Hủy booking
        bool CancelBooking(int bookingId);
    }
}