using System.Collections.Generic;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Interfaces;

/// <summary>
/// Giao diện xử lý nghiệp vụ Quản lý Khách hàng (TV02 - Customer)
/// Đáp ứng các yêu cầu: CRUD, kiểm tra dữ liệu hợp lệ (Validation), tìm kiếm, tra cứu lịch sử.
/// </summary>
public interface ICustomerService
{
	/// <summary>
	/// Lấy toàn bộ danh sách khách hàng
	/// </summary>
	IEnumerable<Customer> GetAllCustomers();

	/// <summary>
	/// Lấy thông tin khách hàng theo ID
	/// </summary>
	Customer? GetCustomerById(int customerId);

	/// <summary>
	/// Tìm khách hàng theo số điện thoại
	/// </summary>
	Customer? GetCustomerByPhone(string phone);

	/// <summary>
	/// Tìm khách hàng theo CCCD/Hộ chiếu
	/// </summary>
	Customer? GetCustomerByIdentityNumber(string identityNumber);

	/// <summary>
	/// Tìm kiếm khách hàng theo từ khóa (Tên, SĐT, CCCD)
	/// </summary>
	IEnumerable<Customer> SearchCustomers(string keyword);

	/// <summary>
	/// Thêm mới một khách hàng sau khi kiểm tra hợp lệ
	/// </summary>
	/// <param name="customer">Đối tượng khách hàng cần thêm</param>
	/// <param name="errorMessage">Thông báo lỗi chi tiết nếu thêm thất bại</param>
	/// <returns>True nếu tạo thành công</returns>
	bool CreateCustomer(Customer customer, out string errorMessage);

	/// <summary>
	/// Cập nhật thông tin khách hàng sau khi kiểm tra hợp lệ
	/// </summary>
	/// <param name="customer">Đối tượng khách hàng cần cập nhật</param>
	/// <param name="errorMessage">Thông báo lỗi chi tiết nếu cập nhật thất bại</param>
	/// <returns>True nếu cập nhật thành công</returns>
	bool UpdateCustomer(Customer customer, out string errorMessage);

	/// <summary>
	/// Xóa khách hàng theo ID
	/// </summary>
	/// <param name="customerId">ID khách hàng cần xóa</param>
	/// <param name="errorMessage">Thông báo lỗi nếu vi phạm ràng buộc dữ liệu hoặc lỗi hệ thống</param>
	/// <returns>True nếu xóa thành công</returns>
	bool DeleteCustomer(int customerId, out string errorMessage);

	/// <summary>
	/// Lấy lịch sử đặt phòng của một khách hàng (Nhiệm vụ CUS05)
	/// </summary>
	/// <param name="customerId">Mã khách hàng cần tra cứu</param>
	/// <returns>Danh sách các đơn đặt phòng của khách hàng kèm chi tiết phòng và thanh toán</returns>
	IEnumerable<Booking> GetCustomerBookingHistory(int customerId);
}
