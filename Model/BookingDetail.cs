
namespace HotelBookingManagement.Models
{
    public class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public decimal PricePerNight { get; set; }
        public int NumberOfNights { get; set; }
        public decimal Subtotal { get; set; }

        public Booking? Booking { get; set; }
        public Room? Room { get; set; }
    }
}
