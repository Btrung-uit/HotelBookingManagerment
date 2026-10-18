
using System;

namespace HotelBookingManagement.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;

        public Booking? Booking { get; set; }
    }
}
