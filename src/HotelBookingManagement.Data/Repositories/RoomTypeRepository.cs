using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Repositories;

/// <summary>
/// Cài đặt truy xuất dữ liệu cho bảng dbo.RoomTypes bằng ADO.NET thuần (TV02 - ROOM01)
/// </summary>
public class RoomTypeRepository : IRoomTypeRepository
{
	public IEnumerable<RoomType> GetAll()
	{
		var list = new List<RoomType>();
		const string query = @"
			SELECT RoomTypeId, Name, Capacity, PricePerNight, Description
			FROM dbo.RoomTypes
			ORDER BY RoomTypeId ASC;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			list.Add(MapReaderToRoomType(reader));
		}

		return list;
	}

	public RoomType? GetById(int roomTypeId)
	{
		const string query = @"
			SELECT RoomTypeId, Name, Capacity, PricePerNight, Description
			FROM dbo.RoomTypes
			WHERE RoomTypeId = @RoomTypeId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = roomTypeId;

		connection.Open();
		using var reader = command.ExecuteReader();
		return reader.Read() ? MapReaderToRoomType(reader) : null;
	}

	public RoomType? GetByName(string name)
	{
		const string query = @"
			SELECT RoomTypeId, Name, Capacity, PricePerNight, Description
			FROM dbo.RoomTypes
			WHERE Name = @Name;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name.Trim();

		connection.Open();
		using var reader = command.ExecuteReader();
		return reader.Read() ? MapReaderToRoomType(reader) : null;
	}

	public int Add(RoomType roomType)
	{
		const string query = @"
			INSERT INTO dbo.RoomTypes (Name, Capacity, PricePerNight, Description)
			VALUES (@Name, @Capacity, @PricePerNight, @Description);
			SELECT SCOPE_IDENTITY();";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = roomType.Name.Trim();
		command.Parameters.Add("@Capacity", SqlDbType.Int).Value = roomType.Capacity;
		command.Parameters.Add("@PricePerNight", SqlDbType.Decimal).Value = roomType.PricePerNight;
		command.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = (object?)roomType.Description?.Trim() ?? DBNull.Value;

		connection.Open();
		object? result = command.ExecuteScalar();
		return result != null && int.TryParse(result.ToString(), out int newId) ? newId : -1;
	}

	public bool Update(RoomType roomType)
	{
		const string query = @"
			UPDATE dbo.RoomTypes
			SET Name = @Name,
			    Capacity = @Capacity,
			    PricePerNight = @PricePerNight,
			    Description = @Description
			WHERE RoomTypeId = @RoomTypeId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = roomType.RoomTypeId;
		command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = roomType.Name.Trim();
		command.Parameters.Add("@Capacity", SqlDbType.Int).Value = roomType.Capacity;
		command.Parameters.Add("@PricePerNight", SqlDbType.Decimal).Value = roomType.PricePerNight;
		command.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = (object?)roomType.Description?.Trim() ?? DBNull.Value;

		connection.Open();
		return command.ExecuteNonQuery() > 0;
	}

	public bool Delete(int roomTypeId)
	{
		// Kiểm tra nếu có phòng nào đang thuộc loại phòng này thì không cho phép xóa (tránh vi phạm khóa ngoại)
		const string checkQuery = @"SELECT COUNT(*) FROM dbo.Rooms WHERE RoomTypeId = @RoomTypeId;";
		const string deleteQuery = @"DELETE FROM dbo.RoomTypes WHERE RoomTypeId = @RoomTypeId;";

		using var connection = DatabaseConnection.GetConnection();
		connection.Open();

		using (var checkCmd = new SqlCommand(checkQuery, connection))
		{
			checkCmd.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = roomTypeId;
			int roomCount = Convert.ToInt32(checkCmd.ExecuteScalar());
			if (roomCount > 0)
			{
				throw new InvalidOperationException($"Không thể xóa loại phòng này vì đang có {roomCount} căn phòng thuộc loại phòng này!");
			}
		}

		using (var deleteCmd = new SqlCommand(deleteQuery, connection))
		{
			deleteCmd.Parameters.Add("@RoomTypeId", SqlDbType.Int).Value = roomTypeId;
			return deleteCmd.ExecuteNonQuery() > 0;
		}
	}

	public bool ExistsByName(string name, int? excludeRoomTypeId = null)
	{
		string query = @"
			SELECT COUNT(*) 
			FROM dbo.RoomTypes 
			WHERE LOWER(Name) = LOWER(@Name)";

		if (excludeRoomTypeId.HasValue)
		{
			query += " AND RoomTypeId <> @ExcludeId;";
		}

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name.Trim();

		if (excludeRoomTypeId.HasValue)
		{
			command.Parameters.Add("@ExcludeId", SqlDbType.Int).Value = excludeRoomTypeId.Value;
		}

		connection.Open();
		return Convert.ToInt32(command.ExecuteScalar()) > 0;
	}

	private static RoomType MapReaderToRoomType(SqlDataReader reader)
	{
		return new RoomType
		{
			RoomTypeId = reader.GetInt32(reader.GetOrdinal("RoomTypeId")),
			Name = reader.GetString(reader.GetOrdinal("Name")),
			Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
			PricePerNight = reader.GetDecimal(reader.GetOrdinal("PricePerNight")),
			Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"))
		};
	}
}
