using System;
using System.Collections.Generic;

namespace HotelBookingManagement.Models;

/// <summary>
/// Đại diện cho bảng dbo.Customers - Khách hàng
/// </summary>
public class Customer
{
	public int CustomerId { get; set; }
	public string FullName { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public string? Email { get; set; }
	public string IdentityNumber { get; set; } = string.Empty; // CCCD / Hộ chiếu
	public string? Address { get; set; }
	public DateTime? DateOfBirth { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;

	// Navigation Property: Danh sách các đơn đặt phòng của khách hàng
	public List<Booking> Bookings { get; set; } = new();
}

/// <summary>
/// Lớp tương thích ngược cho tên cũ Customers (để không phá vỡ code hiện hữu)
/// </summary>
[Obsolete("Sử dụng Customer thay vì Customers theo chuẩn đặt tên số ít")]
public class Customers : Customer
{
}
