using System;
using System.Windows.Forms;
using HotelBookingManagement.Business;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Business.Services;
using HotelBookingManagement.Data.Repositories;

namespace HotelBookingManagement.Forms
{
	public partial class LoginForm : Form
	{
		private readonly IAuthService _authService;
		public LoginForm()
		{
			InitializeComponent();
			var userRepository = new UserRepository();
			_authService =  new AuthService(userRepository);
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{
			StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			AcceptButton = btnLogin;
			CancelButton = btnExit;
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			lbErorrMessage.Text = string.Empty;

			string username  = tbUsername.Text.Trim();
			string password = tbPassword.Text;

			// Gọi tầng BLL để xác thực tài khoản và băm mật khẩu
			if (_authService.Login(username, password, out string errorMessage))
			{
				// Đăng nhập thành công -> Thông báo chào mừng
				MessageBox.Show($"Đăng nhập thành công!\nXin chào: {UserSession.CurrentUser?.FullName}\nVai trò: {UserSession.CurrentUser?.Role}", "Hệ thống quản lý khách sạn",
					MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Đánh dấu kết quả OK và đóng Login Form để chuyển sang form chính
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			else
			{
				// Đăng nhập thất bại -> Hiển thị lỗi màu đỏ lên form
				lbErorrMessage.Text = errorMessage;
				tbPassword.Clear();
				tbPassword.Focus();
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			// Tích chọn: Hiện mật khẩu; Bỏ tích: che bằng chấm tròn đen
			tbPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
		}
	}
}
