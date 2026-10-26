using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HotelBookingManagement.Models;
using HotelBookingManagement.Business.Services;

namespace HotelBookingManagement.Forms
{
    public partial class FrmBooking : Form
    {
        private readonly CustomerService _customerService;
        private readonly RoomService _roomService;
        private readonly BookingService _bookingService;
        private int _currentCustomerId = -1;
        private Customer? _currentCustomer;

        public FrmBooking()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _roomService = new RoomService();
            _bookingService = new BookingService();
        }

        private void FrmBooking_Load(object sender, EventArgs e)
        {
            // [TASK BOOK03] Thiết lập mặc định ngày khi mở Form
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(1);

            dtpCheckIn.ValueChanged += ValidateBookingDates;
            dtpCheckOut.ValueChanged += ValidateBookingDates;

            // Tải luôn danh sách phòng trống cho ngày mặc định
            LoadAvailableRooms();
        }

        // ==========================================
        // TASK BOOK02: CHỌN KHÁCH HÀNG (DB THẬT)
        // ==========================================
        private void btnChonKhachCu_Click(object sender, EventArgs e)
        {
            string cccd = txtCCCD.Text.Trim();
            string phone = txtSDT.Text.Trim();

            if (string.IsNullOrEmpty(cccd) && string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Vui lòng nhập CCCD hoặc Số điện thoại để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nếu nhập CCCD thì tìm theo CCCD, nếu không có hoặc trống thì tìm tiếp theo SĐT
            _currentCustomer = null;
            if (!string.IsNullOrEmpty(cccd))
            {
                _currentCustomer = _customerService.GetCustomerByIdentityNumber(cccd);
            }
            if (_currentCustomer == null && !string.IsNullOrEmpty(phone))
            {
                _currentCustomer = _customerService.GetCustomerByPhone(phone);
            }

            if (_currentCustomer != null)
            {
                // GÁN ID KHÁCH HÀNG ĐỂ NÚT TẠO BOOKING HOẠT ĐỘNG
                _currentCustomerId = _currentCustomer.CustomerId;

                txtHoTen.Text = _currentCustomer.FullName;
                txtCCCD.Text = _currentCustomer.IdentityNumber;
                txtSDT.Text = _currentCustomer.Phone;

                MessageBox.Show($"Đã tìm thấy thông tin khách: {_currentCustomer.FullName} (Mã KH: #{_currentCustomerId})", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _currentCustomerId = -1;
                txtHoTen.Clear();
                MessageBox.Show("Không tìm thấy khách hàng. Bạn có thể nhập Họ tên, SĐT và CCCD trên form để tự động tạo khách mới khi đặt phòng!", "Khách hàng mới", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
            }
        }

        // ==========================================
        // TASK BOOK03: KIỂM TRA NGÀY HỢP LỆ
        // ==========================================
        private void ValidateBookingDates(object? sender, EventArgs e)
        {
            DateTime checkIn = dtpCheckIn.Value.Date;
            DateTime checkOut = dtpCheckOut.Value.Date;

            if (checkIn < DateTime.Today)
            {
                MessageBox.Show("Ngày Check-in không được chọn trong quá khứ!", "Lỗi thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);

                dtpCheckIn.ValueChanged -= ValidateBookingDates;
                dtpCheckIn.Value = DateTime.Today;
                dtpCheckIn.ValueChanged += ValidateBookingDates;
                return;
            }

            if (checkOut <= checkIn)
            {
                MessageBox.Show("Ngày Check-out phải sau ngày Check-in ít nhất 1 ngày!", "Lỗi thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);

                dtpCheckOut.ValueChanged -= ValidateBookingDates;
                dtpCheckOut.Value = checkIn.AddDays(1);
                dtpCheckOut.ValueChanged += ValidateBookingDates;
            }
        }

        private void LoadAvailableRooms()
        {
            try
            {
                DateTime checkIn = dtpCheckIn.Value.Date;
                DateTime checkOut = dtpCheckOut.Value.Date;

                DataTable dtRooms = _roomService.GetAvailableRooms(checkIn, checkOut);
                dgvDanhSachPhong.DataSource = dtRooms;
                dgvDanhSachPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách phòng trống: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKiemTraPhong_Click(object sender, EventArgs e)
        {
            DateTime checkIn = dtpCheckIn.Value.Date;
            DateTime checkOut = dtpCheckOut.Value.Date;

            DataTable dtRooms = _roomService.GetAvailableRooms(checkIn, checkOut);
            dgvDanhSachPhong.DataSource = dtRooms;
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
                // 1. Kiểm tra chọn phòng trên DataGridView
                if (dgvDanhSachPhong.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một phòng từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Xác nhận hoặc tự động tạo khách hàng mới nếu người dùng đã điền đủ thông tin
                if (_currentCustomerId <= 0)
                {
                    string fullName = txtHoTen.Text.Trim();
                    string phone = txtSDT.Text.Trim();
                    string cccd = txtCCCD.Text.Trim();

                    if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(cccd))
                    {
                        var existing = _customerService.GetCustomerByIdentityNumber(cccd) ?? _customerService.GetCustomerByPhone(phone);
                        if (existing != null)
                        {
                            _currentCustomer = existing;
                            _currentCustomerId = existing.CustomerId;
                        }
                        else
                        {
                            var newCust = new Customer
                            {
                                FullName = fullName,
                                Phone = phone,
                                IdentityNumber = cccd
                            };
                            if (_customerService.CreateCustomer(newCust, out string custErr))
                            {
                                _currentCustomer = newCust;
                                _currentCustomerId = newCust.CustomerId;
                            }
                            else
                            {
                                MessageBox.Show(custErr, "Lỗi thông tin khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng bấm tìm khách hàng cũ, hoặc nhập đầy đủ Họ tên, SĐT và CCCD để tạo khách mới trước khi đặt phòng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 3. Gom danh sách các phòng được chọn và tính tổng giá phòng 1 đêm
                List<int> selectedRoomIds = new List<int>();
                decimal pricePerNight = 0;

                foreach (DataGridViewRow row in dgvDanhSachPhong.SelectedRows)
                {
                    if (row.Cells["RoomId"].Value == null) continue;
                    int roomId = Convert.ToInt32(row.Cells["RoomId"].Value);
                    selectedRoomIds.Add(roomId);

                    decimal price = Convert.ToDecimal(row.Cells["PricePerNight"].Value);
                    pricePerNight += price;
                }

                // 4. Tính số đêm và Tổng tiền
                DateTime checkIn = dtpCheckIn.Value.Date;
                DateTime checkOut = dtpCheckOut.Value.Date;
                int totalNights = (int)(checkOut - checkIn).TotalDays;
                if (totalNights <= 0) totalNights = 1;

                decimal totalAmount = pricePerNight * totalNights;

                // 5. Đóng gói dữ liệu vào Object Booking
                Booking newBooking = new Booking
                {
                    CustomerId = _currentCustomerId,
                    BookingDate = DateTime.Now,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut,
                    Status = "PENDING",
                    TotalAmount = totalAmount,
                    CreatedBy = 1,
                    CreatedAt = DateTime.Now
                };

                // 6. Lưu xuống DB qua tầng Business
                Booking? result = _bookingService.CreateBooking(newBooking, selectedRoomIds);

                if (result != null && result.BookingId > 0)
                {
                    MessageBox.Show($"Tạo Booking thành công!\nMã đơn: #{result.BookingId}\nTổng tiền: {result.TotalAmount:N0} VNĐ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _currentCustomerId = -1;
                    _currentCustomer = null;
                    txtHoTen.Clear();
                    txtCCCD.Clear();
                    txtSDT.Clear();

                    LoadAvailableRooms();
                }
                else
                {
                    MessageBox.Show("Lưu dữ liệu thất bại, vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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