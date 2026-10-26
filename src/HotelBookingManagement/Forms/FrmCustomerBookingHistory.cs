using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Business.Services;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Forms
{
	/// <summary>
	/// Giao diện hiển thị lịch sử đặt phòng của một khách hàng (Nhiệm vụ CUS05 - Module TV02)
	/// Tuân thủ kiến trúc 3 lớp: Nhận dữ liệu qua ICustomerService.
	/// </summary>
	public partial class FrmCustomerBookingHistory : Form
	{
		private readonly int _customerId;
		private readonly string? _customerName;
		private readonly ICustomerService _customerService;
		private List<Booking> _allBookings = new();
		private static readonly CultureInfo VnCulture = new("vi-VN");

		public FrmCustomerBookingHistory(int customerId, string? customerName = null, ICustomerService? customerService = null)
		{
			InitializeComponent();
			_customerId = customerId;
			_customerName = customerName;
			_customerService = customerService ?? new CustomerService();
		}

		private void FrmCustomerBookingHistory_Load(object? sender, EventArgs e)
		{
			// Khởi tạo ComboBox lọc trạng thái
			cboStatusFilter.Items.Clear();
			cboStatusFilter.Items.Add("Tất cả trạng thái");
			cboStatusFilter.Items.Add("PENDING - Chờ xác nhận");
			cboStatusFilter.Items.Add("CONFIRMED - Đã xác nhận");
			cboStatusFilter.Items.Add("CHECKED_IN - Đang lưu trú");
			cboStatusFilter.Items.Add("COMPLETED - Đã hoàn tất");
			cboStatusFilter.Items.Add("CANCELLED - Đã hủy");
			cboStatusFilter.SelectedIndex = 0;

			// Tải dữ liệu lịch sử đặt phòng
			LoadHistoryData();
		}

		/// <summary>
		/// Tải toàn bộ danh sách đơn đặt phòng từ tầng Service và cập nhật giao diện
		/// </summary>
		private void LoadHistoryData()
		{
			try
			{
				Cursor = Cursors.WaitCursor;

				// 1. Tải thông tin khách hàng nếu chưa có tên
				var customer = _customerService.GetCustomerById(_customerId);
				if (customer != null)
				{
					Text = $"Lịch sử đặt phòng - {customer.FullName} (Mã KH: #{customer.CustomerId})";
					lblCustomerSummary.Text = $"Khách hàng: {customer.FullName}  |  SĐT: {customer.Phone}  |  CCCD/Hộ chiếu: {customer.IdentityNumber}  |  Mã KH: #{customer.CustomerId}";
				}
				else if (!string.IsNullOrWhiteSpace(_customerName))
				{
					Text = $"Lịch sử đặt phòng - {_customerName} (Mã KH: #{_customerId})";
					lblCustomerSummary.Text = $"Khách hàng: {_customerName}  |  Mã KH: #{_customerId}";
				}

				// 2. Lấy danh sách booking từ ICustomerService (Task CUS05)
				var bookings = _customerService.GetCustomerBookingHistory(_customerId);
				_allBookings = bookings.ToList();

				// 3. Cập nhật các thẻ thống kê tổng quan (Summary Cards)
				UpdateSummaryCards(_allBookings);

				// 4. Áp dụng bộ lọc trạng thái và hiển thị lên Grid
				ApplyFilterAndDisplay();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					$"Lỗi khi tải lịch sử đặt phòng: {ex.Message}",
					"Lỗi hệ thống",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// Cập nhật các thẻ thông tin tóm tắt trên đầu màn hình
		/// </summary>
		private void UpdateSummaryCards(List<Booking> bookings)
		{
			int totalCount = bookings.Count;
			decimal totalAmount = bookings.Where(b => b.Status != "CANCELLED").Sum(b => b.TotalAmount);

			// Tính tổng tiền đã thanh toán từ các giao dịch COMPLETED
			decimal totalPaid = bookings
				.SelectMany(b => b.Payments)
				.Where(p => string.Equals(p.Status, "COMPLETED", StringComparison.OrdinalIgnoreCase))
				.Sum(p => p.Amount);

			lblTotalBookingsValue.Text = $"{totalCount} đơn";
			lblTotalAmountValue.Text = string.Format(VnCulture, "{0:#,##0} ₫", totalAmount);
			lblTotalPaidValue.Text = string.Format(VnCulture, "{0:#,##0} ₫", totalPaid);

			var latest = bookings.OrderByDescending(b => b.BookingDate).FirstOrDefault();
			if (latest != null)
			{
				lblLatestBookingValue.Text = latest.BookingDate.ToString("dd/MM/yyyy HH:mm");
			}
			else
			{
				lblLatestBookingValue.Text = "Chưa có đơn";
			}
		}

		/// <summary>
		/// Áp dụng bộ lọc trạng thái đơn đặt phòng
		/// </summary>
		private void ApplyFilterAndDisplay()
		{
			IEnumerable<Booking> filtered = _allBookings;

			if (cboStatusFilter.SelectedIndex > 0)
			{
				string selectedText = cboStatusFilter.SelectedItem?.ToString() ?? string.Empty;
				string statusKey = selectedText.Split('-')[0].Trim().ToUpperInvariant();

				filtered = filtered.Where(b => string.Equals(b.Status, statusKey, StringComparison.OrdinalIgnoreCase));
			}

			var displayList = filtered.ToList();
			dgvBookings.DataSource = displayList;
			lblBookingCount.Text = $"Hiển thị: {displayList.Count} / {_allBookings.Count} đơn đặt phòng";

			if (displayList.Count > 0)
			{
				dgvBookings.Rows[0].Selected = true;
				DisplayBookingDetails(displayList[0]);
			}
			else
			{
				dgvRooms.DataSource = null;
				dgvPayments.DataSource = null;
			}
		}

		/// <summary>
		/// Hiển thị chi tiết danh sách phòng và thanh toán khi chọn một đơn đặt phòng
		/// </summary>
		private void DisplayBookingDetails(Booking booking)
		{
			// 1. Nạp danh sách phòng chi tiết (Tab 1)
			var roomItems = booking.BookingDetails.Select(d => new
			{
				d.BookingDetailId,
				RoomNumber = d.Room != null ? $"Phòng {d.Room.RoomNumber}" : $"Mã {d.RoomId}",
				RoomType = d.Room?.RoomType?.Name ?? "Tiêu chuẩn",
				d.PricePerNight,
				d.NumberOfNights,
				d.Subtotal,
				RoomStatus = d.Room?.Status ?? "N/A"
			}).ToList();

			dgvRooms.DataSource = roomItems;

			// 2. Nạp danh sách giao dịch thanh toán (Tab 2)
			var paymentItems = booking.Payments.Select(p => new
			{
				p.PaymentId,
				p.Amount,
				PaymentMethod = TranslatePaymentMethod(p.PaymentMethod),
				p.PaymentDate,
				p.Status
			}).ToList();

			dgvPayments.DataSource = paymentItems;
		}

		private void dgvBookings_SelectionChanged(object? sender, EventArgs e)
		{
			if (dgvBookings.CurrentRow?.DataBoundItem is Booking selectedBooking)
			{
				DisplayBookingDetails(selectedBooking);
			}
		}

		private void cboStatusFilter_SelectedIndexChanged(object? sender, EventArgs e)
		{
			ApplyFilterAndDisplay();
		}

		private void btnRefresh_Click(object? sender, EventArgs e)
		{
			LoadHistoryData();
		}

		private void btnClose_Click(object? sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Định dạng dữ liệu và màu sắc trực quan cho DataGridView các đơn đặt phòng
		/// </summary>
		private void dgvBookings_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex >= dgvBookings.Rows.Count) return;

			var column = dgvBookings.Columns[e.ColumnIndex];
			if (dgvBookings.Rows[e.RowIndex].DataBoundItem is not Booking booking) return;

			// 1. Cột STT
			if (column.Name == "colSTT")
			{
				e.Value = (e.RowIndex + 1).ToString();
				e.FormattingApplied = true;
				return;
			}

			// 2. Cột ngày tháng
			if (column.Name is "colBookingDate" or "colCheckInDate" or "colCheckOutDate" && e.Value is DateTime dt)
			{
				e.Value = dt.ToString("dd/MM/yyyy HH:mm");
				e.FormattingApplied = true;
				return;
			}

			// 3. Cột danh sách phòng
			if (column.Name == "colRoomList")
			{
				if (booking.BookingDetails != null && booking.BookingDetails.Count > 0)
				{
					var roomNames = booking.BookingDetails
						.Where(d => d.Room != null)
						.Select(d => d.Room!.RoomNumber)
						.ToList();

					e.Value = roomNames.Count > 0 ? string.Join(", ", roomNames) : $"{booking.BookingDetails.Count} phòng";
				}
				else
				{
					e.Value = "Chưa gán";
				}
				e.FormattingApplied = true;
				return;
			}

			// 4. Cột tổng tiền
			if (column.Name == "colTotalAmount" && e.Value is decimal amount)
			{
				e.Value = string.Format(VnCulture, "{0:#,##0} ₫", amount);
				e.FormattingApplied = true;
				return;
			}

			// 5. Cột đã thanh toán
			if (column.Name == "colPaidAmount")
			{
				decimal paid = booking.Payments
					.Where(p => string.Equals(p.Status, "COMPLETED", StringComparison.OrdinalIgnoreCase))
					.Sum(p => p.Amount);

				e.Value = string.Format(VnCulture, "{0:#,##0} ₫", paid);
				e.FormattingApplied = true;
				return;
			}

			// 6. Cột nhân viên tạo
			if (column.Name == "colStaff")
			{
				e.Value = booking.CreatedByUser?.FullName ?? $"ID: #{booking.CreatedBy}";
				e.FormattingApplied = true;
				return;
			}

			// 7. Cột trạng thái đơn đặt phòng kèm màu sắc Badge
			if (column.Name == "colStatus")
			{
				string status = booking.Status?.ToUpperInvariant() ?? "UNKNOWN";
				switch (status)
				{
					case "PENDING":
						e.Value = "⏳ Đang chờ";
						e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
						e.CellStyle.ForeColor = Color.FromArgb(133, 100, 4);
						break;
					case "CONFIRMED":
						e.Value = "✔️ Đã xác nhận";
						e.CellStyle.BackColor = Color.FromArgb(204, 229, 255);
						e.CellStyle.ForeColor = Color.FromArgb(0, 64, 133);
						break;
					case "CHECKED_IN":
						e.Value = "🏨 Đang lưu trú";
						e.CellStyle.BackColor = Color.FromArgb(212, 237, 218);
						e.CellStyle.ForeColor = Color.FromArgb(21, 87, 36);
						break;
					case "COMPLETED":
						e.Value = "🏁 Hoàn tất";
						e.CellStyle.BackColor = Color.FromArgb(226, 227, 229);
						e.CellStyle.ForeColor = Color.FromArgb(56, 61, 65);
						break;
					case "CANCELLED":
						e.Value = "❌ Đã hủy";
						e.CellStyle.BackColor = Color.FromArgb(248, 215, 218);
						e.CellStyle.ForeColor = Color.FromArgb(114, 28, 36);
						break;
					default:
						e.Value = status;
						break;
				}
				e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
				e.FormattingApplied = true;
			}
		}

		private void dgvRooms_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex >= dgvRooms.Rows.Count) return;

			var column = dgvRooms.Columns[e.ColumnIndex];
			if (column.Name == "colRoomSTT")
			{
				e.Value = (e.RowIndex + 1).ToString();
				e.FormattingApplied = true;
				return;
			}

			if (column.Name is "colPricePerNight" or "colSubtotal" && e.Value is decimal val)
			{
				e.Value = string.Format(VnCulture, "{0:#,##0} ₫", val);
				e.FormattingApplied = true;
				return;
			}
		}

		private void dgvPayments_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex >= dgvPayments.Rows.Count) return;

			var column = dgvPayments.Columns[e.ColumnIndex];
			if (column.Name == "colPaymentSTT")
			{
				e.Value = (e.RowIndex + 1).ToString();
				e.FormattingApplied = true;
				return;
			}

			if (column.Name == "colPaymentAmount" && e.Value is decimal val)
			{
				e.Value = string.Format(VnCulture, "{0:#,##0} ₫", val);
				e.FormattingApplied = true;
				return;
			}

			if (column.Name == "colPaymentDate" && e.Value is DateTime dt)
			{
				e.Value = dt.ToString("dd/MM/yyyy HH:mm");
				e.FormattingApplied = true;
				return;
			}

			if (column.Name == "colPaymentStatus")
			{
				string status = e.Value?.ToString()?.ToUpperInvariant() ?? string.Empty;
				if (status == "COMPLETED")
				{
					e.Value = "Thành công";
					e.CellStyle.ForeColor = Color.FromArgb(25, 135, 84);
				}
				else if (status == "REFUNDED")
				{
					e.Value = "Đã hoàn tiền";
					e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);
				}
				e.FormattingApplied = true;
			}
		}

		private static string TranslatePaymentMethod(string method)
		{
			return method?.ToUpperInvariant() switch
			{
				"CASH" => "Tiền mặt",
				"BANK_TRANSFER" => "Chuyển khoản",
				"CREDIT_CARD" => "Thẻ tín dụng",
				_ => method ?? "Không xác định"
			};
		}
	}
}
