using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Models;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Data.Repositories;

namespace HotelBookingManagement.Business.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

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
        try
        {
            if (!ValidateCustomer(customer, null, out errorMessage)) return false;

            customer.CreatedAt = DateTime.Now;
            int newId = _customerRepository.Add(customer);

            if (newId > 0)
            {
                customer.CustomerId = newId;
                return true;
            }

            errorMessage = "Thêm khách hàng thất bại. Không thể ghi nhận dữ liệu vào DB.";
            return false;
        }
        catch (SqlException ex)
        {
            errorMessage = (ex.Number == 2627 || ex.Number == 2601)
                ? "Dữ liệu bị trùng lặp: Số điện thoại hoặc CCCD/Hộ chiếu đã tồn tại!"
                : $"Lỗi cơ sở dữ liệu: {ex.Message}";
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
        if (customer == null || customer.CustomerId <= 0)
        {
            errorMessage = "Dữ liệu hoặc mã khách hàng không hợp lệ!";
            return false;
        }

        try
        {
            if (!ValidateCustomer(customer, customer.CustomerId, out errorMessage)) return false;

            bool success = _customerRepository.Update(customer);
            if (!success) errorMessage = "Không tìm thấy khách hàng cần cập nhật.";
            return success;
        }
        catch (SqlException ex)
        {
            errorMessage = (ex.Number == 2627 || ex.Number == 2601)
                ? "Dữ liệu bị trùng lặp: Số điện thoại hoặc CCCD/Hộ chiếu đã tồn tại!"
                : $"Lỗi cơ sở dữ liệu: {ex.Message}";
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
            if (!result) errorMessage = "Không tìm thấy khách hàng cần xóa.";
            return result;
        }
        catch (SqlException ex)
        {
            errorMessage = (ex.Number == 547)
                ? "Không thể xóa vì khách hàng này đã có lịch sử đặt phòng (Bookings)!"
                : $"Lỗi cơ sở dữ liệu khi xóa: {ex.Message}";
            return false;
        }
        catch (Exception ex)
        {
            errorMessage = $"Lỗi hệ thống: {ex.Message}";
            return false;
        }
    }

    private bool ValidateCustomer(Customer customer, int? currentCustomerId, out string errorMessage)
    {
        errorMessage = string.Empty;
        if (customer == null)
        {
            errorMessage = "Dữ liệu khách hàng không được để trống (null)!";
            return false;
        }

        if (string.IsNullOrWhiteSpace(customer.FullName) || customer.FullName.Trim().Length < 2 || customer.FullName.Trim().Length > 100)
        {
            errorMessage = "Vui lòng nhập họ và tên khách hàng (2-100 ký tự)!";
            return false;
        }

        string phone = customer.Phone?.Trim() ?? string.Empty;
        if (!Regex.IsMatch(phone, @"^[0-9]{10,15}$"))
        {
            errorMessage = "Số điện thoại không hợp lệ (10-15 chữ số).";
            return false;
        }
        if (_customerRepository.ExistsByPhone(phone, currentCustomerId))
        {
            errorMessage = $"Số điện thoại '{phone}' đã được sử dụng!";
            return false;
        }

        string identity = customer.IdentityNumber?.Trim() ?? string.Empty;
        if (!Regex.IsMatch(identity, @"^[a-zA-Z0-9]{8,20}$"))
        {
            errorMessage = "Số CCCD/Hộ chiếu không hợp lệ (8-20 ký tự).";
            return false;
        }
        if (_customerRepository.ExistsByIdentityNumber(identity, currentCustomerId))
        {
            errorMessage = $"Số CCCD/Hộ chiếu '{identity}' đã tồn tại!";
            return false;
        }

        if (!string.IsNullOrWhiteSpace(customer.Email))
        {
            string email = customer.Email.Trim();
            if (email.Length > 100 || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorMessage = "Địa chỉ email không hợp lệ!";
                return false;
            }
        }

        if (customer.DateOfBirth.HasValue)
        {
            if (customer.DateOfBirth.Value.Date > DateTime.Today || customer.DateOfBirth.Value.Year < 1900)
            {
                errorMessage = "Ngày sinh không hợp lệ!";
                return false;
            }
        }

        if (!string.IsNullOrWhiteSpace(customer.Address) && customer.Address.Trim().Length > 255)
        {
            errorMessage = "Địa chỉ không được vượt quá 255 ký tự!";
            return false;
        }

        return true;
    }
}