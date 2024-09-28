using E_Shopping_BAL.Interfaces;
using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using E_Shopping_DAL.Repository;
using E_Shopping_BAL.Models;
using E_Shopping_DAL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shopping_BAL.Dto;

namespace E_Shopping_BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerService _customerService;
        private readonly EshoppingContext _shoppingContext;
        public UserService(IUserRepository userRepository,ICustomerService customerService, EshoppingContext shoppingContext)
        {
            _userRepository = userRepository;
            _customerService = customerService;
            _shoppingContext = shoppingContext;
        }

        public async Task<UserModel> RegisterUser(CustomerDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Customer data is null");
            }

            using var transaction = await _shoppingContext.Database.BeginTransactionAsync();

            try
            {
                var userEntity = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash)
                };

                await _userRepository.Add(userEntity);

                var customerDetails = new CustomerModel
                {
                    UserId = userEntity.UserId,  // Assuming UserID is generated after adding the user
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName
                };

                await _customerService.AddCustomer(customerDetails);

                await transaction.CommitAsync();

                return new UserModel
                {
                    UserId = userEntity.UserId,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    PhoneNumber = userEntity.PhoneNumber
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new DataAccessException("An error occurred while registering the user.", ex);
            }
        }

        public async Task<UserModel> IsEmailExistAsync(string email)
        {
            return null;
        }
        public async Task<UserModel> ValidateUserCreadentials(string email, string password)
        {
            return null;
        }
    }
}
