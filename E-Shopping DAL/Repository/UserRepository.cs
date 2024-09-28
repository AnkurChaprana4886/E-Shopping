using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using E_Shopping_DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EshoppingContext _context;
        public UserRepository(EshoppingContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log exception (ex)
                throw new DataAccessException("An error occurred while adding a new user.", ex);
            }
        }
        public async Task Delete(int UserId)
        {
            try
            {
                var user = await _context.Users.FindAsync(UserId);
                if (user != null)
                {
                    _context.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException ex)
            {
                // Log exception (ex)
                throw new DataAccessException("An error occurred while deleting the user.", ex);
            }
        }
        public async Task<List<User>> GetAll()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw new DataAccessException("An error occurred while retrieving users.", ex);
            };
        }
        public async Task<User> GetById(int UserId)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.UserId == UserId);
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw new DataAccessException("An error occurred while retrieving the user.", ex);
            }
        }
        public async Task Update(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log exception (ex)
                throw new DataAccessException("An error occurred while updating the user.", ex);
            }
        }
        public async Task<bool> CheckEmailIDExist(string EmailId)
        {
            try
            {
                return await _context.Users.AnyAsync(x => x.Email == EmailId);
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw new DataAccessException("An error occurred while checking if the email ID exists.", ex);
            }
        }
    }
}
