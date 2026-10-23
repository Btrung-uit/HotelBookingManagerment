using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingManagement.Forms
{
	partial class LoginForm
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
			panelBanner = new Panel();
			lblBannerSubtitle = new Label();
			lblBannerTitle = new Label();
			lblBannerIcon = new Label();
			lblUsername = new Label();
			tbUsername = new TextBox();
			lblPassword = new Label();
			tbPassword = new TextBox();
			chkShowPassword = new CheckBox();
			lbErorrMessage = new Label();
			btnLogin = new Button();
			btnExit = new Button();
			lblFooter = new Label();
			panelBanner.SuspendLayout();
			SuspendLayout();
			// 
			// panelBanner
			// 
			panelBanner.BackColor = Color.FromArgb(24, 30, 54);
			panelBanner.Controls.Add(lblBannerSubtitle);
			panelBanner.Controls.Add(lblBannerTitle);
			panelBanner.Controls.Add(lblBannerIcon);
			panelBanner.Dock = DockStyle.Top;
			panelBanner.Location = new Point(0, 0);
			panelBanner.Name = "panelBanner";
			panelBanner.Size = new Size(450, 130);
			panelBanner.TabIndex = 0;
			// 
			// lblBannerSubtitle
			// 
			lblBannerSubtitle.Font = new Font("Segoe UI", 9F);
			lblBannerSubtitle.ForeColor = Color.FromArgb(170, 185, 210);
			lblBannerSubtitle.Location = new Point(0, 92);
			lblBannerSubtitle.Name = "lblBannerSubtitle";
			lblBannerSubtitle.Size = new Size(450, 22);
			lblBannerSubtitle.TabIndex = 2;
			lblBannerSubtitle.Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN";
			lblBannerSubtitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblBannerTitle
			// 
			lblBannerTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
			lblBannerTitle.ForeColor = Color.White;
			lblBannerTitle.Location = new Point(0, 60);
			lblBannerTitle.Name = "lblBannerTitle";
			lblBannerTitle.Size = new Size(450, 32);
			lblBannerTitle.TabIndex = 1;
			lblBannerTitle.Text = "HOTEL MANAGEMENT";
			lblBannerTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblBannerIcon
			// 
			lblBannerIcon.Font = new Font("Segoe UI", 26F);
			lblBannerIcon.ForeColor = Color.White;
			lblBannerIcon.Location = new Point(0, 10);
			lblBannerIcon.Name = "lblBannerIcon";
			lblBannerIcon.Size = new Size(450, 50);
			lblBannerIcon.TabIndex = 0;
			lblBannerIcon.Text = "🏨";
			lblBannerIcon.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblUsername
			// 
			lblUsername.AutoSize = true;
			lblUsername.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
			lblUsername.ForeColor = Color.FromArgb(80, 90, 105);
			lblUsername.Location = new Point(45, 155);
			lblUsername.Name = "lblUsername";
			lblUsername.Size = new Size(130, 20);
			lblUsername.TabIndex = 1;
			lblUsername.Text = "TÊN ĐĂNG NHẬP";
			// 
			// tbUsername
			// 
			tbUsername.BorderStyle = BorderStyle.FixedSingle;
			tbUsername.Font = new Font("Segoe UI", 11F);
			tbUsername.Location = new Point(45, 178);
			tbUsername.Name = "tbUsername";
			tbUsername.Size = new Size(360, 32);
			tbUsername.TabIndex = 2;
			// 
			// lblPassword
			// 
			lblPassword.AutoSize = true;
			lblPassword.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
			lblPassword.ForeColor = Color.FromArgb(80, 90, 105);
			lblPassword.Location = new Point(45, 225);
			lblPassword.Name = "lblPassword";
			lblPassword.Size = new Size(86, 20);
			lblPassword.TabIndex = 3;
			lblPassword.Text = "MẬT KHẨU";
			// 
			// tbPassword
			// 
			tbPassword.BorderStyle = BorderStyle.FixedSingle;
			tbPassword.Font = new Font("Segoe UI", 11F);
			tbPassword.Location = new Point(45, 248);
			tbPassword.Name = "tbPassword";
			tbPassword.Size = new Size(360, 32);
			tbPassword.TabIndex = 4;
			tbPassword.UseSystemPasswordChar = true;
			// 
			// chkShowPassword
			// 
			chkShowPassword.AutoSize = true;
			chkShowPassword.Cursor = Cursors.Hand;
			chkShowPassword.Font = new Font("Segoe UI", 9F);
			chkShowPassword.ForeColor = Color.FromArgb(90, 100, 115);
			chkShowPassword.Location = new Point(45, 288);
			chkShowPassword.Name = "chkShowPassword";
			chkShowPassword.Size = new Size(129, 24);
			chkShowPassword.TabIndex = 5;
			chkShowPassword.Text = "Hiện mật khẩu";
			chkShowPassword.UseVisualStyleBackColor = true;
			chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
			// 
			// lbErorrMessage
			// 
			lbErorrMessage.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
			lbErorrMessage.ForeColor = Color.FromArgb(220, 53, 69);
			lbErorrMessage.Location = new Point(45, 315);
			lbErorrMessage.Name = "lbErorrMessage";
			lbErorrMessage.Size = new Size(360, 36);
			lbErorrMessage.TabIndex = 6;
			lbErorrMessage.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnLogin
			// 
			btnLogin.BackColor = Color.FromArgb(13, 110, 253);
			btnLogin.Cursor = Cursors.Hand;
			btnLogin.FlatAppearance.BorderSize = 0;
			btnLogin.FlatStyle = FlatStyle.Flat;
			btnLogin.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
			btnLogin.ForeColor = Color.White;
			btnLogin.Location = new Point(45, 356);
			btnLogin.Name = "btnLogin";
			btnLogin.Size = new Size(360, 42);
			btnLogin.TabIndex = 7;
			btnLogin.Text = "ĐĂNG NHẬP";
			btnLogin.UseVisualStyleBackColor = false;
			btnLogin.Click += btnLogin_Click;
			// 
			// btnExit
			// 
			btnExit.BackColor = Color.FromArgb(240, 242, 245);
			btnExit.Cursor = Cursors.Hand;
			btnExit.FlatAppearance.BorderSize = 0;
			btnExit.FlatStyle = FlatStyle.Flat;
			btnExit.Font = new Font("Segoe UI", 9F);
			btnExit.ForeColor = Color.FromArgb(100, 110, 125);
			btnExit.Location = new Point(45, 408);
			btnExit.Name = "btnExit";
			btnExit.Size = new Size(360, 36);
			btnExit.TabIndex = 8;
			btnExit.Text = "Thoát ứng dụng";
			btnExit.UseVisualStyleBackColor = false;
			btnExit.Click += btnExit_Click;
			// 
			// lblFooter
			// 
			lblFooter.Font = new Font("Segoe UI", 8F);
			lblFooter.ForeColor = Color.Silver;
			lblFooter.Location = new Point(0, 465);
			lblFooter.Name = "lblFooter";
			lblFooter.Size = new Size(450, 25);
			lblFooter.TabIndex = 9;
			lblFooter.Text = "IT008 - Lập trình trực quan | UIT © 2026";
			lblFooter.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// LoginForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(450, 505);
			Controls.Add(lblFooter);
			Controls.Add(btnExit);
			Controls.Add(btnLogin);
			Controls.Add(lbErorrMessage);
			Controls.Add(chkShowPassword);
			Controls.Add(tbPassword);
			Controls.Add(lblPassword);
			Controls.Add(tbUsername);
			Controls.Add(lblUsername);
			Controls.Add(panelBanner);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "LoginForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Đăng nhập - Hotel Booking Management";
			Load += LoginForm_Load;
			panelBanner.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Panel panelBanner;
		private Label lblBannerIcon;
		private Label lblBannerTitle;
		private Label lblBannerSubtitle;
		private Label lblUsername;
		private TextBox tbUsername;
		private Label lblPassword;
		private TextBox tbPassword;
		private CheckBox chkShowPassword;
		private Label lbErorrMessage;
		private Button btnLogin;
		private Button btnExit;
		private Label lblFooter;
	}
}