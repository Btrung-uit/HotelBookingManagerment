using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingManagement.Models;

public class Customers
{
	public int CustomerId { get; set; }
	public string FullName { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public string? Email { get; set; } // ? cho phép null
	public string IdentityNumber { get; set; } = string.Empty; // CCCD / Hộ chiếu
	public string? Address { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
}
