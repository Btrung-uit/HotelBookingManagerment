using System;
using System.Collections.Generic;

namespace HotelBookingManagement.Models;

/// <summary>
/// Đại diện cho bảng dbo.Rooms - Danh mục phòng
/// </summary>
public class Room
{
	public int RoomId { get; set; }
	public string RoomNumber { get; set; } = string.Empty; // '101', '102'
	public int RoomTypeId { get; set; } // Khóa ngoại trỏ sang RoomTypes
	public int Floor { get; set; } // Tầng: 1, 2, 3...
	public string Status { get; set; } = "AVAILABLE"; // AVAILABLE, RESERVED, OCCUPIED, CLEANING, MAINTENANCE
	public string? Description { get; set; } // Mô tả

	// Navigation Properties
	public RoomType? RoomType { get; set; }
	public List<BookingDetail> BookingDetails { get; set; } = new();
}
