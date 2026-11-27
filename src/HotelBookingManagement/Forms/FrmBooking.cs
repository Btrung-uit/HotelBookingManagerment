using HotelBookingManagement.Business;
using HotelBookingManagement.Business.Services;
using HotelBookingManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HotelBookingManagement.Forms
{
    public partial class FrmBooking : Form
    {
        private readonly CustomerService _customerService;
        private readonly RoomService _roomService;
        private readonly BookingService _bookingService;
        private int _currentCustomerId = -1;
        private Customer? _currentCustomer;
        private bool _isRoomListFresh = false;

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
           
            // Xóa khách đã chọn nếu người dùng sửa lại SĐT hoặc CCCD
            txtSDT.TextChanged += CustomerInput_TextChanged;
            txtCCCD.TextChanged += CustomerInput_TextChanged;

            // Bấm vào bất kỳ ô nào trên lưới cũng chọn cả dòng phòng
            dgvDanhSachPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachPhong.MultiSelect = true;

            // [TASK BOOK08] Tự động tính ước tính giá khi chọn hoặc bỏ chọn phòng
            dgvDanhSachPhong.SelectionChanged += (s, args) => UpdateEstimatedCost();

            // Tải luôn danh sách phòng trống cho ngày mặc định
            LoadAvailableRooms();
            UpdateEstimatedCost();
        }
        private void CustomerInput_TextChanged(object? sender, EventArgs e)
        {
            if (_currentCustomerId > 0 && _currentCustomer != null)
            {
                bool phoneChanged = !string.Equals(txtSDT.Text.Trim(), _currentCustomer.Phone?.Trim(), StringComparison.OrdinalIgnoreCase);
                bool cccdChanged = !string.Equals(txtCCCD.Text.Trim(), _currentCustomer.IdentityNumber?.Trim(), StringComparison.OrdinalIgnoreCase);

                if (phoneChanged || cccdChanged)
                {
                    _currentCustomerId = -1;
                    _currentCustomer = null;
                }
            }
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
            Customer? foundCustomer = null;
            if (!string.IsNullOrEmpty(cccd))
            {
                foundCustomer = _customerService.GetCustomerByIdentityNumber(cccd);
            }
            if (foundCustomer == null && !string.IsNullOrEmpty(phone))
            {
                foundCustomer = _customerService.GetCustomerByPhone(phone);
            }

            if (foundCustomer != null)
            {
                // 1. Điền các ô TextBox từ biến cục bộ trước (tránh TextChanged xóa _currentCustomer giữa chừng)
                txtHoTen.Text = foundCustomer.FullName;
                txtCCCD.Text = foundCustomer.IdentityNumber;
                txtSDT.Text = foundCustomer.Phone;

                // 2. Sau khi TextBox ổn định mới gán biến trạng thái thành viên
                _currentCustomer = foundCustomer;
                _currentCustomerId = foundCustomer.CustomerId;

                MessageBox.Show($"Đã tìm thấy thông tin khách: {foundCustomer.FullName} (Mã KH: #{foundCustomer.CustomerId})", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _currentCustomer = null;
                _currentCustomerId = -1;
                txtHoTen.Clear();
                MessageBox.Show("Không tìm thấy khách hàng. Bạn có thể nhập Họ tên, SĐT và CCCD trên form để tự động tạo khách mới khi đặt phòng!", "Khách hàng mới", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
            }
        }
        private void ClearRoomSelectionForNewDates()
        {
            _isRoomListFresh = false;
            dgvDanhSachPhong.ClearSelection();
            dgvDanhSachPhong.DataSource = null;
            if (lblUocTinhGia != null)
            {
                lblUocTinhGia.Text = "Ước tính giá: 0 VNĐ";
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
                ClearRoomSelectionForNewDates();
                return;
            }

            if (checkOut <= checkIn)
            {
                MessageBox.Show("Ngày Check-out phải sau ngày Check-in ít nhất 1 ngày!", "Lỗi thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);

                dtpCheckOut.ValueChanged -= ValidateBookingDates;
                dtpCheckOut.Value = checkIn.AddDays(1);
                dtpCheckOut.ValueChanged += ValidateBookingDates;
            }
            ClearRoomSelectionForNewDates();  
        }

        private void LoadAvailableRooms()
        {
            try
            {
                DateTime checkIn = dtpCheckIn.Value.Date;
                DateTime checkOut = dtpCheckOut.Value.Date;

                var availableRooms = _roomService.GetAvailableRoomsByDateRange(checkIn, checkOut, null).ToList();

                var displayList = availableRooms.Select(r => new
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    TypeName = r.RoomType?.Name ?? "N/A",
                    PricePerNight = r.RoomType?.PricePerNight ?? 0,
                    Floor = r.Floor,
                    Status = r.Status
                }).ToList();

                dgvDanhSachPhong.DataSource = displayList;
                dgvDanhSachPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Đổi tên và định dạng các cột
                if (dgvDanhSachPhong.Columns["RoomId"] != null) dgvDanhSachPhong.Columns["RoomId"]!.Visible = false;
                if (dgvDanhSachPhong.Columns["RoomNumber"] != null) dgvDanhSachPhong.Columns["RoomNumber"]!.HeaderText = "Số Phòng";
                if (dgvDanhSachPhong.Columns["TypeName"] != null) dgvDanhSachPhong.Columns["TypeName"]!.HeaderText = "Loại Phòng";
                if (dgvDanhSachPhong.Columns["PricePerNight"] != null)
                {
                    dgvDanhSachPhong.Columns["PricePerNight"]!.HeaderText = "Giá/Đêm";
                    dgvDanhSachPhong.Columns["PricePerNight"]!.DefaultCellStyle.Format = "N0";
                }
                if (dgvDanhSachPhong.Columns["Floor"] != null) dgvDanhSachPhong.Columns["Floor"]!.HeaderText = "Tầng";
                if (dgvDanhSachPhong.Columns["Status"] != null) dgvDanhSachPhong.Columns["Status"]!.HeaderText = "Trạng Thái";
                _isRoomListFresh = true;
                UpdateEstimatedCost();
            }
            catch (Exception ex)
            {
                ClearRoomSelectionForNewDates();
                MessageBox.Show("Không thể tải danh sách phòng trống: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKiemTraPhong_Click(object sender, EventArgs e)
        {
            LoadAvailableRooms();

            if (dgvDanhSachPhong.Rows.Count == 0)
            {
                MessageBox.Show("Rất tiếc, không còn phòng trống nào trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnTaoBooking_Click(object sender, EventArgs e)
        {
            try
            {
                // 0. Kiểm tra phiên đăng nhập của nhân viên
                if (UserSession.CurrentUser == null || UserSession.CurrentUser.UserId <= 0)
                {
                    MessageBox.Show("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại!", "Lỗi phiên làm việc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!_isRoomListFresh)
                {
                    MessageBox.Show("Khoảng ngày đặt phòng đã thay đổi. Vui lòng bấm 'Kiểm Tra Phòng Trống' và chọn lại phòng trước khi tạo booking!", "Yêu cầu kiểm tra phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnKiemTraPhong.Focus();
                    return;
                }
                // 1. Kiểm tra chọn phòng trên DataGridView
                if (dgvDanhSachPhong.SelectedRows.Count == 0)
                {
                   
                    MessageBox.Show("Vui lòng chọn ít nhất một phòng từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Xác nhận lại khách hàng nếu chưa chọn hoặc vừa sửa SĐT/CCCD
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
                            var confirmExisting = MessageBox.Show(
                                $"Thông tin trùng với khách hàng đã có:\n- Họ tên: {existing.FullName}\n- SĐT: {existing.Phone}\n- CCCD: {existing.IdentityNumber}\n\nXác nhận đặt phòng cho khách hàng này?",
                                "Xác nhận thông tin khách hàng",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (confirmExisting != DialogResult.Yes)
                                return;

                            // 1. Điền TextBox từ biến cục bộ existing trước
                            txtHoTen.Text = existing.FullName;
                            txtCCCD.Text = existing.IdentityNumber;
                            txtSDT.Text = existing.Phone;

                            // 2. Gán _currentCustomer và _currentCustomerId sau cùng
                            _currentCustomer = existing;
                            _currentCustomerId = existing.CustomerId;
                        }
                        else
                        {
                            var confirmNew = MessageBox.Show(
                                $"Xác nhận tạo khách hàng mới và đặt phòng:\n- Họ tên: {fullName}\n- SĐT: {phone}\n- CCCD: {cccd}",
                                "Xác nhận khách hàng mới",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (confirmNew != DialogResult.Yes)
                                return;

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
                        MessageBox.Show("Vui lòng tìm khách hàng cũ, hoặc nhập đầy đủ Họ tên, SĐT và CCCD để xác nhận khách trước khi đặt phòng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 3. Gom danh sách các phòng được chọn và tính tạm tổng giá phòng 1 đêm
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

                // 4. Tính số đêm và Tổng tiền dự kiến
                DateTime checkIn = dtpCheckIn.Value.Date;
                DateTime checkOut = dtpCheckOut.Value.Date;
                int totalNights = Math.Max(1, (int)(checkOut - checkIn).TotalDays);
                decimal totalAmount = pricePerNight * totalNights;

                // 5. Đóng gói dữ liệu vào Object Booking với CreatedBy lấy từ UserSession
                Booking newBooking = new Booking
                {
                    CustomerId = _currentCustomerId,
                    BookingDate = DateTime.Now,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut,
                    Status = "PENDING",
                    TotalAmount = totalAmount,
                    CreatedBy = UserSession.CurrentUser.UserId,
                    CreatedAt = DateTime.Now
                };

                // 6. Lưu xuống DB qua tầng Business (DB sẽ tính lại TotalAmount từ tổng Subtotal)
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
        // ==========================================
        // TASK BOOK08: ƯỚC TÍNH GIÁ BOOKING
        // ==========================================
        private void UpdateEstimatedCost()
        {
            if (lblUocTinhGia == null)
                return;

            DateTime checkIn = dtpCheckIn.Value.Date;
            DateTime checkOut = dtpCheckOut.Value.Date;

            if (!_isRoomListFresh || checkOut <= checkIn || dgvDanhSachPhong.SelectedRows.Count == 0)
            {
                lblUocTinhGia.Text = "Ước tính giá: 0 VNĐ";
                return;
            }

            var countedRoomIds = new HashSet<int>();
            decimal totalPricePerNight = 0m;

            foreach (DataGridViewRow row in dgvDanhSachPhong.SelectedRows)
            {
                if (row.Cells["RoomId"]?.Value == null || row.Cells["RoomId"].Value == DBNull.Value)
                    continue;

                int roomId = Convert.ToInt32(row.Cells["RoomId"].Value);
                if (!countedRoomIds.Add(roomId))
                    continue;

                object? rawPrice = row.Cells["PricePerNight"]?.Value;
                if (rawPrice != null && rawPrice != DBNull.Value)
                {
                    decimal pricePerNight = rawPrice is decimal d ? d : Convert.ToDecimal(rawPrice);
                    totalPricePerNight += pricePerNight;
                }
            }

            if (countedRoomIds.Count == 0)
            {
                lblUocTinhGia.Text = "Ước tính giá: 0 VNĐ";
                return;
            }

            int nights = Math.Max(1, (checkOut - checkIn).Days);
            decimal estimatedCost = totalPricePerNight * nights;

            lblUocTinhGia.Text = $"Ước tính giá ({countedRoomIds.Count} phòng × {nights} đêm): {estimatedCost:N0} VNĐ";
        }

    }

}