using System;
using System.Data;
using Microsoft.Data.SqlClient;
using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Repositories;

public class UserRepository : IUserRepository
{
	public Users? GetByUsername(string username)
	{
		const string query = @"
		SELECT UserId, Username, PasswordHash, FullName, Role, IsActive, CreatedAt
		FROM dbo.Users
		WHERE Username = @Username";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		// Dùng Sql Parameter để chống triệt để tấng công SQL Injection
		command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;

		connection.Open();
		using var reader = command.ExecuteReader();
		if (reader.Read())
		{
			return MapReaderToUser(reader);
		}

		return null;
	}

	public Users? GetById(int userId)
	{
		const string query = @"
		SELECT UserId, Username, PasswordHash, FullName, Role, IsActive, CreatedAt
		FROM dbo.Users
		WHERE UserId = @UserId";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

		connection.Open();
		using var reader = command.ExecuteReader();
		if (reader.Read())
		{
			return MapReaderToUser(reader);
		}

		return null;
	}
	// Helper map dữ liệu từ SqlDataReader sang Object Model Users
	private static Users MapReaderToUser(SqlDataReader reader)
	{
		return new Users
		{
			UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
			Username = reader.GetString(reader.GetOrdinal("Username")),
			PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
			FullName = reader.GetString(reader.GetOrdinal("Fullname")),
			Role = reader.GetString(reader.GetOrdinal("Role")),
			IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
			CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
		};
	}
}
