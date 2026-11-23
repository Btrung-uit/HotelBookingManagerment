using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using HotelBookingManagement.Data.Database;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Repositories;

/// <summary>
/// Cài đặt truy xuất dữ liệu cho bảng dbo.Bookings và dbo.BookingDetails bằng ADO.NET thuần.
/// Phục vụ tra cứu lịch sử đặt phòng khách hàng (CUS05) và luồng nghiệp vụ Booking.
/// </summary>
public class BookingRepository : IBookingRepository
{
	public IEnumerable<Booking> GetAll()
	{
		var list = new List<Booking>();
		const string query = @"
			SELECT b.BookingId, b.CustomerId, b.BookingDate, b.CheckInDate, b.CheckOutDate, 
			       b.Status, b.TotalAmount, b.CreatedBy, b.CreatedAt,
			       c.FullName AS CustomerName, c.Phone AS CustomerPhone, c.IdentityNumber AS CustomerIdentity,
			       u.FullName AS StaffName, u.Username AS StaffUsername
			FROM dbo.Bookings b
			INNER JOIN dbo.Customers c ON b.CustomerId = c.CustomerId
			LEFT JOIN dbo.Users u ON b.CreatedBy = u.UserId
			ORDER BY b.BookingDate DESC, b.BookingId DESC;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		connection.Open();
		using (var reader = command.ExecuteReader())
		{
			while (reader.Read())
			{
				list.Add(MapReaderToBooking(reader));
			}
		}

		// Nạp chi tiết phòng cho danh sách bookings
		foreach (var booking in list)
		{
			PopulateBookingRelations(booking, connection);
		}

		return list;
	}

	public Booking? GetById(int bookingId)
	{
		const string query = @"
			SELECT b.BookingId, b.CustomerId, b.BookingDate, b.CheckInDate, b.CheckOutDate, 
			       b.Status, b.TotalAmount, b.CreatedBy, b.CreatedAt,
			       c.FullName AS CustomerName, c.Phone AS CustomerPhone, c.IdentityNumber AS CustomerIdentity,
			       u.FullName AS StaffName, u.Username AS StaffUsername
			FROM dbo.Bookings b
			INNER JOIN dbo.Customers c ON b.CustomerId = c.CustomerId
			LEFT JOIN dbo.Users u ON b.CreatedBy = u.UserId
			WHERE b.BookingId = @BookingId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@BookingId", SqlDbType.Int).Value = bookingId;

		connection.Open();
		using var reader = command.ExecuteReader();
		if (!reader.Read())
		{
			return null;
		}

		var booking = MapReaderToBooking(reader);
		reader.Close();

		PopulateBookingRelations(booking, connection);
		return booking;
	}

	/// <summary>
	/// Lấy lịch sử đặt phòng của một khách hàng (Nhiệm vụ CUS05)
	/// Trả về toàn bộ danh sách đơn đặt phòng kèm chi tiết phòng (RoomNumber, RoomType) và các đợt thanh toán.
	/// </summary>
	public IEnumerable<Booking> GetByCustomerId(int customerId)
	{
		var list = new List<Booking>();
		const string query = @"
			SELECT b.BookingId, b.CustomerId, b.BookingDate, b.CheckInDate, b.CheckOutDate, 
			       b.Status, b.TotalAmount, b.CreatedBy, b.CreatedAt,
			       c.FullName AS CustomerName, c.Phone AS CustomerPhone, c.IdentityNumber AS CustomerIdentity,
			       u.FullName AS StaffName, u.Username AS StaffUsername
			FROM dbo.Bookings b
			INNER JOIN dbo.Customers c ON b.CustomerId = c.CustomerId
			LEFT JOIN dbo.Users u ON b.CreatedBy = u.UserId
			WHERE b.CustomerId = @CustomerId
			ORDER BY b.BookingDate DESC, b.BookingId DESC;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;

		connection.Open();
		using (var reader = command.ExecuteReader())
		{
			while (reader.Read())
			{
				list.Add(MapReaderToBooking(reader));
			}
		}

		// Nạp danh sách phòng chi tiết và lịch sử thanh toán cho từng đơn đặt phòng
		foreach (var booking in list)
		{
			PopulateBookingRelations(booking, connection);
		}

		return list;
	}

	public IEnumerable<Booking> GetByStatus(string status)
	{
		var list = new List<Booking>();
		const string query = @"
			SELECT b.BookingId, b.CustomerId, b.BookingDate, b.CheckInDate, b.CheckOutDate, 
			       b.Status, b.TotalAmount, b.CreatedBy, b.CreatedAt,
			       c.FullName AS CustomerName, c.Phone AS CustomerPhone, c.IdentityNumber AS CustomerIdentity,
			       u.FullName AS StaffName, u.Username AS StaffUsername
			FROM dbo.Bookings b
			INNER JOIN dbo.Customers c ON b.CustomerId = c.CustomerId
			LEFT JOIN dbo.Users u ON b.CreatedBy = u.UserId
			WHERE b.Status = @Status
			ORDER BY b.BookingDate DESC, b.BookingId DESC;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = status.Trim().ToUpperInvariant();

		connection.Open();
		using (var reader = command.ExecuteReader())
		{
			while (reader.Read())
			{
				list.Add(MapReaderToBooking(reader));
			}
		}

		foreach (var booking in list)
		{
			PopulateBookingRelations(booking, connection);
		}

		return list;
	}

	public bool HasOverlappingBooking(int roomId, DateTime checkIn, DateTime checkOut, int? excludeBookingId = null)
	{
		const string query = @"
			SELECT COUNT(1)
			FROM dbo.Bookings b
			INNER JOIN dbo.BookingDetails d ON b.BookingId = d.BookingId
			WHERE b.Status != 'CANCELLED'
			  AND b.CheckInDate < @NewCheckOut
			  AND b.CheckOutDate > @NewCheckIn
			  AND d.RoomId = @RoomId
			  AND (@ExcludeBookingId IS NULL OR b.BookingId != @ExcludeBookingId);";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;
		command.Parameters.Add("@NewCheckIn", SqlDbType.DateTime2).Value = checkIn;
		command.Parameters.Add("@NewCheckOut", SqlDbType.DateTime2).Value = checkOut;
		command.Parameters.Add("@ExcludeBookingId", SqlDbType.Int).Value = (object?)excludeBookingId ?? DBNull.Value;

		connection.Open();
		var count = Convert.ToInt32(command.ExecuteScalar());
		return count > 0;
	}

	public IEnumerable<int> GetBookedRoomIdsByDateRange(DateTime checkIn, DateTime checkOut, int? excludeBookingId = null)
	{
		var roomIds = new List<int>();
		const string query = @"
			SELECT DISTINCT d.RoomId
			FROM dbo.Bookings b
			INNER JOIN dbo.BookingDetails d ON b.BookingId = d.BookingId
			WHERE b.Status != 'CANCELLED'
			  AND b.CheckInDate < @NewCheckOut
			  AND b.CheckOutDate > @NewCheckIn
			  AND (@ExcludeBookingId IS NULL OR b.BookingId != @ExcludeBookingId);";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@NewCheckIn", SqlDbType.DateTime2).Value = checkIn;
		command.Parameters.Add("@NewCheckOut", SqlDbType.DateTime2).Value = checkOut;
		command.Parameters.Add("@ExcludeBookingId", SqlDbType.Int).Value = (object?)excludeBookingId ?? DBNull.Value;

		connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			roomIds.Add(reader.GetInt32(0));
		}

		return roomIds;
	}

	public int Add(Booking booking)
	{
		return AddWithDetails(booking, booking.BookingDetails);
	}

    public int AddWithDetails(Booking booking, IReadOnlyCollection<BookingDetail> details)
    {
        if (booking == null) throw new ArgumentNullException(nameof(booking));
        if (details == null || details.Count == 0)
        {
            throw new ArgumentException("Đơn đặt phòng bắt buộc phải có ít nhất một phòng chi tiết!", nameof(details));
        }
        if (booking.CreatedBy <= 0)
        {
            throw new ArgumentException("Không xác định được nhân viên tạo đơn đặt phòng!", nameof(booking));
        }

        using var connection = DatabaseConnection.GetConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

        try
        {
            const string lockRoomSql = @"
				SELECT r.RoomNumber, r.Status
				FROM dbo.Rooms r WITH (UPDLOCK, ROWLOCK)
				WHERE r.RoomId = @RoomId;";

            const string overlapSql = @"
				SELECT COUNT(1)
				FROM dbo.Bookings b WITH (UPDLOCK, HOLDLOCK)
				INNER JOIN dbo.BookingDetails d WITH (UPDLOCK, HOLDLOCK) ON b.BookingId = d.BookingId
				WHERE b.Status != 'CANCELLED'
				  AND b.CheckInDate < @NewCheckOut
				  AND b.CheckOutDate > @NewCheckIn
				  AND d.RoomId = @RoomId;";

            foreach (int roomId in details.Select(d => d.RoomId).Distinct().OrderBy(id => id))
            {
                string roomNumber;
                string roomStatus;

                using (var cmdRoom = new SqlCommand(lockRoomSql, connection, transaction))
                {
                    cmdRoom.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;
                    using var reader = cmdRoom.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException($"Không tìm thấy phòng mã #{roomId}!");
                    }
                    roomNumber = reader.GetString(0);
                    roomStatus = reader.GetString(1).Trim().ToUpperInvariant();
                }

                if (roomStatus == "MAINTENANCE" || roomStatus == "OCCUPIED")
                {
                    throw new InvalidOperationException($"Phòng {roomNumber} hiện đang ở trạng thái {roomStatus}, không thể đặt!");
                }

                using (var cmdOverlap = new SqlCommand(overlapSql, connection, transaction))
                {
                    cmdOverlap.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;
                    cmdOverlap.Parameters.Add("@NewCheckIn", SqlDbType.DateTime2).Value = booking.CheckInDate;
                    cmdOverlap.Parameters.Add("@NewCheckOut", SqlDbType.DateTime2).Value = booking.CheckOutDate;

                    if (Convert.ToInt32(cmdOverlap.ExecuteScalar()) > 0)
                    {
                        throw new InvalidOperationException($"Phòng {roomNumber} đã có đơn đặt trùng lịch trong khoảng thời gian này!");
                    }
                }
            }

            decimal totalAmount = details.Sum(d => d.Subtotal);
            booking.TotalAmount = totalAmount;

            const string insertBookingSql = @"
				INSERT INTO dbo.Bookings (CustomerId, BookingDate, CheckInDate, CheckOutDate, Status, TotalAmount, CreatedBy, CreatedAt)
				VALUES (@CustomerId, @BookingDate, @CheckInDate, @CheckOutDate, @Status, @TotalAmount, @CreatedBy, @CreatedAt);
				SELECT CAST(SCOPE_IDENTITY() AS int);";

            using var cmdBooking = new SqlCommand(insertBookingSql, connection, transaction);
            cmdBooking.Parameters.Add("@CustomerId", SqlDbType.Int).Value = booking.CustomerId;
            cmdBooking.Parameters.Add("@BookingDate", SqlDbType.DateTime2).Value = booking.BookingDate == default ? DateTime.Now : booking.BookingDate;
            cmdBooking.Parameters.Add("@CheckInDate", SqlDbType.DateTime2).Value = booking.CheckInDate;
            cmdBooking.Parameters.Add("@CheckOutDate", SqlDbType.DateTime2).Value = booking.CheckOutDate;
            cmdBooking.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(booking.Status) ? "PENDING" : booking.Status;
            cmdBooking.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = totalAmount;
            cmdBooking.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = booking.CreatedBy;
            cmdBooking.Parameters.Add("@CreatedAt", SqlDbType.DateTime2).Value = booking.CreatedAt == default ? DateTime.Now : booking.CreatedAt;

            int newBookingId = Convert.ToInt32(cmdBooking.ExecuteScalar());
            booking.BookingId = newBookingId;

            const string insertDetailSql = @"
				INSERT INTO dbo.BookingDetails (BookingId, RoomId, PricePerNight, NumberOfNights, Subtotal)
				VALUES (@BookingId, @RoomId, @PricePerNight, @NumberOfNights, @Subtotal);";

            foreach (var detail in details)
            {
                using var cmdDetail = new SqlCommand(insertDetailSql, connection, transaction);
                cmdDetail.Parameters.Add("@BookingId", SqlDbType.Int).Value = newBookingId;
                cmdDetail.Parameters.Add("@RoomId", SqlDbType.Int).Value = detail.RoomId;
                cmdDetail.Parameters.Add("@PricePerNight", SqlDbType.Decimal).Value = detail.PricePerNight;
                cmdDetail.Parameters.Add("@NumberOfNights", SqlDbType.Int).Value = detail.NumberOfNights;
                cmdDetail.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = detail.Subtotal;
                cmdDetail.ExecuteNonQuery();
            }

            transaction.Commit();
            return newBookingId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Tạo Booking kèm BookingDetails trong cùng SqlTransaction có khóa theo phòng (UPDLOCK, ROWLOCK).
    /// </summary>
    public Booking? CreateBooking(Booking newBooking, List<int> selectedRoomIds)
    {
        if (newBooking == null || selectedRoomIds == null || selectedRoomIds.Count == 0)
            return null;

        if (newBooking.CreatedBy <= 0)
            throw new InvalidOperationException("Không xác định được nhân viên tạo đơn đặt phòng!");

        var distinctRoomIds = selectedRoomIds.Distinct().OrderBy(id => id).ToList();

        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);

        try
        {
            int nights = Math.Max(1, (newBooking.CheckOutDate.Date - newBooking.CheckInDate.Date).Days);
            var details = new List<BookingDetail>();

            const string lockRoomAndPriceQuery = @"
				SELECT r.RoomNumber, r.Status, rt.PricePerNight
				FROM dbo.Rooms r WITH (UPDLOCK, ROWLOCK)
				INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
				WHERE r.RoomId = @RoomId;";

            const string overlapCheckQuery = @"
				SELECT COUNT(1)
				FROM dbo.Bookings b WITH (UPDLOCK, HOLDLOCK)
				INNER JOIN dbo.BookingDetails d WITH (UPDLOCK, HOLDLOCK) ON b.BookingId = d.BookingId
				WHERE b.Status != 'CANCELLED'
				  AND b.CheckInDate < @NewCheckOut
				  AND b.CheckOutDate > @NewCheckIn
				  AND d.RoomId = @RoomId;";

            foreach (int roomId in distinctRoomIds)
            {
                string roomNumber;
                string roomStatus;
                decimal pricePerNight;

                // 1. Khóa dòng phòng theo RoomId và đọc trạng thái + đơn giá thực tế từ DB
                using (var cmdRoom = new SqlCommand(lockRoomAndPriceQuery, conn, transaction))
                {
                    cmdRoom.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;
                    using var reader = cmdRoom.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException($"Không tìm thấy phòng mã #{roomId} trong hệ thống!");
                    }

                    roomNumber = reader.GetString(0);
                    roomStatus = reader.GetString(1).Trim().ToUpperInvariant();
                    pricePerNight = reader.GetDecimal(2);
                }

                // 2. Chặn phòng vừa chuyển sang MAINTENANCE hoặc OCCUPIED
                if (roomStatus == "MAINTENANCE" || roomStatus == "OCCUPIED")
                {
                    throw new InvalidOperationException($"Phòng {roomNumber} hiện đang ở trạng thái {roomStatus}, không thể đặt!");
                }

                // 3. Chặn trùng lịch đặt trong cùng transaction sau khi đã giữ khóa phòng
                using (var cmdOverlap = new SqlCommand(overlapCheckQuery, conn, transaction))
                {
                    cmdOverlap.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomId;
                    cmdOverlap.Parameters.Add("@NewCheckIn", SqlDbType.DateTime2).Value = newBooking.CheckInDate;
                    cmdOverlap.Parameters.Add("@NewCheckOut", SqlDbType.DateTime2).Value = newBooking.CheckOutDate;

                    if (Convert.ToInt32(cmdOverlap.ExecuteScalar()) > 0)
                    {
                        throw new InvalidOperationException($"Phòng {roomNumber} đã có đơn đặt trùng lịch trong khoảng thời gian này!");
                    }
                }

                // 4. Giữ nguyên đơn giá 0đ hợp lệ và tính Subtotal từng phòng
                decimal subtotal = pricePerNight * nights;
                details.Add(new BookingDetail
                {
                    RoomId = roomId,
                    PricePerNight = pricePerNight,
                    NumberOfNights = nights,
                    Subtotal = subtotal
                });
            }

            // 5. Tổng tiền đơn bằng tổng BookingDetails.Subtotal
            decimal totalAmount = details.Sum(d => d.Subtotal);

            const string queryBooking = @"
				INSERT INTO dbo.Bookings (CustomerId, BookingDate, CheckInDate, CheckOutDate, Status, TotalAmount, CreatedBy, CreatedAt)
				OUTPUT INSERTED.BookingId
				VALUES (@CustomerId, GETDATE(), @CheckIn, @CheckOut, 'PENDING', @TotalAmount, @CreatedBy, GETDATE());";

            using (var cmd = new SqlCommand(queryBooking, conn, transaction))
            {
                cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = newBooking.CustomerId;
                cmd.Parameters.Add("@CheckIn", SqlDbType.DateTime2).Value = newBooking.CheckInDate;
                cmd.Parameters.Add("@CheckOut", SqlDbType.DateTime2).Value = newBooking.CheckOutDate;
                cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = totalAmount;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = newBooking.CreatedBy;

                newBooking.BookingId = Convert.ToInt32(cmd.ExecuteScalar());
                newBooking.Status = "PENDING";
                newBooking.TotalAmount = totalAmount;
                newBooking.BookingDetails = details;
            }

            const string queryDetail = @"
				INSERT INTO dbo.BookingDetails (BookingId, RoomId, PricePerNight, NumberOfNights, Subtotal)
				VALUES (@BookingId, @RoomId, @PricePerNight, @NumberOfNights, @Subtotal);";

            foreach (var detail in details)
            {
                detail.BookingId = newBooking.BookingId;
                using var cmdDetail = new SqlCommand(queryDetail, conn, transaction);
                cmdDetail.Parameters.Add("@BookingId", SqlDbType.Int).Value = newBooking.BookingId;
                cmdDetail.Parameters.Add("@RoomId", SqlDbType.Int).Value = detail.RoomId;
                cmdDetail.Parameters.Add("@PricePerNight", SqlDbType.Decimal).Value = detail.PricePerNight;
                cmdDetail.Parameters.Add("@NumberOfNights", SqlDbType.Int).Value = detail.NumberOfNights;
                cmdDetail.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = detail.Subtotal;
                cmdDetail.ExecuteNonQuery();
            }

            transaction.Commit();
            return newBooking;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public bool Update(Booking booking)
	{
		if (booking == null) throw new ArgumentNullException(nameof(booking));

		const string query = @"
			UPDATE dbo.Bookings
			SET CheckInDate = @CheckInDate,
			    CheckOutDate = @CheckOutDate,
			    Status = @Status,
			    TotalAmount = @TotalAmount
			WHERE BookingId = @BookingId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@BookingId", SqlDbType.Int).Value = booking.BookingId;
		command.Parameters.Add("@CheckInDate", SqlDbType.DateTime2).Value = booking.CheckInDate;
		command.Parameters.Add("@CheckOutDate", SqlDbType.DateTime2).Value = booking.CheckOutDate;
		command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = booking.Status;
		command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = booking.TotalAmount;

		connection.Open();
		return command.ExecuteNonQuery() > 0;
	}

	public bool UpdateStatus(int bookingId, string newStatus)
	{
		const string query = @"
			UPDATE dbo.Bookings
			SET Status = @Status
			WHERE BookingId = @BookingId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);

		command.Parameters.Add("@BookingId", SqlDbType.Int).Value = bookingId;
		command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = newStatus.Trim().ToUpperInvariant();

		connection.Open();
		return command.ExecuteNonQuery() > 0;
	}

	public IEnumerable<BookingDetail> GetDetailsByBookingId(int bookingId)
	{
		var details = new List<BookingDetail>();
		const string query = @"
			SELECT bd.BookingDetailId, bd.BookingId, bd.RoomId, bd.PricePerNight, bd.NumberOfNights, bd.Subtotal,
			       r.RoomNumber, r.Floor, r.Status AS RoomStatus, r.Description AS RoomDescription,
			       rt.RoomTypeId, rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight AS StandardPricePerNight
			FROM dbo.BookingDetails bd
			INNER JOIN dbo.Rooms r ON bd.RoomId = r.RoomId
			INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
			WHERE bd.BookingId = @BookingId;";

		using var connection = DatabaseConnection.GetConnection();
		using var command = new SqlCommand(query, connection);
		command.Parameters.Add("@BookingId", SqlDbType.Int).Value = bookingId;

		connection.Open();
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			details.Add(MapReaderToBookingDetail(reader));
		}

		return details;
	}

	/// <summary>
	/// Nạp chi tiết phòng và các đợt thanh toán cho đơn đặt phòng
	/// </summary>
	private void PopulateBookingRelations(Booking booking, SqlConnection connection)
	{
		// 1. Nạp chi tiết các phòng đặt
		const string queryDetails = @"
			SELECT bd.BookingDetailId, bd.BookingId, bd.RoomId, bd.PricePerNight, bd.NumberOfNights, bd.Subtotal,
			       r.RoomNumber, r.Floor, r.Status AS RoomStatus, r.Description AS RoomDescription,
			       rt.RoomTypeId, rt.Name AS RoomTypeName, rt.Capacity, rt.PricePerNight AS StandardPricePerNight
			FROM dbo.BookingDetails bd
			INNER JOIN dbo.Rooms r ON bd.RoomId = r.RoomId
			INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
			WHERE bd.BookingId = @BookingId;";

		using (var cmdDetails = new SqlCommand(queryDetails, connection))
		{
			cmdDetails.Parameters.Add("@BookingId", SqlDbType.Int).Value = booking.BookingId;
			using var reader = cmdDetails.ExecuteReader();
			booking.BookingDetails = new List<BookingDetail>();
			while (reader.Read())
			{
				booking.BookingDetails.Add(MapReaderToBookingDetail(reader, booking));
			}
		}

		// 2. Nạp lịch sử thanh toán
		const string queryPayments = @"
			SELECT PaymentId, BookingId, Amount, PaymentMethod, PaymentDate, Status
			FROM dbo.Payments
			WHERE BookingId = @BookingId
			ORDER BY PaymentDate ASC;";

		using (var cmdPayments = new SqlCommand(queryPayments, connection))
		{
			cmdPayments.Parameters.Add("@BookingId", SqlDbType.Int).Value = booking.BookingId;
			using var reader = cmdPayments.ExecuteReader();
			booking.Payments = new List<Payment>();
			while (reader.Read())
			{
				booking.Payments.Add(new Payment
				{
					PaymentId = reader.GetInt32(reader.GetOrdinal("PaymentId")),
					BookingId = reader.GetInt32(reader.GetOrdinal("BookingId")),
					Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
					PaymentMethod = reader.GetString(reader.GetOrdinal("PaymentMethod")),
					PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
					Status = reader.GetString(reader.GetOrdinal("Status")),
					Booking = booking
				});
			}
		}
	}

    private static Booking MapReaderToBooking(SqlDataReader reader)
    {
        var booking = new Booking
        {
            BookingId = reader.GetInt32(reader.GetOrdinal("BookingId")),
            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
            BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
            CheckInDate = reader.GetDateTime(reader.GetOrdinal("CheckInDate")),
            CheckOutDate = reader.GetDateTime(reader.GetOrdinal("CheckOutDate")),
            Status = reader.GetString(reader.GetOrdinal("Status")),
            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
            CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            Customer = new Customer
            {
                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                FullName = reader.GetString(reader.GetOrdinal("CustomerName")),
                Phone = reader.GetString(reader.GetOrdinal("CustomerPhone")),
                IdentityNumber = reader.GetString(reader.GetOrdinal("CustomerIdentity"))
            }
        };

        if (!reader.IsDBNull(reader.GetOrdinal("StaffName")))
        {
            booking.CreatedByUser = new User
            {
                UserId = booking.CreatedBy,
                FullName = reader.GetString(reader.GetOrdinal("StaffName")),
                Username = reader.IsDBNull(reader.GetOrdinal("StaffUsername")) ? string.Empty : reader.GetString(reader.GetOrdinal("StaffUsername"))
            };
        }

        return booking;
    }

    private static BookingDetail MapReaderToBookingDetail(SqlDataReader reader, Booking? parentBooking = null)
    {
        var roomType = new RoomType
        {
            RoomTypeId = reader.GetInt32(reader.GetOrdinal("RoomTypeId")),
            Name = reader.GetString(reader.GetOrdinal("RoomTypeName")),
            Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
            PricePerNight = reader.GetDecimal(reader.GetOrdinal("StandardPricePerNight"))
        };

        var room = new Room
        {
            RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),
            RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
            RoomTypeId = roomType.RoomTypeId,
            Floor = reader.GetInt32(reader.GetOrdinal("Floor")),
            Status = reader.GetString(reader.GetOrdinal("RoomStatus")),
            Description = reader.IsDBNull(reader.GetOrdinal("RoomDescription")) ? null : reader.GetString(reader.GetOrdinal("RoomDescription")),
            RoomType = roomType
        };

        return new BookingDetail
        {
            BookingDetailId = reader.GetInt32(reader.GetOrdinal("BookingDetailId")),
            BookingId = reader.GetInt32(reader.GetOrdinal("BookingId")),
            RoomId = room.RoomId,
            PricePerNight = reader.GetDecimal(reader.GetOrdinal("PricePerNight")),
            NumberOfNights = reader.GetInt32(reader.GetOrdinal("NumberOfNights")),
            Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
            Booking = parentBooking,
            Room = room
        };
    }
}