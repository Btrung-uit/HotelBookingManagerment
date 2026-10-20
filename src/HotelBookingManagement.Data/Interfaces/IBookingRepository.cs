using System.Collections.Generic;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Interfaces
{
    public interface IBookingRepository
    {
        Booking CreateBooking(Booking newBooking, List<int> selectedRoomIds);
    }
}