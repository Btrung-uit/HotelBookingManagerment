using System;
using System.Collections.Generic;

namespace HotelBookingManagement.Models;

/// <summary>
/// Đại diện cho bảng dbo.BookingDetails - Chi tiết đặt phòng
/// </summary>
public class BookingDetail
{
	public int BookingDetailId { get; set; }
	public int BookingId { get; set; }
	public int RoomId { get; set; }
	public decimal PricePerNight { get; set; }
	public int NumberOfNights { get; set; } = 1; // Số đêm lưu trú (> 0)
	public decimal Subtotal { get; set; } // PricePerNight * NumberOfNights

	// Navigation Properties
	public Booking? Booking { get; set; }
	public Room? Room { get; set; }
}
