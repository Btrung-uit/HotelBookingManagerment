using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingManagement.Models;

public class Booking
{
	public int BookingId { get; set; }
	public int CustomerId { get; set; }
	public DateTime BookingDate { get; set; } = DateTime.Now;
	public DateTime CheckOutDate { get; set; }
	public DateTime CheckInDate { get; set; }
	public string Status { get; set; } = string.Empty; // PENDING, CONFIRMED, CHECKED_IN, COMPLETED, CANCELLED
	public decimal TotalAmount { get; set; } = 0;
	public int CreatedBy { get; set; } // Nhân viên tạo đơn
	public DateTime CreatedAt { get; set; } = DateTime.Now;

	// Navigation Properties hỗ trợ kết nối dữ liệu trực quan
	public Customers? Customers { get; set; }
	public Users? CreatedByUser { get; set; }

	// Một đơn đặt phòng có thể có nhiều phòng được chọn (Danh sách chi tiết):
	public List<BookingDetail> BookingDetails { get; set; } = new();
}