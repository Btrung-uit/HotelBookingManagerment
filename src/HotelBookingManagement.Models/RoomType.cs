using System;
using System.Collections.Generic;

namespace HotelBookingManagement.Models;

/// <summary>
/// Đại diện cho bảng dbo.RoomTypes - Loại phòng
/// </summary>
public class RoomType
{
	public int RoomTypeId { get; set; }
	public string Name { get; set; } = string.Empty; // Standard Single, Deluxe Family,...
	public int Capacity { get; set; } // Sức chứa (> 0)
	public decimal PricePerNight { get; set; } // Giá mỗi đêm (>= 0)
	public string? Description { get; set; } // Mô tả tiện ích

	// Navigation Property: Danh sách phòng thuộc loại phòng này
	public List<Room> Rooms { get; set; } = new();
}
