using E_Shopping_BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerModel> AddCustomer(CustomerModel customer);
        Task<CustomerModel> GetCustomerById(int customerId);
        Task<List<CustomerModel>> GetAllCustomers();
        Task UpdateCustomer(CustomerModel customer, int userId);
        Task DeleteCustomer(int customerId);

    }
}
