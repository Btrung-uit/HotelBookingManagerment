using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingManagement.Models;

public class Room
{
	public int RoomId { get; set; }
	public string RoomNumber { get; set; } = string.Empty; // '101', '102'
	public int RoomTypeId { get; set; } // Khóa ngoại trỏ sang RoomType
	public string Status { get; set; } = string.Empty; // AVAILABLE, RESERVED, OCCUPIED, CLEANING, MAINTENANCE
	public string? Description { get; set; } // Mô tả

	// Thuộc tính tiện ích để hiển thị  tên loại phòng trên giao diện WinForm
	public RoomType? RoomType { get; set; }
}
