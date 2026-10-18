using HotelBookingManagement.Models;
namespace HotelBookingManagement.Data.Interfaces;

public interface IUserRepository
{
	// Tìm kiếm người dùng theo Username (đăng nhập)
	User? GetByUsername(string username);

	// Lấy thông tin người dùng theo Id
	User? GetById(int userId);
}
