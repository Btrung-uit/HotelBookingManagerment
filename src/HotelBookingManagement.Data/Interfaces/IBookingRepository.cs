using System;
using System.Collections.Generic;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Interfaces;

/// <summary>
/// Hợp đồng truy xuất dữ liệu cho bảng dbo.Bookings và dbo.BookingDetails (ADO.NET)
/// Phục vụ các nghiệp vụ: Đặt phòng, kiểm tra trùng lịch (BR-02), tìm kiếm theo khách hàng, check-in, check-out.
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
	/// Kiểm tra xem một phòng cụ thể có bị trùng lịch đặt trong khoảng thời gian hay không (Nghiệp vụ BR-02).
	/// Điều kiện SQL chống trùng lịch:
	///   b.Status != 'CANCELLED'
	///   AND b.CheckInDate &lt; @NewCheckOut
	///   AND b.CheckOutDate &gt; @NewCheckIn
	///   AND d.RoomId = @RoomId
	/// </summary>
	/// <param name="roomId">Mã phòng cần kiểm tra</param>
	/// <param name="checkIn">Ngày nhận phòng dự kiến</param>
	/// <param name="checkOut">Ngày trả phòng dự kiến</param>
	/// <param name="excludeBookingId">Mã đơn đặt phòng cần loại trừ (dùng khi cập nhật đơn hiện có)</param>
	/// <returns>True nếu phòng đã bị trùng lịch; False nếu phòng khả dụng</returns>
	bool HasOverlappingBooking(int roomId, DateTime checkIn, DateTime checkOut, int? excludeBookingId = null);

	/// <summary>
	/// Lấy danh sách mã các phòng (RoomId) đã bị đặt trong khoảng thời gian chỉ định (loại trừ các booking CANCELLED).
	/// </summary>
	IEnumerable<int> GetBookedRoomIdsByDateRange(DateTime checkIn, DateTime checkOut, int? excludeBookingId = null);

	/// <summary>
	/// Tạo mới một đơn đặt phòng kèm toàn bộ chi tiết phòng (BookingDetails) trong cùng một SqlTransaction nguyên tử.
	/// Implementation BẮT BUỘC:
	/// 1. Mở SqlTransaction.
	/// 2. INSERT vào dbo.Bookings -> Lấy SCOPE_IDENTITY() làm BookingId.
	/// 3. Lặp qua booking.BookingDetails, gán BookingId và INSERT vào dbo.BookingDetails.
	/// 4. Commit transaction nếu toàn bộ thành công, Rollback nếu xảy ra bất kỳ lỗi nào.
	/// </summary>
	/// <param name="booking">Thông tin đơn đặt phòng (chứa danh sách BookingDetails)</param>
	/// <returns>BookingId vừa sinh (hoặc -1 nếu thất bại/rollback)</returns>
	int Add(Booking booking);

	/// <summary>
	/// Tạo mới đơn đặt phòng kèm danh sách chi tiết phòng truyền tường minh trong cùng một SqlTransaction.
	/// </summary>
	/// <param name="booking">Thông tin đơn đặt phòng</param>
	/// <param name="details">Danh sách các phòng đặt kèm đơn</param>
	/// <returns>BookingId vừa sinh</returns>
	int AddWithDetails(Booking booking, IReadOnlyCollection<BookingDetail> details);

	/// <summary>
	/// Cập nhật thông tin đơn đặt phòng
	/// </summary>
	bool Update(Booking booking);

	/// <summary>
	/// Cập nhật trạng thái đơn đặt phòng (Check-in, Check-out, Hoàn tất, Hủy)
	/// </summary>
	bool UpdateStatus(int bookingId, string newStatus);

	/// <summary>
	/// Lấy danh sách các dòng chi tiết phòng trong một đơn đặt phòng
	/// </summary>
	IEnumerable<BookingDetail> GetDetailsByBookingId(int bookingId);
}
