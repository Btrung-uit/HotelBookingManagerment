using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingManagement.Forms
{
	partial class FrmRoomType
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

			panelTopHeader = new Panel();
			lblHeader = new Label();
			lblSub = new Label();

			panelMainContent = new Panel();
			panelLeftForm = new Panel();
			grpRoomTypeInfo = new GroupBox();

			lblRoomTypeId = new Label();
			txtRoomTypeId = new TextBox();
			lblName = new Label();
			txtName = new TextBox();
			lblCapacity = new Label();
			numCapacity = new NumericUpDown();
			lblPricePerNight = new Label();
			numPricePerNight = new NumericUpDown();
			lblDescription = new Label();
			txtDescription = new TextBox();

			panelActionButtons = new Panel();
			btnAdd = new Button();
			btnUpdate = new Button();
			btnDelete = new Button();
			btnClear = new Button();
			btnClose = new Button();

			panelRightGrid = new Panel();
			grpRoomTypeList = new GroupBox();
			lblTotalCount = new Label();

			dgvRoomTypes = new DataGridView();
			colSTT = new DataGridViewTextBoxColumn();
			colRoomTypeId = new DataGridViewTextBoxColumn();
			colName = new DataGridViewTextBoxColumn();
			colCapacity = new DataGridViewTextBoxColumn();
			colPricePerNight = new DataGridViewTextBoxColumn();
			colDescription = new DataGridViewTextBoxColumn();

			panelTopHeader.SuspendLayout();
			panelMainContent.SuspendLayout();
			panelLeftForm.SuspendLayout();
			grpRoomTypeInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numCapacity).BeginInit();
			((System.ComponentModel.ISupportInitialize)numPricePerNight).BeginInit();
			panelActionButtons.SuspendLayout();
			panelRightGrid.SuspendLayout();
			grpRoomTypeList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvRoomTypes).BeginInit();
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
			panelTopHeader.Size = new Size(950, 75);
			panelTopHeader.TabIndex = 0;

			//
			// lblHeader
			//
			lblHeader.AutoSize = true;
			lblHeader.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
			lblHeader.ForeColor = Color.FromArgb(24, 30, 54);
			lblHeader.Location = new Point(18, 10);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(420, 31);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "🏷️ QUẢN LÝ LOẠI PHÒNG (ROOM TYPES)";

			//
			// lblSub
			//
			lblSub.AutoSize = true;
			lblSub.Font = new Font("Segoe UI", 9.5F);
			lblSub.ForeColor = Color.FromArgb(108, 117, 125);
			lblSub.Location = new Point(20, 43);
			lblSub.Name = "lblSub";
			lblSub.Size = new Size(580, 21);
			lblSub.TabIndex = 1;
			lblSub.Text = "Module TV02 - Task ROOM01: Thêm, sửa, xóa danh mục loại phòng, đơn giá và sức chứa";

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
			panelMainContent.Size = new Size(950, 530);
			panelMainContent.TabIndex = 1;

			//
			// panelLeftForm
			//
			panelLeftForm.BackColor = Color.Transparent;
			panelLeftForm.Controls.Add(grpRoomTypeInfo);
			panelLeftForm.Dock = DockStyle.Left;
			panelLeftForm.Location = new Point(15, 15);
			panelLeftForm.Name = "panelLeftForm";
			panelLeftForm.Size = new Size(350, 500);
			panelLeftForm.TabIndex = 0;

			//
			// grpRoomTypeInfo
			//
			grpRoomTypeInfo.BackColor = Color.White;
			grpRoomTypeInfo.Controls.Add(lblRoomTypeId);
			grpRoomTypeInfo.Controls.Add(txtRoomTypeId);
			grpRoomTypeInfo.Controls.Add(lblName);
			grpRoomTypeInfo.Controls.Add(txtName);
			grpRoomTypeInfo.Controls.Add(lblCapacity);
			grpRoomTypeInfo.Controls.Add(numCapacity);
			grpRoomTypeInfo.Controls.Add(lblPricePerNight);
			grpRoomTypeInfo.Controls.Add(numPricePerNight);
			grpRoomTypeInfo.Controls.Add(lblDescription);
			grpRoomTypeInfo.Controls.Add(txtDescription);
			grpRoomTypeInfo.Controls.Add(panelActionButtons);
			grpRoomTypeInfo.Dock = DockStyle.Fill;
			grpRoomTypeInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			grpRoomTypeInfo.ForeColor = Color.FromArgb(24, 30, 54);
			grpRoomTypeInfo.Location = new Point(0, 0);
			grpRoomTypeInfo.Name = "grpRoomTypeInfo";
			grpRoomTypeInfo.Padding = new Padding(15, 10, 15, 10);
			grpRoomTypeInfo.Size = new Size(350, 500);
			grpRoomTypeInfo.TabIndex = 0;
			grpRoomTypeInfo.TabStop = false;
			grpRoomTypeInfo.Text = " Thông tin loại phòng ";

			//
			// lblRoomTypeId
			//
			lblRoomTypeId.AutoSize = true;
			lblRoomTypeId.Font = new Font("Segoe UI", 9F);
			lblRoomTypeId.ForeColor = Color.FromArgb(73, 80, 87);
			lblRoomTypeId.Location = new Point(15, 30);
			lblRoomTypeId.Name = "lblRoomTypeId";
			lblRoomTypeId.Size = new Size(106, 20);
			lblRoomTypeId.TabIndex = 0;
			lblRoomTypeId.Text = "Mã loại phòng:";

			//
			// txtRoomTypeId
			//
			txtRoomTypeId.BackColor = Color.FromArgb(233, 236, 239);
			txtRoomTypeId.Font = new Font("Segoe UI", 9.5F);
			txtRoomTypeId.ForeColor = Color.FromArgb(108, 117, 125);
			txtRoomTypeId.Location = new Point(15, 53);
			txtRoomTypeId.Name = "txtRoomTypeId";
			txtRoomTypeId.PlaceholderText = "(Tự động sinh)";
			txtRoomTypeId.ReadOnly = true;
			txtRoomTypeId.Size = new Size(320, 29);
			txtRoomTypeId.TabIndex = 1;

			//
			// lblName
			//
			lblName.AutoSize = true;
			lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblName.ForeColor = Color.FromArgb(73, 80, 87);
			lblName.Location = new Point(15, 90);
			lblName.Name = "lblName";
			lblName.Size = new Size(141, 20);
			lblName.TabIndex = 2;
			lblName.Text = "Tên loại phòng: (*)";

			//
			// txtName
			//
			txtName.Font = new Font("Segoe UI", 9.5F);
			txtName.Location = new Point(15, 113);
			txtName.MaxLength = 50;
			txtName.Name = "txtName";
			txtName.PlaceholderText = "Ví dụ: Standard Single, Suite VIP...";
			txtName.Size = new Size(320, 29);
			txtName.TabIndex = 3;

			//
			// lblCapacity
			//
			lblCapacity.AutoSize = true;
			lblCapacity.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblCapacity.ForeColor = Color.FromArgb(73, 80, 87);
			lblCapacity.Location = new Point(15, 150);
			lblCapacity.Name = "lblCapacity";
			lblCapacity.Size = new Size(160, 20);
			lblCapacity.TabIndex = 4;
			lblCapacity.Text = "Sức chứa tối đa: (*)";

			//
			// numCapacity
			//
			numCapacity.Font = new Font("Segoe UI", 9.5F);
			numCapacity.Location = new Point(15, 173);
			numCapacity.Minimum = 1;
			numCapacity.Maximum = 20;
			numCapacity.Value = 2;
			numCapacity.Name = "numCapacity";
			numCapacity.Size = new Size(320, 29);
			numCapacity.TabIndex = 5;

			//
			// lblPricePerNight
			//
			lblPricePerNight.AutoSize = true;
			lblPricePerNight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblPricePerNight.ForeColor = Color.FromArgb(73, 80, 87);
			lblPricePerNight.Location = new Point(15, 210);
			lblPricePerNight.Name = "lblPricePerNight";
			lblPricePerNight.Size = new Size(181, 20);
			lblPricePerNight.TabIndex = 6;
			lblPricePerNight.Text = "Đơn giá mỗi đêm (VNĐ): (*)";

			//
			// numPricePerNight
			//
			numPricePerNight.Font = new Font("Segoe UI", 9.5F);
			numPricePerNight.Increment = 50000;
			numPricePerNight.Location = new Point(15, 233);
			numPricePerNight.Maximum = 100000000;
			numPricePerNight.Minimum = 0;
			numPricePerNight.ThousandsSeparator = true;
			numPricePerNight.Value = 500000;
			numPricePerNight.Name = "numPricePerNight";
			numPricePerNight.Size = new Size(320, 29);
			numPricePerNight.TabIndex = 7;

			//
			// lblDescription
			//
			lblDescription.AutoSize = true;
			lblDescription.Font = new Font("Segoe UI", 9F);
			lblDescription.ForeColor = Color.FromArgb(73, 80, 87);
			lblDescription.Location = new Point(15, 270);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new Size(125, 20);
			lblDescription.TabIndex = 8;
			lblDescription.Text = "Mô tả & Tiện nghi:";

			//
			// txtDescription
			//
			txtDescription.Font = new Font("Segoe UI", 9.5F);
			txtDescription.Location = new Point(15, 293);
			txtDescription.MaxLength = 255;
			txtDescription.Multiline = true;
			txtDescription.Name = "txtDescription";
			txtDescription.PlaceholderText = "Mô tả tiện ích phòng (giường, wifi, view...)";
			txtDescription.ScrollBars = ScrollBars.Vertical;
			txtDescription.Size = new Size(320, 75);
			txtDescription.TabIndex = 9;

			//
			// panelActionButtons
			//
			panelActionButtons.Controls.Add(btnAdd);
			panelActionButtons.Controls.Add(btnUpdate);
			panelActionButtons.Controls.Add(btnDelete);
			panelActionButtons.Controls.Add(btnClear);
			panelActionButtons.Controls.Add(btnClose);
			panelActionButtons.Dock = DockStyle.Bottom;
			panelActionButtons.Location = new Point(15, 380);
			panelActionButtons.Name = "panelActionButtons";
			panelActionButtons.Size = new Size(320, 110);
			panelActionButtons.TabIndex = 10;

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
			btnAdd.Size = new Size(100, 36);
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
			btnUpdate.Location = new Point(110, 5);
			btnUpdate.Name = "btnUpdate";
			btnUpdate.Size = new Size(100, 36);
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
			btnDelete.Location = new Point(220, 5);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(100, 36);
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
			btnClear.Location = new Point(0, 50);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(155, 36);
			btnClear.TabIndex = 3;
			btnClear.Text = "🔄 Làm mới";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += btnClear_Click;

			//
			// btnClose
			//
			btnClose.BackColor = Color.FromArgb(52, 58, 64);
			btnClose.Cursor = Cursors.Hand;
			btnClose.FlatAppearance.BorderSize = 0;
			btnClose.FlatStyle = FlatStyle.Flat;
			btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			btnClose.ForeColor = Color.White;
			btnClose.Location = new Point(165, 50);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(155, 36);
			btnClose.TabIndex = 4;
			btnClose.Text = "✖️ Đóng";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += btnClose_Click;

			//
			// panelRightGrid
			//
			panelRightGrid.BackColor = Color.Transparent;
			panelRightGrid.Controls.Add(grpRoomTypeList);
			panelRightGrid.Dock = DockStyle.Fill;
			panelRightGrid.Location = new Point(365, 15);
			panelRightGrid.Name = "panelRightGrid";
			panelRightGrid.Size = new Size(570, 500);
			panelRightGrid.TabIndex = 1;

			//
			// grpRoomTypeList
			//
			grpRoomTypeList.BackColor = Color.White;
			grpRoomTypeList.Controls.Add(dgvRoomTypes);
			grpRoomTypeList.Controls.Add(lblTotalCount);
			grpRoomTypeList.Dock = DockStyle.Fill;
			grpRoomTypeList.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			grpRoomTypeList.ForeColor = Color.FromArgb(24, 30, 54);
			grpRoomTypeList.Location = new Point(0, 0);
			grpRoomTypeList.Name = "grpRoomTypeList";
			grpRoomTypeList.Padding = new Padding(12);
			grpRoomTypeList.Size = new Size(570, 500);
			grpRoomTypeList.TabIndex = 0;
			grpRoomTypeList.TabStop = false;
			grpRoomTypeList.Text = " Danh mục loại phòng hiện có ";

			//
			// lblTotalCount
			//
			lblTotalCount.Dock = DockStyle.Bottom;
			lblTotalCount.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
			lblTotalCount.ForeColor = Color.FromArgb(108, 117, 125);
			lblTotalCount.Location = new Point(12, 463);
			lblTotalCount.Name = "lblTotalCount";
			lblTotalCount.Padding = new Padding(5, 5, 0, 0);
			lblTotalCount.Size = new Size(546, 25);
			lblTotalCount.TabIndex = 1;
			lblTotalCount.Text = "Tổng số: 0 loại phòng";

			//
			// dgvRoomTypes
			//
			dgvRoomTypes.AllowUserToAddRows = false;
			dgvRoomTypes.AllowUserToDeleteRows = false;
			dgvRoomTypes.AllowUserToResizeRows = false;
			dgvRoomTypes.BackgroundColor = Color.White;
			dgvRoomTypes.BorderStyle = BorderStyle.None;
			dgvRoomTypes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvRoomTypes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

			dgvHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvHeaderStyle.BackColor = Color.FromArgb(24, 30, 54);
			dgvHeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			dgvHeaderStyle.ForeColor = Color.White;
			dgvHeaderStyle.Padding = new Padding(6, 8, 6, 8);
			dgvHeaderStyle.WrapMode = DataGridViewTriState.True;
			dgvRoomTypes.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
			dgvRoomTypes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

			dgvDefaultStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvDefaultStyle.BackColor = Color.White;
			dgvDefaultStyle.Font = new Font("Segoe UI", 9.5F);
			dgvDefaultStyle.ForeColor = Color.FromArgb(33, 37, 41);
			dgvDefaultStyle.Padding = new Padding(4);
			dgvDefaultStyle.SelectionBackColor = Color.FromArgb(209, 231, 221);
			dgvDefaultStyle.SelectionForeColor = Color.FromArgb(15, 81, 50);
			dgvDefaultStyle.WrapMode = DataGridViewTriState.False;
			dgvRoomTypes.DefaultCellStyle = dgvDefaultStyle;

			dgvAltStyle.BackColor = Color.FromArgb(248, 249, 250);
			dgvRoomTypes.AlternatingRowsDefaultCellStyle = dgvAltStyle;

			dgvRoomTypes.Columns.AddRange(new DataGridViewColumn[] {
				colSTT,
				colRoomTypeId,
				colName,
				colCapacity,
				colPricePerNight,
				colDescription
			});

			dgvRoomTypes.Dock = DockStyle.Fill;
			dgvRoomTypes.EnableHeadersVisualStyles = false;
			dgvRoomTypes.GridColor = Color.FromArgb(222, 226, 230);
			dgvRoomTypes.Location = new Point(12, 35);
			dgvRoomTypes.MultiSelect = false;
			dgvRoomTypes.Name = "dgvRoomTypes";
			dgvRoomTypes.ReadOnly = true;
			dgvRoomTypes.RowHeadersVisible = false;
			dgvRoomTypes.RowHeadersWidth = 51;
			dgvRoomTypes.RowTemplate.Height = 35;
			dgvRoomTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvRoomTypes.Size = new Size(546, 428);
			dgvRoomTypes.TabIndex = 0;
			dgvRoomTypes.CellClick += dgvRoomTypes_CellClick;
			dgvRoomTypes.CellFormatting += dgvRoomTypes_CellFormatting;

			//
			// colSTT
			//
			colSTT.HeaderText = "STT";
			colSTT.MinimumWidth = 45;
			colSTT.Name = "colSTT";
			colSTT.ReadOnly = true;
			colSTT.Width = 50;

			//
			// colRoomTypeId
			//
			colRoomTypeId.DataPropertyName = "RoomTypeId";
			colRoomTypeId.HeaderText = "Mã";
			colRoomTypeId.MinimumWidth = 40;
			colRoomTypeId.Name = "colRoomTypeId";
			colRoomTypeId.ReadOnly = true;
			colRoomTypeId.Width = 50;

			//
			// colName
			//
			colName.DataPropertyName = "Name";
			colName.HeaderText = "Tên loại phòng";
			colName.MinimumWidth = 140;
			colName.Name = "colName";
			colName.ReadOnly = true;
			colName.Width = 150;

			//
			// colCapacity
			//
			colCapacity.DataPropertyName = "Capacity";
			colCapacity.HeaderText = "Sức chứa";
			colCapacity.MinimumWidth = 80;
			colCapacity.Name = "colCapacity";
			colCapacity.ReadOnly = true;
			colCapacity.Width = 90;

			//
			// colPricePerNight
			//
			colPricePerNight.DataPropertyName = "PricePerNight";
			colPricePerNight.HeaderText = "Đơn giá/đêm";
			colPricePerNight.MinimumWidth = 110;
			colPricePerNight.Name = "colPricePerNight";
			colPricePerNight.ReadOnly = true;
			colPricePerNight.Width = 120;

			//
			// colDescription
			//
			colDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			colDescription.DataPropertyName = "Description";
			colDescription.HeaderText = "Mô tả tiện ích";
			colDescription.MinimumWidth = 120;
			colDescription.Name = "colDescription";
			colDescription.ReadOnly = true;

			//
			// FrmRoomType
			//
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(950, 605);
			Controls.Add(panelMainContent);
			Controls.Add(panelTopHeader);
			Font = new Font("Segoe UI", 9F);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FrmRoomType";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Quản lý Danh mục Loại phòng - Module TV02 (ROOM01)";
			Load += FrmRoomType_Load;

			panelTopHeader.ResumeLayout(false);
			panelTopHeader.PerformLayout();
			panelMainContent.ResumeLayout(false);
			panelLeftForm.ResumeLayout(false);
			grpRoomTypeInfo.ResumeLayout(false);
			grpRoomTypeInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numCapacity).EndInit();
			((System.ComponentModel.ISupportInitialize)numPricePerNight).EndInit();
			panelActionButtons.ResumeLayout(false);
			panelRightGrid.ResumeLayout(false);
			grpRoomTypeList.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvRoomTypes).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelTopHeader;
		private Label lblHeader;
		private Label lblSub;
		private Panel panelMainContent;
		private Panel panelLeftForm;
		private GroupBox grpRoomTypeInfo;
		private Label lblRoomTypeId;
		private TextBox txtRoomTypeId;
		private Label lblName;
		private TextBox txtName;
		private Label lblCapacity;
		private NumericUpDown numCapacity;
		private Label lblPricePerNight;
		private NumericUpDown numPricePerNight;
		private Label lblDescription;
		private TextBox txtDescription;
		private Panel panelActionButtons;
		private Button btnAdd;
		private Button btnUpdate;
		private Button btnDelete;
		private Button btnClear;
		private Button btnClose;
		private Panel panelRightGrid;
		private GroupBox grpRoomTypeList;
		private Label lblTotalCount;
		private DataGridView dgvRoomTypes;
		private DataGridViewTextBoxColumn colSTT;
		private DataGridViewTextBoxColumn colRoomTypeId;
		private DataGridViewTextBoxColumn colName;
		private DataGridViewTextBoxColumn colCapacity;
		private DataGridViewTextBoxColumn colPricePerNight;
		private DataGridViewTextBoxColumn colDescription;
	}
}
