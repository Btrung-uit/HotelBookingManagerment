using System;
using Microsoft.Data.SqlClient;
using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;
using System.Drawing.Drawing2D;

namespace HotelBookingManagement.Data.Repositories;

public class DashboardRepository : IDashboardRepository
{
	public DashboardStats GetDashboardStats()
	{
		var stats = new DashboardStats();

		const string query = @"
			SELECT
				(SELECT COUNT(*) FROM dbo.Rooms) AS TotalRooms,
				(SELECT COUNT(*) FROM dbo.Rooms WHERE Status = 'AVAILABLE') AS AvailableRooms,
				(SELECT COUNT(*) FROM dbo.Bookings) AS TotalBookings,
				(SELECT COUNT(*) FROM dbo.Customers) AS TotalCustomers;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		connection.Open();
		using var reader = command.ExecuteReader();
		if (reader.Read())
		{
			stats.TotalRooms = reader.GetInt32(reader.GetOrdinal("TotalRooms"));
			stats.AvailableRooms = reader.GetInt32(reader.GetOrdinal("AvailableRooms"));
			stats.TotalBookings = reader.GetInt32(reader.GetOrdinal("TotalBookings"));
			stats.TotalCustomers = reader.GetInt32(reader.GetOrdinal("TotalCustomers"));
		}

		return stats;
	}
}
