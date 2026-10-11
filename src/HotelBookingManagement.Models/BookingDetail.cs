using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingManagement.Models;

public class BookingDetail
{
	public int BookingDetailId { get; set; }
	public int BookingId { get; set; }
	public int RoomId { get; set; }
	public decimal PricePerNight { get; set; }
	public decimal Subtotal { get; set; } // PricePerNight * NumberOfNights

	// Navigation Property: Để lấy thông tin số phòng, loại phòng khi hiển thị lên Form
	public Room? Room { get; set; }
}
