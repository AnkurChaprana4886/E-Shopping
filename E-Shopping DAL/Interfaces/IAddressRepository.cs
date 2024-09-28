using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Interfaces
{
    public interface IAddressRepository
    {
        Task Add(Address address);
        Task<Address> GetById(long addressId);
        Task<IEnumerable<Address>> GetByUserId(long? userId);
        Task Update(Address address);
        Task Delete(long addressId);
    }
}
