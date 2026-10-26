using System;
using System.Data;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Data.Repositories;
using HotelBookingManagement.Business.Interfaces;

namespace HotelBookingManagement.Business.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepo;

        public RoomService()
        {
            _roomRepo = new RoomRepository();
        }

        public DataTable GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            return _roomRepo.GetAvailableRooms(checkInDate, checkOutDate);
        }
    }
}