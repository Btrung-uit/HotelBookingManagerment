using HotelBookingManagement.Forms;

namespace HotelBookingManagement
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			
			// Mở form đăng nhập đầu tiên
			using var loginForm = new LoginForm();

			// Nếu đăng nhập thành công thì mới mở form chính (form 1)
			if (loginForm.ShowDialog() == DialogResult.OK)
			{
				Application.Run(new Form1());
			}
			else
			{
				// Người dùng bấm Thoát hoặc tắt cửa sổ đăng nhập -> Thoát ứng dụng
				Application.Exit();
			}
		}
	}
}