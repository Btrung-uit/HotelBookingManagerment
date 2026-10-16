using System;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Interfaces
{
    public interface ICheckInService
    {
        Booking GetBookingForCheckIn(int bookingId);
        bool ProcessCheckIn(int bookingId, DateTime actualCheckInTime);
        decimal CalculateFinalBill(int bookingId, DateTime actualCheckOutTime);
        bool ProcessCheckOut(int bookingId, DateTime actualCheckOutTime);
    }
}