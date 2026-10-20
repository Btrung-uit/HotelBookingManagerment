using System;
using System.Data;

namespace HotelBookingManagement.Business.Interfaces
{
    public interface IRoomService
    {
        DataTable GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate);
    }
}