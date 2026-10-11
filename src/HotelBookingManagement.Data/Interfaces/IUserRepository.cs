using HotelBookingManagement.Models;
namespace HotelBookingManagement.Data.Interfaces;

public interface IUserRepository
{
	// Tìm kiếm người dùng theo Username (đăng nhập)
	Users? GetByUsername(string username);

	// Lấy thông tin người dùng theo Id
	Users? GetById(int userId);
}
