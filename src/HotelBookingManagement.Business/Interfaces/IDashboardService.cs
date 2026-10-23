using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Interfaces;

public interface IDashboardService
{
	DashboardStats GetStats();
}
