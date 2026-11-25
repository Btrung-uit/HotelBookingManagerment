using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Business.Services;
using HotelBookingManagement.Forms;

namespace HotelBookingManagement.Controls
{
    public partial class BookingsControl : UserControl
    {
        private readonly IBookingService _bookingService;
        private DataGridView _dgvBookings = null!;
        private Button _btnOpenCreateBooking = null!;
        private Button _btnOpenBookingList = null!;
        private Button _btnCancelSelected = null!;
        private Button _btnRefresh = null!;

        public BookingsControl()
        {
            InitializeComponent();
            _bookingService = new BookingService();
            BuildBookingDashboardUI();
        }

        private void BuildBookingDashboardUI()
        {
            Control host = this.Controls.ContainsKey("panelContainer") ? this.Controls["panelContainer"]! : this;
            host.Controls.Clear();

            var topBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(12, 10, 12, 10),
                BackColor = Color.FromArgb(248, 250, 252)
            };

            _btnOpenCreateBooking = new Button
            {
                Text = "+ Tạo Đặt Phòng Mới",
                Size = new Size(170, 38),
                Location = new Point(12, 11),
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _btnOpenCreateBooking.FlatAppearance.BorderSize = 0;
            _btnOpenCreateBooking.Click += BtnOpenCreateBooking_Click;

            _btnOpenBookingList = new Button
            {
                Text = "Mở Cửa Sổ Quản Lý",
                Size = new Size(160, 38),
                Location = new Point(192, 11),
                BackColor = Color.FromArgb(15, 118, 110),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _btnOpenBookingList.FlatAppearance.BorderSize = 0;
            _btnOpenBookingList.Click += BtnOpenBookingList_Click;

            _btnCancelSelected = new Button
            {
                Text = "Hủy Đơn Đang Chọn",
                Size = new Size(165, 38),
                Location = new Point(362, 11),
                BackColor = Color.FromArgb(220, 38, 38),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _btnCancelSelected.FlatAppearance.BorderSize = 0;
            _btnCancelSelected.Click += BtnCancelSelected_Click;

            _btnRefresh = new Button
            {
                Text = "Làm mới",
                Size = new Size(105, 38),
                Location = new Point(537, 11),
                BackColor = Color.FromArgb(226, 232, 240),
                ForeColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            _btnRefresh.FlatAppearance.BorderSize = 0;
            _btnRefresh.Click += (s, e) => LoadBookingsToGrid();

            topBar.Controls.Add(_btnOpenCreateBooking);
            topBar.Controls.Add(_btnOpenBookingList);
            topBar.Controls.Add(_btnCancelSelected);
            topBar.Controls.Add(_btnRefresh);

            _dgvBookings = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false
            };

            host.Controls.Add(_dgvBookings);
            host.Controls.Add(topBar);

            this.Load += (s, e) => LoadBookingsToGrid();
        }

        private void BtnOpenCreateBooking_Click(object? sender, EventArgs e)
        {
            using var frm = new FrmBooking();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog(this);
            LoadBookingsToGrid();
        }

        private void BtnOpenBookingList_Click(object? sender, EventArgs e)
        {
            using var frm = new FrmBookingList();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog(this);
            LoadBookingsToGrid();
        }

        private void BtnCancelSelected_Click(object? sender, EventArgs e)
        {
            if (_dgvBookings.CurrentRow == null || _dgvBookings.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn đặt phòng trên danh sách để hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookingId = Convert.ToInt32(_dgvBookings.CurrentRow.Cells["BookingId"].Value);
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn hủy đơn đặt phòng mã #{bookingId} không?",
                                          "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                if (_bookingService.CancelBooking(bookingId, out string errorMsg))
                {
                    MessageBox.Show("Đã hủy đơn đặt phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookingsToGrid();
                }
                else
                {
                    MessageBox.Show(errorMsg, "Không thể hủy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LoadBookingsToGrid()
        {
            try
            {
                var list = _bookingService.GetAll().Select(b => new
                {
                    BookingId = b.BookingId,
                    CustomerName = b.Customer?.FullName ?? $"KH #{b.CustomerId}",
                    Phone = b.Customer?.Phone ?? string.Empty,
                    Rooms = b.BookingDetails != null && b.BookingDetails.Count > 0
                        ? string.Join(", ", b.BookingDetails.Select(d => d.Room?.RoomNumber ?? d.RoomId.ToString()))
                        : "Chưa gán",
                    CheckInDate = b.CheckInDate.ToString("dd/MM/yyyy"),
                    CheckOutDate = b.CheckOutDate.ToString("dd/MM/yyyy"),
                    TotalAmount = b.TotalAmount,
                    Status = b.Status
                }).ToList();

                _dgvBookings.DataSource = list;

                if (_dgvBookings.Columns["BookingId"] != null) _dgvBookings.Columns["BookingId"]!.HeaderText = "Mã Đơn";
                if (_dgvBookings.Columns["CustomerName"] != null) _dgvBookings.Columns["CustomerName"]!.HeaderText = "Khách Hàng";
                if (_dgvBookings.Columns["Phone"] != null) _dgvBookings.Columns["Phone"]!.HeaderText = "Số ĐT";
                if (_dgvBookings.Columns["Rooms"] != null) _dgvBookings.Columns["Rooms"]!.HeaderText = "Phòng Đặt";
                if (_dgvBookings.Columns["CheckInDate"] != null) _dgvBookings.Columns["CheckInDate"]!.HeaderText = "Ngày Nhận";
                if (_dgvBookings.Columns["CheckOutDate"] != null) _dgvBookings.Columns["CheckOutDate"]!.HeaderText = "Ngày Trả";
                if (_dgvBookings.Columns["TotalAmount"] != null)
                {
                    _dgvBookings.Columns["TotalAmount"]!.HeaderText = "Tổng Tiền (VNĐ)";
                    _dgvBookings.Columns["TotalAmount"]!.DefaultCellStyle.Format = "N0";
                }
                if (_dgvBookings.Columns["Status"] != null) _dgvBookings.Columns["Status"]!.HeaderText = "Trạng Thái";
            }
            catch
            {
                // Bỏ qua lỗi hiển thị ban đầu nếu chưa kết nối SQL Server
            }
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}