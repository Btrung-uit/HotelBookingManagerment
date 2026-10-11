namespace HotelBookingManagement.Business.Interfaces;

/// <summary>
/// Hợp đồng nghiệp vụ xác thực tài khoản và bảo mật mật khẩu.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Xác thực đăng nhập: kiểm tra username, password, mã hóa SHA256 và trạng thái IsActive.
    /// </summary>
    /// <param name="username">Tên tài khoản người dùng nhập</param>
    /// <param name="password">Mật khẩu thô (chưa băm)</param>
    /// <param name="errorMessage">Thông báo lỗi trả về cho giao diện nếu đăng nhập thất bại</param>
    /// <returns>True nếu đăng nhập thành công; False nếu thất bại</returns>
    bool Login(string username, string password, out string errorMessage);

    /// <summary>
    /// Băm mật khẩu người dùng nhập sang chuẩn mã hóa SHA-256 (64 ký tự Hex chữ hoa).
    /// Khớp 100% với hàm HASHBYTES('SHA2_256', ...) trong Database SQL Server.
    /// </summary>
    /// <param name="rawPassword">Mật khẩu thô</param>
    /// <returns>Chuỗi mã băm SHA-256</returns>
    string HashPassword(string rawPassword);
}
