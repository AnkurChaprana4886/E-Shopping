using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly EshoppingContext _context;

        public AddressRepository(EshoppingContext context)
        {
            _context = context;
        }

        public async Task Add(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task<Address> GetById(long addressId)
        {
            return await _context.Addresses.FindAsync(addressId);
        }

        public async Task<IEnumerable<Address>> GetByUserId(long? userId)
        {
            return await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task Update(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long addressId)
        {
            var address = await GetById(addressId);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
        }

    }
}
