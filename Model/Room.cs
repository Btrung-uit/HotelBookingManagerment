
using System.Collections.Generic;

namespace HotelBookingManagement.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Description { get; set; }

        public RoomType? RoomType { get; set; }

        public ICollection<BookingDetail> BookingDetails { get; set; }
            = new List<BookingDetail>();
    }
}
