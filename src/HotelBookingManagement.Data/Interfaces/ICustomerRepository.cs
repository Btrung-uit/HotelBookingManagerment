using System.Collections.Generic;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Interfaces;

public interface ICustomerRepository
{
    IEnumerable<Customer> GetAll();
    Customer? GetById(int customerId);
    Customer? GetByPhone(string phone);
    Customer? GetByIdentityNumber(string identityNumber);
    IEnumerable<Customer> Search(string keyword);
    int Add(Customer customer);
    bool Update(Customer customer);
    bool Delete(int customerId);
    bool ExistsByPhone(string phone, int? excludeCustomerId = null);
    bool ExistsByIdentityNumber(string identityNumber, int? excludeCustomerId = null);
}