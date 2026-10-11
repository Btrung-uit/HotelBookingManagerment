using System;
using System.Drawing;
using System.Windows.Forms;
using HotelBookingManagement.Business;

namespace HotelBookingManagement.Forms
{
	public partial class MainForm : Form
	{
		private readonly Color _activeNavColor = Color.FromArgb(13, 110, 253);
		private readonly Color _defaultNavColor = Color.FromArgb(34, 40, 60);

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// 1. Hiển thị thông tin người đăng nhập từ UserSession
			if (UserSession.IsLoggedIn)
			{
				string roleName = UserSession.IsManager ? "Quản lý (Manager)" : "Tiếp tân (Receptionist)";
				lblUserGreeting.Text = $"Xin chào: {UserSession.CurrentUser?.FullName} | Vai trò: {roleName}";
			}
			else
			{
				lblUserGreeting.Text = "Chưa đăng nhập";
			}

			// 2. PHÂN QUYỀN (Role-Based Authorization):
			// Nút Báo cáo & Doanh thu chỉ Quản lý mới được phép truy cập
			btnNavReports.Visible = UserSession.IsManager;
		}

		// Xử lý đổi trạng thái nút Sidebar đang chọn
		private void SetActiveTab(Button activeButton, string pageTitle)
		{
			// Đặt lại màu mặc định cho toàn bộ nút trên Sidebar
			btnNavDashboard.BackColor = _defaultNavColor;
			btnNavRooms.BackColor = _defaultNavColor;
			btnNavCustomers.BackColor = _defaultNavColor;
			btnNavBookings.BackColor = _defaultNavColor;
			btnNavCheckIn.BackColor = _defaultNavColor;
			btnNavReports.BackColor = _defaultNavColor;

			// Nổi bật nút đang được click
			activeButton.BackColor = _activeNavColor;
			lblPageTitle.Text = pageTitle;
		}

		private void btnNavDashboard_Click(object sender, EventArgs e)
		{
			SetActiveTab(btnNavDashboard, "TỔNG QUAN HỆ THỐNG");
			panelWelcome.Visible = true;
		}

		private void btnNavRooms_Click(object sender, EventArgs e)
		{
			SetActiveTab(btnNavRooms, "QUẢN LÝ PHÒNG (MODULE TV02)");
			// Nơi TV02 sẽ nhúng UserControl RoomManagement vào panelBody
		}

		private void btnNavCustomers_Click(object sender, EventArgs e)
		{
			SetActiveTab(btnNavCustomers, "QUẢN LÝ KHÁCH HÀNG (MODULE TV02)");
			// Nơi TV02 sẽ nhúng UserControl CustomerManagement vào panelBody
		}

		private void btnNavBookings_Click(object sender, EventArgs e)
		{
			SetActiveTab(btnNavBookings, "ĐẶT PHÒNG - BOOKING (MODULE TV03)");
			// Nơi TV03 sẽ nhúng UserControl BookingManagement vào panelBody
		}

		private void btnNavCheckIn_Click(object sender, EventArgs e)
		{
			SetActiveTab(btnNavCheckIn, "NHẬN VÀ TRẢ PHÒNG (MODULE TV03)");
			// Nơi TV03 sẽ nhúng UserControl CheckInCheckOut vào panelBody
		}

		private void btnNavReports_Click(object sender, EventArgs e)
		{
			SetActiveTab(btnNavReports, "BÁO CÁO DOANH THU (MODULE TV04)");
			// Nơi TV04 sẽ nhúng UserControl ReportsDashboard vào panelBody
		}

		// Xử lý sự kiện bấm Đăng xuất
		private void btnLogout_Click(object sender, EventArgs e)
		{
			var confirm = MessageBox.Show(
				"Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
				"Xác nhận đăng xuất",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (confirm == DialogResult.Yes)
			{
				// Xóa thông tin phiên làm việc hiện tại
				UserSession.Logout();
				// Đóng MainForm để vòng lặp Program.cs tự động quay lại LoginForm
				this.Close();
			}
		}
	}
}
