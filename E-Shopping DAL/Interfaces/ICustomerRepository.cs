using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Interfaces
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task<Customer> GetById(int customerId);
        Task<List<Customer>> GetAll();
        Task Update(Customer customer);
        Task Delete(int customerId);
    }
}
