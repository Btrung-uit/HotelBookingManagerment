using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingManagement.Controls
{
	partial class RoomsControl
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
			grpRoomInfo = new GroupBox();

			lblRoomId = new Label();
			txtRoomId = new TextBox();
			lblRoomNumber = new Label();
			txtRoomNumber = new TextBox();
			lblRoomType = new Label();
			cboRoomType = new ComboBox();
			lblFloor = new Label();
			numFloor = new NumericUpDown();
			lblStatus = new Label();
			cboStatus = new ComboBox();
			lblDescription = new Label();
			txtDescription = new TextBox();

			panelActionButtons = new Panel();
			btnAdd = new Button();
			btnUpdate = new Button();
			btnDelete = new Button();
			btnClear = new Button();
			btnManageRoomTypes = new Button();

			grpQuickStatus = new GroupBox();
			btnSetAvailable = new Button();
			btnSetMaintenance = new Button();
			btnFinishMaintenance = new Button();

			panelRightGrid = new Panel();
			grpRoomList = new GroupBox();
			panelSearchFilter = new Panel();
			txtSearch = new TextBox();
			btnSearch = new Button();
			btnResetFilter = new Button();
			cboFilterType = new ComboBox();
			cboFilterStatus = new ComboBox();
			cboFilterFloor = new ComboBox();

			panelStatusSummary = new FlowLayoutPanel();
			lblSummaryAll = new Label();
			lblSummaryAvailable = new Label();
			lblSummaryOccupied = new Label();
			lblSummaryReserved = new Label();
			lblSummaryCleaning = new Label();
			lblSummaryMaintenance = new Label();

			dgvRooms = new DataGridView();
			colSTT = new DataGridViewTextBoxColumn();
			colRoomId = new DataGridViewTextBoxColumn();
			colRoomNumber = new DataGridViewTextBoxColumn();
			colRoomType = new DataGridViewTextBoxColumn();
			colFloor = new DataGridViewTextBoxColumn();
			colPrice = new DataGridViewTextBoxColumn();
			colCapacity = new DataGridViewTextBoxColumn();
			colStatus = new DataGridViewTextBoxColumn();
			colDescription = new DataGridViewTextBoxColumn();

			panelTopHeader.SuspendLayout();
			panelMainContent.SuspendLayout();
			panelLeftForm.SuspendLayout();
			grpRoomInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numFloor).BeginInit();
			panelActionButtons.SuspendLayout();
			grpQuickStatus.SuspendLayout();
			panelRightGrid.SuspendLayout();
			grpRoomList.SuspendLayout();
			panelSearchFilter.SuspendLayout();
			panelStatusSummary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvRooms).BeginInit();
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
			panelTopHeader.Size = new Size(1180, 75);
			panelTopHeader.TabIndex = 0;

			//
			// lblHeader
			//
			lblHeader.AutoSize = true;
			lblHeader.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
			lblHeader.ForeColor = Color.FromArgb(24, 30, 54);
			lblHeader.Location = new Point(18, 10);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(470, 31);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "🏨 QUẢN LÝ PHÒNG & LOẠI PHÒNG (ROOMS)";

			//
			// lblSub
			//
			lblSub.AutoSize = true;
			lblSub.Font = new Font("Segoe UI", 9.5F);
			lblSub.ForeColor = Color.FromArgb(108, 117, 125);
			lblSub.Location = new Point(20, 43);
			lblSub.Name = "lblSub";
			lblSub.Size = new Size(680, 21);
			lblSub.TabIndex = 1;
			lblSub.Text = "Module TV02: Thêm mới, cập nhật, tra cứu, lọc và chuyển đổi trạng thái buồng phòng (ROOM01 - ROOM06)";

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
			panelMainContent.Size = new Size(1180, 685);
			panelMainContent.TabIndex = 1;

			//
			// panelLeftForm
			//
			panelLeftForm.BackColor = Color.Transparent;
			panelLeftForm.Controls.Add(grpRoomInfo);
			panelLeftForm.Dock = DockStyle.Left;
			panelLeftForm.Location = new Point(15, 15);
			panelLeftForm.Name = "panelLeftForm";
			panelLeftForm.Size = new Size(390, 655);
			panelLeftForm.TabIndex = 0;

			//
			// grpRoomInfo
			//
			grpRoomInfo.BackColor = Color.White;
			grpRoomInfo.Controls.Add(lblRoomId);
			grpRoomInfo.Controls.Add(txtRoomId);
			grpRoomInfo.Controls.Add(lblRoomNumber);
			grpRoomInfo.Controls.Add(txtRoomNumber);
			grpRoomInfo.Controls.Add(lblRoomType);
			grpRoomInfo.Controls.Add(cboRoomType);
			grpRoomInfo.Controls.Add(lblFloor);
			grpRoomInfo.Controls.Add(numFloor);
			grpRoomInfo.Controls.Add(lblStatus);
			grpRoomInfo.Controls.Add(cboStatus);
			grpRoomInfo.Controls.Add(lblDescription);
			grpRoomInfo.Controls.Add(txtDescription);
			grpRoomInfo.Controls.Add(panelActionButtons);
			grpRoomInfo.Controls.Add(grpQuickStatus);
			grpRoomInfo.Dock = DockStyle.Fill;
			grpRoomInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			grpRoomInfo.ForeColor = Color.FromArgb(24, 30, 54);
			grpRoomInfo.Location = new Point(0, 0);
			grpRoomInfo.Name = "grpRoomInfo";
			grpRoomInfo.Padding = new Padding(15, 10, 15, 10);
			grpRoomInfo.Size = new Size(390, 655);
			grpRoomInfo.TabIndex = 0;
			grpRoomInfo.TabStop = false;
			grpRoomInfo.Text = " Thông tin phòng ";

			//
			// lblRoomId
			//
			lblRoomId.AutoSize = true;
			lblRoomId.Font = new Font("Segoe UI", 9F);
			lblRoomId.ForeColor = Color.FromArgb(73, 80, 87);
			lblRoomId.Location = new Point(15, 26);
			lblRoomId.Name = "lblRoomId";
			lblRoomId.Size = new Size(80, 20);
			lblRoomId.TabIndex = 0;
			lblRoomId.Text = "Mã phòng:";

			//
			// txtRoomId
			//
			txtRoomId.BackColor = Color.FromArgb(233, 236, 239);
			txtRoomId.Font = new Font("Segoe UI", 9.5F);
			txtRoomId.ForeColor = Color.FromArgb(108, 117, 125);
			txtRoomId.Location = new Point(15, 47);
			txtRoomId.Name = "txtRoomId";
			txtRoomId.PlaceholderText = "(Tự động sinh)";
			txtRoomId.ReadOnly = true;
			txtRoomId.Size = new Size(360, 29);
			txtRoomId.TabIndex = 1;

			//
			// lblRoomNumber
			//
			lblRoomNumber.AutoSize = true;
			lblRoomNumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblRoomNumber.ForeColor = Color.FromArgb(73, 80, 87);
			lblRoomNumber.Location = new Point(15, 80);
			lblRoomNumber.Name = "lblRoomNumber";
			lblRoomNumber.Size = new Size(103, 20);
			lblRoomNumber.TabIndex = 2;
			lblRoomNumber.Text = "Số phòng: (*)";

			//
			// txtRoomNumber
			//
			txtRoomNumber.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			txtRoomNumber.Location = new Point(15, 101);
			txtRoomNumber.MaxLength = 10;
			txtRoomNumber.Name = "txtRoomNumber";
			txtRoomNumber.PlaceholderText = "Ví dụ: 101, 201, VIP01...";
			txtRoomNumber.Size = new Size(360, 29);
			txtRoomNumber.TabIndex = 3;

			//
			// lblRoomType
			//
			lblRoomType.AutoSize = true;
			lblRoomType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblRoomType.ForeColor = Color.FromArgb(73, 80, 87);
			lblRoomType.Location = new Point(15, 134);
			lblRoomType.Name = "lblRoomType";
			lblRoomType.Size = new Size(111, 20);
			lblRoomType.TabIndex = 4;
			lblRoomType.Text = "Loại phòng: (*)";

			//
			// cboRoomType
			//
			cboRoomType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboRoomType.Font = new Font("Segoe UI", 9.5F);
			cboRoomType.FormattingEnabled = true;
			cboRoomType.Location = new Point(15, 155);
			cboRoomType.Name = "cboRoomType";
			cboRoomType.Size = new Size(360, 29);
			cboRoomType.TabIndex = 5;

			//
			// lblFloor
			//
			lblFloor.AutoSize = true;
			lblFloor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblFloor.ForeColor = Color.FromArgb(73, 80, 87);
			lblFloor.Location = new Point(15, 190);
			lblFloor.Name = "lblFloor";
			lblFloor.Size = new Size(71, 20);
			lblFloor.TabIndex = 6;
			lblFloor.Text = "Tầng: (*)";

			//
			// numFloor
			//
			numFloor.Font = new Font("Segoe UI", 9.5F);
			numFloor.Location = new Point(15, 211);
			numFloor.Maximum = 50;
			numFloor.Minimum = 1;
			numFloor.Name = "numFloor";
			numFloor.Size = new Size(360, 29);
			numFloor.TabIndex = 7;
			numFloor.Value = 1;

			//
			// lblStatus
			//
			lblStatus.AutoSize = true;
			lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblStatus.ForeColor = Color.FromArgb(73, 80, 87);
			lblStatus.Location = new Point(15, 244);
			lblStatus.Name = "lblStatus";
			lblStatus.Size = new Size(147, 20);
			lblStatus.TabIndex = 8;
			lblStatus.Text = "Trạng thái phòng: (*)";

			//
			// cboStatus
			//
			cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
			cboStatus.Font = new Font("Segoe UI", 9.5F);
			cboStatus.FormattingEnabled = true;
			cboStatus.Location = new Point(15, 265);
			cboStatus.Name = "cboStatus";
			cboStatus.Size = new Size(360, 29);
			cboStatus.TabIndex = 9;

			//
			// lblDescription
			//
			lblDescription.AutoSize = true;
			lblDescription.Font = new Font("Segoe UI", 9F);
			lblDescription.ForeColor = Color.FromArgb(73, 80, 87);
			lblDescription.Location = new Point(15, 298);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new Size(100, 20);
			lblDescription.TabIndex = 10;
			lblDescription.Text = "Mô tả phòng:";

			//
			// txtDescription
			//
			txtDescription.Font = new Font("Segoe UI", 9.5F);
			txtDescription.Location = new Point(15, 319);
			txtDescription.MaxLength = 255;
			txtDescription.Multiline = true;
			txtDescription.Name = "txtDescription";
			txtDescription.PlaceholderText = "Ghi chú đặc điểm (view hồ bơi, yên tĩnh...)";
			txtDescription.ScrollBars = ScrollBars.Vertical;
			txtDescription.Size = new Size(360, 52);
			txtDescription.TabIndex = 11;

			//
			// panelActionButtons
			//
			panelActionButtons.Controls.Add(btnAdd);
			panelActionButtons.Controls.Add(btnUpdate);
			panelActionButtons.Controls.Add(btnDelete);
			panelActionButtons.Controls.Add(btnClear);
			panelActionButtons.Controls.Add(btnManageRoomTypes);
			panelActionButtons.Location = new Point(15, 380);
			panelActionButtons.Name = "panelActionButtons";
			panelActionButtons.Size = new Size(360, 125);
			panelActionButtons.TabIndex = 12;

			//
			// btnAdd
			//
			btnAdd.BackColor = Color.FromArgb(0, 120, 214);
			btnAdd.Cursor = Cursors.Hand;
			btnAdd.FlatAppearance.BorderSize = 0;
			btnAdd.FlatStyle = FlatStyle.Flat;
			btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnAdd.ForeColor = Color.White;
			btnAdd.Location = new Point(0, 5);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(115, 36);
			btnAdd.TabIndex = 0;
			btnAdd.Text = "➕ Thêm";
			btnAdd.UseVisualStyleBackColor = false;
			btnAdd.Click += btnAdd_Click;

			//
			// btnUpdate
			//
			btnUpdate.BackColor = Color.FromArgb(40, 167, 69);
			btnUpdate.Cursor = Cursors.Hand;
			btnUpdate.FlatAppearance.BorderSize = 0;
			btnUpdate.FlatStyle = FlatStyle.Flat;
			btnUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnUpdate.ForeColor = Color.White;
			btnUpdate.Location = new Point(122, 5);
			btnUpdate.Name = "btnUpdate";
			btnUpdate.Size = new Size(115, 36);
			btnUpdate.TabIndex = 1;
			btnUpdate.Text = "✏️ Sửa";
			btnUpdate.UseVisualStyleBackColor = false;
			btnUpdate.Click += btnUpdate_Click;

			//
			// btnDelete
			//
			btnDelete.BackColor = Color.FromArgb(220, 53, 69);
			btnDelete.Cursor = Cursors.Hand;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnDelete.ForeColor = Color.White;
			btnDelete.Location = new Point(245, 5);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(115, 36);
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
			btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnClear.ForeColor = Color.White;
			btnClear.Location = new Point(0, 48);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(115, 36);
			btnClear.TabIndex = 3;
			btnClear.Text = "🔄 Làm mới";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += btnClear_Click;

			//
			// btnManageRoomTypes
			//
			btnManageRoomTypes.BackColor = Color.FromArgb(111, 66, 193);
			btnManageRoomTypes.Cursor = Cursors.Hand;
			btnManageRoomTypes.FlatAppearance.BorderSize = 0;
			btnManageRoomTypes.FlatStyle = FlatStyle.Flat;
			btnManageRoomTypes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnManageRoomTypes.ForeColor = Color.White;
			btnManageRoomTypes.Location = new Point(122, 48);
			btnManageRoomTypes.Name = "btnManageRoomTypes";
			btnManageRoomTypes.Size = new Size(238, 36);
			btnManageRoomTypes.TabIndex = 4;
			btnManageRoomTypes.Text = "🏷️ Quản lý Loại phòng...";
			btnManageRoomTypes.UseVisualStyleBackColor = false;
			btnManageRoomTypes.Click += btnManageRoomTypes_Click;

			//
			// grpQuickStatus
			//
			grpQuickStatus.Controls.Add(btnSetAvailable);
			grpQuickStatus.Controls.Add(btnSetMaintenance);
			grpQuickStatus.Controls.Add(btnFinishMaintenance);
			grpQuickStatus.Dock = DockStyle.Bottom;
			grpQuickStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			grpQuickStatus.ForeColor = Color.FromArgb(13, 110, 253);
			grpQuickStatus.Location = new Point(15, 515);
			grpQuickStatus.Name = "grpQuickStatus";
			grpQuickStatus.Size = new Size(360, 130);
			grpQuickStatus.TabIndex = 13;
			grpQuickStatus.TabStop = false;
			grpQuickStatus.Text = "⚡ Chuyển trạng thái nhanh (ROOM03)";

			//
			// btnSetAvailable
			//
			btnSetAvailable.BackColor = Color.FromArgb(25, 135, 84);
			btnSetAvailable.Cursor = Cursors.Hand;
			btnSetAvailable.FlatAppearance.BorderSize = 0;
			btnSetAvailable.FlatStyle = FlatStyle.Flat;
			btnSetAvailable.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
			btnSetAvailable.ForeColor = Color.White;
			btnSetAvailable.Location = new Point(10, 26);
			btnSetAvailable.Name = "btnSetAvailable";
			btnSetAvailable.Size = new Size(340, 30);
			btnSetAvailable.TabIndex = 0;
			btnSetAvailable.Text = "🧹 Đã dọn xong ➔ SẴN SÀNG (AVAILABLE)";
			btnSetAvailable.UseVisualStyleBackColor = false;
			btnSetAvailable.Click += btnSetAvailable_Click;

			//
			// btnSetMaintenance
			//
			btnSetMaintenance.BackColor = Color.FromArgb(253, 126, 20);
			btnSetMaintenance.Cursor = Cursors.Hand;
			btnSetMaintenance.FlatAppearance.BorderSize = 0;
			btnSetMaintenance.FlatStyle = FlatStyle.Flat;
			btnSetMaintenance.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
			btnSetMaintenance.ForeColor = Color.White;
			btnSetMaintenance.Location = new Point(10, 60);
			btnSetMaintenance.Name = "btnSetMaintenance";
			btnSetMaintenance.Size = new Size(165, 30);
			btnSetMaintenance.TabIndex = 1;
			btnSetMaintenance.Text = "🔧 Báo bảo trì";
			btnSetMaintenance.UseVisualStyleBackColor = false;
			btnSetMaintenance.Click += btnSetMaintenance_Click;

			//
			// btnFinishMaintenance
			//
			btnFinishMaintenance.BackColor = Color.FromArgb(13, 110, 253);
			btnFinishMaintenance.Cursor = Cursors.Hand;
			btnFinishMaintenance.FlatAppearance.BorderSize = 0;
			btnFinishMaintenance.FlatStyle = FlatStyle.Flat;
			btnFinishMaintenance.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
			btnFinishMaintenance.ForeColor = Color.White;
			btnFinishMaintenance.Location = new Point(185, 60);
			btnFinishMaintenance.Name = "btnFinishMaintenance";
			btnFinishMaintenance.Size = new Size(165, 30);
			btnFinishMaintenance.TabIndex = 2;
			btnFinishMaintenance.Text = "✅ Xong bảo trì";
			btnFinishMaintenance.UseVisualStyleBackColor = false;
			btnFinishMaintenance.Click += btnFinishMaintenance_Click;

			//
			// panelRightGrid
			//
			panelRightGrid.BackColor = Color.Transparent;
			panelRightGrid.Controls.Add(grpRoomList);
			panelRightGrid.Dock = DockStyle.Fill;
			panelRightGrid.Location = new Point(405, 15);
			panelRightGrid.Name = "panelRightGrid";
			panelRightGrid.Size = new Size(760, 655);
			panelRightGrid.TabIndex = 1;

			//
			// grpRoomList
			//
			grpRoomList.BackColor = Color.White;
			grpRoomList.Controls.Add(dgvRooms);
			grpRoomList.Controls.Add(panelStatusSummary);
			grpRoomList.Controls.Add(panelSearchFilter);
			grpRoomList.Dock = DockStyle.Fill;
			grpRoomList.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			grpRoomList.ForeColor = Color.FromArgb(24, 30, 54);
			grpRoomList.Location = new Point(0, 0);
			grpRoomList.Name = "grpRoomList";
			grpRoomList.Padding = new Padding(12);
			grpRoomList.Size = new Size(760, 655);
			grpRoomList.TabIndex = 0;
			grpRoomList.TabStop = false;
			grpRoomList.Text = " Danh sách phòng khách sạn ";

			//
			// panelSearchFilter
			//
			panelSearchFilter.BackColor = Color.FromArgb(248, 249, 250);
			panelSearchFilter.BorderStyle = BorderStyle.FixedSingle;
			panelSearchFilter.Controls.Add(txtSearch);
			panelSearchFilter.Controls.Add(btnSearch);
			panelSearchFilter.Controls.Add(cboFilterType);
			panelSearchFilter.Controls.Add(cboFilterStatus);
			panelSearchFilter.Controls.Add(cboFilterFloor);
			panelSearchFilter.Controls.Add(btnResetFilter);
			panelSearchFilter.Dock = DockStyle.Top;
			panelSearchFilter.Location = new Point(12, 35);
			panelSearchFilter.Name = "panelSearchFilter";
			panelSearchFilter.Padding = new Padding(8);
			panelSearchFilter.Size = new Size(736, 85);
			panelSearchFilter.TabIndex = 0;

			//
			// txtSearch
			//
			txtSearch.Font = new Font("Segoe UI", 9.5F);
			txtSearch.Location = new Point(8, 8);
			txtSearch.Name = "txtSearch";
			txtSearch.PlaceholderText = "🔍 Nhập số phòng hoặc mô tả để tìm kiếm...";
			txtSearch.Size = new Size(320, 29);
			txtSearch.TabIndex = 0;
			txtSearch.KeyDown += txtSearch_KeyDown;

			//
			// btnSearch
			//
			btnSearch.BackColor = Color.FromArgb(0, 120, 214);
			btnSearch.Cursor = Cursors.Hand;
			btnSearch.FlatAppearance.BorderSize = 0;
			btnSearch.FlatStyle = FlatStyle.Flat;
			btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnSearch.ForeColor = Color.White;
			btnSearch.Location = new Point(335, 7);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(100, 31);
			btnSearch.TabIndex = 1;
			btnSearch.Text = "🔍 Tìm kiếm";
			btnSearch.UseVisualStyleBackColor = false;
			btnSearch.Click += btnSearch_Click;

			//
			// btnResetFilter
			//
			btnResetFilter.BackColor = Color.FromArgb(108, 117, 125);
			btnResetFilter.Cursor = Cursors.Hand;
			btnResetFilter.FlatAppearance.BorderSize = 0;
			btnResetFilter.FlatStyle = FlatStyle.Flat;
			btnResetFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnResetFilter.ForeColor = Color.White;
			btnResetFilter.Location = new Point(442, 7);
			btnResetFilter.Name = "btnResetFilter";
			btnResetFilter.Size = new Size(115, 31);
			btnResetFilter.TabIndex = 2;
			btnResetFilter.Text = "🔄 Đặt lại lọc";
			btnResetFilter.UseVisualStyleBackColor = false;
			btnResetFilter.Click += btnResetFilter_Click;

			//
			// cboFilterType
			//
			cboFilterType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboFilterType.Font = new Font("Segoe UI", 9F);
			cboFilterType.FormattingEnabled = true;
			cboFilterType.Location = new Point(8, 45);
			cboFilterType.Name = "cboFilterType";
			cboFilterType.Size = new Size(230, 28);
			cboFilterType.TabIndex = 3;
			cboFilterType.SelectedIndexChanged += Filter_SelectedIndexChanged;

			//
			// cboFilterStatus
			//
			cboFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
			cboFilterStatus.Font = new Font("Segoe UI", 9F);
			cboFilterStatus.FormattingEnabled = true;
			cboFilterStatus.Location = new Point(245, 45);
			cboFilterStatus.Name = "cboFilterStatus";
			cboFilterStatus.Size = new Size(240, 28);
			cboFilterStatus.TabIndex = 4;
			cboFilterStatus.SelectedIndexChanged += Filter_SelectedIndexChanged;

			//
			// cboFilterFloor
			//
			cboFilterFloor.DropDownStyle = ComboBoxStyle.DropDownList;
			cboFilterFloor.Font = new Font("Segoe UI", 9F);
			cboFilterFloor.FormattingEnabled = true;
			cboFilterFloor.Location = new Point(492, 45);
			cboFilterFloor.Name = "cboFilterFloor";
			cboFilterFloor.Size = new Size(170, 28);
			cboFilterFloor.TabIndex = 5;
			cboFilterFloor.SelectedIndexChanged += Filter_SelectedIndexChanged;

			//
			// panelStatusSummary
			//
			panelStatusSummary.BackColor = Color.FromArgb(244, 246, 249);
			panelStatusSummary.Controls.Add(lblSummaryAll);
			panelStatusSummary.Controls.Add(lblSummaryAvailable);
			panelStatusSummary.Controls.Add(lblSummaryOccupied);
			panelStatusSummary.Controls.Add(lblSummaryReserved);
			panelStatusSummary.Controls.Add(lblSummaryCleaning);
			panelStatusSummary.Controls.Add(lblSummaryMaintenance);
			panelStatusSummary.Dock = DockStyle.Bottom;
			panelStatusSummary.Location = new Point(12, 608);
			panelStatusSummary.Name = "panelStatusSummary";
			panelStatusSummary.Padding = new Padding(8, 5, 8, 5);
			panelStatusSummary.Size = new Size(736, 35);
			panelStatusSummary.TabIndex = 2;

			//
			// lblSummaryAll
			//
			lblSummaryAll.AutoSize = true;
			lblSummaryAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblSummaryAll.ForeColor = Color.FromArgb(33, 37, 41);
			lblSummaryAll.Location = new Point(11, 5);
			lblSummaryAll.Name = "lblSummaryAll";
			lblSummaryAll.Padding = new Padding(0, 0, 15, 0);
			lblSummaryAll.Size = new Size(97, 20);
			lblSummaryAll.TabIndex = 0;
			lblSummaryAll.Text = "📊 Tổng: 0";

			//
			// lblSummaryAvailable
			//
			lblSummaryAvailable.AutoSize = true;
			lblSummaryAvailable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblSummaryAvailable.ForeColor = Color.FromArgb(25, 135, 84);
			lblSummaryAvailable.Location = new Point(114, 5);
			lblSummaryAvailable.Name = "lblSummaryAvailable";
			lblSummaryAvailable.Padding = new Padding(0, 0, 15, 0);
			lblSummaryAvailable.Size = new Size(119, 20);
			lblSummaryAvailable.TabIndex = 1;
			lblSummaryAvailable.Text = "🟢 Trống: 0";

			//
			// lblSummaryOccupied
			//
			lblSummaryOccupied.AutoSize = true;
			lblSummaryOccupied.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblSummaryOccupied.ForeColor = Color.FromArgb(220, 53, 69);
			lblSummaryOccupied.Location = new Point(239, 5);
			lblSummaryOccupied.Name = "lblSummaryOccupied";
			lblSummaryOccupied.Padding = new Padding(0, 0, 15, 0);
			lblSummaryOccupied.Size = new Size(137, 20);
			lblSummaryOccupied.TabIndex = 2;
			lblSummaryOccupied.Text = "🔴 Đang ở: 0";

			//
			// lblSummaryReserved
			//
			lblSummaryReserved.AutoSize = true;
			lblSummaryReserved.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblSummaryReserved.ForeColor = Color.FromArgb(13, 110, 253);
			lblSummaryReserved.Location = new Point(382, 5);
			lblSummaryReserved.Name = "lblSummaryReserved";
			lblSummaryReserved.Padding = new Padding(0, 0, 15, 0);
			lblSummaryReserved.Size = new Size(130, 20);
			lblSummaryReserved.TabIndex = 3;
			lblSummaryReserved.Text = "🔵 Đã đặt: 0";

			//
			// lblSummaryCleaning
			//
			lblSummaryCleaning.AutoSize = true;
			lblSummaryCleaning.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblSummaryCleaning.ForeColor = Color.FromArgb(217, 119, 6);
			lblSummaryCleaning.Location = new Point(518, 5);
			lblSummaryCleaning.Name = "lblSummaryCleaning";
			lblSummaryCleaning.Padding = new Padding(0, 0, 15, 0);
			lblSummaryCleaning.Size = new Size(128, 20);
			lblSummaryCleaning.TabIndex = 4;
			lblSummaryCleaning.Text = "🟡 Dọn dẹp: 0";

			//
			// lblSummaryMaintenance
			//
			lblSummaryMaintenance.AutoSize = true;
			lblSummaryMaintenance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblSummaryMaintenance.ForeColor = Color.FromArgb(108, 117, 125);
			lblSummaryMaintenance.Location = new Point(652, 5);
			lblSummaryMaintenance.Name = "lblSummaryMaintenance";
			lblSummaryMaintenance.Size = new Size(99, 20);
			lblSummaryMaintenance.TabIndex = 5;
			lblSummaryMaintenance.Text = "⚪ Bảo trì: 0";

			//
			// dgvRooms
			//
			dgvRooms.AllowUserToAddRows = false;
			dgvRooms.AllowUserToDeleteRows = false;
			dgvRooms.AllowUserToResizeRows = false;
			dgvRooms.BackgroundColor = Color.White;
			dgvRooms.BorderStyle = BorderStyle.None;
			dgvRooms.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvRooms.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

			dgvHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvHeaderStyle.BackColor = Color.FromArgb(24, 30, 54);
			dgvHeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			dgvHeaderStyle.ForeColor = Color.White;
			dgvHeaderStyle.Padding = new Padding(6, 8, 6, 8);
			dgvHeaderStyle.WrapMode = DataGridViewTriState.True;
			dgvRooms.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
			dgvRooms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

			dgvDefaultStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvDefaultStyle.BackColor = Color.White;
			dgvDefaultStyle.Font = new Font("Segoe UI", 9.5F);
			dgvDefaultStyle.ForeColor = Color.FromArgb(33, 37, 41);
			dgvDefaultStyle.Padding = new Padding(4);
			dgvDefaultStyle.SelectionBackColor = Color.FromArgb(209, 231, 221);
			dgvDefaultStyle.SelectionForeColor = Color.FromArgb(15, 81, 50);
			dgvDefaultStyle.WrapMode = DataGridViewTriState.False;
			dgvRooms.DefaultCellStyle = dgvDefaultStyle;

			dgvAltStyle.BackColor = Color.FromArgb(248, 249, 250);
			dgvRooms.AlternatingRowsDefaultCellStyle = dgvAltStyle;

			dgvRooms.Columns.AddRange(new DataGridViewColumn[] {
				colSTT,
				colRoomId,
				colRoomNumber,
				colRoomType,
				colFloor,
				colPrice,
				colCapacity,
				colStatus,
				colDescription
			});

			dgvRooms.Dock = DockStyle.Fill;
			dgvRooms.EnableHeadersVisualStyles = false;
			dgvRooms.GridColor = Color.FromArgb(222, 226, 230);
			dgvRooms.Location = new Point(12, 120);
			dgvRooms.MultiSelect = false;
			dgvRooms.Name = "dgvRooms";
			dgvRooms.ReadOnly = true;
			dgvRooms.RowHeadersVisible = false;
			dgvRooms.RowHeadersWidth = 51;
			dgvRooms.RowTemplate.Height = 35;
			dgvRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvRooms.Size = new Size(736, 488);
			dgvRooms.TabIndex = 1;
			dgvRooms.CellClick += dgvRooms_CellClick;
			dgvRooms.CellFormatting += dgvRooms_CellFormatting;

			//
			// colSTT
			//
			colSTT.HeaderText = "STT";
			colSTT.MinimumWidth = 45;
			colSTT.Name = "colSTT";
			colSTT.ReadOnly = true;
			colSTT.Width = 50;

			//
			// colRoomId
			//
			colRoomId.DataPropertyName = "RoomId";
			colRoomId.HeaderText = "ID";
			colRoomId.MinimumWidth = 40;
			colRoomId.Name = "colRoomId";
			colRoomId.ReadOnly = true;
			colRoomId.Width = 50;

			//
			// colRoomNumber
			//
			colRoomNumber.DataPropertyName = "RoomNumber";
			colRoomNumber.HeaderText = "Số phòng";
			colRoomNumber.MinimumWidth = 85;
			colRoomNumber.Name = "colRoomNumber";
			colRoomNumber.ReadOnly = true;
			colRoomNumber.Width = 95;

			//
			// colRoomType
			//
			colRoomType.HeaderText = "Loại phòng";
			colRoomType.MinimumWidth = 140;
			colRoomType.Name = "colRoomType";
			colRoomType.ReadOnly = true;
			colRoomType.Width = 150;

			//
			// colFloor
			//
			colFloor.DataPropertyName = "Floor";
			colFloor.HeaderText = "Tầng";
			colFloor.MinimumWidth = 60;
			colFloor.Name = "colFloor";
			colFloor.ReadOnly = true;
			colFloor.Width = 70;

			//
			// colPrice
			//
			colPrice.HeaderText = "Đơn giá/đêm";
			colPrice.MinimumWidth = 110;
			colPrice.Name = "colPrice";
			colPrice.ReadOnly = true;
			colPrice.Width = 120;

			//
			// colCapacity
			//
			colCapacity.HeaderText = "Sức chứa";
			colCapacity.MinimumWidth = 80;
			colCapacity.Name = "colCapacity";
			colCapacity.ReadOnly = true;
			colCapacity.Width = 85;

			//
			// colStatus
			//
			colStatus.DataPropertyName = "Status";
			colStatus.HeaderText = "Trạng thái phòng";
			colStatus.MinimumWidth = 150;
			colStatus.Name = "colStatus";
			colStatus.ReadOnly = true;
			colStatus.Width = 160;

			//
			// colDescription
			//
			colDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			colDescription.DataPropertyName = "Description";
			colDescription.HeaderText = "Mô tả / Tiện ích";
			colDescription.MinimumWidth = 120;
			colDescription.Name = "colDescription";
			colDescription.ReadOnly = true;

			//
			// RoomsControl
			//
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(244, 246, 249);
			Controls.Add(panelMainContent);
			Controls.Add(panelTopHeader);
			Font = new Font("Segoe UI", 9F);
			Name = "RoomsControl";
			Size = new Size(1180, 760);
			Load += RoomsControl_Load;

			panelTopHeader.ResumeLayout(false);
			panelTopHeader.PerformLayout();
			panelMainContent.ResumeLayout(false);
			panelLeftForm.ResumeLayout(false);
			grpRoomInfo.ResumeLayout(false);
			grpRoomInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numFloor).EndInit();
			panelActionButtons.ResumeLayout(false);
			grpQuickStatus.ResumeLayout(false);
			panelRightGrid.ResumeLayout(false);
			grpRoomList.ResumeLayout(false);
			panelSearchFilter.ResumeLayout(false);
			panelSearchFilter.PerformLayout();
			panelStatusSummary.ResumeLayout(false);
			panelStatusSummary.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvRooms).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelTopHeader;
		private Label lblHeader;
		private Label lblSub;
		private Panel panelMainContent;
		private Panel panelLeftForm;
		private GroupBox grpRoomInfo;
		private Label lblRoomId;
		private TextBox txtRoomId;
		private Label lblRoomNumber;
		private TextBox txtRoomNumber;
		private Label lblRoomType;
		private ComboBox cboRoomType;
		private Label lblFloor;
		private NumericUpDown numFloor;
		private Label lblStatus;
		private ComboBox cboStatus;
		private Label lblDescription;
		private TextBox txtDescription;
		private Panel panelActionButtons;
		private Button btnAdd;
		private Button btnUpdate;
		private Button btnDelete;
		private Button btnClear;
		private Button btnManageRoomTypes;
		private GroupBox grpQuickStatus;
		private Button btnSetAvailable;
		private Button btnSetMaintenance;
		private Button btnFinishMaintenance;
		private Panel panelRightGrid;
		private GroupBox grpRoomList;
		private Panel panelSearchFilter;
		private TextBox txtSearch;
		private Button btnSearch;
		private Button btnResetFilter;
		private ComboBox cboFilterType;
		private ComboBox cboFilterStatus;
		private ComboBox cboFilterFloor;
		private FlowLayoutPanel panelStatusSummary;
		private Label lblSummaryAll;
		private Label lblSummaryAvailable;
		private Label lblSummaryOccupied;
		private Label lblSummaryReserved;
		private Label lblSummaryCleaning;
		private Label lblSummaryMaintenance;
		private DataGridView dgvRooms;
		private DataGridViewTextBoxColumn colSTT;
		private DataGridViewTextBoxColumn colRoomId;
		private DataGridViewTextBoxColumn colRoomNumber;
		private DataGridViewTextBoxColumn colRoomType;
		private DataGridViewTextBoxColumn colFloor;
		private DataGridViewTextBoxColumn colPrice;
		private DataGridViewTextBoxColumn colCapacity;
		private DataGridViewTextBoxColumn colStatus;
		private DataGridViewTextBoxColumn colDescription;
	}
}
