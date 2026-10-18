using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingManagement.Models;

namespace HotelManagement.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByBookingIdAsync(int bookingId);
        Task<decimal> GetTotalPaidAmountByBookingIdAsync(int bookingId);
        Task AddAsync(Payment payment);
        void Update(Payment payment);
        void Delete(Payment payment);
        Task<bool> SaveChangesAsync();
    }
}