using HotelBookingManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingManagement.Data.Interfaces;

public interface IDashboardRepository
{
	DashboardStats GetDashboardStats();
}
