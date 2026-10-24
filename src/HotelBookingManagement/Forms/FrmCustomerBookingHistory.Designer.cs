using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingManagement.Forms
{
	partial class FrmCustomerBookingHistory
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			DataGridViewCellStyle dgvHeaderStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dgvDefaultStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dgvAltStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dgvRoomsHeaderStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dgvRoomsDefaultStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dgvPaymentsHeaderStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dgvPaymentsDefaultStyle = new DataGridViewCellStyle();

			panelTopHeader = new Panel();
			lblHeader = new Label();
			lblCustomerSummary = new Label();

			panelSummaryCards = new Panel();
			pnlCardTotalBookings = new Panel();
			lblTotalBookingsTitle = new Label();
			lblTotalBookingsValue = new Label();

			pnlCardTotalAmount = new Panel();
			lblTotalAmountTitle = new Label();
			lblTotalAmountValue = new Label();

			pnlCardTotalPaid = new Panel();
			lblTotalPaidTitle = new Label();
			lblTotalPaidValue = new Label();

			pnlCardLatestBooking = new Panel();
			lblLatestBookingTitle = new Label();
			lblLatestBookingValue = new Label();

			panelFilterBar = new Panel();
			lblFilter = new Label();
			cboStatusFilter = new ComboBox();
			btnRefresh = new Button();
			lblBookingCount = new Label();
			btnClose = new Button();

			splitMain = new SplitContainer();
			grpBookingsList = new GroupBox();
			dgvBookings = new DataGridView();
			colSTT = new DataGridViewTextBoxColumn();
			colBookingId = new DataGridViewTextBoxColumn();
			colBookingDate = new DataGridViewTextBoxColumn();
			colCheckInDate = new DataGridViewTextBoxColumn();
			colCheckOutDate = new DataGridViewTextBoxColumn();
			colRoomList = new DataGridViewTextBoxColumn();
			colTotalAmount = new DataGridViewTextBoxColumn();
			colStatus = new DataGridViewTextBoxColumn();
			colPaidAmount = new DataGridViewTextBoxColumn();
			colStaff = new DataGridViewTextBoxColumn();

			tabDetails = new TabControl();
			tabRooms = new TabPage();
			dgvRooms = new DataGridView();
			colRoomSTT = new DataGridViewTextBoxColumn();
			colRoomNumber = new DataGridViewTextBoxColumn();
			colRoomType = new DataGridViewTextBoxColumn();
			colPricePerNight = new DataGridViewTextBoxColumn();
			colNumberOfNights = new DataGridViewTextBoxColumn();
			colSubtotal = new DataGridViewTextBoxColumn();
			colRoomStatus = new DataGridViewTextBoxColumn();

			tabPayments = new TabPage();
			dgvPayments = new DataGridView();
			colPaymentSTT = new DataGridViewTextBoxColumn();
			colPaymentId = new DataGridViewTextBoxColumn();
			colPaymentAmount = new DataGridViewTextBoxColumn();
			colPaymentMethod = new DataGridViewTextBoxColumn();
			colPaymentDate = new DataGridViewTextBoxColumn();
			colPaymentStatus = new DataGridViewTextBoxColumn();

			panelTopHeader.SuspendLayout();
			panelSummaryCards.SuspendLayout();
			pnlCardTotalBookings.SuspendLayout();
			pnlCardTotalAmount.SuspendLayout();
			pnlCardTotalPaid.SuspendLayout();
			pnlCardLatestBooking.SuspendLayout();
			panelFilterBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
			splitMain.Panel1.SuspendLayout();
			splitMain.Panel2.SuspendLayout();
			splitMain.SuspendLayout();
			grpBookingsList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvBookings).BeginInit();
			tabDetails.SuspendLayout();
			tabRooms.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvRooms).BeginInit();
			tabPayments.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
			SuspendLayout();

			//
			// panelTopHeader
			//
			panelTopHeader.BackColor = Color.White;
			panelTopHeader.Controls.Add(lblHeader);
			panelTopHeader.Controls.Add(lblCustomerSummary);
			panelTopHeader.Dock = DockStyle.Top;
			panelTopHeader.Location = new Point(0, 0);
			panelTopHeader.Name = "panelTopHeader";
			panelTopHeader.Padding = new Padding(20, 10, 20, 10);
			panelTopHeader.Size = new Size(1060, 75);
			panelTopHeader.TabIndex = 0;

			//
			// lblHeader
			//
			lblHeader.AutoSize = true;
			lblHeader.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
			lblHeader.ForeColor = Color.FromArgb(24, 30, 54);
			lblHeader.Location = new Point(18, 10);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(500, 31);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "📜 LỊCH SỬ ĐẶT PHÒNG KHÁCH HÀNG (CUS05)";

			//
			// lblCustomerSummary
			//
			lblCustomerSummary.AutoSize = true;
			lblCustomerSummary.Font = new Font("Segoe UI", 9.5F);
			lblCustomerSummary.ForeColor = Color.FromArgb(108, 117, 125);
			lblCustomerSummary.Location = new Point(20, 43);
			lblCustomerSummary.Name = "lblCustomerSummary";
			lblCustomerSummary.Size = new Size(400, 21);
			lblCustomerSummary.TabIndex = 1;
			lblCustomerSummary.Text = "Khách hàng: ... | SĐT: ... | CCCD: ...";

			//
			// panelSummaryCards
			//
			panelSummaryCards.BackColor = Color.Transparent;
			panelSummaryCards.Controls.Add(pnlCardLatestBooking);
			panelSummaryCards.Controls.Add(pnlCardTotalPaid);
			panelSummaryCards.Controls.Add(pnlCardTotalAmount);
			panelSummaryCards.Controls.Add(pnlCardTotalBookings);
			panelSummaryCards.Dock = DockStyle.Top;
			panelSummaryCards.Location = new Point(0, 75);
			panelSummaryCards.Name = "panelSummaryCards";
			panelSummaryCards.Padding = new Padding(15, 10, 15, 5);
			panelSummaryCards.Size = new Size(1060, 80);
			panelSummaryCards.TabIndex = 1;

			//
			// pnlCardTotalBookings
			//
			pnlCardTotalBookings.BackColor = Color.White;
			pnlCardTotalBookings.BorderStyle = BorderStyle.FixedSingle;
			pnlCardTotalBookings.Controls.Add(lblTotalBookingsTitle);
			pnlCardTotalBookings.Controls.Add(lblTotalBookingsValue);
			pnlCardTotalBookings.Location = new Point(15, 10);
			pnlCardTotalBookings.Name = "pnlCardTotalBookings";
			pnlCardTotalBookings.Size = new Size(240, 60);
			pnlCardTotalBookings.TabIndex = 0;

			//
			// lblTotalBookingsTitle
			//
			lblTotalBookingsTitle.AutoSize = true;
			lblTotalBookingsTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
			lblTotalBookingsTitle.ForeColor = Color.FromArgb(108, 117, 125);
			lblTotalBookingsTitle.Location = new Point(10, 8);
			lblTotalBookingsTitle.Name = "lblTotalBookingsTitle";
			lblTotalBookingsTitle.Size = new Size(123, 19);
			lblTotalBookingsTitle.TabIndex = 0;
			lblTotalBookingsTitle.Text = "TỔNG SỐ ĐƠN ĐẶT";

			//
			// lblTotalBookingsValue
			//
			lblTotalBookingsValue.AutoSize = true;
			lblTotalBookingsValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblTotalBookingsValue.ForeColor = Color.FromArgb(13, 110, 253);
			lblTotalBookingsValue.Location = new Point(10, 27);
			lblTotalBookingsValue.Name = "lblTotalBookingsValue";
			lblTotalBookingsValue.Size = new Size(68, 28);
			lblTotalBookingsValue.TabIndex = 1;
			lblTotalBookingsValue.Text = "0 đơn";

			//
			// pnlCardTotalAmount
			//
			pnlCardTotalAmount.BackColor = Color.White;
			pnlCardTotalAmount.BorderStyle = BorderStyle.FixedSingle;
			pnlCardTotalAmount.Controls.Add(lblTotalAmountTitle);
			pnlCardTotalAmount.Controls.Add(lblTotalAmountValue);
			pnlCardTotalAmount.Location = new Point(270, 10);
			pnlCardTotalAmount.Name = "pnlCardTotalAmount";
			pnlCardTotalAmount.Size = new Size(245, 60);
			pnlCardTotalAmount.TabIndex = 1;

			//
			// lblTotalAmountTitle
			//
			lblTotalAmountTitle.AutoSize = true;
			lblTotalAmountTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
			lblTotalAmountTitle.ForeColor = Color.FromArgb(108, 117, 125);
			lblTotalAmountTitle.Location = new Point(10, 8);
			lblTotalAmountTitle.Name = "lblTotalAmountTitle";
			lblTotalAmountTitle.Size = new Size(130, 19);
			lblTotalAmountTitle.TabIndex = 0;
			lblTotalAmountTitle.Text = "TỔNG DOANH THU";

			//
			// lblTotalAmountValue
			//
			lblTotalAmountValue.AutoSize = true;
			lblTotalAmountValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblTotalAmountValue.ForeColor = Color.FromArgb(25, 135, 84);
			lblTotalAmountValue.Location = new Point(10, 27);
			lblTotalAmountValue.Name = "lblTotalAmountValue";
			lblTotalAmountValue.Size = new Size(60, 28);
			lblTotalAmountValue.TabIndex = 1;
			lblTotalAmountValue.Text = "0 đ";

			//
			// pnlCardTotalPaid
			//
			pnlCardTotalPaid.BackColor = Color.White;
			pnlCardTotalPaid.BorderStyle = BorderStyle.FixedSingle;
			pnlCardTotalPaid.Controls.Add(lblTotalPaidTitle);
			pnlCardTotalPaid.Controls.Add(lblTotalPaidValue);
			pnlCardTotalPaid.Location = new Point(530, 10);
			pnlCardTotalPaid.Name = "pnlCardTotalPaid";
			pnlCardTotalPaid.Size = new Size(245, 60);
			pnlCardTotalPaid.TabIndex = 2;

			//
			// lblTotalPaidTitle
			//
			lblTotalPaidTitle.AutoSize = true;
			lblTotalPaidTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
			lblTotalPaidTitle.ForeColor = Color.FromArgb(108, 117, 125);
			lblTotalPaidTitle.Location = new Point(10, 8);
			lblTotalPaidTitle.Name = "lblTotalPaidTitle";
			lblTotalPaidTitle.Size = new Size(137, 19);
			lblTotalPaidTitle.TabIndex = 0;
			lblTotalPaidTitle.Text = "ĐÃ THANH TOÁN";

			//
			// lblTotalPaidValue
			//
			lblTotalPaidValue.AutoSize = true;
			lblTotalPaidValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblTotalPaidValue.ForeColor = Color.FromArgb(13, 202, 240);
			lblTotalPaidValue.Location = new Point(10, 27);
			lblTotalPaidValue.Name = "lblTotalPaidValue";
			lblTotalPaidValue.Size = new Size(60, 28);
			lblTotalPaidValue.TabIndex = 1;
			lblTotalPaidValue.Text = "0 đ";

			//
			// pnlCardLatestBooking
			//
			pnlCardLatestBooking.BackColor = Color.White;
			pnlCardLatestBooking.BorderStyle = BorderStyle.FixedSingle;
			pnlCardLatestBooking.Controls.Add(lblLatestBookingTitle);
			pnlCardLatestBooking.Controls.Add(lblLatestBookingValue);
			pnlCardLatestBooking.Location = new Point(790, 10);
			pnlCardLatestBooking.Name = "pnlCardLatestBooking";
			pnlCardLatestBooking.Size = new Size(250, 60);
			pnlCardLatestBooking.TabIndex = 3;

			//
			// lblLatestBookingTitle
			//
			lblLatestBookingTitle.AutoSize = true;
			lblLatestBookingTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
			lblLatestBookingTitle.ForeColor = Color.FromArgb(108, 117, 125);
			lblLatestBookingTitle.Location = new Point(10, 8);
			lblLatestBookingTitle.Name = "lblLatestBookingTitle";
			lblLatestBookingTitle.Size = new Size(117, 19);
			lblLatestBookingTitle.TabIndex = 0;
			lblLatestBookingTitle.Text = "ĐƠN GẦN NHẤT";

			//
			// lblLatestBookingValue
			//
			lblLatestBookingValue.AutoSize = true;
			lblLatestBookingValue.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
			lblLatestBookingValue.ForeColor = Color.FromArgb(33, 37, 41);
			lblLatestBookingValue.Location = new Point(10, 28);
			lblLatestBookingValue.Name = "lblLatestBookingValue";
			lblLatestBookingValue.Size = new Size(130, 28);
			lblLatestBookingValue.TabIndex = 1;
			lblLatestBookingValue.Text = "Chưa có đơn";

			//
			// panelFilterBar
			//
			panelFilterBar.BackColor = Color.Transparent;
			panelFilterBar.Controls.Add(btnClose);
			panelFilterBar.Controls.Add(btnRefresh);
			panelFilterBar.Controls.Add(cboStatusFilter);
			panelFilterBar.Controls.Add(lblFilter);
			panelFilterBar.Controls.Add(lblBookingCount);
			panelFilterBar.Dock = DockStyle.Top;
			panelFilterBar.Location = new Point(0, 155);
			panelFilterBar.Name = "panelFilterBar";
			panelFilterBar.Padding = new Padding(15, 5, 15, 5);
			panelFilterBar.Size = new Size(1060, 45);
			panelFilterBar.TabIndex = 2;

			//
			// lblFilter
			//
			lblFilter.AutoSize = true;
			lblFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblFilter.ForeColor = Color.FromArgb(73, 80, 87);
			lblFilter.Location = new Point(15, 12);
			lblFilter.Name = "lblFilter";
			lblFilter.Size = new Size(149, 20);
			lblFilter.TabIndex = 0;
			lblFilter.Text = "Lọc theo trạng thái:";

			//
			// cboStatusFilter
			//
			cboStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
			cboStatusFilter.Font = new Font("Segoe UI", 9.5F);
			cboStatusFilter.FormattingEnabled = true;
			cboStatusFilter.Location = new Point(168, 7);
			cboStatusFilter.Name = "cboStatusFilter";
			cboStatusFilter.Size = new Size(200, 29);
			cboStatusFilter.TabIndex = 1;
			cboStatusFilter.SelectedIndexChanged += cboStatusFilter_SelectedIndexChanged;

			//
			// btnRefresh
			//
			btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
			btnRefresh.Cursor = Cursors.Hand;
			btnRefresh.FlatAppearance.BorderSize = 0;
			btnRefresh.FlatStyle = FlatStyle.Flat;
			btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnRefresh.ForeColor = Color.White;
			btnRefresh.Location = new Point(380, 7);
			btnRefresh.Name = "btnRefresh";
			btnRefresh.Size = new Size(95, 29);
			btnRefresh.TabIndex = 2;
			btnRefresh.Text = "🔄 Tải lại";
			btnRefresh.UseVisualStyleBackColor = false;
			btnRefresh.Click += btnRefresh_Click;

			//
			// lblBookingCount
			//
			lblBookingCount.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
			lblBookingCount.ForeColor = Color.FromArgb(108, 117, 125);
			lblBookingCount.Location = new Point(485, 11);
			lblBookingCount.Name = "lblBookingCount";
			lblBookingCount.Size = new Size(300, 23);
			lblBookingCount.TabIndex = 3;
			lblBookingCount.Text = "Hiển thị: 0 đơn đặt phòng";
			lblBookingCount.TextAlign = ContentAlignment.MiddleLeft;

			//
			// btnClose
			//
			btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnClose.BackColor = Color.FromArgb(220, 53, 69);
			btnClose.Cursor = Cursors.Hand;
			btnClose.FlatAppearance.BorderSize = 0;
			btnClose.FlatStyle = FlatStyle.Flat;
			btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnClose.ForeColor = Color.White;
			btnClose.Location = new Point(945, 7);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(95, 29);
			btnClose.TabIndex = 4;
			btnClose.Text = "✖️ Đóng";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += btnClose_Click;

			//
			// splitMain
			//
			splitMain.Dock = DockStyle.Fill;
			splitMain.Location = new Point(0, 200);
			splitMain.Name = "splitMain";
			splitMain.Orientation = Orientation.Horizontal;
			splitMain.Padding = new Padding(15, 0, 15, 15);
			splitMain.Panel1.Controls.Add(grpBookingsList);
			splitMain.Panel2.Controls.Add(tabDetails);
			splitMain.Size = new Size(1060, 480);
			splitMain.SplitterDistance = 250;
			splitMain.TabIndex = 3;

			//
			// grpBookingsList
			//
			grpBookingsList.BackColor = Color.White;
			grpBookingsList.Controls.Add(dgvBookings);
			grpBookingsList.Dock = DockStyle.Fill;
			grpBookingsList.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			grpBookingsList.ForeColor = Color.FromArgb(24, 30, 54);
			grpBookingsList.Location = new Point(0, 0);
			grpBookingsList.Name = "grpBookingsList";
			grpBookingsList.Padding = new Padding(10);
			grpBookingsList.Size = new Size(1030, 250);
			grpBookingsList.TabIndex = 0;
			grpBookingsList.TabStop = false;
			grpBookingsList.Text = "  Danh sách các đơn đặt phòng  ";

			//
			// dgvBookings
			//
			dgvBookings.AllowUserToAddRows = false;
			dgvBookings.AllowUserToDeleteRows = false;
			dgvBookings.AllowUserToResizeRows = false;
			dgvBookings.AutoGenerateColumns = false;
			dgvBookings.BackgroundColor = Color.White;
			dgvBookings.BorderStyle = BorderStyle.None;
			dgvBookings.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvBookings.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

			dgvHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvHeaderStyle.BackColor = Color.FromArgb(24, 30, 54);
			dgvHeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			dgvHeaderStyle.ForeColor = Color.White;
			dgvHeaderStyle.Padding = new Padding(5, 0, 0, 0);
			dgvHeaderStyle.SelectionBackColor = Color.FromArgb(24, 30, 54);
			dgvHeaderStyle.SelectionForeColor = Color.White;
			dgvBookings.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
			dgvBookings.ColumnHeadersHeight = 36;
			dgvBookings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

			dgvDefaultStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvDefaultStyle.BackColor = Color.White;
			dgvDefaultStyle.Font = new Font("Segoe UI", 9F);
			dgvDefaultStyle.ForeColor = Color.FromArgb(33, 37, 41);
			dgvDefaultStyle.Padding = new Padding(5, 0, 0, 0);
			dgvDefaultStyle.SelectionBackColor = Color.FromArgb(217, 237, 247);
			dgvDefaultStyle.SelectionForeColor = Color.FromArgb(24, 30, 54);
			dgvBookings.DefaultCellStyle = dgvDefaultStyle;

			dgvAltStyle.BackColor = Color.FromArgb(248, 249, 250);
			dgvAltStyle.ForeColor = Color.FromArgb(33, 37, 41);
			dgvAltStyle.SelectionBackColor = Color.FromArgb(217, 237, 247);
			dgvAltStyle.SelectionForeColor = Color.FromArgb(24, 30, 54);
			dgvBookings.AlternatingRowsDefaultCellStyle = dgvAltStyle;

			colSTT.HeaderText = "STT";
			colSTT.Name = "colSTT";
			colSTT.ReadOnly = true;
			colSTT.Width = 50;

			colBookingId.DataPropertyName = "BookingId";
			colBookingId.HeaderText = "Mã đơn";
			colBookingId.Name = "colBookingId";
			colBookingId.ReadOnly = true;
			colBookingId.Width = 85;

			colBookingDate.DataPropertyName = "BookingDate";
			colBookingDate.HeaderText = "Ngày đặt";
			colBookingDate.Name = "colBookingDate";
			colBookingDate.ReadOnly = true;
			colBookingDate.Width = 135;

			colCheckInDate.DataPropertyName = "CheckInDate";
			colCheckInDate.HeaderText = "Nhận phòng";
			colCheckInDate.Name = "colCheckInDate";
			colCheckInDate.ReadOnly = true;
			colCheckInDate.Width = 135;

			colCheckOutDate.DataPropertyName = "CheckOutDate";
			colCheckOutDate.HeaderText = "Trả phòng";
			colCheckOutDate.Name = "colCheckOutDate";
			colCheckOutDate.ReadOnly = true;
			colCheckOutDate.Width = 135;

			colRoomList.HeaderText = "Phòng đặt";
			colRoomList.Name = "colRoomList";
			colRoomList.ReadOnly = true;
			colRoomList.Width = 130;

			colTotalAmount.DataPropertyName = "TotalAmount";
			colTotalAmount.HeaderText = "Tổng tiền";
			colTotalAmount.Name = "colTotalAmount";
			colTotalAmount.ReadOnly = true;
			colTotalAmount.Width = 120;

			colStatus.DataPropertyName = "Status";
			colStatus.HeaderText = "Trạng thái";
			colStatus.Name = "colStatus";
			colStatus.ReadOnly = true;
			colStatus.Width = 120;

			colPaidAmount.HeaderText = "Đã thanh toán";
			colPaidAmount.Name = "colPaidAmount";
			colPaidAmount.ReadOnly = true;
			colPaidAmount.Width = 120;

			colStaff.HeaderText = "Nhân viên tạo";
			colStaff.Name = "colStaff";
			colStaff.ReadOnly = true;
			colStaff.Width = 130;

			dgvBookings.Columns.AddRange(new DataGridViewColumn[] {
				colSTT,
				colBookingId,
				colBookingDate,
				colCheckInDate,
				colCheckOutDate,
				colRoomList,
				colTotalAmount,
				colStatus,
				colPaidAmount,
				colStaff
			});
			dgvBookings.Dock = DockStyle.Fill;
			dgvBookings.EnableHeadersVisualStyles = false;
			dgvBookings.GridColor = Color.FromArgb(222, 226, 230);
			dgvBookings.Location = new Point(10, 33);
			dgvBookings.MultiSelect = false;
			dgvBookings.Name = "dgvBookings";
			dgvBookings.ReadOnly = true;
			dgvBookings.RowHeadersVisible = false;
			dgvBookings.RowTemplate.Height = 32;
			dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvBookings.Size = new Size(1010, 207);
			dgvBookings.TabIndex = 0;
			dgvBookings.CellFormatting += dgvBookings_CellFormatting;
			dgvBookings.SelectionChanged += dgvBookings_SelectionChanged;

			//
			// tabDetails
			//
			tabDetails.Controls.Add(tabRooms);
			tabDetails.Controls.Add(tabPayments);
			tabDetails.Dock = DockStyle.Fill;
			tabDetails.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
			tabDetails.Location = new Point(0, 0);
			tabDetails.Name = "tabDetails";
			tabDetails.SelectedIndex = 0;
			tabDetails.Size = new Size(1030, 226);
			tabDetails.TabIndex = 0;

			//
			// tabRooms
			//
			tabRooms.BackColor = Color.White;
			tabRooms.Controls.Add(dgvRooms);
			tabRooms.Location = new Point(4, 30);
			tabRooms.Name = "tabRooms";
			tabRooms.Padding = new Padding(5);
			tabRooms.Size = new Size(1022, 192);
			tabRooms.TabIndex = 0;
			tabRooms.Text = "🏨 Chi tiết phòng trong đơn";

			//
			// dgvRooms
			//
			dgvRooms.AllowUserToAddRows = false;
			dgvRooms.AllowUserToDeleteRows = false;
			dgvRooms.AllowUserToResizeRows = false;
			dgvRooms.AutoGenerateColumns = false;
			dgvRooms.BackgroundColor = Color.White;
			dgvRooms.BorderStyle = BorderStyle.None;
			dgvRooms.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvRooms.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

			dgvRoomsHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvRoomsHeaderStyle.BackColor = Color.FromArgb(52, 58, 64);
			dgvRoomsHeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			dgvRoomsHeaderStyle.ForeColor = Color.White;
			dgvRoomsHeaderStyle.Padding = new Padding(5, 0, 0, 0);
			dgvRoomsHeaderStyle.SelectionBackColor = Color.FromArgb(52, 58, 64);
			dgvRoomsHeaderStyle.SelectionForeColor = Color.White;
			dgvRooms.ColumnHeadersDefaultCellStyle = dgvRoomsHeaderStyle;
			dgvRooms.ColumnHeadersHeight = 32;
			dgvRooms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

			dgvRoomsDefaultStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvRoomsDefaultStyle.BackColor = Color.White;
			dgvRoomsDefaultStyle.Font = new Font("Segoe UI", 9F);
			dgvRoomsDefaultStyle.ForeColor = Color.FromArgb(33, 37, 41);
			dgvRoomsDefaultStyle.Padding = new Padding(5, 0, 0, 0);
			dgvRoomsDefaultStyle.SelectionBackColor = Color.FromArgb(233, 236, 239);
			dgvRoomsDefaultStyle.SelectionForeColor = Color.FromArgb(24, 30, 54);
			dgvRooms.DefaultCellStyle = dgvRoomsDefaultStyle;

			colRoomSTT.HeaderText = "STT";
			colRoomSTT.Name = "colRoomSTT";
			colRoomSTT.ReadOnly = true;
			colRoomSTT.Width = 50;

			colRoomNumber.HeaderText = "Số phòng";
			colRoomNumber.Name = "colRoomNumber";
			colRoomNumber.ReadOnly = true;
			colRoomNumber.Width = 100;

			colRoomType.HeaderText = "Loại phòng";
			colRoomType.Name = "colRoomType";
			colRoomType.ReadOnly = true;
			colRoomType.Width = 150;

			colPricePerNight.HeaderText = "Đơn giá / đêm";
			colPricePerNight.Name = "colPricePerNight";
			colPricePerNight.ReadOnly = true;
			colPricePerNight.Width = 130;

			colNumberOfNights.HeaderText = "Số đêm";
			colNumberOfNights.Name = "colNumberOfNights";
			colNumberOfNights.ReadOnly = true;
			colNumberOfNights.Width = 90;

			colSubtotal.HeaderText = "Thành tiền";
			colSubtotal.Name = "colSubtotal";
			colSubtotal.ReadOnly = true;
			colSubtotal.Width = 140;

			colRoomStatus.HeaderText = "Trạng thái phòng";
			colRoomStatus.Name = "colRoomStatus";
			colRoomStatus.ReadOnly = true;
			colRoomStatus.Width = 140;

			dgvRooms.Columns.AddRange(new DataGridViewColumn[] {
				colRoomSTT,
				colRoomNumber,
				colRoomType,
				colPricePerNight,
				colNumberOfNights,
				colSubtotal,
				colRoomStatus
			});
			dgvRooms.Dock = DockStyle.Fill;
			dgvRooms.EnableHeadersVisualStyles = false;
			dgvRooms.GridColor = Color.FromArgb(222, 226, 230);
			dgvRooms.Location = new Point(5, 5);
			dgvRooms.MultiSelect = false;
			dgvRooms.Name = "dgvRooms";
			dgvRooms.ReadOnly = true;
			dgvRooms.RowHeadersVisible = false;
			dgvRooms.RowTemplate.Height = 28;
			dgvRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvRooms.Size = new Size(1012, 182);
			dgvRooms.TabIndex = 0;
			dgvRooms.CellFormatting += dgvRooms_CellFormatting;

			//
			// tabPayments
			//
			tabPayments.BackColor = Color.White;
			tabPayments.Controls.Add(dgvPayments);
			tabPayments.Location = new Point(4, 30);
			tabPayments.Name = "tabPayments";
			tabPayments.Padding = new Padding(5);
			tabPayments.Size = new Size(1022, 192);
			tabPayments.TabIndex = 1;
			tabPayments.Text = "💳 Lịch sử giao dịch thanh toán";

			//
			// dgvPayments
			//
			dgvPayments.AllowUserToAddRows = false;
			dgvPayments.AllowUserToDeleteRows = false;
			dgvPayments.AllowUserToResizeRows = false;
			dgvPayments.AutoGenerateColumns = false;
			dgvPayments.BackgroundColor = Color.White;
			dgvPayments.BorderStyle = BorderStyle.None;
			dgvPayments.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvPayments.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

			dgvPaymentsHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvPaymentsHeaderStyle.BackColor = Color.FromArgb(52, 58, 64);
			dgvPaymentsHeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			dgvPaymentsHeaderStyle.ForeColor = Color.White;
			dgvPaymentsHeaderStyle.Padding = new Padding(5, 0, 0, 0);
			dgvPaymentsHeaderStyle.SelectionBackColor = Color.FromArgb(52, 58, 64);
			dgvPaymentsHeaderStyle.SelectionForeColor = Color.White;
			dgvPayments.ColumnHeadersDefaultCellStyle = dgvPaymentsHeaderStyle;
			dgvPayments.ColumnHeadersHeight = 32;
			dgvPayments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

			dgvPaymentsDefaultStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvPaymentsDefaultStyle.BackColor = Color.White;
			dgvPaymentsDefaultStyle.Font = new Font("Segoe UI", 9F);
			dgvPaymentsDefaultStyle.ForeColor = Color.FromArgb(33, 37, 41);
			dgvPaymentsDefaultStyle.Padding = new Padding(5, 0, 0, 0);
			dgvPaymentsDefaultStyle.SelectionBackColor = Color.FromArgb(233, 236, 239);
			dgvPaymentsDefaultStyle.SelectionForeColor = Color.FromArgb(24, 30, 54);
			dgvPayments.DefaultCellStyle = dgvPaymentsDefaultStyle;

			colPaymentSTT.HeaderText = "STT";
			colPaymentSTT.Name = "colPaymentSTT";
			colPaymentSTT.ReadOnly = true;
			colPaymentSTT.Width = 50;

			colPaymentId.HeaderText = "Mã giao dịch";
			colPaymentId.Name = "colPaymentId";
			colPaymentId.ReadOnly = true;
			colPaymentId.Width = 120;

			colPaymentAmount.HeaderText = "Số tiền thanh toán";
			colPaymentAmount.Name = "colPaymentAmount";
			colPaymentAmount.ReadOnly = true;
			colPaymentAmount.Width = 160;

			colPaymentMethod.HeaderText = "Hình thức";
			colPaymentMethod.Name = "colPaymentMethod";
			colPaymentMethod.ReadOnly = true;
			colPaymentMethod.Width = 140;

			colPaymentDate.HeaderText = "Thời gian thanh toán";
			colPaymentDate.Name = "colPaymentDate";
			colPaymentDate.ReadOnly = true;
			colPaymentDate.Width = 170;

			colPaymentStatus.HeaderText = "Trạng thái";
			colPaymentStatus.Name = "colPaymentStatus";
			colPaymentStatus.ReadOnly = true;
			colPaymentStatus.Width = 130;

			dgvPayments.Columns.AddRange(new DataGridViewColumn[] {
				colPaymentSTT,
				colPaymentId,
				colPaymentAmount,
				colPaymentMethod,
				colPaymentDate,
				colPaymentStatus
			});
			dgvPayments.Dock = DockStyle.Fill;
			dgvPayments.EnableHeadersVisualStyles = false;
			dgvPayments.GridColor = Color.FromArgb(222, 226, 230);
			dgvPayments.Location = new Point(5, 5);
			dgvPayments.MultiSelect = false;
			dgvPayments.Name = "dgvPayments";
			dgvPayments.ReadOnly = true;
			dgvPayments.RowHeadersVisible = false;
			dgvPayments.RowTemplate.Height = 28;
			dgvPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvPayments.Size = new Size(1012, 182);
			dgvPayments.TabIndex = 0;
			dgvPayments.CellFormatting += dgvPayments_CellFormatting;

			//
			// FrmCustomerBookingHistory
			//
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(244, 246, 249);
			ClientSize = new Size(1060, 680);
			Controls.Add(splitMain);
			Controls.Add(panelFilterBar);
			Controls.Add(panelSummaryCards);
			Controls.Add(panelTopHeader);
			Font = new Font("Segoe UI", 9F);
			MinimumSize = new Size(950, 600);
			Name = "FrmCustomerBookingHistory";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Lịch sử đặt phòng khách hàng";
			Load += FrmCustomerBookingHistory_Load;

			panelTopHeader.ResumeLayout(false);
			panelTopHeader.PerformLayout();
			panelSummaryCards.ResumeLayout(false);
			pnlCardTotalBookings.ResumeLayout(false);
			pnlCardTotalBookings.PerformLayout();
			pnlCardTotalAmount.ResumeLayout(false);
			pnlCardTotalAmount.PerformLayout();
			pnlCardTotalPaid.ResumeLayout(false);
			pnlCardTotalPaid.PerformLayout();
			pnlCardLatestBooking.ResumeLayout(false);
			pnlCardLatestBooking.PerformLayout();
			panelFilterBar.ResumeLayout(false);
			panelFilterBar.PerformLayout();
			splitMain.Panel1.ResumeLayout(false);
			splitMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
			splitMain.ResumeLayout(false);
			grpBookingsList.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvBookings).EndInit();
			tabDetails.ResumeLayout(false);
			tabRooms.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvRooms).EndInit();
			tabPayments.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelTopHeader;
		private Label lblHeader;
		private Label lblCustomerSummary;

		private Panel panelSummaryCards;
		private Panel pnlCardTotalBookings;
		private Label lblTotalBookingsTitle;
		private Label lblTotalBookingsValue;
		private Panel pnlCardTotalAmount;
		private Label lblTotalAmountTitle;
		private Label lblTotalAmountValue;
		private Panel pnlCardTotalPaid;
		private Label lblTotalPaidTitle;
		private Label lblTotalPaidValue;
		private Panel pnlCardLatestBooking;
		private Label lblLatestBookingTitle;
		private Label lblLatestBookingValue;

		private Panel panelFilterBar;
		private Label lblFilter;
		private ComboBox cboStatusFilter;
		private Button btnRefresh;
		private Label lblBookingCount;
		private Button btnClose;

		private SplitContainer splitMain;
		private GroupBox grpBookingsList;
		private DataGridView dgvBookings;
		private DataGridViewTextBoxColumn colSTT;
		private DataGridViewTextBoxColumn colBookingId;
		private DataGridViewTextBoxColumn colBookingDate;
		private DataGridViewTextBoxColumn colCheckInDate;
		private DataGridViewTextBoxColumn colCheckOutDate;
		private DataGridViewTextBoxColumn colRoomList;
		private DataGridViewTextBoxColumn colTotalAmount;
		private DataGridViewTextBoxColumn colStatus;
		private DataGridViewTextBoxColumn colPaidAmount;
		private DataGridViewTextBoxColumn colStaff;

		private TabControl tabDetails;
		private TabPage tabRooms;
		private DataGridView dgvRooms;
		private DataGridViewTextBoxColumn colRoomSTT;
		private DataGridViewTextBoxColumn colRoomNumber;
		private DataGridViewTextBoxColumn colRoomType;
		private DataGridViewTextBoxColumn colPricePerNight;
		private DataGridViewTextBoxColumn colNumberOfNights;
		private DataGridViewTextBoxColumn colSubtotal;
		private DataGridViewTextBoxColumn colRoomStatus;

		private TabPage tabPayments;
		private DataGridView dgvPayments;
		private DataGridViewTextBoxColumn colPaymentSTT;
		private DataGridViewTextBoxColumn colPaymentId;
		private DataGridViewTextBoxColumn colPaymentAmount;
		private DataGridViewTextBoxColumn colPaymentMethod;
		private DataGridViewTextBoxColumn colPaymentDate;
		private DataGridViewTextBoxColumn colPaymentStatus;
	}
}
