using System;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business;
/// <summary>
/// Quản lý phiên làm việc của người dùng hiện tại trong toàn bộ ứng dụng.
/// Sử dụng static class vì tại một thời điểm, chỉ có 1 nhân viên ngồi trước máy tính sử dụng app.
/// </summary>
public class UserSession
{
	// Dấu ? biểu thị có thể null (khi chưa ai đăng nhập)
	// private set để bảo vệ: chỉ cho phép gán thông qua hàm Login() bên dưới
	public static User? CurrentUser { get; private set; }

	// Kiểm tra xem hiện tại đã có người đăng nhập hay chưa
	public static bool IsLoggedIn => CurrentUser != null;

	// Kiểm tra nhanh người dùng có quyền Quản lý hay không	(Không phân biệt hoa thường)
	public static bool IsManager => CurrentUser?.Role.Equals("Manager", StringComparison.OrdinalIgnoreCase) == true;

	// Kiểm tra nhanh người dùng có quyền Tiếp tân hay không
	public static bool IsReceptionist => CurrentUser?.Role.Equals("Receptionist", StringComparison.OrdinalIgnoreCase) == true;

	// Thiết lập phiên làm việc khi người dùng đăng nhập thành công
	public static void Login(User user)
	{
		CurrentUser = user ?? throw new ArgumentNullException(nameof(user));
	}

	// Xóa phiên làm việc khi người dùng bấm Đăng xuất
	public static void Logout()
	{
		CurrentUser = null;
	}
}
