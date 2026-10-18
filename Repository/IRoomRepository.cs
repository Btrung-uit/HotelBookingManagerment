using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingManagement.Models;

namespace HotelManagement.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
        Task AddAsync(Room room);
        void Update(Room room);
        void Delete(Room room);
        Task<bool> SaveChangesAsync();
    }
}