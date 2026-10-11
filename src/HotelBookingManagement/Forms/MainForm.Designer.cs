using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingManagement.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			panelTop = new Panel();
			lblAppTitle = new Label();
			lblUserGreeting = new Label();
			btnLogout = new Button();
			panelSidebar = new Panel();
			btnNavDashboard = new Button();
			btnNavRooms = new Button();
			btnNavCustomers = new Button();
			btnNavBookings = new Button();
			btnNavCheckIn = new Button();
			btnNavReports = new Button();
			lblSidebarFooter = new Label();
			panelContent = new Panel();
			panelBody = new Panel();
			panelWelcome = new Panel();
			lblWelcomeDesc = new Label();
			lblWelcomeTitle = new Label();
			panelCardBookings = new Panel();
			lblCardBookingsValue = new Label();
			lblCardBookingsTitle = new Label();
			panelCardAvailable = new Panel();
			lblCardAvailableValue = new Label();
			lblCardAvailableTitle = new Label();
			panelCardRooms = new Panel();
			lblCardRoomsValue = new Label();
			lblCardRoomsTitle = new Label();
			panelContentHeader = new Panel();
			lblPageTitle = new Label();
			panelTop.SuspendLayout();
			panelSidebar.SuspendLayout();
			panelContent.SuspendLayout();
			panelBody.SuspendLayout();
			panelWelcome.SuspendLayout();
			panelCardBookings.SuspendLayout();
			panelCardAvailable.SuspendLayout();
			panelCardRooms.SuspendLayout();
			panelContentHeader.SuspendLayout();
			SuspendLayout();
			// 
			// panelTop
			// 
			panelTop.BackColor = Color.FromArgb(24, 30, 54);
			panelTop.Controls.Add(lblAppTitle);
			panelTop.Controls.Add(lblUserGreeting);
			panelTop.Controls.Add(btnLogout);
			panelTop.Dock = DockStyle.Top;
			panelTop.Location = new Point(0, 0);
			panelTop.Name = "panelTop";
			panelTop.Size = new Size(1100, 65);
			panelTop.TabIndex = 0;
			// 
			// lblAppTitle
			// 
			lblAppTitle.AutoSize = true;
			lblAppTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
			lblAppTitle.ForeColor = Color.White;
			lblAppTitle.Location = new Point(20, 18);
			lblAppTitle.Name = "lblAppTitle";
			lblAppTitle.Size = new Size(406, 31);
			lblAppTitle.TabIndex = 0;
			lblAppTitle.Text = "🏨 HOTEL BOOKING MANAGEMENT";
			// 
			// lblUserGreeting
			// 
			lblUserGreeting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lblUserGreeting.Font = new Font("Segoe UI", 10F);
			lblUserGreeting.ForeColor = Color.FromArgb(220, 224, 230);
			lblUserGreeting.Location = new Point(480, 20);
			lblUserGreeting.Name = "lblUserGreeting";
			lblUserGreeting.Size = new Size(470, 25);
			lblUserGreeting.TabIndex = 1;
			lblUserGreeting.Text = "Xin chào: Nguyễn Quản Lý | Vai trò: Manager";
			lblUserGreeting.TextAlign = ContentAlignment.MiddleRight;
			// 
			// btnLogout
			// 
			btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnLogout.BackColor = Color.FromArgb(220, 53, 69);
			btnLogout.Cursor = Cursors.Hand;
			btnLogout.FlatAppearance.BorderSize = 0;
			btnLogout.FlatStyle = FlatStyle.Flat;
			btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnLogout.ForeColor = Color.White;
			btnLogout.Location = new Point(965, 16);
			btnLogout.Name = "btnLogout";
			btnLogout.Size = new Size(110, 33);
			btnLogout.TabIndex = 2;
			btnLogout.Text = "Đăng xuất";
			btnLogout.UseVisualStyleBackColor = false;
			btnLogout.Click += btnLogout_Click;
			// 
			// panelSidebar
			// 
			panelSidebar.BackColor = Color.FromArgb(34, 40, 60);
			panelSidebar.Controls.Add(btnNavDashboard);
			panelSidebar.Controls.Add(btnNavRooms);
			panelSidebar.Controls.Add(btnNavCustomers);
			panelSidebar.Controls.Add(btnNavBookings);
			panelSidebar.Controls.Add(btnNavCheckIn);
			panelSidebar.Controls.Add(btnNavReports);
			panelSidebar.Controls.Add(lblSidebarFooter);
			panelSidebar.Dock = DockStyle.Left;
			panelSidebar.Location = new Point(0, 65);
			panelSidebar.Name = "panelSidebar";
			panelSidebar.Size = new Size(230, 635);
			panelSidebar.TabIndex = 1;
			// 
			// btnNavDashboard
			// 
			btnNavDashboard.BackColor = Color.FromArgb(13, 110, 253);
			btnNavDashboard.Cursor = Cursors.Hand;
			btnNavDashboard.FlatAppearance.BorderSize = 0;
			btnNavDashboard.FlatStyle = FlatStyle.Flat;
			btnNavDashboard.Font = new Font("Segoe UI", 10.5F);
			btnNavDashboard.ForeColor = Color.White;
			btnNavDashboard.Location = new Point(0, 15);
			btnNavDashboard.Name = "btnNavDashboard";
			btnNavDashboard.Padding = new Padding(20, 0, 0, 0);
			btnNavDashboard.Size = new Size(230, 48);
			btnNavDashboard.TabIndex = 0;
			btnNavDashboard.Text = "📊  Tổng quan";
			btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft;
			btnNavDashboard.UseVisualStyleBackColor = false;
			btnNavDashboard.Click += btnNavDashboard_Click;
			// 
			// btnNavRooms
			// 
			btnNavRooms.BackColor = Color.FromArgb(34, 40, 60);
			btnNavRooms.Cursor = Cursors.Hand;
			btnNavRooms.FlatAppearance.BorderSize = 0;
			btnNavRooms.FlatStyle = FlatStyle.Flat;
			btnNavRooms.Font = new Font("Segoe UI", 10.5F);
			btnNavRooms.ForeColor = Color.Gainsboro;
			btnNavRooms.Location = new Point(0, 68);
			btnNavRooms.Name = "btnNavRooms";
			btnNavRooms.Padding = new Padding(20, 0, 0, 0);
			btnNavRooms.Size = new Size(230, 48);
			btnNavRooms.TabIndex = 1;
			btnNavRooms.Text = "🛏️  Quản lý Phòng";
			btnNavRooms.TextAlign = ContentAlignment.MiddleLeft;
			btnNavRooms.UseVisualStyleBackColor = false;
			btnNavRooms.Click += btnNavRooms_Click;
			// 
			// btnNavCustomers
			// 
			btnNavCustomers.BackColor = Color.FromArgb(34, 40, 60);
			btnNavCustomers.Cursor = Cursors.Hand;
			btnNavCustomers.FlatAppearance.BorderSize = 0;
			btnNavCustomers.FlatStyle = FlatStyle.Flat;
			btnNavCustomers.Font = new Font("Segoe UI", 10.5F);
			btnNavCustomers.ForeColor = Color.Gainsboro;
			btnNavCustomers.Location = new Point(0, 121);
			btnNavCustomers.Name = "btnNavCustomers";
			btnNavCustomers.Padding = new Padding(20, 0, 0, 0);
			btnNavCustomers.Size = new Size(230, 48);
			btnNavCustomers.TabIndex = 2;
			btnNavCustomers.Text = "👥  Quản lý Khách";
			btnNavCustomers.TextAlign = ContentAlignment.MiddleLeft;
			btnNavCustomers.UseVisualStyleBackColor = false;
			btnNavCustomers.Click += btnNavCustomers_Click;
			// 
			// btnNavBookings
			// 
			btnNavBookings.BackColor = Color.FromArgb(34, 40, 60);
			btnNavBookings.Cursor = Cursors.Hand;
			btnNavBookings.FlatAppearance.BorderSize = 0;
			btnNavBookings.FlatStyle = FlatStyle.Flat;
			btnNavBookings.Font = new Font("Segoe UI", 10.5F);
			btnNavBookings.ForeColor = Color.Gainsboro;
			btnNavBookings.Location = new Point(0, 174);
			btnNavBookings.Name = "btnNavBookings";
			btnNavBookings.Padding = new Padding(20, 0, 0, 0);
			btnNavBookings.Size = new Size(230, 48);
			btnNavBookings.TabIndex = 3;
			btnNavBookings.Text = "📅  Đặt phòng";
			btnNavBookings.TextAlign = ContentAlignment.MiddleLeft;
			btnNavBookings.UseVisualStyleBackColor = false;
			btnNavBookings.Click += btnNavBookings_Click;
			// 
			// btnNavCheckIn
			// 
			btnNavCheckIn.BackColor = Color.FromArgb(34, 40, 60);
			btnNavCheckIn.Cursor = Cursors.Hand;
			btnNavCheckIn.FlatAppearance.BorderSize = 0;
			btnNavCheckIn.FlatStyle = FlatStyle.Flat;
			btnNavCheckIn.Font = new Font("Segoe UI", 10.5F);
			btnNavCheckIn.ForeColor = Color.Gainsboro;
			btnNavCheckIn.Location = new Point(0, 227);
			btnNavCheckIn.Name = "btnNavCheckIn";
			btnNavCheckIn.Padding = new Padding(20, 0, 0, 0);
			btnNavCheckIn.Size = new Size(230, 48);
			btnNavCheckIn.TabIndex = 4;
			btnNavCheckIn.Text = "🔑  Nhận / Trả phòng";
			btnNavCheckIn.TextAlign = ContentAlignment.MiddleLeft;
			btnNavCheckIn.UseVisualStyleBackColor = false;
			btnNavCheckIn.Click += btnNavCheckIn_Click;
			// 
			// btnNavReports
			// 
			btnNavReports.BackColor = Color.FromArgb(34, 40, 60);
			btnNavReports.Cursor = Cursors.Hand;
			btnNavReports.FlatAppearance.BorderSize = 0;
			btnNavReports.FlatStyle = FlatStyle.Flat;
			btnNavReports.Font = new Font("Segoe UI", 10.5F);
			btnNavReports.ForeColor = Color.Gainsboro;
			btnNavReports.Location = new Point(0, 280);
			btnNavReports.Name = "btnNavReports";
			btnNavReports.Padding = new Padding(20, 0, 0, 0);
			btnNavReports.Size = new Size(230, 48);
			btnNavReports.TabIndex = 5;
			btnNavReports.Text = "📈  Báo cáo doanh thu";
			btnNavReports.TextAlign = ContentAlignment.MiddleLeft;
			btnNavReports.UseVisualStyleBackColor = false;
			btnNavReports.Click += btnNavReports_Click;
			// 
			// lblSidebarFooter
			// 
			lblSidebarFooter.Dock = DockStyle.Bottom;
			lblSidebarFooter.Font = new Font("Segoe UI", 8.5F);
			lblSidebarFooter.ForeColor = Color.Gray;
			lblSidebarFooter.Location = new Point(0, 585);
			lblSidebarFooter.Name = "lblSidebarFooter";
			lblSidebarFooter.Size = new Size(230, 50);
			lblSidebarFooter.TabIndex = 6;
			lblSidebarFooter.Text = "IT008 - Đồ án Cuối kỳ\nv1.0.0";
			lblSidebarFooter.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// panelContent
			// 
			panelContent.BackColor = Color.FromArgb(244, 246, 249);
			panelContent.Controls.Add(panelBody);
			panelContent.Controls.Add(panelContentHeader);
			panelContent.Dock = DockStyle.Fill;
			panelContent.Location = new Point(230, 65);
			panelContent.Name = "panelContent";
			panelContent.Size = new Size(870, 635);
			panelContent.TabIndex = 2;
			// 
			// panelBody
			// 
			panelBody.AutoScroll = true;
			panelBody.Controls.Add(panelWelcome);
			panelBody.Controls.Add(panelCardBookings);
			panelBody.Controls.Add(panelCardAvailable);
			panelBody.Controls.Add(panelCardRooms);
			panelBody.Dock = DockStyle.Fill;
			panelBody.Location = new Point(0, 55);
			panelBody.Name = "panelBody";
			panelBody.Padding = new Padding(25);
			panelBody.Size = new Size(870, 580);
			panelBody.TabIndex = 1;
			// 
			// panelWelcome
			// 
			panelWelcome.BackColor = Color.White;
			panelWelcome.BorderStyle = BorderStyle.FixedSingle;
			panelWelcome.Controls.Add(lblWelcomeDesc);
			panelWelcome.Controls.Add(lblWelcomeTitle);
			panelWelcome.Location = new Point(25, 160);
			panelWelcome.Name = "panelWelcome";
			panelWelcome.Padding = new Padding(20);
			panelWelcome.Size = new Size(760, 267);
			panelWelcome.TabIndex = 3;
			// 
			// lblWelcomeDesc
			// 
			lblWelcomeDesc.Font = new Font("Segoe UI", 10F);
			lblWelcomeDesc.ForeColor = Color.FromArgb(70, 80, 95);
			lblWelcomeDesc.Location = new Point(20, 60);
			lblWelcomeDesc.Name = "lblWelcomeDesc";
			lblWelcomeDesc.Size = new Size(710, 185);
			lblWelcomeDesc.TabIndex = 1;
			lblWelcomeDesc.Text = resources.GetString("lblWelcomeDesc.Text");
			// 
			// lblWelcomeTitle
			// 
			lblWelcomeTitle.AutoSize = true;
			lblWelcomeTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblWelcomeTitle.ForeColor = Color.FromArgb(24, 30, 54);
			lblWelcomeTitle.Location = new Point(20, 20);
			lblWelcomeTitle.Name = "lblWelcomeTitle";
			lblWelcomeTitle.Size = new Size(514, 28);
			lblWelcomeTitle.TabIndex = 0;
			lblWelcomeTitle.Text = "Chào mừng bạn đến với Hệ thống Quản lý Khách sạn";
			// 
			// panelCardBookings
			// 
			panelCardBookings.BackColor = Color.FromArgb(255, 193, 7);
			panelCardBookings.Controls.Add(lblCardBookingsValue);
			panelCardBookings.Controls.Add(lblCardBookingsTitle);
			panelCardBookings.Location = new Point(545, 25);
			panelCardBookings.Name = "panelCardBookings";
			panelCardBookings.Size = new Size(240, 110);
			panelCardBookings.TabIndex = 2;
			// 
			// lblCardBookingsValue
			// 
			lblCardBookingsValue.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
			lblCardBookingsValue.ForeColor = Color.FromArgb(33, 37, 41);
			lblCardBookingsValue.Location = new Point(15, 38);
			lblCardBookingsValue.Name = "lblCardBookingsValue";
			lblCardBookingsValue.Size = new Size(210, 60);
			lblCardBookingsValue.TabIndex = 1;
			lblCardBookingsValue.Text = "7";
			// 
			// lblCardBookingsTitle
			// 
			lblCardBookingsTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			lblCardBookingsTitle.ForeColor = Color.FromArgb(33, 37, 41);
			lblCardBookingsTitle.Location = new Point(15, 15);
			lblCardBookingsTitle.Name = "lblCardBookingsTitle";
			lblCardBookingsTitle.Size = new Size(210, 23);
			lblCardBookingsTitle.TabIndex = 0;
			lblCardBookingsTitle.Text = "ĐƠN ĐẶT PHÒNG";
			// 
			// panelCardAvailable
			// 
			panelCardAvailable.BackColor = Color.FromArgb(25, 135, 84);
			panelCardAvailable.Controls.Add(lblCardAvailableValue);
			panelCardAvailable.Controls.Add(lblCardAvailableTitle);
			panelCardAvailable.Location = new Point(285, 25);
			panelCardAvailable.Name = "panelCardAvailable";
			panelCardAvailable.Size = new Size(240, 110);
			panelCardAvailable.TabIndex = 1;
			// 
			// lblCardAvailableValue
			// 
			lblCardAvailableValue.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
			lblCardAvailableValue.ForeColor = Color.White;
			lblCardAvailableValue.Location = new Point(15, 38);
			lblCardAvailableValue.Name = "lblCardAvailableValue";
			lblCardAvailableValue.Size = new Size(210, 60);
			lblCardAvailableValue.TabIndex = 1;
			lblCardAvailableValue.Text = "20";
			// 
			// lblCardAvailableTitle
			// 
			lblCardAvailableTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			lblCardAvailableTitle.ForeColor = Color.White;
			lblCardAvailableTitle.Location = new Point(15, 15);
			lblCardAvailableTitle.Name = "lblCardAvailableTitle";
			lblCardAvailableTitle.Size = new Size(210, 23);
			lblCardAvailableTitle.TabIndex = 0;
			lblCardAvailableTitle.Text = "PHÒNG TRỐNG";
			// 
			// panelCardRooms
			// 
			panelCardRooms.BackColor = Color.FromArgb(13, 110, 253);
			panelCardRooms.Controls.Add(lblCardRoomsValue);
			panelCardRooms.Controls.Add(lblCardRoomsTitle);
			panelCardRooms.Location = new Point(25, 25);
			panelCardRooms.Name = "panelCardRooms";
			panelCardRooms.Size = new Size(240, 110);
			panelCardRooms.TabIndex = 0;
			// 
			// lblCardRoomsValue
			// 
			lblCardRoomsValue.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
			lblCardRoomsValue.ForeColor = Color.White;
			lblCardRoomsValue.Location = new Point(15, 38);
			lblCardRoomsValue.Name = "lblCardRoomsValue";
			lblCardRoomsValue.Size = new Size(210, 60);
			lblCardRoomsValue.TabIndex = 1;
			lblCardRoomsValue.Text = "20";
			// 
			// lblCardRoomsTitle
			// 
			lblCardRoomsTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			lblCardRoomsTitle.ForeColor = Color.White;
			lblCardRoomsTitle.Location = new Point(15, 15);
			lblCardRoomsTitle.Name = "lblCardRoomsTitle";
			lblCardRoomsTitle.Size = new Size(210, 23);
			lblCardRoomsTitle.TabIndex = 0;
			lblCardRoomsTitle.Text = "TỔNG SỐ PHÒNG";
			// 
			// panelContentHeader
			// 
			panelContentHeader.BackColor = Color.White;
			panelContentHeader.Controls.Add(lblPageTitle);
			panelContentHeader.Dock = DockStyle.Top;
			panelContentHeader.Location = new Point(0, 0);
			panelContentHeader.Name = "panelContentHeader";
			panelContentHeader.Size = new Size(870, 55);
			panelContentHeader.TabIndex = 0;
			// 
			// lblPageTitle
			// 
			lblPageTitle.AutoSize = true;
			lblPageTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
			lblPageTitle.ForeColor = Color.FromArgb(33, 37, 41);
			lblPageTitle.Location = new Point(25, 14);
			lblPageTitle.Name = "lblPageTitle";
			lblPageTitle.Size = new Size(257, 30);
			lblPageTitle.TabIndex = 0;
			lblPageTitle.Text = "TỔNG QUAN HỆ THỐNG";
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1100, 700);
			Controls.Add(panelContent);
			Controls.Add(panelSidebar);
			Controls.Add(panelTop);
			MinimumSize = new Size(950, 600);
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Hệ thống Quản lý Khách sạn - Hotel Booking Management";
			Load += MainForm_Load;
			panelTop.ResumeLayout(false);
			panelTop.PerformLayout();
			panelSidebar.ResumeLayout(false);
			panelContent.ResumeLayout(false);
			panelBody.ResumeLayout(false);
			panelWelcome.ResumeLayout(false);
			panelWelcome.PerformLayout();
			panelCardBookings.ResumeLayout(false);
			panelCardAvailable.ResumeLayout(false);
			panelCardRooms.ResumeLayout(false);
			panelContentHeader.ResumeLayout(false);
			panelContentHeader.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelTop;
		private Label lblAppTitle;
		private Label lblUserGreeting;
		private Button btnLogout;
		private Panel panelSidebar;
		private Button btnNavDashboard;
		private Button btnNavRooms;
		private Button btnNavCustomers;
		private Button btnNavBookings;
		private Button btnNavCheckIn;
		private Button btnNavReports;
		private Label lblSidebarFooter;
		private Panel panelContent;
		private Panel panelContentHeader;
		private Label lblPageTitle;
		private Panel panelBody;
		private Panel panelCardRooms;
		private Label lblCardRoomsTitle;
		private Label lblCardRoomsValue;
		private Panel panelCardAvailable;
		private Label lblCardAvailableTitle;
		private Label lblCardAvailableValue;
		private Panel panelCardBookings;
		private Label lblCardBookingsTitle;
		private Label lblCardBookingsValue;
		private Panel panelWelcome;
		private Label lblWelcomeTitle;
		private Label lblWelcomeDesc;
	}
}