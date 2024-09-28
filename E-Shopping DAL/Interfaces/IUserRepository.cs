using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Delete(int UserId);
        Task<List<User>> GetAll();
        Task<User> GetById(int UserId);
        Task Update(User user);
        Task<bool> CheckEmailIDExist(string EmailId);
    }
}
