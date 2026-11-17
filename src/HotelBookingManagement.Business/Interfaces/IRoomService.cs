using System;
using System.Collections.Generic;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Interfaces;

/// <summary>
/// Hợp đồng dịch vụ nghiệp vụ Quản lý Phòng và Loại phòng (TV02 - ROOM01 -> ROOM06)
/// </summary>
public interface IRoomService
{
	#region ROOM01 - Quản lý Loại phòng (RoomType)

	/// <summary>
	/// Lấy tất cả loại phòng
	/// </summary>
	IEnumerable<RoomType> GetAllRoomTypes();

	/// <summary>
	/// Lấy thông tin loại phòng theo ID
	/// </summary>
	RoomType? GetRoomTypeById(int roomTypeId);

	/// <summary>
	/// Thêm mới loại phòng (có validation tên, sức chứa > 0, giá >= 0)
	/// </summary>
	int CreateRoomType(RoomType roomType);

	/// <summary>
	/// Cập nhật thông tin loại phòng
	/// </summary>
	bool UpdateRoomType(RoomType roomType);

	/// <summary>
	/// Xóa loại phòng (kiểm tra ràng buộc phòng liên quan)
	/// </summary>
	bool DeleteRoomType(int roomTypeId);

	#endregion

	#region ROOM02 & ROOM06 - Quản lý Phòng & Validation (Room CRUD & Validation)

	/// <summary>
	/// Lấy danh sách toàn bộ các phòng kèm thông tin loại phòng
	/// </summary>
	IEnumerable<Room> GetAllRooms();

	/// <summary>
	/// Lấy thông tin chi tiết một phòng theo mã ID
	/// </summary>
	Room? GetRoomById(int roomId);

	/// <summary>
	/// Lấy phòng theo số phòng
	/// </summary>
	Room? GetRoomByNumber(string roomNumber);

	/// <summary>
	/// Thêm mới phòng (thực thi validation: chống trùng số phòng, tầng > 0, loại phòng hợp lệ)
	/// </summary>
	int CreateRoom(Room room);

	/// <summary>
	/// Cập nhật thông tin phòng
	/// </summary>
	bool UpdateRoom(Room room);

	/// <summary>
	/// Xóa phòng khỏi hệ thống
	/// </summary>
	bool DeleteRoom(int roomId);

	#endregion

	#region ROOM03 - Quản lý Trạng thái phòng (Room Status Management & Transition)

	/// <summary>
	/// Cập nhật nhanh trạng thái phòng (AVAILABLE, RESERVED, OCCUPIED, CLEANING, MAINTENANCE)
	/// </summary>
	bool UpdateRoomStatus(int roomId, string newStatus);

	/// <summary>
	/// Kiểm tra hợp lệ chu trình chuyển đổi trạng thái phòng theo Algorithm 3
	/// </summary>
	bool ValidateStatusTransition(string currentStatus, string newStatus, out string errorMessage);

	/// <summary>
	/// Lấy tổng kết số lượng phòng theo từng trạng thái (phục vụ thống kê nhanh & Dashboard - DASH02)
	/// </summary>
	(int Total, int Available, int Reserved, int Occupied, int Cleaning, int Maintenance) GetRoomStatusSummary();

	#endregion

	#region ROOM04 & ROOM05 - Tìm kiếm & Lọc phòng (Search & Multi-criteria Filter)

	/// <summary>
	/// Tìm kiếm phòng theo số phòng hoặc mô tả
	/// </summary>
	IEnumerable<Room> SearchRooms(string keyword);

	/// <summary>
	/// Lọc danh sách phòng theo Loại phòng, Trạng thái và Tầng
	/// </summary>
	IEnumerable<Room> FilterRooms(int? roomTypeId, string? status, int? floor);

	/// <summary>
	/// Lấy danh sách các tầng thực tế đang có phòng
	/// </summary>
	IEnumerable<int> GetDistinctFloors();

	/// <summary>
	/// Lấy danh sách phòng sẵn sàng (AVAILABLE)
	/// </summary>
	IEnumerable<Room> GetAvailableRooms(int? roomTypeId = null);

	/// <summary>
	/// Lấy danh sách phòng trống trong khoảng thời gian (BR-02 Overlap checking)
	/// </summary>
	IEnumerable<Room> GetAvailableRoomsByDateRange(DateTime checkIn, DateTime checkOut, int? roomTypeId = null);

	#endregion
}
