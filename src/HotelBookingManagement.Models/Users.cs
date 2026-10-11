using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingManagement.Models;

public class Users
{
	public int UserId { get; set; }
	public string Username { get; set; } = string.Empty;
	public string PasswordHash { get; set; } = string.Empty;
	public string FullName { get; set; } = string.Empty;
	public string Role { get; set; } = string.Empty; // Manager or Receptionist
	public bool IsActive { get; set; } = true;
	public DateTime CreatedAt { get; set; } = DateTime.Now;
}
