
using System;
using System.Collections.Generic;

namespace HotelBookingManagement.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Customer? Customer { get; set; }
        public User? Creator { get; set; }

        public ICollection<BookingDetail> BookingDetails { get; set; }
            = new List<BookingDetail>();

        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
}
