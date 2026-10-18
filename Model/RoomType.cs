
using System.Collections.Generic;

namespace HotelBookingManagement.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string? Description { get; set; }

        public ICollection<Room> Rooms { get; set; }
            = new List<Room>();
    }
}
