using System;
using System.Collections.Generic;
using System.Linq;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Data.Repositories;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Services;

/// <summary>
/// Cài đặt xử lý logic nghiệp vụ cho module Quản lý Phòng & Loại phòng (TV02 - ROOM01 -> ROOM06)
/// Thực thi validation chặt chẽ (ROOM06) và quy tắc chuyển đổi trạng thái (ROOM03 - Algorithm 3).
/// </summary>
public class RoomService : IRoomService
{
	private readonly IRoomRepository _roomRepository;
	private readonly IRoomTypeRepository _roomTypeRepository;

	private static readonly HashSet<string> ValidStatuses = new(StringComparer.OrdinalIgnoreCase)
	{
		"AVAILABLE", "RESERVED", "OCCUPIED", "CLEANING", "MAINTENANCE"
	};

	/// <summary>
	/// Constructor hỗ trợ Dependency Injection với đầy đủ Repository
	/// </summary>
	public RoomService(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository)
	{
		_roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
		_roomTypeRepository = roomTypeRepository ?? throw new ArgumentNullException(nameof(roomTypeRepository));
	}

	/// <summary>
	/// Constructor mặc định phục vụ WinForms khởi tạo nhanh
	/// </summary>
	public RoomService() : this(new RoomRepository(), new RoomTypeRepository())
	{
	}

	#region ROOM01 - Quản lý Loại phòng (RoomType)

	public IEnumerable<RoomType> GetAllRoomTypes()
	{
		return _roomTypeRepository.GetAll();
	}

	public RoomType? GetRoomTypeById(int roomTypeId)
	{
		if (roomTypeId <= 0) return null;
		return _roomTypeRepository.GetById(roomTypeId);
	}

	public int CreateRoomType(RoomType roomType)
	{
		ValidateRoomType(roomType, isUpdate: false);
		return _roomTypeRepository.Add(roomType);
	}

	public bool UpdateRoomType(RoomType roomType)
	{
		if (roomType.RoomTypeId <= 0)
		{
			throw new ArgumentException("Mã loại phòng không hợp lệ để cập nhật.");
		}

		ValidateRoomType(roomType, isUpdate: true);
		return _roomTypeRepository.Update(roomType);
	}

	public bool DeleteRoomType(int roomTypeId)
	{
		if (roomTypeId <= 0)
		{
			throw new ArgumentException("Mã loại phòng không hợp lệ để xóa.");
		}

		var existing = _roomTypeRepository.GetById(roomTypeId);
		if (existing == null)
		{
			throw new InvalidOperationException($"Không tìm thấy loại phòng có mã #{roomTypeId} để xóa.");
		}

		return _roomTypeRepository.Delete(roomTypeId);
	}

	private void ValidateRoomType(RoomType roomType, bool isUpdate)
	{
		if (roomType == null)
		{
			throw new ArgumentNullException(nameof(roomType), "Dữ liệu loại phòng không được để trống.");
		}

		if (string.IsNullOrWhiteSpace(roomType.Name))
		{
			throw new ArgumentException("Tên loại phòng không được để trống.");
		}

		roomType.Name = roomType.Name.Trim();
		if (roomType.Name.Length > 50)
		{
			throw new ArgumentException("Tên loại phòng không được vượt quá 50 ký tự.");
		}

		if (roomType.Capacity <= 0)
		{
			throw new ArgumentException("Sức chứa loại phòng phải lớn hơn 0 người.");
		}

		if (roomType.PricePerNight < 0)
		{
			throw new ArgumentException("Đơn giá mỗi đêm không được là số âm.");
		}

		int? excludeId = isUpdate ? roomType.RoomTypeId : null;
		if (_roomTypeRepository.ExistsByName(roomType.Name, excludeId))
		{
			throw new ArgumentException($"Tên loại phòng '{roomType.Name}' đã tồn tại trong hệ thống! Vui lòng chọn tên khác.");
		}
	}

	#endregion

	#region ROOM02 & ROOM06 - Quản lý Phòng & Validation (Room CRUD)

	public IEnumerable<Room> GetAllRooms()
	{
		return _roomRepository.GetAll();
	}

	public Room? GetRoomById(int roomId)
	{
		if (roomId <= 0) return null;
		return _roomRepository.GetById(roomId);
	}

	public Room? GetRoomByNumber(string roomNumber)
	{
		if (string.IsNullOrWhiteSpace(roomNumber)) return null;
		return _roomRepository.GetByRoomNumber(roomNumber.Trim());
	}

	public int CreateRoom(Room room)
	{
		ValidateRoom(room, isUpdate: false);
		return _roomRepository.Add(room);
	}

	public bool UpdateRoom(Room room)
	{
		if (room.RoomId <= 0)
		{
			throw new ArgumentException("Mã phòng không hợp lệ để cập nhật.");
		}

		ValidateRoom(room, isUpdate: true);
		return _roomRepository.Update(room);
	}

	public bool DeleteRoom(int roomId)
	{
		if (roomId <= 0)
		{
			throw new ArgumentException("Mã phòng không hợp lệ để xóa.");
		}

		var existing = _roomRepository.GetById(roomId);
		if (existing == null)
		{
			throw new InvalidOperationException($"Không tìm thấy phòng có mã #{roomId} để xóa.");
		}

		if (string.Equals(existing.Status, "OCCUPIED", StringComparison.OrdinalIgnoreCase))
		{
			throw new InvalidOperationException($"Không thể xóa phòng {existing.RoomNumber} vì phòng đang có khách ở (OCCUPIED)!");
		}

		if (string.Equals(existing.Status, "RESERVED", StringComparison.OrdinalIgnoreCase))
		{
			throw new InvalidOperationException($"Không thể xóa phòng {existing.RoomNumber} vì phòng đang có lịch đặt trước (RESERVED)!");
		}

		return _roomRepository.Delete(roomId);
	}

	private void ValidateRoom(Room room, bool isUpdate)
	{
		if (room == null)
		{
			throw new ArgumentNullException(nameof(room), "Dữ liệu phòng không được để trống.");
		}

		if (string.IsNullOrWhiteSpace(room.RoomNumber))
		{
			throw new ArgumentException("Số phòng không được để trống.");
		}

		room.RoomNumber = room.RoomNumber.Trim().ToUpperInvariant();
		if (room.RoomNumber.Length > 10)
		{
			throw new ArgumentException("Số phòng không được vượt quá 10 ký tự.");
		}

		if (room.Floor <= 0)
		{
			throw new ArgumentException("Số tầng phải lớn hơn 0 (ví dụ: Tầng 1, 2, 3...).");
		}

		if (room.RoomTypeId <= 0)
		{
			throw new ArgumentException("Vui lòng chọn loại phòng hợp lệ.");
		}

		var roomType = _roomTypeRepository.GetById(room.RoomTypeId);
		if (roomType == null)
		{
			throw new ArgumentException($"Loại phòng có mã #{room.RoomTypeId} không tồn tại trong hệ thống.");
		}

		if (string.IsNullOrWhiteSpace(room.Status))
		{
			room.Status = "AVAILABLE";
		}
		else
		{
			room.Status = room.Status.Trim().ToUpperInvariant();
		}

		if (!ValidStatuses.Contains(room.Status))
		{
			throw new ArgumentException($"Trạng thái phòng '{room.Status}' không hợp lệ. Các trạng thái hợp lệ gồm: AVAILABLE, RESERVED, OCCUPIED, CLEANING, MAINTENANCE.");
		}

		int? excludeId = isUpdate ? room.RoomId : null;
		if (_roomRepository.ExistsByRoomNumber(room.RoomNumber, excludeId))
		{
			throw new ArgumentException($"Số phòng '{room.RoomNumber}' đã tồn tại trong hệ thống! Vui lòng chọn số phòng khác.");
		}
	}

	#endregion

	#region ROOM03 - Quản lý Trạng thái phòng (Room Status Management & Transition)

	public bool UpdateRoomStatus(int roomId, string newStatus)
	{
		if (roomId <= 0)
		{
			throw new ArgumentException("Mã phòng không hợp lệ.");
		}

		if (string.IsNullOrWhiteSpace(newStatus))
		{
			throw new ArgumentException("Trạng thái phòng mới không được để trống.");
		}

		newStatus = newStatus.Trim().ToUpperInvariant();
		if (!ValidStatuses.Contains(newStatus))
		{
			throw new ArgumentException($"Trạng thái '{newStatus}' không hợp lệ.");
		}

		var currentRoom = _roomRepository.GetById(roomId);
		if (currentRoom == null)
		{
			throw new InvalidOperationException($"Không tìm thấy phòng có mã #{roomId}.");
		}

		if (string.Equals(currentRoom.Status, newStatus, StringComparison.OrdinalIgnoreCase))
		{
			return true; // Trạng thái đã trùng khớp, không cần cập nhật
		}

		if (!ValidateStatusTransition(currentRoom.Status, newStatus, out string errorMessage))
		{
			throw new InvalidOperationException(errorMessage);
		}

		return _roomRepository.UpdateStatus(roomId, newStatus);
	}

	public bool ValidateStatusTransition(string currentStatus, string newStatus, out string errorMessage)
	{
		errorMessage = string.Empty;
		currentStatus = currentStatus?.Trim().ToUpperInvariant() ?? string.Empty;
		newStatus = newStatus?.Trim().ToUpperInvariant() ?? string.Empty;

		if (currentStatus == newStatus) return true;

		// Kiểm tra quy tắc Algorithm 3 - Room Status Transition:
		// AVAILABLE -> RESERVED, OCCUPIED, MAINTENANCE
		// RESERVED -> OCCUPIED, AVAILABLE, MAINTENANCE
		// OCCUPIED -> CLEANING, MAINTENANCE (đặc biệt không được nhảy thẳng về AVAILABLE mà chưa dọn dẹp)
		// CLEANING -> AVAILABLE, MAINTENANCE
		// MAINTENANCE -> AVAILABLE, CLEANING

		switch (currentStatus)
		{
			case "AVAILABLE":
				if (newStatus is "RESERVED" or "OCCUPIED" or "MAINTENANCE") return true;
				errorMessage = $"Không thể chuyển phòng từ Sẵn sàng ({currentStatus}) sang {newStatus}.";
				return false;

			case "RESERVED":
				if (newStatus is "OCCUPIED" or "AVAILABLE" or "MAINTENANCE") return true;
				errorMessage = $"Phòng đang đặt trước ({currentStatus}) chỉ có thể nhận phòng (OCCUPIED), hủy đặt (AVAILABLE) hoặc báo bảo trì (MAINTENANCE).";
				return false;

			case "OCCUPIED":
				if (newStatus is "CLEANING" or "MAINTENANCE") return true;
				if (newStatus == "AVAILABLE")
				{
					errorMessage = "Quy tắc nghiệp vụ: Phòng đang có khách ở (OCCUPIED) sau khi trả phòng PHẢI chuyển sang Dọn dẹp (CLEANING) trước khi Sẵn sàng đón khách mới!";
					return false;
				}
				errorMessage = $"Phòng đang có khách ở ({currentStatus}) không thể chuyển trực tiếp sang {newStatus}.";
				return false;

			case "CLEANING":
				if (newStatus is "AVAILABLE" or "MAINTENANCE") return true;
				errorMessage = $"Phòng đang dọn dẹp ({currentStatus}) sau khi hoàn tất chỉ có thể chuyển sang Sẵn sàng (AVAILABLE) hoặc Bảo trì (MAINTENANCE).";
				return false;

			case "MAINTENANCE":
				if (newStatus is "AVAILABLE" or "CLEANING") return true;
				errorMessage = $"Phòng đang bảo trì ({currentStatus}) sau khi sửa xong chỉ có thể chuyển sang Sẵn sàng (AVAILABLE) hoặc Dọn dẹp (CLEANING).";
				return false;

			default:
				return true;
		}
	}

	public (int Total, int Available, int Reserved, int Occupied, int Cleaning, int Maintenance) GetRoomStatusSummary()
	{
		var allRooms = _roomRepository.GetAll().ToList();
		int total = allRooms.Count;
		int available = allRooms.Count(r => string.Equals(r.Status, "AVAILABLE", StringComparison.OrdinalIgnoreCase));
		int reserved = allRooms.Count(r => string.Equals(r.Status, "RESERVED", StringComparison.OrdinalIgnoreCase));
		int occupied = allRooms.Count(r => string.Equals(r.Status, "OCCUPIED", StringComparison.OrdinalIgnoreCase));
		int cleaning = allRooms.Count(r => string.Equals(r.Status, "CLEANING", StringComparison.OrdinalIgnoreCase));
		int maintenance = allRooms.Count(r => string.Equals(r.Status, "MAINTENANCE", StringComparison.OrdinalIgnoreCase));

		return (total, available, reserved, occupied, cleaning, maintenance);
	}

	#endregion

	#region ROOM04 & ROOM05 - Tìm kiếm & Lọc phòng

	public IEnumerable<Room> SearchRooms(string keyword)
	{
		if (string.IsNullOrWhiteSpace(keyword))
		{
			return GetAllRooms();
		}

		string term = keyword.Trim();
		var allRooms = GetAllRooms();

		return allRooms.Where(r =>
			r.RoomNumber.Contains(term, StringComparison.OrdinalIgnoreCase) ||
			(r.Description != null && r.Description.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
			(r.RoomType != null && r.RoomType.Name.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
			r.Status.Contains(term, StringComparison.OrdinalIgnoreCase));
	}

	public IEnumerable<Room> FilterRooms(int? roomTypeId, string? status, int? floor)
	{
		return _roomRepository.GetByFilter(roomTypeId, status, floor);
	}

	public IEnumerable<int> GetDistinctFloors()
	{
		return _roomRepository.GetAll()
			.Select(r => r.Floor)
			.Distinct()
			.OrderBy(f => f);
	}

	public IEnumerable<Room> GetAvailableRooms(int? roomTypeId = null)
	{
		return _roomRepository.GetAvailableRooms(roomTypeId);
	}

	public IEnumerable<Room> GetAvailableRoomsByDateRange(DateTime checkIn, DateTime checkOut, int? roomTypeId = null)
	{
		if (checkOut <= checkIn)
		{
			throw new ArgumentException("Ngày trả phòng (Check-out) phải sau ngày nhận phòng (Check-in).");
		}

		return _roomRepository.GetAvailableRoomsByDateRange(checkIn, checkOut, roomTypeId);
	}

	#endregion
}
