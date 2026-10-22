using System;
using System.Collections.Generic;

namespace HotelBookingManagement.Models;

/// <summary>
/// Đại diện cho bảng dbo.Bookings - Đặt phòng
/// </summary>
public class Booking
{
	public int BookingId { get; set; }
	public int CustomerId { get; set; }
	public DateTime BookingDate { get; set; } = DateTime.Now;
	public DateTime CheckInDate { get; set; }
	public DateTime CheckOutDate { get; set; }
	public string Status { get; set; } = "PENDING"; // PENDING, CONFIRMED, CHECKED_IN, COMPLETED, CANCELLED
	public decimal TotalAmount { get; set; } = 0m;
	public int CreatedBy { get; set; } // UserId của nhân viên tạo đơn
	public DateTime CreatedAt { get; set; } = DateTime.Now;

	// Navigation Properties
	public Customer? Customer { get; set; }
	public User? CreatedByUser { get; set; }

	// Chi tiết đặt phòng và danh sách thanh toán
	public List<BookingDetail> BookingDetails { get; set; } = new();
	public List<Payment> Payments { get; set; } = new();
}