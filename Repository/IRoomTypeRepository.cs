using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingManagement.Models;

namespace HotelManagement.Repositories
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetAllAsync();
        Task<RoomType?> GetByIdAsync(int id);
        Task<IEnumerable<RoomType>> GetByCapacityAsync(int capacity);
        Task AddAsync(RoomType roomType);
        void Update(RoomType roomType);
        void Delete(RoomType roomType);
        Task<bool> SaveChangesAsync();
    }
}