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

        public bool CancelBooking(int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}