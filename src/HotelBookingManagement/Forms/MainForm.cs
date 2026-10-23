using System;
using System.Drawing;
using System.Windows.Forms;
using HotelBookingManagement.Business;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Business.Services;
using HotelBookingManagement.Controls;
using HotelBookingManagement.Data.Repositories;

namespace HotelBookingManagement.Forms
{
	public partial class MainForm : Form
	{
		private readonly Color _activeNavColor = Color.FromArgb(13, 110, 253);
		private readonly Color _defaultNavColor = Color.FromArgb(34, 40, 60);
		private readonly IDashboardService _dashboardService;

		// Khai báo các UserControl đại diện cho module của từng thành viên
		private RoomsControl? _roomsControl;
		private CustomersControl? _customersControl;
		private BookingsControl? _bookingsControl;
		private CheckInOutControl? _checkInOutControl;
		private ReportsControl? _reportsControl;

		public MainForm()
		{
			InitializeComponent();
			var dashboardRepo = new DashboardRepository();
			_dashboardService = new DashboardService(dashboardRepo);
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
			
			// Nạp dữ liệu thống kê ban đầu cho Dashboard
			ShowDashboard();
		}

		// Nạp số liệu thống kê thật từ database lên 3 Card
		private void LoadDashboardStats()
		{
			try
			{
				var stats = _dashboardService.GetStats();
				lblCardRoomsValue.Text = stats.TotalRooms.ToString();
				lblCardAvailableValue.Text = stats.AvailableRooms.ToString();
				lblCardBookingsValue.Text = stats.TotalBookings.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Không thể tải dữ liệu thống kê: {ex.Message}", "Lỗi dữ liệu",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// Đổi màu nút Sidebar đang chọn
		private void SetActiveTab(Button activeButton, string pageTitle)
		{
			btnNavDashboard.BackColor = _defaultNavColor;
			btnNavRooms.BackColor = _defaultNavColor;
			btnNavCustomers.BackColor = _defaultNavColor;
			btnNavBookings.BackColor = _defaultNavColor;
			btnNavCheckIn.BackColor = _defaultNavColor;
			btnNavReports.BackColor = _defaultNavColor;

			activeButton.BackColor = _activeNavColor;
			lblPageTitle.Text = pageTitle;
		}

		// Cơ chế điều hướng: Hiển thị Dashboard
		private void ShowDashboard()
		{
			SetActiveTab(btnNavDashboard, "TỔNG QUAN HỆ THỐNG");

			// Ẩn tất cả các UserControl con nếu đang mở
			foreach (Control c in panelBody.Controls)
			{
				if (c is UserControl uc)
				{
					uc.Visible = false;
				}
			}

			// Hiện lại các thẻ thống kê của Dashboard
			panelCardRooms.Visible = true;
			panelCardAvailable.Visible = true;
			panelCardBookings.Visible = true;
			panelWelcome.Visible = true;

			LoadDashboardStats();
		}

		// Cơ chế điều hướng: Nhúng một UserControl vào panelBody (Navigation Container Pattern)
		private void ShowView(UserControl view, string pageTitle)
		{
			// Ẩn các thành phần của Dashboard
			panelCardRooms.Visible = false;
			panelCardAvailable.Visible = false;
			panelCardBookings.Visible = false;
			panelWelcome.Visible = false;

			// Ẩn các UserControl khác
			foreach (Control c in panelBody.Controls)
			{
				if (c is UserControl uc && uc != view)
				{
					uc.Visible = false;
				}
			}

			// Nếu UserControl chưa có trong panelBody thì thêm vào
			if (!panelBody.Controls.Contains(view))
			{
				view.Dock = DockStyle.Fill;
				panelBody.Controls.Add(view);
			}

			view.Visible = true;
			view.BringToFront();
		}

		// 1. Click nút Tổng quan (Dashboard)
		private void btnNavDashboard_Click(object sender, EventArgs e)
		{
			ShowDashboard();
		}

		// 2. Click nút Quản lý Phòng (Module TV02)
		private void btnNavRooms_Click(object sender, EventArgs e)
		{
			_roomsControl ??= new RoomsControl();
			SetActiveTab(btnNavRooms, "QUẢN LÝ PHÒNG (ROOMS)");
			ShowView(_roomsControl, "QUẢN LÝ PHÒNG (ROOMS)");
		}

		// 3. Click nút Quản lý Khách hàng (Module TV02)
		private void btnNavCustomers_Click(object sender, EventArgs e)
		{
			_customersControl ??= new CustomersControl();
			SetActiveTab(btnNavCustomers, "QUẢN LÝ KHÁCH HÀNG (CUSTOMERS)");
			ShowView(_customersControl, "QUẢN LÝ KHÁCH HÀNG (CUSTOMERS)");
		}

		// 4. Click nút Đặt phòng (Module TV03)
		private void btnNavBookings_Click(object sender, EventArgs e)
		{
			_bookingsControl ??= new BookingsControl();
			SetActiveTab(btnNavBookings, "ĐẶT PHÒNG - BOOKING");
			ShowView(_bookingsControl, "ĐẶT PHÒNG - BOOKING");
		}

		// 5. Click nút Nhận / Trả phòng (Module TV03)
		private void btnNavCheckIn_Click(object sender, EventArgs e)
		{
			_checkInOutControl ??= new CheckInOutControl();
			SetActiveTab(btnNavCheckIn, "NHẬN VÀ TRẢ PHÒNG (CHECK-IN / OUT)");
			ShowView(_checkInOutControl, "NHẬN VÀ TRẢ PHÒNG (CHECK-IN / OUT)");
		}

		// 6. Click nút Báo cáo doanh thu (Module TV04)
		private void btnNavReports_Click(object sender, EventArgs e)
		{
			_reportsControl ??= new ReportsControl();
			SetActiveTab(btnNavReports, "BÁO CÁO DOANH THU (REPORTS & KPI)");
			ShowView(_reportsControl, "BÁO CÁO DOANH THU (REPORTS & KPI)");
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
				UserSession.Logout();
				this.Close();
			}
		}
	}
}
