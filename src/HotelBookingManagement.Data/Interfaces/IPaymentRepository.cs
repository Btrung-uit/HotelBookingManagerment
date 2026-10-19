using System.Collections.Generic;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Interfaces;

/// <summary>
/// Hợp đồng truy xuất dữ liệu cho bảng dbo.Payments (ADO.NET)
/// Phục vụ các nghiệp vụ: Thanh toán đơn đặt phòng, hóa đơn và lịch sử giao dịch.
/// </summary>
public interface IPaymentRepository
{
	/// <summary>
	/// Lấy toàn bộ danh sách thanh toán
	/// </summary>
	IEnumerable<Payment> GetAll();

	/// <summary>
	/// Lấy thông tin thanh toán theo mã PaymentId
	/// </summary>
	Payment? GetById(int paymentId);

	/// <summary>
	/// Lấy danh sách các khoản thanh toán của một đơn đặt phòng
	/// </summary>
	IEnumerable<Payment> GetByBookingId(int bookingId);

	/// <summary>
	/// Tính tổng số tiền đã thanh toán thành công cho một đơn đặt phòng (SUM(Amount) WHERE Status = 'COMPLETED').
	/// Phục vụ quy tắc nghiệp vụ: Kiểm tra không cho phép thanh toán vượt quá TotalAmount của Booking.
	/// </summary>
	decimal GetTotalPaidByBookingId(int bookingId);

	/// <summary>
	/// Thêm mới một bản ghi thanh toán
	/// </summary>
	/// <returns>PaymentId vừa sinh</returns>
	int Add(Payment payment);

	/// <summary>
	/// Cập nhật trạng thái thanh toán ('PENDING', 'COMPLETED', 'REFUNDED')
	/// </summary>
	bool UpdateStatus(int paymentId, string newStatus);
}
