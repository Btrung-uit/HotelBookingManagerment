using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingManagement.Models;

namespace HotelManagement.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId);
        Task AddAsync(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);
        Task<bool> SaveChangesAsync();
    }
}