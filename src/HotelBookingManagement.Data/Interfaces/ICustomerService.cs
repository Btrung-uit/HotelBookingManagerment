using HotelBookingManagement.Models;

namespace HotelBookingManagement.Business.Interfaces
{
    public interface ICustomerService
    {
        Customers FindCustomer(string keyword);
    }
}