using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
// Thêm 2 thư viện này để gọi Model và Business Layer
using HotelBookingManagement.Models;
using HotelBookingManagement.Business.Services;

namespace HotelBookingManagement.Forms
{
    public partial class FrmBooking : Form
    {
        // Khởi tạo Service kết nối tầng Business
        private readonly CustomerService _customerService;
        private readonly RoomService _roomService;
        private readonly BookingService _bookingService;
        private int _currentCustomerId = -1;
        // Biến lưu thông tin khách hàng hiện tại
        private Customers _currentCustomer;

        public FrmBooking()
        {
            InitializeComponent();
            _customerService = new CustomerService(); // Nối với Database khi mở form
            _roomService = new RoomService(); // Nối với Database 
            _bookingService = new BookingService();
        }

        private void FrmBooking_Load(object sender, EventArgs e)
        {
            // [TASK BOOK03] Thiết lập mặc định ngày khi mở Form
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(1);

            // Bắt sự kiện: Tự động chạy hàm Validate mỗi khi người dùng đổi ngày
            dtpCheckIn.ValueChanged += ValidateBookingDates;
            dtpCheckOut.ValueChanged += ValidateBookingDates;
        }

        // ==========================================
        // TASK BOOK02: CHỌN KHÁCH HÀNG (DB THẬT)
        // ==========================================
        private void btnChonKhachCu_Click(object sender, EventArgs e)
        {
            // Ưu tiên lấy CCCD, nếu CCCD trống thì lấy SDT để tìm
            string keyword = !string.IsNullOrWhiteSpace(txtCCCD.Text)
                             ? txtCCCD.Text.Trim()
                             : txtSDT.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập CCCD hoặc Số điện thoại để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi DB thật qua tầng Business
            _currentCustomer = _customerService.FindCustomer(keyword);

            if (_currentCustomer != null)
            {
                // Điền dữ liệu ra màn hình
                txtHoTen.Text = _currentCustomer.FullName;
                txtCCCD.Text = _currentCustomer.IdentityNumber;
                txtSDT.Text = _currentCustomer.Phone;

                MessageBox.Show($"Đã tìm thấy thông tin khách: {_currentCustomer.FullName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtHoTen.Clear();
                MessageBox.Show("Không tìm thấy khách hàng. Vui lòng nhập thông tin để tạo khách mới!", "Khách hàng mới", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
            }
        }

        // ==========================================
        // TASK BOOK03: KIỂM TRA NGÀY HỢP LỆ
        // ==========================================
        private void ValidateBookingDates(object sender, EventArgs e)
        {
            DateTime checkIn = dtpCheckIn.Value.Date;
            DateTime checkOut = dtpCheckOut.Value.Date;

            // ĐK 1: Check-in không được trong quá khứ
            if (checkIn < DateTime.Today)
            {
                MessageBox.Show("Ngày Check-in không được chọn trong quá khứ!", "Lỗi thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);

                dtpCheckIn.ValueChanged -= ValidateBookingDates;
                dtpCheckIn.Value = DateTime.Today;
                dtpCheckIn.ValueChanged += ValidateBookingDates;
                return;
            }

            // ĐK 2: Check-out bắt buộc phải sau Check-in
            if (checkOut <= checkIn)
            {
                MessageBox.Show("Ngày Check-out phải sau ngày Check-in ít nhất 1 ngày!", "Lỗi thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);

                dtpCheckOut.ValueChanged -= ValidateBookingDates;
                dtpCheckOut.Value = checkIn.AddDays(1);
                dtpCheckOut.ValueChanged += ValidateBookingDates;
            }
        }

        private void btnKiemTraPhong_Click(object sender, EventArgs e)
        {
            DateTime checkIn = dtpCheckIn.Value.Date;
            DateTime checkOut = dtpCheckOut.Value.Date;

            // Gọi tầng Business kéo dữ liệu phòng trống từ SQL lên
            DataTable dtRooms = _roomService.GetAvailableRooms(checkIn, checkOut);

            // Đổ dữ liệu vào bảng DataGridView
            dgvDanhSachPhong.DataSource = dtRooms;

            // Chỉnh lại kích thước các cột cho đẹp
            dgvDanhSachPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dtRooms.Rows.Count == 0)
            {
                MessageBox.Show("Rất tiếc, không còn phòng trống nào trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTaoBooking_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem người dùng đã chọn phòng nào trên lưới (DataGridView) chưa
                if (dgvDanhSachPhong.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một phòng từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Gom danh sách các phòng được chọn và tính tổng giá phòng 1 đêm
                List<int> selectedRoomIds = new List<int>();
                decimal pricePerNight = 0;

                foreach (DataGridViewRow row in dgvDanhSachPhong.SelectedRows)
                {
                    // Lấy ID phòng và Giá từ dòng đang chọn trên DataGridView
                    int roomId = Convert.ToInt32(row.Cells["RoomId"].Value);
                    selectedRoomIds.Add(roomId);

                    decimal price = Convert.ToDecimal(row.Cells["PricePerNight"].Value);
                    pricePerNight += price;
                }

                // 3. Tính số đêm và Tổng tiền
                DateTime checkIn = dtpCheckIn.Value.Date;
                DateTime checkOut = dtpCheckOut.Value.Date;
                int totalNights = (int)(checkOut - checkIn).TotalDays;

                if (totalNights <= 0) totalNights = 1; // Mặc định ít nhất là 1 đêm
                decimal totalAmount = pricePerNight * totalNights;

                // 4. LẤY ID KHÁCH HÀNG
                if (_currentCustomerId == -1)
                {
                    MessageBox.Show("Vui lòng tìm và xác nhận thông tin khách hàng trước khi tạo Booking!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int customerId = _currentCustomerId;

                // 5. Đóng gói dữ liệu vào Object Booking
                Booking newBooking = new Booking
                {
                    CustomerId = customerId,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut,
                    TotalAmount = totalAmount
                };

                // 6. Chuyển xuống tầng Business để lưu qua Transaction an toàn
                Booking result = _bookingService.CreateBooking(newBooking, selectedRoomIds);

                if (result != null)
                {
                    MessageBox.Show($"Tạo Booking thành công!\nMã đơn: {result.BookingId}\nTổng tiền: {totalAmount:N0} VNĐ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 1. Chống lỗi đặt nhầm: Reset lại biến nhớ ID khách hàng để chuẩn bị cho khách tiếp theo
                    _currentCustomerId = -1;

                    // 2. Cập nhật lại danh sách: Tự động gọi lại nút "Kiểm Tra Phòng" để hiển thị các phòng còn trống
                    btnKiemTraPhong.PerformClick();
                }
                else
                {
                    MessageBox.Show("Lưu dữ liệu thất bại, vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi Nghiêm Trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDanhSachPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}