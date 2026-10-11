using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingManagement.Models;

public class RoomType
{
	public int RoomId { get; set; }
	public string Name { get; set; } = string.Empty; // Standdard Single, Deluxe Family,...
	public int Capacity { get; set; } // Sức chứa
	public decimal PricePerNight { get; set; } // Giá mỗi đêm
	public string? Description { get; set; } // Mô tả tiện ích
}
