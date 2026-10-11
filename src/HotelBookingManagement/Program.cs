using System;
using System.Windows.Forms;
using HotelBookingManagement.Business;
using HotelBookingManagement.Forms;

namespace HotelBookingManagement
{
	internal static class Program
	{
		/// <summary>
		///  Điểm khởi nhập chính của toàn bộ ứng dụng Hotel Booking Management.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();

			// Vòng lặp điều hướng phiên làm việc: Hỗ trợ Đăng xuất -> Quay lại Đăng nhập
			while (true)
			{
				using var loginForm = new LoginForm();

				// Nếu người dùng bấm Thoát hoặc tắt cửa sổ Login -> Thoát toàn bộ ứng dụng
				if (loginForm.ShowDialog() != DialogResult.OK)
				{
					break;
				}

				// Đăng nhập thành công -> Mở Form chính của ứng dụng
				using var mainForm = new MainForm();
				Application.Run(mainForm);

				// Nếu người dùng đã bấm nút Đăng xuất (UserSession đã Logout)
				// -> Vòng lặp tiếp tục chạy lại để mở lại LoginForm
				if (!UserSession.IsLoggedIn)
				{
					continue;
				}
				else
				{
					// Người dùng đóng cửa sổ MainForm bằng dấu [X] -> Thoát ứng dụng
					break;
				}
			}
		}
	}
}