using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace HotelBookingManagement.Data.Database;
public static class DatabaseConnection
{
    // Chuỗi kết nối mặc định sử dụng Windows Authentication
    // Lưu ý: TrustServerCertificate=True là bắt buộc đối với Microsoft.Data.SqlClient thế hệ mới
    public static string ConnectionString = @"Server=DÁN_TÊN_SERVER_VỪA_COPY_VÀO_ĐÂY; Database=HotelBookingDB; Integrated Security=True; TrustServerCertificate=True;";
    /// <summary>
    /// Tạo và trả về một đối tượng SqlConnection mới
    /// </summary>
    public static SqlConnection GetConnection()
	{
		return new SqlConnection(ConnectionString);
	}

	/// <summary>
	/// Kiểm tra kết nối tới cơ sở dữ liệu SQL Server
	/// </summary>
	/// <param name="message">Thông điệp chi tiết trả về</param>
	/// <returns>True nếu kết nối thành công, False nếu thất bại</returns>
	public static bool TestConnection(out string message)
	{
		try
		{
			using var connection = GetConnection();
			connection.Open();
			message = $"Kết nối thành công tới Database [{connection.Database}] trên Server [{connection.DataSource}]!";
			return true;
		}
		catch (Exception ex)
		{
			message = $"Lỗi kết nối CSDL: {ex.Message}";
			return false;
		}
	}
}