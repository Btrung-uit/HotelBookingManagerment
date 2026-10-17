namespace HotelBookingManagement.Forms
{
    partial class FrmBooking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            txtCCCD = new TextBox();
            txtSDT = new TextBox();
            label3 = new Label();
            label2 = new Label();
            btnChonKhachCu = new Button();
            txtHoTen = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            dtpCheckOut = new DateTimePicker();
            btnKiemTraPhong = new Button();
            cmbLoaiPhong = new ComboBox();
            dtpCheckIn = new DateTimePicker();
            dgvDanhSachPhong = new DataGridView();
            btnTaoBooking = new Button();
            btnHuy = new Button();
            lblUocTinhGia = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDanhSachPhong).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtCCCD);
            groupBox1.Controls.Add(txtSDT);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnChonKhachCu);
            groupBox1.Controls.Add(txtHoTen);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(22, 48);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(354, 214);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin Khách hàng";
            // 
            // txtCCCD
            // 
            txtCCCD.Location = new Point(16, 143);
            txtCCCD.Name = "txtCCCD";
            txtCCCD.Size = new Size(199, 23);
            txtCCCD.TabIndex = 6;
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(16, 94);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(199, 23);
            txtSDT.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 122);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 4;
            label3.Text = "CCCD/CMND";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 78);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 3;
            label2.Text = "Số điện thoại";
            // 
            // btnChonKhachCu
            // 
            btnChonKhachCu.Location = new Point(216, 175);
            btnChonKhachCu.Name = "btnChonKhachCu";
            btnChonKhachCu.Size = new Size(118, 23);
            btnChonKhachCu.TabIndex = 2;
            btnChonKhachCu.Text = "Chọn Khách Cũ";
            btnChonKhachCu.UseVisualStyleBackColor = true;
            btnChonKhachCu.Click += btnChonKhachCu_Click;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(15, 47);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(200, 23);
            txtHoTen.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 29);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 0;
            label1.Text = "Họ tên";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dtpCheckOut);
            groupBox2.Controls.Add(btnKiemTraPhong);
            groupBox2.Controls.Add(cmbLoaiPhong);
            groupBox2.Controls.Add(dtpCheckIn);
            groupBox2.Location = new Point(411, 48);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(365, 214);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thông tin Đặt phòng";
            // 
            // dtpCheckOut
            // 
            dtpCheckOut.Location = new Point(14, 78);
            dtpCheckOut.Name = "dtpCheckOut";
            dtpCheckOut.Size = new Size(200, 23);
            dtpCheckOut.TabIndex = 3;
            // 
            // btnKiemTraPhong
            // 
            btnKiemTraPhong.Location = new Point(213, 175);
            btnKiemTraPhong.Name = "btnKiemTraPhong";
            btnKiemTraPhong.Size = new Size(137, 23);
            btnKiemTraPhong.TabIndex = 2;
            btnKiemTraPhong.Text = "Kiểm Tra Phòng Trống";
            btnKiemTraPhong.UseVisualStyleBackColor = true;
            btnKiemTraPhong.Click += btnKiemTraPhong_Click;
            // 
            // cmbLoaiPhong
            // 
            cmbLoaiPhong.FormattingEnabled = true;
            cmbLoaiPhong.Location = new Point(14, 119);
            cmbLoaiPhong.Name = "cmbLoaiPhong";
            cmbLoaiPhong.Size = new Size(121, 23);
            cmbLoaiPhong.TabIndex = 1;
            // 
            // dtpCheckIn
            // 
            dtpCheckIn.Location = new Point(14, 37);
            dtpCheckIn.Name = "dtpCheckIn";
            dtpCheckIn.Size = new Size(200, 23);
            dtpCheckIn.TabIndex = 0;
            // 
            // dgvDanhSachPhong
            // 
            dgvDanhSachPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDanhSachPhong.Location = new Point(22, 287);
            dgvDanhSachPhong.Name = "dgvDanhSachPhong";
            dgvDanhSachPhong.Size = new Size(354, 139);
            dgvDanhSachPhong.TabIndex = 2;
            // 
            // btnTaoBooking
            // 
            btnTaoBooking.Location = new Point(471, 377);
            btnTaoBooking.Name = "btnTaoBooking";
            btnTaoBooking.Size = new Size(96, 23);
            btnTaoBooking.TabIndex = 4;
            btnTaoBooking.Text = "Tạo Booking";
            btnTaoBooking.UseVisualStyleBackColor = true;
            btnTaoBooking.Click += btnTaoBooking_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(651, 377);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(75, 23);
            btnHuy.TabIndex = 5;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // lblUocTinhGia
            // 
            lblUocTinhGia.AutoSize = true;
            lblUocTinhGia.Location = new Point(552, 344);
            lblUocTinhGia.Name = "lblUocTinhGia";
            lblUocTinhGia.Size = new Size(110, 15);
            lblUocTinhGia.TabIndex = 6;
            lblUocTinhGia.Text = "Ước tính giá: 0 VNĐ";
            // 
            // FrmBooking
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblUocTinhGia);
            Controls.Add(btnHuy);
            Controls.Add(btnTaoBooking);
            Controls.Add(dgvDanhSachPhong);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FrmBooking";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmBooking";
            Load += FrmBooking_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDanhSachPhong).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtHoTen;
        private Label label1;
        private Button btnChonKhachCu;
        private GroupBox groupBox2;
        private Button btnKiemTraPhong;
        private ComboBox cmbLoaiPhong;
        private DateTimePicker dtpCheckIn;
        private DataGridView dgvDanhSachPhong;
        private Button btnTaoBooking;
        private Button btnHuy;
        private DateTimePicker dtpCheckOut;
        private Label label3;
        private Label label2;
        private Label lblUocTinhGia;
        private TextBox txtCCCD;
        private TextBox txtSDT;
    }
}