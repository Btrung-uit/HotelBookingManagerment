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
	/// Giao diện Quản lý Loại phòng (TV02 - Screen 05 / Task ROOM01)
	/// Tuân thủ kiến trúc 3 lớp: Gọi IRoomService, không truy vấn trực tiếp CSDL.
	/// </summary>
	public partial class FrmRoomType : Form
	{
		private readonly IRoomService _roomService;
		private static readonly CultureInfo VnCulture = new("vi-VN");

		/// <summary>
		/// Đánh dấu xem danh mục loại phòng có thay đổi hay không để Form cha cập nhật lại ComboBox
		/// </summary>
		public bool HasDataChanged { get; private set; } = false;

		public FrmRoomType() : this(new RoomService())
		{
		}

		public FrmRoomType(IRoomService roomService)
		{
			InitializeComponent();
			_roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
		}

		private void FrmRoomType_Load(object? sender, EventArgs e)
		{
			dgvRoomTypes.AutoGenerateColumns = false;
			colPricePerNight.DefaultCellStyle.Format = "N0";
			colPricePerNight.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			colCapacity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			colRoomTypeId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			colSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

			ResetForm();
			LoadRoomTypeData();
		}

		/// <summary>
		/// Tải danh sách loại phòng lên bảng DataGridView
		/// </summary>
		private void LoadRoomTypeData()
		{
			try
			{
				var list = _roomService.GetAllRoomTypes().ToList();
				dgvRoomTypes.DataSource = list;
				lblTotalCount.Text = $"Tổng số: {list.Count} loại phòng";
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					$"Lỗi khi tải danh mục loại phòng: {ex.Message}",
					"Lỗi hệ thống",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Đánh số thứ tự liên tục 1, 2, 3...
		/// </summary>
		private void dgvRoomTypes_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex >= 0 && dgvRoomTypes.Columns[e.ColumnIndex].Name == "colSTT")
			{
				e.Value = (e.RowIndex + 1).ToString();
				e.FormattingApplied = true;
			}
			else if (e.RowIndex >= 0 && dgvRoomTypes.Columns[e.ColumnIndex].Name == "colPricePerNight" && e.Value != null)
			{
				if (decimal.TryParse(e.Value.ToString(), out decimal price))
				{
					e.Value = price.ToString("#,##0 đ", VnCulture);
					e.FormattingApplied = true;
				}
			}
		}

		/// <summary>
		/// Chọn dòng trên bảng -> nạp lên Form
		/// </summary>
		private void dgvRoomTypes_CellClick(object? sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex >= dgvRoomTypes.Rows.Count) return;

			var row = dgvRoomTypes.Rows[e.RowIndex];
			if (row.DataBoundItem is RoomType rt)
			{
				PopulateForm(rt);
			}
		}

		private void PopulateForm(RoomType rt)
		{
			txtRoomTypeId.Text = rt.RoomTypeId.ToString();
			txtName.Text = rt.Name;
			numCapacity.Value = Math.Max(numCapacity.Minimum, Math.Min(numCapacity.Maximum, rt.Capacity));
			numPricePerNight.Value = Math.Max(numPricePerNight.Minimum, Math.Min(numPricePerNight.Maximum, rt.PricePerNight));
			txtDescription.Text = rt.Description ?? string.Empty;
		}

		private void ResetForm()
		{
			txtRoomTypeId.Clear();
			txtName.Clear();
			numCapacity.Value = 2;
			numPricePerNight.Value = 500000;
			txtDescription.Clear();
			txtName.Focus();
		}

		private void btnAdd_Click(object? sender, EventArgs e)
		{
			var roomType = new RoomType
			{
				Name = txtName.Text.Trim(),
				Capacity = (int)numCapacity.Value,
				PricePerNight = numPricePerNight.Value,
				Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim()
			};

			try
			{
				int newId = _roomService.CreateRoomType(roomType);
				if (newId > 0)
				{
					HasDataChanged = true;
					MessageBox.Show(
						$"Thêm mới loại phòng '{roomType.Name}' thành công (Mã: #{newId})!",
						"Thành công",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);

					ResetForm();
					LoadRoomTypeData();
				}
				else
				{
					MessageBox.Show("Không thể thêm loại phòng. Vui lòng thử lại.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnUpdate_Click(object? sender, EventArgs e)
		{
			if (!int.TryParse(txtRoomTypeId.Text, out int id) || id <= 0)
			{
				MessageBox.Show("Vui lòng chọn một loại phòng từ danh sách để sửa thông tin.", "Chưa chọn loại phòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			var roomType = new RoomType
			{
				RoomTypeId = id,
				Name = txtName.Text.Trim(),
				Capacity = (int)numCapacity.Value,
				PricePerNight = numPricePerNight.Value,
				Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim()
			};

			try
			{
				bool success = _roomService.UpdateRoomType(roomType);
				if (success)
				{
					HasDataChanged = true;
					MessageBox.Show(
						$"Cập nhật thông tin loại phòng '{roomType.Name}' thành công!",
						"Thành công",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);

					LoadRoomTypeData();
				}
				else
				{
					MessageBox.Show("Cập nhật không thành công. Bản ghi có thể không còn tồn tại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnDelete_Click(object? sender, EventArgs e)
		{
			if (!int.TryParse(txtRoomTypeId.Text, out int id) || id <= 0)
			{
				MessageBox.Show("Vui lòng chọn một loại phòng từ danh sách để xóa.", "Chưa chọn loại phòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			string name = txtName.Text.Trim();
			var confirm = MessageBox.Show(
				$"Bạn có chắc chắn muốn xóa loại phòng '{name}' (Mã: #{id}) không?\n\nLưu ý: Chỉ xóa được loại phòng khi không có phòng nào đang thuộc loại phòng này.",
				"Xác nhận xóa loại phòng",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (confirm == DialogResult.Yes)
			{
				try
				{
					bool deleted = _roomService.DeleteRoomType(id);
					if (deleted)
					{
						HasDataChanged = true;
						MessageBox.Show(
							$"Đã xóa thành công loại phòng '{name}'!",
							"Thành công",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);

						ResetForm();
						LoadRoomTypeData();
					}
					else
					{
						MessageBox.Show("Xóa không thành công. Bản ghi không còn tồn tại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Không thể xóa loại phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		private void btnClear_Click(object? sender, EventArgs e)
		{
			ResetForm();
		}

		private void btnClose_Click(object? sender, EventArgs e)
		{
			Close();
		}
	}
}
