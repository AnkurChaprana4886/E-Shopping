using E_Shopping_BAL.Interfaces;
using E_Shopping_BAL.Models;
using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Exceptions;
using E_Shopping_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<CustomerModel> AddCustomer(CustomerModel customer)
        {
            try
            {
                var customerEntity = new Customer
                {
                    UserId = customer.UserId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                };

                await _customerRepository.Add(customerEntity);

                return customer;
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException("An error occurred while adding the customer.", ex);
            }
        }

        public async Task<CustomerModel> GetCustomerById(int customerId)
        {
            try
            {
                var customer = await _customerRepository.GetById(customerId);
                if (customer == null) return null;

                return new CustomerModel
                {
                    UserId = customer.UserId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                };
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException("An error occurred while retrieving the customer.", ex);
            }
        }

        public async Task<List<CustomerModel>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAll();
                var customerModels = new List<CustomerModel>();

                foreach (var customer in customers)
                {
                    customerModels.Add(new CustomerModel
                    {
                        UserId = customer.UserId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                    });
                }

                return customerModels;
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException("An error occurred while retrieving customers.", ex);
            }
        }

        public async Task UpdateCustomer(CustomerModel customer, int userId)
        {
            try
            {
                var customerUser = await _customerRepository.GetById(userId);
                if (customerUser == null) return ;
                var customerEntity = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                };

                await _customerRepository.Update(customerEntity);
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException("An error occurred while updating the customer.", ex);
            }
        }

        public async Task DeleteCustomer(int customerId)
        {
            try
            {
                await _customerRepository.Delete(customerId);
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException("An error occurred while deleting the customer.", ex);
            }
        }
    }
}
