using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingManagement.Models;

public class Room
{
	public int RoomId { get; set; }
	public string RoomNumber { get; set; } = string.Empty; // '101', '102'
	public int RoomTypeId { get; set; } // Khóa ngoại trỏ sang RoomType
	public int Floor { get; set; } // Tầng: 1, 2, 3...
	public string Satus { get; set; } // AVAILABLE, "RESERVED, OCCUPIED, CLEANING, MAINTAINANCE
	public string? Desrciption { get; set; } // Mô tả

	// Thuộc tính tiện ích để hiển thị  tên loại phòng trên giao diện WinForm
	public RoomType? RoomType { get; set; }
}
