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
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			tbUsername = new TextBox();
			tbPassword = new TextBox();
			chkShowPassword = new CheckBox();
			btnLogin = new Button();
			btnExit = new Button();
			lbErorrMessage = new Label();
			SuspendLayout();
			// 
			// label1
			// 
			label1.Font = new Font("Segoe UI", 20F);
			label1.Location = new Point(169, 19);
			label1.Name = "label1";
			label1.Size = new Size(462, 46);
			label1.TabIndex = 0;
			label1.Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN";
			label1.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(143, 157);
			label2.Name = "label2";
			label2.Size = new Size(107, 20);
			label2.TabIndex = 1;
			label2.Text = "Tên đăng nhập";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(143, 217);
			label3.Name = "label3";
			label3.Size = new Size(70, 20);
			label3.TabIndex = 2;
			label3.Text = "Mật khẩu";
			// 
			// tbUsername
			// 
			tbUsername.Location = new Point(291, 154);
			tbUsername.Name = "tbUsername";
			tbUsername.Size = new Size(298, 27);
			tbUsername.TabIndex = 3;
			// 
			// tbPassword
			// 
			tbPassword.Location = new Point(291, 214);
			tbPassword.Name = "tbPassword";
			tbPassword.Size = new Size(298, 27);
			tbPassword.TabIndex = 4;
			tbPassword.UseSystemPasswordChar = true;
			// 
			// chkShowPassword
			// 
			chkShowPassword.AutoSize = true;
			chkShowPassword.Location = new Point(606, 217);
			chkShowPassword.Name = "chkShowPassword";
			chkShowPassword.Size = new Size(127, 24);
			chkShowPassword.TabIndex = 5;
			chkShowPassword.Text = "Hiện mật khẩu";
			chkShowPassword.UseVisualStyleBackColor = true;
			chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
			// 
			// btnLogin
			// 
			btnLogin.Location = new Point(385, 273);
			btnLogin.Name = "btnLogin";
			btnLogin.Size = new Size(94, 29);
			btnLogin.TabIndex = 6;
			btnLogin.Text = "Đăng nhập";
			btnLogin.UseVisualStyleBackColor = true;
			btnLogin.Click += btnLogin_Click;
			// 
			// btnExit
			// 
			btnExit.Location = new Point(495, 273);
			btnExit.Name = "btnExit";
			btnExit.Size = new Size(94, 29);
			btnExit.TabIndex = 7;
			btnExit.Text = "Thoát";
			btnExit.UseVisualStyleBackColor = true;
			btnExit.Click += btnExit_Click;
			// 
			// lbErorrMessage
			// 
			lbErorrMessage.AutoSize = true;
			lbErorrMessage.ForeColor = Color.Red;
			lbErorrMessage.Location = new Point(495, 244);
			lbErorrMessage.Name = "lbErorrMessage";
			lbErorrMessage.Size = new Size(0, 20);
			lbErorrMessage.TabIndex = 8;
			// 
			// LoginForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(lbErorrMessage);
			Controls.Add(btnExit);
			Controls.Add(btnLogin);
			Controls.Add(chkShowPassword);
			Controls.Add(tbPassword);
			Controls.Add(tbUsername);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Name = "LoginForm";
			Text = "LoginForm";
			Load += LoginForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Label label2;
		private Label label3;
		private TextBox tbUsername;
		private TextBox tbPassword;
		private CheckBox chkShowPassword;
		private Button btnLogin;
		private Button btnExit;
		private Label lbErorrMessage;
	}
}