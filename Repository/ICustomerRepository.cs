using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingManagement.Models;

namespace HotelManagement.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetByIdentityNumberAsync(string identityNumber);
        Task AddAsync(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        Task<bool> SaveChangesAsync();
    }
}