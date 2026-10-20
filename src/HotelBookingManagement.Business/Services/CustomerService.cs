using HotelBookingManagement.Models;
using HotelBookingManagement.Data.Interfaces;
using HotelBookingManagement.Data.Repositories;
using HotelBookingManagement.Business.Interfaces;

namespace HotelBookingManagement.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService()
        {
            // Khởi tạo repository để giao tiếp với DB
            _customerRepo = new CustomerRepository();
        }

        public Customers FindCustomer(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return null;
            }

            // Xóa khoảng trắng thừa 2 đầu và đẩy xuống tầng Data tìm kiếm
            return _customerRepo.GetCustomerByPhoneOrIdentity(keyword.Trim());
        }
    }
}