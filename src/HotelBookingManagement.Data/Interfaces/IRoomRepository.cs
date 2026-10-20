using System;
using System.Data;

namespace HotelBookingManagement.Data.Interfaces
{
    public interface IRoomRepository
    {
        DataTable GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate);
    }
}