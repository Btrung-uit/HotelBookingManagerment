using System;
using System.Security.Cryptography;
using System.Text;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Services;

/// <summary>
/// Xử lý toàn bộ logic nghiệp vụ đăng nhập, mã hóa mật khẩu và phân quyền.
/// </summary>
public class AuthService : IAuthService
{
	private readonly IUserRepository _userRepository;

	// Kỹ thuật Constructor Dependency Injection:
	// Nhận IUserRepository từ bên ngoài truyền vào, giúp tách biệt hoàn toàn Bussiness và Data
	public AuthService(IUserRepository userRepository)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	}

	public bool Login(string username, string password, out string errorMessage)
	{
		errorMessage = string.Empty;

		// Bước 1: Validate dữ liệu đầu vào cơ bản
		if (string.IsNullOrWhiteSpace(username))
		{
			errorMessage = "Vui lòng nhập tên đăng nhâp!";
			return false;
		}

		if (string.IsNullOrWhiteSpace(password))
		{
			errorMessage = "Vui lòng nhập mật khẩu!";
			return false;
		}

		// Bước 2: Gọi tầng Data để truy vấn thông tin User từ Database
		var user = _userRepository.GetByUsername(username.Trim());
		if (user == null)
		{
			// Dùng chung câu thông báo để ngăn chặn kẻ xấu dò quét username
			errorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác!";
			return false;
		}

		// Bước 3: Kiểm tra trạng thái tài khoản có bị khóa không
		if (!user.IsActive)
		{
			errorMessage = "Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ Quản lý!";
			return false;
		}

		// Bước 4: Băm mật khẩu vừa nhập ra SHA-256 và so khớp với PasswordHash trong Database
		string hashedPassword = HashPassword(password);
		if (!string.Equals(user.PasswordHash, hashedPassword, StringComparison.OrdinalIgnoreCase))
		{
			errorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác!";
			return false;
		}

		// Bước 5: Đăng nhập thành công -> Thiết lập phiên đăng nhập toàn cục
		UserSession.Login(user);
		return true;
	}

	/// <summary>
	/// Thuật toán băm mật khẩu SHA-256 (khớp 100% với hàm HASHBYTES trong SQL Server)
	/// </summary>
	public string HashPassword(string rawPassword)
	{
		using var sha256 = SHA256.Create();
		byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
		
		// Convert.ToHexString trả về chuỗi Hex chữ hoa (64 ký tự)
		return Convert.ToHexString(bytes);
	}

}
