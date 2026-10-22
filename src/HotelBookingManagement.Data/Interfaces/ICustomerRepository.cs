using System.Collections.Generic;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Interfaces;

/// <summary>
/// Hợp đồng truy xuất dữ liệu cho bảng dbo.Customers (ADO.NET)
/// Phục vụ các nghiệp vụ: Quản lý khách hàng, tìm kiếm, kiểm tra trùng SĐT/CCCD, lịch sử đặt phòng.
/// </summary>
namespace HotelBookingManagement.Data.Interfaces
{
public interface ICustomerRepository
{
	/// <summary>
	/// Lấy toàn bộ danh sách khách hàng
	/// </summary>
	IEnumerable<Customer> GetAll();

	/// <summary>
	/// Lấy khách hàng theo mã ID
	/// </summary>
	Customer? GetById(int customerId);

	/// <summary>
	/// Tìm khách hàng theo số điện thoại (chính xác)
	/// </summary>
	Customer? GetByPhone(string phone);

	/// <summary>
	/// Tìm khách hàng theo số CCCD/Hộ chiếu (chính xác)
	/// </summary>
	Customer? GetByIdentityNumber(string identityNumber);

	/// <summary>
	/// Tìm kiếm danh sách khách hàng theo từ khóa (Họ tên, Số điện thoại hoặc CCCD)
	/// </summary>
	IEnumerable<Customer> Search(string keyword);

	/// <summary>
	/// Thêm mới một khách hàng vào cơ sở dữ liệu
	/// </summary>
	/// <returns>CustomerId vừa sinh (hoặc -1 nếu thất bại)</returns>
	int Add(Customer customer);

	/// <summary>
	/// Cập nhật thông tin khách hàng
	/// </summary>
	/// <returns>True nếu cập nhật thành công (ảnh hưởng ít nhất 1 dòng)</returns>
	bool Update(Customer customer);

	/// <summary>
	/// Xóa khách hàng theo mã ID
	/// </summary>
	/// <returns>True nếu xóa thành công</returns>
	bool Delete(int customerId);

	/// <summary>
	/// Kiểm tra số điện thoại đã tồn tại trong hệ thống chưa (dùng cho validation chống trùng)
	/// </summary>
	bool ExistsByPhone(string phone, int? excludeCustomerId = null);

	/// <summary>
	/// Kiểm tra số CCCD/Hộ chiếu đã tồn tại chưa (dùng cho validation chống trùng)
	/// </summary>
	bool ExistsByIdentityNumber(string identityNumber, int? excludeCustomerId = null);
        Customers GetCustomerByPhoneOrIdentity(string keyword);
    }
}
