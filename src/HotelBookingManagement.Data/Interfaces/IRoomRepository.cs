using System.Collections.Generic;
using HotelBookingManagement.Models;
using System;
using System.Data;

namespace HotelBookingManagement.Data.Interfaces;

/// <summary>
/// Hợp đồng truy xuất dữ liệu cho bảng dbo.Rooms (ADO.NET)
/// Phục vụ các nghiệp vụ: CRUD phòng, tìm kiếm theo số phòng, lọc theo tầng/loại/trạng thái, cập nhật trạng thái phòng.
/// </summary>
namespace HotelBookingManagement.Data.Interfaces
{
public interface IRoomRepository
{
	/// <summary>
	/// Lấy toàn bộ danh sách phòng (kèm thông tin RoomType để hiển thị)
	/// </summary>
	IEnumerable<Room> GetAll();

	/// <summary>
	/// Lấy thông tin phòng theo mã RoomId
	/// </summary>
	Room? GetById(int roomId);

	/// <summary>
	/// Lấy thông tin phòng theo số phòng (ví dụ: '101', '102')
	/// </summary>
	Room? GetByRoomNumber(string roomNumber);

	/// <summary>
	/// Lọc danh sách phòng theo các tiêu chí linh hoạt
	/// </summary>
	/// <param name="roomTypeId">Mã loại phòng (null nếu chọn tất cả)</param>
	/// <param name="status">Trạng thái phòng ('AVAILABLE', 'OCCUPIED'..., null nếu chọn tất cả)</param>
	/// <param name="floor">Tầng (null nếu chọn tất cả)</param>
	IEnumerable<Room> GetByFilter(int? roomTypeId = null, string? status = null, int? floor = null);

	/// <summary>
	/// Lấy danh sách các phòng đang ở trạng thái trống ('AVAILABLE')
	/// </summary>
	IEnumerable<Room> GetAvailableRooms(int? roomTypeId = null);

	/// <summary>
	/// Lấy danh sách các phòng trống và không bị trùng lịch đặt trong khoảng thời gian (BR-02)
	/// </summary>
	IEnumerable<Room> GetAvailableRoomsByDateRange(System.DateTime checkIn, System.DateTime checkOut, int? roomTypeId = null);

	/// <summary>
	/// Thêm mới một phòng vào hệ thống
	/// </summary>
	/// <returns>RoomId vừa sinh</returns>
	int Add(Room room);

	/// <summary>
	/// Cập nhật thông tin phòng (số phòng, loại phòng, tầng, mô tả)
	/// </summary>
	bool Update(Room room);

	/// <summary>
	/// Cập nhật nhanh trạng thái phòng (AVAILABLE, RESERVED, OCCUPIED, CLEANING, MAINTENANCE)
	/// </summary>
	bool UpdateStatus(int roomId, string newStatus);

	/// <summary>
	/// Xóa phòng theo mã ID
	/// </summary>
	bool Delete(int roomId);

	/// <summary>
	/// Kiểm tra số phòng đã tồn tại chưa (chống trùng lặp số phòng)
	/// </summary>
	bool ExistsByRoomNumber(string roomNumber, int? excludeRoomId = null);

	/// <summary>
	/// Đếm số lượng phòng theo trạng thái (phục vụ dashboard và thống kê)
	/// </summary>
	int CountByStatus(string status);
        DataTable GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate);
    }
}
