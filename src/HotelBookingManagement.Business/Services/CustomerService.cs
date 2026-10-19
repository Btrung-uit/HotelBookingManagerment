using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Data.Repositories;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Services;

/// <summary>
/// Cài đặt xử lý logic nghiệp vụ cho module Quản lý Khách hàng (TV02 - Customer)
/// Thực thi validation chặt chẽ (Task CUS03) trước khi gọi tầng Data.
/// </summary>
public class CustomerService : ICustomerService
{
	private readonly ICustomerRepository _customerRepository;

	/// <summary>
	/// Constructor hỗ trợ Dependency Injection
	/// </summary>
	public CustomerService(ICustomerRepository customerRepository)
	{
		_customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
	}

	/// <summary>
	/// Constructor mặc định phục vụ WinForms khởi tạo nhanh
	/// </summary>
	public CustomerService() : this(new CustomerRepository())
	{
	}

	public IEnumerable<Customer> GetAllCustomers()
	{
		return _customerRepository.GetAll();
	}

	public Customer? GetCustomerById(int customerId)
	{
		if (customerId <= 0) return null;
		return _customerRepository.GetById(customerId);
	}

	public Customer? GetCustomerByPhone(string phone)
	{
		if (string.IsNullOrWhiteSpace(phone)) return null;
		return _customerRepository.GetByPhone(phone.Trim());
	}

	public Customer? GetCustomerByIdentityNumber(string identityNumber)
	{
		if (string.IsNullOrWhiteSpace(identityNumber)) return null;
		return _customerRepository.GetByIdentityNumber(identityNumber.Trim());
	}

	public IEnumerable<Customer> SearchCustomers(string keyword)
	{
		if (string.IsNullOrWhiteSpace(keyword))
		{
			return _customerRepository.GetAll();
		}

		return _customerRepository.Search(keyword.Trim());
	}

	public bool CreateCustomer(Customer customer, out string errorMessage)
	{
		errorMessage = string.Empty;

		if (!ValidateCustomer(customer, null, out errorMessage))
		{
			return false;
		}

		customer.CreatedAt = DateTime.Now;

		try
		{
			int newId = _customerRepository.Add(customer);
			if (newId > 0)
			{
				customer.CustomerId = newId;
				return true;
			}

			errorMessage = "Thêm khách hàng thất bại. Không thể ghi nhận dữ liệu vào cơ sở dữ liệu.";
			return false;
		}
		catch (SqlException ex)
		{
			if (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
			{
				errorMessage = "Dữ liệu bị trùng lặp: Số điện thoại hoặc CCCD/Hộ chiếu đã tồn tại!";
			}
			else
			{
				errorMessage = $"Lỗi cơ sở dữ liệu: {ex.Message}";
			}
			return false;
		}
		catch (Exception ex)
		{
			errorMessage = $"Lỗi hệ thống: {ex.Message}";
			return false;
		}
	}

	public bool UpdateCustomer(Customer customer, out string errorMessage)
	{
		errorMessage = string.Empty;

		if (customer.CustomerId <= 0)
		{
			errorMessage = "Mã khách hàng không hợp lệ!";
			return false;
		}

		if (!ValidateCustomer(customer, customer.CustomerId, out errorMessage))
		{
			return false;
		}

		try
		{
			bool success = _customerRepository.Update(customer);
			if (!success)
			{
				errorMessage = "Không tìm thấy khách hàng cần cập nhật hoặc thông tin không có thay đổi.";
			}
			return success;
		}
		catch (SqlException ex)
		{
			if (ex.Number == 2627 || ex.Number == 2601)
			{
				errorMessage = "Dữ liệu bị trùng lặp: Số điện thoại hoặc CCCD/Hộ chiếu đã tồn tại!";
			}
			else
			{
				errorMessage = $"Lỗi cơ sở dữ liệu: {ex.Message}";
			}
			return false;
		}
		catch (Exception ex)
		{
			errorMessage = $"Lỗi hệ thống: {ex.Message}";
			return false;
		}
	}

	public bool DeleteCustomer(int customerId, out string errorMessage)
	{
		errorMessage = string.Empty;

		if (customerId <= 0)
		{
			errorMessage = "Mã khách hàng không hợp lệ!";
			return false;
		}

		try
		{
			bool result = _customerRepository.Delete(customerId);
			if (!result)
			{
				errorMessage = "Không tìm thấy khách hàng cần xóa.";
				return false;
			}
			return true;
		}
		catch (SqlException ex)
		{
			// Mã lỗi 547: Foreign Key constraint violation
			if (ex.Number == 547)
			{
				errorMessage = "Không thể xóa khách hàng vì khách hàng này đã có lịch sử đặt phòng (Bookings) trong hệ thống!";
			}
			else
			{
				errorMessage = $"Lỗi cơ sở dữ liệu khi xóa khách hàng: {ex.Message}";
			}
			return false;
		}
		catch (Exception ex)
		{
			errorMessage = $"Lỗi hệ thống: {ex.Message}";
			return false;
		}
	}

	/// <summary>
	/// Kiểm tra toàn diện tính hợp lệ của dữ liệu khách hàng (Task CUS03)
	/// </summary>
	private bool ValidateCustomer(Customer customer, int? currentCustomerId, out string errorMessage)
	{
		errorMessage = string.Empty;

		if (customer == null)
		{
			errorMessage = "Dữ liệu khách hàng không được để trống (null)!";
			return false;
		}

		// 1. Họ tên bắt buộc, độ dài từ 2 đến 100
		if (string.IsNullOrWhiteSpace(customer.FullName))
		{
			errorMessage = "Vui lòng nhập họ và tên khách hàng!";
			return false;
		}

		string fullName = customer.FullName.Trim();
		if (fullName.Length < 2 || fullName.Length > 100)
		{
			errorMessage = "Họ và tên khách hàng phải có độ dài từ 2 đến 100 ký tự!";
			return false;
		}

		// 2. Số điện thoại bắt buộc, từ 10 đến 15 chữ số, duy nhất
		if (string.IsNullOrWhiteSpace(customer.Phone))
		{
			errorMessage = "Vui lòng nhập số điện thoại khách hàng!";
			return false;
		}

		string phone = customer.Phone.Trim();
		if (!Regex.IsMatch(phone, @"^[0-9]{10,15}$"))
		{
			errorMessage = "Số điện thoại không hợp lệ! Vui lòng nhập từ 10 đến 15 chữ số (chỉ gồm số).";
			return false;
		}

		if (_customerRepository.ExistsByPhone(phone, currentCustomerId))
		{
			errorMessage = $"Số điện thoại '{phone}' đã được sử dụng bởi khách hàng khác!";
			return false;
		}

		// 3. Số CCCD / Hộ chiếu bắt buộc, 8 - 20 ký tự, duy nhất
		if (string.IsNullOrWhiteSpace(customer.IdentityNumber))
		{
			errorMessage = "Vui lòng nhập số CCCD hoặc Hộ chiếu của khách hàng!";
			return false;
		}

		string identity = customer.IdentityNumber.Trim();
		if (!Regex.IsMatch(identity, @"^[a-zA-Z0-9]{8,20}$"))
		{
			errorMessage = "Số CCCD/Hộ chiếu không hợp lệ! Vui lòng nhập từ 8 đến 20 ký tự chữ hoặc số.";
			return false;
		}

		if (_customerRepository.ExistsByIdentityNumber(identity, currentCustomerId))
		{
			errorMessage = $"Số CCCD/Hộ chiếu '{identity}' đã tồn tại trong hệ thống!";
			return false;
		}

		// 4. Email không bắt buộc, nếu có phải đúng định dạng
		if (!string.IsNullOrWhiteSpace(customer.Email))
		{
			string email = customer.Email.Trim();
			if (email.Length > 100)
			{
				errorMessage = "Email không được dài quá 100 ký tự!";
				return false;
			}

			if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
			{
				errorMessage = "Địa chỉ email không đúng định dạng (ví dụ: customer@example.com)!";
				return false;
			}
		}

		// 5. Ngày sinh không bắt buộc, nếu có không được vượt quá ngày hiện tại
		if (customer.DateOfBirth.HasValue)
		{
			if (customer.DateOfBirth.Value.Date > DateTime.Today)
			{
				errorMessage = "Ngày sinh không thể là ngày trong tương lai!";
				return false;
			}

			if (customer.DateOfBirth.Value.Year < 1900)
			{
				errorMessage = "Năm sinh không hợp lệ (phải từ năm 1900 trở lại đây)!";
				return false;
			}
		}

		// 6. Địa chỉ không bắt buộc, tối đa 255 ký tự
		if (!string.IsNullOrWhiteSpace(customer.Address) && customer.Address.Trim().Length > 255)
		{
			errorMessage = "Địa chỉ không được vượt quá 255 ký tự!";
			return false;
		}

		return true;
	}
}
