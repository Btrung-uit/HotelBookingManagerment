using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Business.Services;

namespace HotelBookingManagement
{
    public class FrmBookingList : Form
    {
        private Panel _panelTop = null!;
        private Button _btnRefresh = null!;
        private Button _btnCancel = null!;
        private DataGridView _dgvBookings = null!;
        private readonly IBookingService _bookingService;

        public FrmBookingList()
        {
            _bookingService = new BookingService();
            InitializeUI();
            LoadBookings();
        }

        private void InitializeUI()
        {
            this.Text = "Quản lý danh sách đặt phòng";
            this.Size = new Size(980, 540);
            this.StartPosition = FormStartPosition.CenterScreen;

            _panelTop = new Panel { Dock = DockStyle.Top, Height = 55, Padding = new Padding(10) };

            _btnRefresh = new Button
            {
                Text = "Làm mới",
                Width = 110,
                Dock = DockStyle.Left,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular)
            };
            _btnRefresh.Click += (s, e) => LoadBookings();

            _btnCancel = new Button
            {
                Text = "Hủy Booking",
                Width = 130,
                Dock = DockStyle.Right,
                BackColor = Color.LightCoral,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            _btnCancel.Click += BtnCancel_Click;

            _panelTop.Controls.Add(_btnRefresh);
            _panelTop.Controls.Add(_btnCancel);

            _dgvBookings = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            this.Controls.Add(_dgvBookings);
            this.Controls.Add(_panelTop);
        }

        public void LoadBookings()
        {
            try
            {
                var bookings = _bookingService.GetAll().ToList();

                var displayList = bookings.Select(b => new
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
                    Status = b.Status,
                    BookingDate = b.BookingDate.ToString("dd/MM/yyyy HH:mm")
                }).ToList();

                _dgvBookings.DataSource = displayList;

                if (_dgvBookings.Columns["BookingId"] != null) _dgvBookings.Columns["BookingId"]!.HeaderText = "Mã Đơn";
                if (_dgvBookings.Columns["CustomerName"] != null) _dgvBookings.Columns["CustomerName"]!.HeaderText = "Khách Hàng";
                if (_dgvBookings.Columns["Phone"] != null) _dgvBookings.Columns["Phone"]!.HeaderText = "Số ĐT";
                if (_dgvBookings.Columns["Rooms"] != null) _dgvBookings.Columns["Rooms"]!.HeaderText = "Phòng";
                if (_dgvBookings.Columns["CheckInDate"] != null) _dgvBookings.Columns["CheckInDate"]!.HeaderText = "Ngày Nhận";
                if (_dgvBookings.Columns["CheckOutDate"] != null) _dgvBookings.Columns["CheckOutDate"]!.HeaderText = "Ngày Trả";
                if (_dgvBookings.Columns["TotalAmount"] != null)
                {
                    _dgvBookings.Columns["TotalAmount"]!.HeaderText = "Tổng Tiền (VNĐ)";
                    _dgvBookings.Columns["TotalAmount"]!.DefaultCellStyle.Format = "N0";
                }
                if (_dgvBookings.Columns["Status"] != null) _dgvBookings.Columns["Status"]!.HeaderText = "Trạng Thái";
                if (_dgvBookings.Columns["BookingDate"] != null) _dgvBookings.Columns["BookingDate"]!.HeaderText = "Ngày Tạo";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách đặt phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ClientSize = new Size(980, 540);
            Name = "FrmBookingList";
            Load += FrmBookingList_Load;
            ResumeLayout(false);
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            if (_dgvBookings.CurrentRow == null || _dgvBookings.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn đặt phòng trên lưới để hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookingId = Convert.ToInt32(_dgvBookings.CurrentRow.Cells["BookingId"].Value);

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn hủy đơn đặt phòng mã #{bookingId} không?",
                                          "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool isSuccess = _bookingService.CancelBooking(bookingId, out string errorMessage);

                if (isSuccess)
                {
                    MessageBox.Show("Hủy đơn đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookings();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Không thể hủy đặt phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void FrmBookingList_Load(object? sender, EventArgs e)
        {
            LoadBookings();
        }
    }
}