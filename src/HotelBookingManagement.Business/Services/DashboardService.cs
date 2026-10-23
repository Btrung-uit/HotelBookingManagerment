using System;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Services;

public class DashboardService : IDashboardService
{
	private readonly IDashboardRepository _repository;

	public DashboardService(IDashboardRepository repository)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
	}

	public DashboardStats GetStats()
	{
		return _repository.GetDashboardStats();
	}
}
