using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Repositories;

/// <summary>
/// Cài đặt truy xuất dữ liệu cho bảng dbo.Rooms bằng ADO.NET thuần (TV02 - ROOM02 -> ROOM06)
/// </summary>
public class RoomRepository : IRoomRepository
{
	public IEnumerable<Room> GetAll()
	{
		var list = new List<Room>();
		const string query = @"
			SELECT r.RoomId, r.RoomNumber, r.RoomTypeId, r.Floor, r.Status, r.Description,
			       rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight, rt.Description AS RoomTypeDescription
			FROM dbo.Rooms r
			INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
			ORDER BY r.Floor ASC, r.RoomNumber ASC;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			list.Add(MapReaderToRoom(reader));
		}

		return list;
	}

	public Room? GetById(int roomId)
	{
		const string query = @"
			SELECT r.RoomId, r.RoomNumber, r.RoomTypeId, r.Floor, r.Status, r.Description,
			       rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight, rt.Description AS RoomTypeDescription
			FROM dbo.Rooms r
			INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
			WHERE r.RoomId = @RoomId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;

		connection.Open();
		using var reader = command.ExecuteReader();
		return reader.Read() ? MapReaderToRoom(reader) : null;
	}

	public Room? GetByRoomNumber(string roomNumber)
	{
		const string query = @"
			SELECT r.RoomId, r.RoomNumber, r.RoomTypeId, r.Floor, r.Status, r.Description,
			       rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight, rt.Description AS RoomTypeDescription
			FROM dbo.Rooms r
			INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
			WHERE r.RoomNumber = @RoomNumber;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@RoomNumber", SqlDbType.VarChar, 10).Value = roomNumber.Trim();

		connection.Open();
		using var reader = command.ExecuteReader();
		return reader.Read() ? MapReaderToRoom(reader) : null;
	}

	public IEnumerable<Room> GetByFilter(int? roomTypeId = null, string? status = null, int? floor = null)
	{
		var list = new List<Room>();
		const string query = @"
			SELECT r.RoomId, r.RoomNumber, r.RoomTypeId, r.Floor, r.Status, r.Description,
			       rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight, rt.Description AS RoomTypeDescription
			FROM dbo.Rooms r
			INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
			WHERE (@RoomTypeId IS NULL OR r.RoomTypeId = @RoomTypeId)
			  AND (@Status IS NULL OR @Status = '' OR r.Status = @Status)
			  AND (@Floor IS NULL OR r.Floor = @Floor)
			ORDER BY r.Floor ASC, r.RoomNumber ASC;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = (object?)roomTypeId ?? DBNull.Value;
		command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(status) ? DBNull.Value : status.Trim();
		command.Parameters.Add("@Floor", SqlDbType.Int).Value = (object?)floor ?? DBNull.Value;

		connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			list.Add(MapReaderToRoom(reader));
		}

		return list;
	}

	public IEnumerable<Room> GetAvailableRooms(int? roomTypeId = null)
	{
		return GetByFilter(roomTypeId, "AVAILABLE", null);
	}

	public IEnumerable<Room> GetAvailableRoomsByDateRange(DateTime checkIn, DateTime checkOut, int? roomTypeId = null)
	{
		var list = new List<Room>();
		const string query = @"
			SELECT r.RoomId, r.RoomNumber, r.RoomTypeId, r.Floor, r.Status, r.Description,
			       rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight, rt.Description AS RoomTypeDescription
			FROM dbo.Rooms r
			INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
			WHERE r.Status <> 'MAINTENANCE'
			  AND (@RoomTypeId IS NULL OR r.RoomTypeId = @RoomTypeId)
			  AND r.RoomId NOT IN (
			      SELECT bd.RoomId
			      FROM dbo.BookingDetails bd
			      INNER JOIN dbo.Bookings b ON bd.BookingId = b.BookingId
			      WHERE b.Status IN ('PENDING', 'CONFIRMED', 'CHECKED_IN')
			        AND @CheckIn < b.CheckOutDate
			        AND @CheckOut > b.CheckInDate
			  )
			ORDER BY r.Floor ASC, r.RoomNumber ASC;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@CheckIn", SqlDbType.DateTime2).Value = checkIn;
		command.Parameters.Add("@CheckOut", SqlDbType.DateTime2).Value = checkOut;
		command.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = (object?)roomTypeId ?? DBNull.Value;

		connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			list.Add(MapReaderToRoom(reader));
		}

		return list;
	}

	public int Add(Room room)
	{
		const string query = @"
			INSERT INTO dbo.Rooms (RoomNumber, RoomTypeId, Floor, Status, Description)
			VALUES (@RoomNumber, @RoomTypeId, @Floor, @Status, @Description);
			SELECT SCOPE_IDENTITY();";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@RoomNumber", SqlDbType.VarChar, 10).Value = room.RoomNumber.Trim();
		command.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = room.RoomTypeId;
		command.Parameters.Add("@Floor", SqlDbType.Int).Value = room.Floor;
		command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(room.Status) ? "AVAILABLE" : room.Status.Trim();
		command.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = (object?)room.Description?.Trim() ?? DBNull.Value;

		connection.Open();
		object? result = command.ExecuteScalar();
		return result != null && int.TryParse(result.ToString(), out int newId) ? newId : -1;
	}

	public bool Update(Room room)
	{
		const string query = @"
			UPDATE dbo.Rooms
			SET RoomNumber = @RoomNumber,
			    RoomTypeId = @RoomTypeId,
			    Floor = @Floor,
			    Status = @Status,
			    Description = @Description
			WHERE RoomId = @RoomId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@RoomId", SqlDbType.Int).Value = room.RoomId;
		command.Parameters.Add("@RoomNumber", SqlDbType.VarChar, 10).Value = room.RoomNumber.Trim();
		command.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = room.RoomTypeId;
		command.Parameters.Add("@Floor", SqlDbType.Int).Value = room.Floor;
		command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = room.Status.Trim();
		command.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = (object?)room.Description?.Trim() ?? DBNull.Value;

		connection.Open();
		return command.ExecuteNonQuery() > 0;
	}

	public bool UpdateStatus(int roomId, string newStatus)
	{
		const string query = @"
			UPDATE dbo.Rooms
			SET Status = @Status
			WHERE RoomId = @RoomId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;
		command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = newStatus.Trim().ToUpperInvariant();

		connection.Open();
		return command.ExecuteNonQuery() > 0;
	}

	public bool Delete(int roomId)
	{
		// Kiểm tra nếu phòng đã phát sinh chi tiết đặt phòng (BookingDetails)
		const string checkQuery = @"SELECT COUNT(*) FROM dbo.BookingDetails WHERE RoomId = @RoomId;";
		const string deleteQuery = @"DELETE FROM dbo.Rooms WHERE RoomId = @RoomId;";

		using var connection = DatabaseConnection.GetConnection();
		connection.Open();

		using (var checkCmd = new SqlCommand(checkQuery, connection))
		{
			checkCmd.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;
			int count = Convert.ToInt32(checkCmd.ExecuteScalar());
			if (count > 0)
			{
				throw new InvalidOperationException($"Không thể xóa phòng này vì đã tồn tại {count} lịch sử/chi tiết đặt phòng liên quan! Bạn có thể chuyển trạng thái sang MAINTENANCE (Bảo trì) thay vì xóa.");
			}
		}

		using (var deleteCmd = new SqlCommand(deleteQuery, connection))
		{
			deleteCmd.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;
			return deleteCmd.ExecuteNonQuery() > 0;
		}
	}

	public bool ExistsByRoomNumber(string roomNumber, int? excludeRoomId = null)
	{
		string query = @"
			SELECT COUNT(*) 
			FROM dbo.Rooms 
			WHERE LOWER(RoomNumber) = LOWER(@RoomNumber)";

		if (excludeRoomId.HasValue)
		{
			query += " AND RoomId <> @ExcludeId;";
		}

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@RoomNumber", SqlDbType.VarChar, 10).Value = roomNumber.Trim();

		if (excludeRoomId.HasValue)
		{
			command.Parameters.Add("@ExcludeId", SqlDbType.Int).Value = excludeRoomId.Value;
		}

		connection.Open();
		return Convert.ToInt32(command.ExecuteScalar()) > 0;
	}

	public int CountByStatus(string status)
	{
		const string query = @"
			SELECT COUNT(*) 
			FROM dbo.Rooms 
			WHERE (@Status IS NULL OR @Status = '' OR Status = @Status);";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(status) ? DBNull.Value : status.Trim();

		connection.Open();
		return Convert.ToInt32(command.ExecuteScalar());
	}

	private static Room MapReaderToRoom(SqlDataReader reader)
	{
		var room = new Room
		{
			RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),
			RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
			RoomTypeId = reader.GetInt32(reader.GetOrdinal("RoomTypeId")),
			Floor = reader.GetInt32(reader.GetOrdinal("Floor")),
			Status = reader.GetString(reader.GetOrdinal("Status")),
			Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"))
		};

		// Nạp dữ liệu RoomType liên kết nếu có trong câu SELECT
		if (HasColumn(reader, "RoomTypeName") && !reader.IsDBNull(reader.GetOrdinal("RoomTypeName")))
		{
			room.RoomType = new RoomType
			{
				RoomTypeId = room.RoomTypeId,
				Name = reader.GetString(reader.GetOrdinal("RoomTypeName")),
				Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
				PricePerNight = reader.GetDecimal(reader.GetOrdinal("PricePerNight")),
				Description = reader.IsDBNull(reader.GetOrdinal("RoomTypeDescription")) ? null : reader.GetString(reader.GetOrdinal("RoomTypeDescription"))
			};
		}

		return room;
	}

	private static bool HasColumn(SqlDataReader reader, string columnName)
	{
		for (int i = 0; i < reader.FieldCount; i++)
		{
			if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}
}
