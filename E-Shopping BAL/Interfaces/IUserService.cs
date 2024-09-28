using System;
using E_Shopping_BAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shopping_BAL.Dto;

namespace E_Shopping_BAL.Interfaces
{
    public interface IUserService
    {
        Task <UserModel> RegisterUser(CustomerDto user);
        Task<UserModel> IsEmailExistAsync(string email);
        Task<UserModel> ValidateUserCreadentials (string email, string password);
    }
}
