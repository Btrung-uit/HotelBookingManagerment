using System;
using System.Collections.Generic;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Data.Repositories;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        public BookingService()
        {
            _bookingRepo = new BookingRepository();
        }

        // --- TASK BOOK06 (Bạn phụ trách) ---
        public Booking CreateBooking(Booking newBooking, List<int> selectedRoomIds)
        {
            return _bookingRepo.CreateBooking(newBooking, selectedRoomIds);
        }

        // --- CÁC HÀM CỦA LEADER VÀ TEAM (Tạm để trống để không báo lỗi) ---
        public List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int roomTypeId)
        {
            throw new NotImplementedException();
        }

        public bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            throw new NotImplementedException();
        }

        public decimal EstimateBookingCost(List<int> roomIds, DateTime checkInDate, DateTime checkOutDate)
        {
            throw new NotImplementedException();
        }

        public bool CancelBooking(int bookingId, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (bookingId <= 0)
            {
                errorMessage = "Mã đặt phòng không hợp lệ.";
                return false;
            }

            try
            {
                var currentBooking = _bookingRepo.GetById(bookingId);
                if (currentBooking == null)
                {
                    errorMessage = "Không tìm thấy thông tin đơn đặt phòng.";
                    return false;
                }

                // Chặn các trạng thái không được phép hủy
                if (currentBooking.Status == "CANCELLED")
                {
                    errorMessage = "Đơn đặt phòng này đã được hủy từ trước.";
                    return false;
                }
                if (currentBooking.Status == "CHECKED_IN" || currentBooking.Status == "COMPLETED")
                {
                    errorMessage = "Không thể hủy vì phòng đang được sử dụng hoặc giao dịch đã hoàn tất.";
                    return false;
                }

                // Nếu hợp lệ, tiến hành gọi xuống tầng Data để cập nhật
                bool success = _bookingRepo.UpdateStatus(bookingId, "CANCELLED");
                if (!success)
                {
                    errorMessage = "Lỗi cập nhật trạng thái hủy vào cơ sở dữ liệu.";
                }
                return success;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi hệ thống: {ex.Message}";
                return false;
            }
        }
        public IEnumerable<Booking> GetAll()
        {
            return _bookingRepo.GetAll();
        }
    }
}