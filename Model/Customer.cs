
using System;
using System.Collections.Generic;

namespace HotelBookingManagement.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        public ICollection<Booking> Bookings { get; set; }
            = new List<Booking>();
    }
}
