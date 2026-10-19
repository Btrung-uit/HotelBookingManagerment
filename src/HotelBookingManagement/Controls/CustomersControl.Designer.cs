using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingManagement.Controls
{
	partial class CustomersControl
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

		#region Component Designer generated code

		private void InitializeComponent()
		{
			DataGridViewCellStyle dgvHeaderStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dgvDefaultStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dgvAltStyle = new DataGridViewCellStyle();

			panelTopHeader = new Panel();
			lblHeader = new Label();
			lblSub = new Label();

			panelMainContent = new Panel();
			panelLeftForm = new Panel();
			grpCustomerInfo = new GroupBox();
			
			lblCustomerId = new Label();
			txtCustomerId = new TextBox();
			lblFullName = new Label();
			txtFullName = new TextBox();
			lblPhone = new Label();
			txtPhone = new TextBox();
			lblIdentityNumber = new Label();
			txtIdentityNumber = new TextBox();
			lblEmail = new Label();
			txtEmail = new TextBox();
			lblDateOfBirth = new Label();
			dtpDateOfBirth = new DateTimePicker();
			lblAddress = new Label();
			txtAddress = new TextBox();

			panelActionButtons = new Panel();
			btnAdd = new Button();
			btnUpdate = new Button();
			btnDelete = new Button();
			btnClear = new Button();

			panelRightGrid = new Panel();
			grpCustomerList = new GroupBox();
			panelSearchHeader = new Panel();
			lblSearch = new Label();
			txtSearch = new TextBox();
			btnSearch = new Button();
			btnRefresh = new Button();
			lblTotalCount = new Label();

			dgvCustomers = new DataGridView();
			colCustomerId = new DataGridViewTextBoxColumn();
			colFullName = new DataGridViewTextBoxColumn();
			colPhone = new DataGridViewTextBoxColumn();
			colIdentityNumber = new DataGridViewTextBoxColumn();
			colEmail = new DataGridViewTextBoxColumn();
			colDateOfBirth = new DataGridViewTextBoxColumn();
			colAddress = new DataGridViewTextBoxColumn();
			colCreatedAt = new DataGridViewTextBoxColumn();

			panelTopHeader.SuspendLayout();
			panelMainContent.SuspendLayout();
			panelLeftForm.SuspendLayout();
			grpCustomerInfo.SuspendLayout();
			panelActionButtons.SuspendLayout();
			panelRightGrid.SuspendLayout();
			grpCustomerList.SuspendLayout();
			panelSearchHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
			SuspendLayout();

			// 
			// panelTopHeader
			// 
			panelTopHeader.BackColor = Color.White;
			panelTopHeader.Controls.Add(lblHeader);
			panelTopHeader.Controls.Add(lblSub);
			panelTopHeader.Dock = DockStyle.Top;
			panelTopHeader.Location = new Point(0, 0);
			panelTopHeader.Name = "panelTopHeader";
			panelTopHeader.Padding = new Padding(20, 12, 20, 12);
			panelTopHeader.Size = new Size(1100, 75);
			panelTopHeader.TabIndex = 0;

			// 
			// lblHeader
			// 
			lblHeader.AutoSize = true;
			lblHeader.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
			lblHeader.ForeColor = Color.FromArgb(24, 30, 54);
			lblHeader.Location = new Point(18, 10);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(410, 31);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "👥 QUẢN LÝ KHÁCH HÀNG (CUSTOMERS)";

			// 
			// lblSub
			// 
			lblSub.AutoSize = true;
			lblSub.Font = new Font("Segoe UI", 9.5F);
			lblSub.ForeColor = Color.FromArgb(108, 117, 125);
			lblSub.Location = new Point(20, 43);
			lblSub.Name = "lblSub";
			lblSub.Size = new Size(620, 21);
			lblSub.TabIndex = 1;
			lblSub.Text = "Module TV02: Thêm mới, cập nhật, tra cứu thông tin khách hàng và lịch sử lưu trú";

			// 
			// panelMainContent
			// 
			panelMainContent.BackColor = Color.FromArgb(244, 246, 249);
			panelMainContent.Controls.Add(panelRightGrid);
			panelMainContent.Controls.Add(panelLeftForm);
			panelMainContent.Dock = DockStyle.Fill;
			panelMainContent.Location = new Point(0, 75);
			panelMainContent.Name = "panelMainContent";
			panelMainContent.Padding = new Padding(15);
			panelMainContent.Size = new Size(1100, 605);
			panelMainContent.TabIndex = 1;

			// 
			// panelLeftForm
			// 
			panelLeftForm.BackColor = Color.Transparent;
			panelLeftForm.Controls.Add(grpCustomerInfo);
			panelLeftForm.Dock = DockStyle.Left;
			panelLeftForm.Location = new Point(15, 15);
			panelLeftForm.Name = "panelLeftForm";
			panelLeftForm.Size = new Size(380, 575);
			panelLeftForm.TabIndex = 0;

			// 
			// grpCustomerInfo
			// 
			grpCustomerInfo.BackColor = Color.White;
			grpCustomerInfo.Controls.Add(panelActionButtons);
			grpCustomerInfo.Controls.Add(txtAddress);
			grpCustomerInfo.Controls.Add(lblAddress);
			grpCustomerInfo.Controls.Add(dtpDateOfBirth);
			grpCustomerInfo.Controls.Add(lblDateOfBirth);
			grpCustomerInfo.Controls.Add(txtEmail);
			grpCustomerInfo.Controls.Add(lblEmail);
			grpCustomerInfo.Controls.Add(txtIdentityNumber);
			grpCustomerInfo.Controls.Add(lblIdentityNumber);
			grpCustomerInfo.Controls.Add(txtPhone);
			grpCustomerInfo.Controls.Add(lblPhone);
			grpCustomerInfo.Controls.Add(txtFullName);
			grpCustomerInfo.Controls.Add(lblFullName);
			grpCustomerInfo.Controls.Add(txtCustomerId);
			grpCustomerInfo.Controls.Add(lblCustomerId);
			grpCustomerInfo.Dock = DockStyle.Fill;
			grpCustomerInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			grpCustomerInfo.ForeColor = Color.FromArgb(24, 30, 54);
			grpCustomerInfo.Location = new Point(0, 0);
			grpCustomerInfo.Name = "grpCustomerInfo";
			grpCustomerInfo.Padding = new Padding(12);
			grpCustomerInfo.Size = new Size(380, 575);
			grpCustomerInfo.TabIndex = 0;
			grpCustomerInfo.TabStop = false;
			grpCustomerInfo.Text = "  Thông tin khách hàng  ";

			// 
			// lblCustomerId
			// 
			lblCustomerId.AutoSize = true;
			lblCustomerId.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblCustomerId.ForeColor = Color.FromArgb(73, 80, 87);
			lblCustomerId.Location = new Point(16, 32);
			lblCustomerId.Name = "lblCustomerId";
			lblCustomerId.Size = new Size(111, 20);
			lblCustomerId.TabIndex = 0;
			lblCustomerId.Text = "Mã khách hàng:";

			// 
			// txtCustomerId
			// 
			txtCustomerId.BackColor = Color.FromArgb(233, 236, 239);
			txtCustomerId.BorderStyle = BorderStyle.FixedSingle;
			txtCustomerId.Font = new Font("Segoe UI", 9.5F);
			txtCustomerId.ForeColor = Color.FromArgb(73, 80, 87);
			txtCustomerId.Location = new Point(16, 54);
			txtCustomerId.Name = "txtCustomerId";
			txtCustomerId.ReadOnly = true;
			txtCustomerId.Size = new Size(346, 29);
			txtCustomerId.TabIndex = 1;
			txtCustomerId.Text = "(Tự động sinh)";

			// 
			// lblFullName
			// 
			lblFullName.AutoSize = true;
			lblFullName.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblFullName.ForeColor = Color.FromArgb(73, 80, 87);
			lblFullName.Location = new Point(16, 88);
			lblFullName.Name = "lblFullName";
			lblFullName.Size = new Size(117, 20);
			lblFullName.TabIndex = 2;
			lblFullName.Text = "Họ và tên (*):";

			// 
			// txtFullName
			// 
			txtFullName.BorderStyle = BorderStyle.FixedSingle;
			txtFullName.Font = new Font("Segoe UI", 9.5F);
			txtFullName.Location = new Point(16, 110);
			txtFullName.MaxLength = 100;
			txtFullName.Name = "txtFullName";
			txtFullName.PlaceholderText = "Nhập họ tên đầy đủ...";
			txtFullName.Size = new Size(346, 29);
			txtFullName.TabIndex = 3;

			// 
			// lblPhone
			// 
			lblPhone.AutoSize = true;
			lblPhone.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblPhone.ForeColor = Color.FromArgb(73, 80, 87);
			lblPhone.Location = new Point(16, 144);
			lblPhone.Name = "lblPhone";
			lblPhone.Size = new Size(139, 20);
			lblPhone.TabIndex = 4;
			lblPhone.Text = "Số điện thoại (*):";

			// 
			// txtPhone
			// 
			txtPhone.BorderStyle = BorderStyle.FixedSingle;
			txtPhone.Font = new Font("Segoe UI", 9.5F);
			txtPhone.Location = new Point(16, 166);
			txtPhone.MaxLength = 15;
			txtPhone.Name = "txtPhone";
			txtPhone.PlaceholderText = "10 đến 15 chữ số (duy nhất)...";
			txtPhone.Size = new Size(346, 29);
			txtPhone.TabIndex = 5;

			// 
			// lblIdentityNumber
			// 
			lblIdentityNumber.AutoSize = true;
			lblIdentityNumber.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblIdentityNumber.ForeColor = Color.FromArgb(73, 80, 87);
			lblIdentityNumber.Location = new Point(16, 200);
			lblIdentityNumber.Name = "lblIdentityNumber";
			lblIdentityNumber.Size = new Size(160, 20);
			lblIdentityNumber.TabIndex = 6;
			lblIdentityNumber.Text = "CCCD / Hộ chiếu (*):";

			// 
			// txtIdentityNumber
			// 
			txtIdentityNumber.BorderStyle = BorderStyle.FixedSingle;
			txtIdentityNumber.Font = new Font("Segoe UI", 9.5F);
			txtIdentityNumber.Location = new Point(16, 222);
			txtIdentityNumber.MaxLength = 20;
			txtIdentityNumber.Name = "txtIdentityNumber";
			txtIdentityNumber.PlaceholderText = "Số CCCD hoặc Hộ chiếu (duy nhất)...";
			txtIdentityNumber.Size = new Size(346, 29);
			txtIdentityNumber.TabIndex = 7;

			// 
			// lblEmail
			// 
			lblEmail.AutoSize = true;
			lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblEmail.ForeColor = Color.FromArgb(73, 80, 87);
			lblEmail.Location = new Point(16, 256);
			lblEmail.Name = "lblEmail";
			lblEmail.Size = new Size(49, 20);
			lblEmail.TabIndex = 8;
			lblEmail.Text = "Email:";

			// 
			// txtEmail
			// 
			txtEmail.BorderStyle = BorderStyle.FixedSingle;
			txtEmail.Font = new Font("Segoe UI", 9.5F);
			txtEmail.Location = new Point(16, 278);
			txtEmail.MaxLength = 100;
			txtEmail.Name = "txtEmail";
			txtEmail.PlaceholderText = "customer@example.com (tùy chọn)";
			txtEmail.Size = new Size(346, 29);
			txtEmail.TabIndex = 9;

			// 
			// lblDateOfBirth
			// 
			lblDateOfBirth.AutoSize = true;
			lblDateOfBirth.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblDateOfBirth.ForeColor = Color.FromArgb(73, 80, 87);
			lblDateOfBirth.Location = new Point(16, 312);
			lblDateOfBirth.Name = "lblDateOfBirth";
			lblDateOfBirth.Size = new Size(77, 20);
			lblDateOfBirth.TabIndex = 10;
			lblDateOfBirth.Text = "Ngày sinh:";

			// 
			// dtpDateOfBirth
			// 
			dtpDateOfBirth.CustomFormat = "dd/MM/yyyy";
			dtpDateOfBirth.Font = new Font("Segoe UI", 9.5F);
			dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
			dtpDateOfBirth.Location = new Point(16, 334);
			dtpDateOfBirth.Name = "dtpDateOfBirth";
			dtpDateOfBirth.ShowCheckBox = true;
			dtpDateOfBirth.Checked = false;
			dtpDateOfBirth.Size = new Size(346, 29);
			dtpDateOfBirth.TabIndex = 11;

			// 
			// lblAddress
			// 
			lblAddress.AutoSize = true;
			lblAddress.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblAddress.ForeColor = Color.FromArgb(73, 80, 87);
			lblAddress.Location = new Point(16, 368);
			lblAddress.Name = "lblAddress";
			lblAddress.Size = new Size(58, 20);
			lblAddress.TabIndex = 12;
			lblAddress.Text = "Địa chỉ:";

			// 
			// txtAddress
			// 
			txtAddress.BorderStyle = BorderStyle.FixedSingle;
			txtAddress.Font = new Font("Segoe UI", 9.5F);
			txtAddress.Location = new Point(16, 390);
			txtAddress.MaxLength = 255;
			txtAddress.Multiline = true;
			txtAddress.Name = "txtAddress";
			txtAddress.PlaceholderText = "Địa chỉ cư trú khách hàng (tùy chọn)...";
			txtAddress.ScrollBars = ScrollBars.Vertical;
			txtAddress.Size = new Size(346, 60);
			txtAddress.TabIndex = 13;

			// 
			// panelActionButtons
			// 
			panelActionButtons.Controls.Add(btnClear);
			panelActionButtons.Controls.Add(btnDelete);
			panelActionButtons.Controls.Add(btnUpdate);
			panelActionButtons.Controls.Add(btnAdd);
			panelActionButtons.Location = new Point(16, 460);
			panelActionButtons.Name = "panelActionButtons";
			panelActionButtons.Size = new Size(346, 95);
			panelActionButtons.TabIndex = 14;

			// 
			// btnAdd
			// 
			btnAdd.BackColor = Color.FromArgb(13, 110, 253);
			btnAdd.Cursor = Cursors.Hand;
			btnAdd.FlatAppearance.BorderSize = 0;
			btnAdd.FlatStyle = FlatStyle.Flat;
			btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			btnAdd.ForeColor = Color.White;
			btnAdd.Location = new Point(0, 5);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(165, 38);
			btnAdd.TabIndex = 0;
			btnAdd.Text = "➕ Thêm mới";
			btnAdd.UseVisualStyleBackColor = false;
			btnAdd.Click += btnAdd_Click;

			// 
			// btnUpdate
			// 
			btnUpdate.BackColor = Color.FromArgb(25, 135, 84);
			btnUpdate.Cursor = Cursors.Hand;
			btnUpdate.FlatAppearance.BorderSize = 0;
			btnUpdate.FlatStyle = FlatStyle.Flat;
			btnUpdate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			btnUpdate.ForeColor = Color.White;
			btnUpdate.Location = new Point(181, 5);
			btnUpdate.Name = "btnUpdate";
			btnUpdate.Size = new Size(165, 38);
			btnUpdate.TabIndex = 1;
			btnUpdate.Text = "✏️ Cập nhật";
			btnUpdate.UseVisualStyleBackColor = false;
			btnUpdate.Click += btnUpdate_Click;

			// 
			// btnDelete
			// 
			btnDelete.BackColor = Color.FromArgb(220, 53, 69);
			btnDelete.Cursor = Cursors.Hand;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			btnDelete.ForeColor = Color.White;
			btnDelete.Location = new Point(0, 50);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(165, 38);
			btnDelete.TabIndex = 2;
			btnDelete.Text = "🗑️ Xóa";
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += btnDelete_Click;

			// 
			// btnClear
			// 
			btnClear.BackColor = Color.FromArgb(108, 117, 125);
			btnClear.Cursor = Cursors.Hand;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = FlatStyle.Flat;
			btnClear.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			btnClear.ForeColor = Color.White;
			btnClear.Location = new Point(181, 50);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(165, 38);
			btnClear.TabIndex = 3;
			btnClear.Text = "🔄 Làm mới";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += btnClear_Click;

			// 
			// panelRightGrid
			// 
			panelRightGrid.BackColor = Color.Transparent;
			panelRightGrid.Controls.Add(grpCustomerList);
			panelRightGrid.Dock = DockStyle.Fill;
			panelRightGrid.Location = new Point(395, 15);
			panelRightGrid.Name = "panelRightGrid";
			panelRightGrid.Padding = new Padding(10, 0, 0, 0);
			panelRightGrid.Size = new Size(690, 575);
			panelRightGrid.TabIndex = 1;

			// 
			// grpCustomerList
			// 
			grpCustomerList.BackColor = Color.White;
			grpCustomerList.Controls.Add(dgvCustomers);
			grpCustomerList.Controls.Add(panelSearchHeader);
			grpCustomerList.Dock = DockStyle.Fill;
			grpCustomerList.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			grpCustomerList.ForeColor = Color.FromArgb(24, 30, 54);
			grpCustomerList.Location = new Point(10, 0);
			grpCustomerList.Name = "grpCustomerList";
			grpCustomerList.Padding = new Padding(12);
			grpCustomerList.Size = new Size(680, 575);
			grpCustomerList.TabIndex = 0;
			grpCustomerList.TabStop = false;
			grpCustomerList.Text = "  Danh sách khách hàng  ";

			// 
			// panelSearchHeader
			// 
			panelSearchHeader.Controls.Add(lblTotalCount);
			panelSearchHeader.Controls.Add(btnRefresh);
			panelSearchHeader.Controls.Add(btnSearch);
			panelSearchHeader.Controls.Add(txtSearch);
			panelSearchHeader.Controls.Add(lblSearch);
			panelSearchHeader.Dock = DockStyle.Top;
			panelSearchHeader.Location = new Point(12, 35);
			panelSearchHeader.Name = "panelSearchHeader";
			panelSearchHeader.Size = new Size(656, 50);
			panelSearchHeader.TabIndex = 0;

			// 
			// lblSearch
			// 
			lblSearch.AutoSize = true;
			lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblSearch.ForeColor = Color.FromArgb(73, 80, 87);
			lblSearch.Location = new Point(4, 14);
			lblSearch.Name = "lblSearch";
			lblSearch.Size = new Size(73, 20);
			lblSearch.TabIndex = 0;
			lblSearch.Text = "Tìm kiếm:";

			// 
			// txtSearch
			// 
			txtSearch.BorderStyle = BorderStyle.FixedSingle;
			txtSearch.Font = new Font("Segoe UI", 9.5F);
			txtSearch.Location = new Point(83, 10);
			txtSearch.Name = "txtSearch";
			txtSearch.PlaceholderText = "Nhập Họ tên, Số điện thoại hoặc CCCD/Hộ chiếu...";
			txtSearch.Size = new Size(290, 29);
			txtSearch.TabIndex = 1;
			txtSearch.KeyDown += txtSearch_KeyDown;

			// 
			// btnSearch
			// 
			btnSearch.BackColor = Color.FromArgb(13, 110, 253);
			btnSearch.Cursor = Cursors.Hand;
			btnSearch.FlatAppearance.BorderSize = 0;
			btnSearch.FlatStyle = FlatStyle.Flat;
			btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnSearch.ForeColor = Color.White;
			btnSearch.Location = new Point(380, 9);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(95, 31);
			btnSearch.TabIndex = 2;
			btnSearch.Text = "🔍 Tìm kiếm";
			btnSearch.UseVisualStyleBackColor = false;
			btnSearch.Click += btnSearch_Click;

			// 
			// btnRefresh
			// 
			btnRefresh.BackColor = Color.FromArgb(108, 117, 125);
			btnRefresh.Cursor = Cursors.Hand;
			btnRefresh.FlatAppearance.BorderSize = 0;
			btnRefresh.FlatStyle = FlatStyle.Flat;
			btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnRefresh.ForeColor = Color.White;
			btnRefresh.Location = new Point(482, 9);
			btnRefresh.Name = "btnRefresh";
			btnRefresh.Size = new Size(80, 31);
			btnRefresh.TabIndex = 3;
			btnRefresh.Text = "🔄 Tải lại";
			btnRefresh.UseVisualStyleBackColor = false;
			btnRefresh.Click += btnRefresh_Click;

			// 
			// lblTotalCount
			// 
			lblTotalCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lblTotalCount.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
			lblTotalCount.ForeColor = Color.FromArgb(108, 117, 125);
			lblTotalCount.Location = new Point(470, 14);
			lblTotalCount.Name = "lblTotalCount";
			lblTotalCount.Size = new Size(180, 23);
			lblTotalCount.TabIndex = 4;
			lblTotalCount.Text = "Tổng số: 0 khách hàng";
			lblTotalCount.TextAlign = ContentAlignment.MiddleRight;

			// 
			// dgvCustomers
			// 
			dgvCustomers.AllowUserToAddRows = false;
			dgvCustomers.AllowUserToDeleteRows = false;
			dgvCustomers.AllowUserToResizeRows = false;
			dgvCustomers.BackgroundColor = Color.White;
			dgvCustomers.BorderStyle = BorderStyle.None;
			dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvCustomers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			
			dgvHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvHeaderStyle.BackColor = Color.FromArgb(24, 30, 54);
			dgvHeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			dgvHeaderStyle.ForeColor = Color.White;
			dgvHeaderStyle.Padding = new Padding(5, 0, 0, 0);
			dgvHeaderStyle.SelectionBackColor = Color.FromArgb(24, 30, 54);
			dgvHeaderStyle.SelectionForeColor = Color.White;
			dgvCustomers.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
			dgvCustomers.ColumnHeadersHeight = 38;
			dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

			dgvDefaultStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvDefaultStyle.BackColor = Color.White;
			dgvDefaultStyle.Font = new Font("Segoe UI", 9F);
			dgvDefaultStyle.ForeColor = Color.FromArgb(33, 37, 41);
			dgvDefaultStyle.Padding = new Padding(5, 0, 0, 0);
			dgvDefaultStyle.SelectionBackColor = Color.FromArgb(217, 237, 247);
			dgvDefaultStyle.SelectionForeColor = Color.FromArgb(24, 30, 54);
			dgvCustomers.DefaultCellStyle = dgvDefaultStyle;

			dgvAltStyle.BackColor = Color.FromArgb(248, 249, 250);
			dgvAltStyle.ForeColor = Color.FromArgb(33, 37, 41);
			dgvAltStyle.SelectionBackColor = Color.FromArgb(217, 237, 247);
			dgvAltStyle.SelectionForeColor = Color.FromArgb(24, 30, 54);
			dgvCustomers.AlternatingRowsDefaultCellStyle = dgvAltStyle;

			dgvCustomers.Columns.AddRange(new DataGridViewColumn[] {
				colCustomerId,
				colFullName,
				colPhone,
				colIdentityNumber,
				colEmail,
				colDateOfBirth,
				colAddress,
				colCreatedAt
			});
			dgvCustomers.Dock = DockStyle.Fill;
			dgvCustomers.EnableHeadersVisualStyles = false;
			dgvCustomers.GridColor = Color.FromArgb(222, 226, 230);
			dgvCustomers.Location = new Point(12, 85);
			dgvCustomers.MultiSelect = false;
			dgvCustomers.Name = "dgvCustomers";
			dgvCustomers.ReadOnly = true;
			dgvCustomers.RowHeadersVisible = false;
			dgvCustomers.RowHeadersWidth = 51;
			dgvCustomers.RowTemplate.Height = 34;
			dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvCustomers.Size = new Size(656, 478);
			dgvCustomers.TabIndex = 1;
			dgvCustomers.CellClick += dgvCustomers_CellClick;

			// 
			// colCustomerId
			// 
			colCustomerId.DataPropertyName = "CustomerId";
			colCustomerId.HeaderText = "Mã KH";
			colCustomerId.MinimumWidth = 60;
			colCustomerId.Name = "colCustomerId";
			colCustomerId.ReadOnly = true;
			colCustomerId.Width = 70;

			// 
			// colFullName
			// 
			colFullName.DataPropertyName = "FullName";
			colFullName.HeaderText = "Họ và tên";
			colFullName.MinimumWidth = 140;
			colFullName.Name = "colFullName";
			colFullName.ReadOnly = true;
			colFullName.Width = 160;

			// 
			// colPhone
			// 
			colPhone.DataPropertyName = "Phone";
			colPhone.HeaderText = "Số điện thoại";
			colPhone.MinimumWidth = 110;
			colPhone.Name = "colPhone";
			colPhone.ReadOnly = true;
			colPhone.Width = 120;

			// 
			// colIdentityNumber
			// 
			colIdentityNumber.DataPropertyName = "IdentityNumber";
			colIdentityNumber.HeaderText = "CCCD/Hộ chiếu";
			colIdentityNumber.MinimumWidth = 120;
			colIdentityNumber.Name = "colIdentityNumber";
			colIdentityNumber.ReadOnly = true;
			colIdentityNumber.Width = 130;

			// 
			// colEmail
			// 
			colEmail.DataPropertyName = "Email";
			colEmail.HeaderText = "Email";
			colEmail.MinimumWidth = 130;
			colEmail.Name = "colEmail";
			colEmail.ReadOnly = true;
			colEmail.Width = 140;

			// 
			// colDateOfBirth
			// 
			colDateOfBirth.DataPropertyName = "DateOfBirth";
			colDateOfBirth.HeaderText = "Ngày sinh";
			colDateOfBirth.MinimumWidth = 90;
			colDateOfBirth.Name = "colDateOfBirth";
			colDateOfBirth.ReadOnly = true;
			colDateOfBirth.Width = 100;

			// 
			// colAddress
			// 
			colAddress.DataPropertyName = "Address";
			colAddress.HeaderText = "Địa chỉ";
			colAddress.MinimumWidth = 150;
			colAddress.Name = "colAddress";
			colAddress.ReadOnly = true;
			colAddress.Width = 180;

			// 
			// colCreatedAt
			// 
			colCreatedAt.DataPropertyName = "CreatedAt";
			colCreatedAt.HeaderText = "Ngày tạo";
			colCreatedAt.MinimumWidth = 120;
			colCreatedAt.Name = "colCreatedAt";
			colCreatedAt.ReadOnly = true;
			colCreatedAt.Width = 130;

			// 
			// CustomersControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(244, 246, 249);
			Controls.Add(panelMainContent);
			Controls.Add(panelTopHeader);
			Name = "CustomersControl";
			Size = new Size(1100, 680);
			Load += CustomersControl_Load;

			panelTopHeader.ResumeLayout(false);
			panelTopHeader.PerformLayout();
			panelMainContent.ResumeLayout(false);
			panelLeftForm.ResumeLayout(false);
			grpCustomerInfo.ResumeLayout(false);
			grpCustomerInfo.PerformLayout();
			panelActionButtons.ResumeLayout(false);
			panelRightGrid.ResumeLayout(false);
			grpCustomerList.ResumeLayout(false);
			panelSearchHeader.ResumeLayout(false);
			panelSearchHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelTopHeader;
		private Label lblHeader;
		private Label lblSub;

		private Panel panelMainContent;
		private Panel panelLeftForm;
		private GroupBox grpCustomerInfo;

		private Label lblCustomerId;
		private TextBox txtCustomerId;
		private Label lblFullName;
		private TextBox txtFullName;
		private Label lblPhone;
		private TextBox txtPhone;
		private Label lblIdentityNumber;
		private TextBox txtIdentityNumber;
		private Label lblEmail;
		private TextBox txtEmail;
		private Label lblDateOfBirth;
		private DateTimePicker dtpDateOfBirth;
		private Label lblAddress;
		private TextBox txtAddress;

		private Panel panelActionButtons;
		private Button btnAdd;
		private Button btnUpdate;
		private Button btnDelete;
		private Button btnClear;

		private Panel panelRightGrid;
		private GroupBox grpCustomerList;
		private Panel panelSearchHeader;
		private Label lblSearch;
		private TextBox txtSearch;
		private Button btnSearch;
		private Button btnRefresh;
		private Label lblTotalCount;

		private DataGridView dgvCustomers;
		private DataGridViewTextBoxColumn colCustomerId;
		private DataGridViewTextBoxColumn colFullName;
		private DataGridViewTextBoxColumn colPhone;
		private DataGridViewTextBoxColumn colIdentityNumber;
		private DataGridViewTextBoxColumn colEmail;
		private DataGridViewTextBoxColumn colDateOfBirth;
		private DataGridViewTextBoxColumn colAddress;
		private DataGridViewTextBoxColumn colCreatedAt;
	}
}
