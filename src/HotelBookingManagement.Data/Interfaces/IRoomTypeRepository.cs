using System.Collections.Generic;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Interfaces;

/// <summary>
/// Hợp đồng truy xuất dữ liệu cho bảng dbo.RoomTypes (ADO.NET)
/// Phục vụ các nghiệp vụ: Quản lý danh mục loại phòng, cấu hình giá và sức chứa.
/// </summary>
public interface IRoomTypeRepository
{
	/// <summary>
	/// Lấy toàn bộ danh mục loại phòng
	/// </summary>
	IEnumerable<RoomType> GetAll();

	/// <summary>
	/// Lấy loại phòng theo mã ID
	/// </summary>
	RoomType? GetById(int roomTypeId);

	/// <summary>
	/// Lấy loại phòng theo tên
	/// </summary>
	RoomType? GetByName(string name);

	/// <summary>
	/// Thêm mới một loại phòng
	/// </summary>
	/// <returns>RoomTypeId vừa sinh (hoặc -1 nếu thất bại)</returns>
	int Add(RoomType roomType);

	/// <summary>
	/// Cập nhật thông tin loại phòng
	/// </summary>
	bool Update(RoomType roomType);

	/// <summary>
	/// Xóa loại phòng
	/// </summary>
	bool Delete(int roomTypeId);

	/// <summary>
	/// Kiểm tra tên loại phòng đã tồn tại chưa (chống trùng tên)
	/// </summary>
	bool ExistsByName(string name, int? excludeRoomTypeId = null);
}
