using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Exceptions;
using E_Shopping_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EshoppingContext _context;

        public CustomerRepository(EshoppingContext context)
        {
            _context = context;
        }

        public async Task Add(Customer customer)
        {
            //using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                    //await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    //await transaction.RollbackAsync();
                    throw new DataAccessException("An error occurred while adding a new customer.", ex);
                }
            }
        }

        public async Task<Customer> GetById(int customerId)
        {
            try
            {
                return await _context.Customers.FirstOrDefaultAsync(c => c.UserId == customerId);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieving the customer.", ex);
            }
        }

        public async Task<List<Customer>> GetAll()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieving customers.", ex);
            }
        }

        public async Task Update(Customer customer)
        {
            //using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                    //await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    //await transaction.RollbackAsync();
                    throw new DataAccessException("An error occurred while updating the customer.", ex);
                }
            }
        }

        public async Task Delete(int customerId)
        {
            //using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var customer = await GetById(customerId);
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                    //await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    //await transaction.RollbackAsync();
                    throw new DataAccessException("An error occurred while deleting the customer.", ex);
                }
            }
        }
    }
}
