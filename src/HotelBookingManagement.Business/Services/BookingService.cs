using System;
using System.Collections.Generic;
using System.Linq;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Data.Repositories;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IRoomRepository _roomRepo;

        public BookingService() : this(new BookingRepository(), new RoomRepository())
        {
        }

        public BookingService(IBookingRepository bookingRepo, IRoomRepository roomRepo)
        {
            _bookingRepo = bookingRepo ?? throw new ArgumentNullException(nameof(bookingRepo));
            _roomRepo = roomRepo ?? throw new ArgumentNullException(nameof(roomRepo));
        }

        // --- TASK BOOK05 & BOOK06: Tạo Booking và chặn trùng lịch ---
        public Booking? CreateBooking(Booking newBooking, List<int> selectedRoomIds)
        {
            if (newBooking == null || selectedRoomIds == null || selectedRoomIds.Count == 0)
                return null;

            if (newBooking.CheckOutDate.Date <= newBooking.CheckInDate.Date)
                throw new InvalidOperationException("Ngày Check-out phải lớn hơn ngày Check-in!");

            // Kiểm tra chống trùng lịch (BOOK05) cho từng phòng trước khi lưu
            foreach (int roomId in selectedRoomIds.Distinct())
            {
                if (_bookingRepo.HasOverlappingBooking(roomId, newBooking.CheckInDate, newBooking.CheckOutDate))
                {
                    throw new InvalidOperationException($"Phòng mã #{roomId} vừa có người đặt trong khoảng thời gian này. Vui lòng làm mới danh sách phòng!");
                }
            }

            if (_bookingRepo is BookingRepository concreteRepo)
            {
                return concreteRepo.CreateBooking(newBooking, selectedRoomIds);
            }

            return null;
        }

        // --- TASK BOOK04: Lấy danh sách phòng trống ---
        public List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int roomTypeId)
        {
            int? filterTypeId = roomTypeId > 0 ? roomTypeId : null;
            return _roomRepo.GetAvailableRoomsByDateRange(checkInDate, checkOutDate, filterTypeId).ToList();
        }

        // --- TASK BOOK05: Kiểm tra phòng có trống trong khoảng ngày không ---
        public bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            if (roomId <= 0 || checkOutDate <= checkInDate)
                return false;

            var room = _roomRepo.GetById(roomId);
            if (room == null || !string.Equals(room.Status, "AVAILABLE", StringComparison.OrdinalIgnoreCase))
                return false;

            return !_bookingRepo.HasOverlappingBooking(roomId, checkInDate, checkOutDate);
        }

        // --- Tính toán chi phí dự kiến ---
        public decimal EstimateBookingCost(List<int> roomIds, DateTime checkInDate, DateTime checkOutDate)
        {
            if (roomIds == null || roomIds.Count == 0 || checkOutDate <= checkInDate)
                return 0m;

            int nights = Math.Max(1, (checkOutDate.Date - checkInDate.Date).Days);
            decimal totalPerNight = 0m;

            foreach (int roomId in roomIds.Distinct())
            {
                var room = _roomRepo.GetById(roomId);
                if (room?.RoomType != null)
                {
                    totalPerNight += room.RoomType.PricePerNight;
                }
            }

            return totalPerNight * nights;
        }

        // --- TASK BOOK07: Hủy Booking ---
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