using System;
using System.Drawing;
using System.Windows.Forms;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Business.Services;

namespace HotelBookingManagement 
{
    public class FrmBookingList : Form
    {
        private Panel _panelTop;
        private Button _btnRefresh;
        private Button _btnCancel;
        private DataGridView _dgvBookings;
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
            this.Size = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            _panelTop = new Panel { Dock = DockStyle.Top, Height = 55, Padding = new Padding(10) };

            _btnRefresh = new Button { Text = "Làm mới", Width = 100, Dock = DockStyle.Left };
            _btnRefresh.Click += (s, e) => LoadBookings();

            _btnCancel = new Button { Text = "Hủy Booking", Width = 120, Dock = DockStyle.Right, BackColor = Color.LightCoral, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
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
                BackgroundColor = Color.White
            };

            this.Controls.Add(_dgvBookings);
            this.Controls.Add(_panelTop);
        }

        private void LoadBookings()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // FrmBookingList
            // 
            ClientSize = new Size(637, 352);
            Name = "FrmBookingList";
            Load += FrmBookingList_Load;
            ResumeLayout(false);

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_dgvBookings.CurrentRow == null || _dgvBookings.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn đặt phòng trên lưới để hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookingId = Convert.ToInt32(_dgvBookings.CurrentRow.Cells["BookingId"].Value);

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn hủy đơn đặt phòng mã #{bookingId} không?",
                                          "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool isSuccess = _bookingService.CancelBooking(bookingId, out string errorMessage);

                if (isSuccess)
                {
                    MessageBox.Show("Hủy đơn đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookings(); _dgvBookings.DataSource = _bookingService.GetAll().ToList();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Lỗi hủy phòng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FrmBookingList_Load(object sender, EventArgs e)
        {

        }
    }
}