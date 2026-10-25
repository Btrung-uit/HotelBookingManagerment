using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Business.Services;
using HotelBookingManagement.Forms;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Controls
{
	/// <summary>
	/// Giao diện Quản lý Phòng và Loại phòng (TV02 - Screen 04 / Task ROOM01 -> ROOM06)
	/// Tuân thủ kiến trúc 3 lớp: Gọi IRoomService, không truy vấn trực tiếp CSDL.
	/// </summary>
	public partial class RoomsControl : UserControl
	{
		private readonly IRoomService _roomService;
		private List<RoomType> _allRoomTypes = new();
		private bool _isLoadingFilters = false;
		private static readonly CultureInfo VnCulture = new("vi-VN");

		public RoomsControl() : this(new RoomService())
		{
		}

		/// <summary>
		/// Constructor hỗ trợ Dependency Injection (phục vụ Unit Test hoặc Mocking)
		/// </summary>
		public RoomsControl(IRoomService roomService)
		{
			InitializeComponent();
			_roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
		}

		private void RoomsControl_Load(object? sender, EventArgs e)
		{
			// Ngăn DataGridView tự động sinh cột từ navigation properties
			dgvRooms.AutoGenerateColumns = false;

			colFloor.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			colRoomNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			colCapacity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			colSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			colRoomId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

			InitStatusDropdowns();
			LoadRoomTypes();
			LoadFloors();
			ResetForm();
			LoadRooms();
		}

		private void InitStatusDropdowns()
		{
			// Khởi tạo ComboBox trạng thái trong Form nhập liệu
			cboStatus.Items.Clear();
			cboStatus.Items.Add(new ComboBoxItem("AVAILABLE", "AVAILABLE - Sẵn sàng đón khách"));
			cboStatus.Items.Add(new ComboBoxItem("RESERVED", "RESERVED - Đã đặt trước"));
			cboStatus.Items.Add(new ComboBoxItem("OCCUPIED", "OCCUPIED - Đang có khách ở"));
			cboStatus.Items.Add(new ComboBoxItem("CLEANING", "CLEANING - Đang dọn dẹp vệ sinh"));
			cboStatus.Items.Add(new ComboBoxItem("MAINTENANCE", "MAINTENANCE - Đang bảo trì / Sửa chữa"));
			cboStatus.SelectedIndex = 0;

			// Khởi tạo ComboBox lọc trạng thái trên thanh công cụ (ROOM05)
			_isLoadingFilters = true;
			cboFilterStatus.Items.Clear();
			cboFilterStatus.Items.Add(new ComboBoxItem(null, "-- Tất cả trạng thái phòng --"));
			cboFilterStatus.Items.Add(new ComboBoxItem("AVAILABLE", "🟢 Sẵn sàng (AVAILABLE)"));
			cboFilterStatus.Items.Add(new ComboBoxItem("OCCUPIED", "🔴 Đang có khách (OCCUPIED)"));
			cboFilterStatus.Items.Add(new ComboBoxItem("RESERVED", "🔵 Đã đặt trước (RESERVED)"));
			cboFilterStatus.Items.Add(new ComboBoxItem("CLEANING", "🟡 Đang dọn dẹp (CLEANING)"));
			cboFilterStatus.Items.Add(new ComboBoxItem("MAINTENANCE", "⚪ Đang bảo trì (MAINTENANCE)"));
			cboFilterStatus.SelectedIndex = 0;
			_isLoadingFilters = false;
		}

		/// <summary>
		/// Tải danh mục loại phòng lên cả 2 ComboBox: nhập liệu và bộ lọc (ROOM01)
		/// </summary>
		private void LoadRoomTypes()
		{
			try
			{
				_allRoomTypes = _roomService.GetAllRoomTypes().ToList();

				// Nạp ComboBox nhập liệu phòng
				cboRoomType.Items.Clear();
				foreach (var rt in _allRoomTypes)
				{
					cboRoomType.Items.Add(new ComboBoxItem(rt.RoomTypeId, $"{rt.Name} ({rt.Capacity} người - {rt.PricePerNight.ToString("#,##0 đ", VnCulture)})"));
				}
				if (cboRoomType.Items.Count > 0)
				{
					cboRoomType.SelectedIndex = 0;
				}

				// Nạp ComboBox lọc loại phòng
				_isLoadingFilters = true;
				cboFilterType.Items.Clear();
				cboFilterType.Items.Add(new ComboBoxItem(null, "-- Tất cả loại phòng --"));
				foreach (var rt in _allRoomTypes)
				{
					cboFilterType.Items.Add(new ComboBoxItem(rt.RoomTypeId, rt.Name));
				}
				cboFilterType.SelectedIndex = 0;
				_isLoadingFilters = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi tải danh mục loại phòng: {ex.Message}", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Tải danh sách tầng lên ComboBox lọc (ROOM05)
		/// </summary>
		private void LoadFloors()
		{
			try
			{
				_isLoadingFilters = true;
				cboFilterFloor.Items.Clear();
				cboFilterFloor.Items.Add(new ComboBoxItem(null, "-- Tất cả các tầng --"));

				var floors = _roomService.GetDistinctFloors();
				foreach (var floor in floors)
				{
					cboFilterFloor.Items.Add(new ComboBoxItem(floor, $"Tầng {floor}"));
				}
				cboFilterFloor.SelectedIndex = 0;
				_isLoadingFilters = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi nạp danh sách tầng: {ex.Message}", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Nạp danh sách phòng dựa trên từ khóa tìm kiếm (ROOM04) và bộ lọc đa tiêu chí (ROOM05)
		/// </summary>
		private void LoadRooms()
		{
			if (_isLoadingFilters) return;

			try
			{
				string? searchKeyword = txtSearch.Text.Trim();
				int? filterTypeId = (cboFilterType.SelectedItem as ComboBoxItem)?.Value as int?;
				string? filterStatus = (cboFilterStatus.SelectedItem as ComboBoxItem)?.Value as string;
				int? filterFloor = (cboFilterFloor.SelectedItem as ComboBoxItem)?.Value as int?;

				IEnumerable<Room> rooms;

				if (!string.IsNullOrWhiteSpace(searchKeyword))
				{
					// Nếu có từ khóa tìm kiếm: Tìm theo từ khóa trước, sau đó áp dụng bộ lọc kết hợp
					rooms = _roomService.SearchRooms(searchKeyword);
					if (filterTypeId.HasValue)
					{
						rooms = rooms.Where(r => r.RoomTypeId == filterTypeId.Value);
					}
					if (!string.IsNullOrWhiteSpace(filterStatus))
					{
						rooms = rooms.Where(r => string.Equals(r.Status, filterStatus, StringComparison.OrdinalIgnoreCase));
					}
					if (filterFloor.HasValue)
					{
						rooms = rooms.Where(r => r.Floor == filterFloor.Value);
					}
				}
				else
				{
					// Lọc trực tiếp qua SQL Query đa tiêu chí
					rooms = _roomService.FilterRooms(filterTypeId, filterStatus, filterFloor);
				}

				var list = rooms.ToList();
				dgvRooms.DataSource = list;

				UpdateStatusSummary();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi tải danh sách phòng: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Cập nhật thanh tóm tắt số liệu các trạng thái phòng (DASH02)
		/// </summary>
		private void UpdateStatusSummary()
		{
			try
			{
				var (total, available, reserved, occupied, cleaning, maintenance) = _roomService.GetRoomStatusSummary();
				lblSummaryAll.Text = $"📊 Tổng: {total}";
				lblSummaryAvailable.Text = $"🟢 Trống: {available}";
				lblSummaryOccupied.Text = $"🔴 Đang ở: {occupied}";
				lblSummaryReserved.Text = $"🔵 Đã đặt: {reserved}";
				lblSummaryCleaning.Text = $"🟡 Dọn dẹp: {cleaning}";
				lblSummaryMaintenance.Text = $"⚪ Bảo trì: {maintenance}";
			}
			catch
			{
				// Tránh crash nếu thống kê tạm thời gặp sự cố
			}
		}

		/// <summary>
		/// Chuẩn hóa hiển thị và định dạng màu sắc cho DataGridView (UI05)
		/// </summary>
		private void dgvRooms_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex >= dgvRooms.Rows.Count) return;

			var room = dgvRooms.Rows[e.RowIndex].DataBoundItem as Room;
			if (room == null) return;

			string colName = dgvRooms.Columns[e.ColumnIndex].Name;

			if (colName == "colSTT")
			{
				e.Value = (e.RowIndex + 1).ToString();
				e.FormattingApplied = true;
			}
			else if (colName == "colRoomType")
			{
				e.Value = room.RoomType?.Name ?? $"Loại #{room.RoomTypeId}";
				e.FormattingApplied = true;
			}
			else if (colName == "colPrice" && room.RoomType != null)
			{
				e.Value = room.RoomType.PricePerNight.ToString("#,##0 đ", VnCulture);
				e.FormattingApplied = true;
			}
			else if (colName == "colCapacity" && room.RoomType != null)
			{
				e.Value = $"{room.RoomType.Capacity} người";
				e.FormattingApplied = true;
			}
			else if (colName == "colStatus")
			{
				string status = room.Status?.ToUpperInvariant() ?? string.Empty;
				switch (status)
				{
					case "AVAILABLE":
						e.Value = "🟢 Sẵn sàng";
						e.CellStyle.ForeColor = Color.FromArgb(15, 81, 50);
						e.CellStyle.BackColor = Color.FromArgb(209, 231, 221);
						e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
						break;

					case "OCCUPIED":
						e.Value = "🔴 Đang có khách";
						e.CellStyle.ForeColor = Color.FromArgb(132, 32, 41);
						e.CellStyle.BackColor = Color.FromArgb(248, 215, 218);
						e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
						break;

					case "RESERVED":
						e.Value = "🔵 Đã đặt trước";
						e.CellStyle.ForeColor = Color.FromArgb(5, 44, 106);
						e.CellStyle.BackColor = Color.FromArgb(207, 226, 255);
						e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
						break;

					case "CLEANING":
						e.Value = "🟡 Đang dọn dẹp";
						e.CellStyle.ForeColor = Color.FromArgb(102, 77, 3);
						e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
						e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
						break;

					case "MAINTENANCE":
						e.Value = "⚪ Đang bảo trì";
						e.CellStyle.ForeColor = Color.FromArgb(65, 70, 75);
						e.CellStyle.BackColor = Color.FromArgb(226, 227, 229);
						e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
						break;
				}
				e.FormattingApplied = true;
			}
		}

		/// <summary>
		/// Khi click một dòng trên bảng -> Nạp thông tin phòng vào Form
		/// </summary>
		private void dgvRooms_CellClick(object? sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex >= dgvRooms.Rows.Count) return;

			var row = dgvRooms.Rows[e.RowIndex];
			if (row.DataBoundItem is Room room)
			{
				PopulateForm(room);
			}
		}

		private void PopulateForm(Room room)
		{
			txtRoomId.Text = room.RoomId.ToString();
			txtRoomNumber.Text = room.RoomNumber;
			numFloor.Value = Math.Max(numFloor.Minimum, Math.Min(numFloor.Maximum, room.Floor));
			txtDescription.Text = room.Description ?? string.Empty;

			// Chọn Loại phòng tương ứng trong ComboBox
			for (int i = 0; i < cboRoomType.Items.Count; i++)
			{
				if (cboRoomType.Items[i] is ComboBoxItem item && item.Value is int typeId && typeId == room.RoomTypeId)
				{
					cboRoomType.SelectedIndex = i;
					break;
				}
			}

			// Chọn Trạng thái tương ứng trong ComboBox
			for (int i = 0; i < cboStatus.Items.Count; i++)
			{
				if (cboStatus.Items[i] is ComboBoxItem item && item.Value is string status && string.Equals(status, room.Status, StringComparison.OrdinalIgnoreCase))
				{
					cboStatus.SelectedIndex = i;
					break;
				}
			}
		}

		private void ResetForm()
		{
			txtRoomId.Clear();
			txtRoomNumber.Clear();
			numFloor.Value = 1;
			if (cboRoomType.Items.Count > 0) cboRoomType.SelectedIndex = 0;
			cboStatus.SelectedIndex = 0;
			txtDescription.Clear();
			txtRoomNumber.Focus();
		}

		#region Thao tác CRUD (ROOM02 & ROOM06)

		private void btnAdd_Click(object? sender, EventArgs e)
		{
			if (cboRoomType.SelectedItem is not ComboBoxItem typeItem || typeItem.Value is not int selectedTypeId)
			{
				MessageBox.Show("Vui lòng chọn loại phòng hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string selectedStatus = (cboStatus.SelectedItem as ComboBoxItem)?.Value as string ?? "AVAILABLE";

			var room = new Room
			{
				RoomNumber = txtRoomNumber.Text.Trim(),
				RoomTypeId = selectedTypeId,
				Floor = (int)numFloor.Value,
				Status = selectedStatus,
				Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim()
			};

			try
			{
				int newId = _roomService.CreateRoom(room);
				if (newId > 0)
				{
					MessageBox.Show(
						$"Thêm mới phòng '{room.RoomNumber}' thành công (Mã: #{newId})!",
						"Thành công",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);

					ResetForm();
					LoadFloors();
					LoadRooms();
				}
				else
				{
					MessageBox.Show("Không thể thêm phòng mới. Vui lòng thử lại.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnUpdate_Click(object? sender, EventArgs e)
		{
			if (!int.TryParse(txtRoomId.Text, out int id) || id <= 0)
			{
				MessageBox.Show("Vui lòng chọn một căn phòng từ danh sách để sửa thông tin.", "Chưa chọn phòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (cboRoomType.SelectedItem is not ComboBoxItem typeItem || typeItem.Value is not int selectedTypeId)
			{
				MessageBox.Show("Vui lòng chọn loại phòng hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string selectedStatus = (cboStatus.SelectedItem as ComboBoxItem)?.Value as string ?? "AVAILABLE";

			var room = new Room
			{
				RoomId = id,
				RoomNumber = txtRoomNumber.Text.Trim(),
				RoomTypeId = selectedTypeId,
				Floor = (int)numFloor.Value,
				Status = selectedStatus,
				Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim()
			};

			try
			{
				bool success = _roomService.UpdateRoom(room);
				if (success)
				{
					MessageBox.Show(
						$"Cập nhật thông tin phòng '{room.RoomNumber}' thành công!",
						"Thành công",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);

					LoadRooms();
				}
				else
				{
					MessageBox.Show("Cập nhật không thành công. Phòng có thể đã bị xóa trước đó.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnDelete_Click(object? sender, EventArgs e)
		{
			if (!int.TryParse(txtRoomId.Text, out int id) || id <= 0)
			{
				MessageBox.Show("Vui lòng chọn một căn phòng từ danh sách để xóa.", "Chưa chọn phòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			string roomNumber = txtRoomNumber.Text.Trim();
			var confirm = MessageBox.Show(
				$"Bạn có chắc chắn muốn xóa phòng '{roomNumber}' (Mã: #{id}) không?\n\nLưu ý: Không thể xóa nếu phòng đã từng phát sinh giao dịch đặt phòng hoặc đang có khách ở.",
				"Xác nhận xóa phòng",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (confirm == DialogResult.Yes)
			{
				try
				{
					bool deleted = _roomService.DeleteRoom(id);
					if (deleted)
					{
						MessageBox.Show(
							$"Đã xóa thành công phòng '{roomNumber}'!",
							"Thành công",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);

						ResetForm();
						LoadFloors();
						LoadRooms();
					}
					else
					{
						MessageBox.Show("Xóa không thành công. Phòng không còn tồn tại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Không thể xóa phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		private void btnClear_Click(object? sender, EventArgs e)
		{
			ResetForm();
		}

		#endregion

		#region Quản lý Loại phòng Dialog (ROOM01)

		private void btnManageRoomTypes_Click(object? sender, EventArgs e)
		{
			using var frm = new FrmRoomType(_roomService);
			frm.ShowDialog(this);

			// Nếu danh mục loại phòng có thay đổi -> Làm mới lại danh sách
			if (frm.HasDataChanged)
			{
				LoadRoomTypes();
				LoadRooms();
			}
		}

		#endregion

		#region Chuyển đổi trạng thái nhanh (ROOM03 - Algorithm 3)

		private void btnSetAvailable_Click(object? sender, EventArgs e)
		{
			QuickUpdateStatus("AVAILABLE", "Chuyển trạng thái phòng sang: SẴN SÀNG ĐÓN KHÁCH (AVAILABLE)?");
		}

		private void btnSetMaintenance_Click(object? sender, EventArgs e)
		{
			QuickUpdateStatus("MAINTENANCE", "Báo hỏng phòng và chuyển trạng thái sang: ĐANG BẢO TRÌ (MAINTENANCE)?");
		}

		private void btnFinishMaintenance_Click(object? sender, EventArgs e)
		{
			QuickUpdateStatus("AVAILABLE", "Hoàn tất bảo trì và đưa phòng trở lại trạng thái: SẴN SÀNG (AVAILABLE)?");
		}

		private void QuickUpdateStatus(string targetStatus, string confirmPrompt)
		{
			if (!int.TryParse(txtRoomId.Text, out int id) || id <= 0)
			{
				MessageBox.Show("Vui lòng chọn một căn phòng từ danh sách để cập nhật trạng thái.", "Chưa chọn phòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			string roomNum = txtRoomNumber.Text.Trim();
			var confirm = MessageBox.Show(
				$"{confirmPrompt}\n\nPhòng được chọn: {roomNum}",
				"Xác nhận cập nhật trạng thái nhanh",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (confirm == DialogResult.Yes)
			{
				try
				{
					bool updated = _roomService.UpdateRoomStatus(id, targetStatus);
					if (updated)
					{
						MessageBox.Show(
							$"Đã cập nhật trạng thái phòng '{roomNum}' sang {targetStatus} thành công!",
							"Thành công",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);

						LoadRooms();

						// Đồng bộ lại ComboBox trạng thái trên form
						for (int i = 0; i < cboStatus.Items.Count; i++)
						{
							if (cboStatus.Items[i] is ComboBoxItem item && item.Value is string st && string.Equals(st, targetStatus, StringComparison.OrdinalIgnoreCase))
							{
								cboStatus.SelectedIndex = i;
								break;
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Không thể chuyển trạng thái", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		#endregion

		#region Tìm kiếm & Lọc phòng (ROOM04 & ROOM05)

		private void btnSearch_Click(object? sender, EventArgs e)
		{
			LoadRooms();
		}

		private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				LoadRooms();
			}
		}

		private void Filter_SelectedIndexChanged(object? sender, EventArgs e)
		{
			LoadRooms();
		}

		private void btnResetFilter_Click(object? sender, EventArgs e)
		{
			_isLoadingFilters = true;
			txtSearch.Clear();
			cboFilterType.SelectedIndex = 0;
			cboFilterStatus.SelectedIndex = 0;
			cboFilterFloor.SelectedIndex = 0;
			_isLoadingFilters = false;

			LoadRooms();
		}

		#endregion

		/// <summary>
		/// Lớp hỗ trợ binding hiển thị văn bản thân thiện kèm giá trị thực tế cho ComboBox
		/// </summary>
		private class ComboBoxItem
		{
			public object? Value { get; }
			public string Text { get; }

			public ComboBoxItem(object? value, string text)
			{
				Value = value;
				Text = text;
			}

			public override string ToString() => Text;
		}
	}
}
