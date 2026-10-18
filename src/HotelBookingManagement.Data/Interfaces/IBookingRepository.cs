using System;
using System.Collections.Generic;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Interfaces;

/// <summary>
/// Hợp đồng truy xuất dữ liệu cho bảng dbo.Bookings và dbo.BookingDetails (ADO.NET)
/// Phục vụ các nghiệp vụ: Đặt phòng, tìm kiếm theo khách hàng, kiểm tra phòng trống theo khoảng ngày, check-in, check-out.
/// </summary>
public interface IBookingRepository
{
	/// <summary>
	/// Lấy toàn bộ danh sách đơn đặt phòng
	/// </summary>
	IEnumerable<Booking> GetAll();

	/// <summary>
	/// Lấy chi tiết đơn đặt phòng theo BookingId (kèm thông tin Customer, Chi tiết phòng)
	/// </summary>
	Booking? GetById(int bookingId);

	/// <summary>
	/// Lấy lịch sử đặt phòng của một khách hàng (phục vụ task CUS05)
	/// </summary>
	IEnumerable<Booking> GetByCustomerId(int customerId);

	/// <summary>
	/// Lọc danh sách đơn đặt phòng theo trạng thái ('PENDING', 'CONFIRMED', 'CHECKED_IN', 'COMPLETED', 'CANCELLED')
	/// </summary>
	IEnumerable<Booking> GetByStatus(string status);

	/// <summary>
	/// Lấy các đơn đặt phòng có hiệu lực trong khoảng thời gian để phát hiện trùng lịch (Overlap Detection)
	/// </summary>
	IEnumerable<Booking> GetActiveBookingsByDateRange(DateTime checkIn, DateTime checkOut);

	/// <summary>
	/// Tạo mới một đơn đặt phòng chính
	/// </summary>
	/// <returns>BookingId vừa sinh</returns>
	int Add(Booking booking);

	/// <summary>
	/// Thêm một dòng chi tiết phòng vào đơn đặt phòng
	/// </summary>
	bool AddDetail(BookingDetail detail);

	/// <summary>
	/// Cập nhật thông tin đơn đặt phòng
	/// </summary>
	bool Update(Booking booking);

	/// <summary>
	/// Cập nhật trạng thái đơn đặt phòng (Check-in, Check-out, Hoàn tất, Hủy)
	/// </summary>
	bool UpdateStatus(int bookingId, string newStatus);

	/// <summary>
	/// Lấy danh sách các phòng trong một đơn đặt phòng
	/// </summary>
	IEnumerable<BookingDetail> GetDetailsByBookingId(int bookingId);
}
