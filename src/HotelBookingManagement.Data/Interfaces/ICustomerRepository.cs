using HotelBookingManagement.Models;

namespace HotelBookingManagement.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Customers GetCustomerByPhoneOrIdentity(string keyword);
    }
}