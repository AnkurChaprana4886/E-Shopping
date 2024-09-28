using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_BAL.Models;
using E_Shopping_Common.Models;
using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly EshoppingContext _context;
        public AddressService(IAddressRepository addressRepository,EshoppingContext eshoppingContext)
        {
            _addressRepository = addressRepository;
            _context = eshoppingContext;
        }

        public async Task<AddressModel> AddAddress(AddressDto addressDto)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var userAddress = await GetAddressesByUserId(addressDto.UserId);

                // If the new address is marked as primary
                if (addressDto.IsPrimary == true)
                {
                    var existingPrimaryAddress = userAddress.FirstOrDefault(a => a.IsPrimary == true);

                    if (existingPrimaryAddress != null)
                    {
                        // Update the existing primary address to non-primary
                        existingPrimaryAddress.IsPrimary = false;

                        var existingAddressEntity = await _addressRepository.GetById(existingPrimaryAddress.AddressId);
                        existingAddressEntity.IsPrimary = false;

                        await _addressRepository.Update(existingAddressEntity);
                    }
                }
                else
                {
                    // If there is no address for the user, make this one primary by default
                    if (!userAddress.Any())
                    {
                        addressDto.IsPrimary = true;
                    }
                }

                // Create the new address
                var address = new Address
                {
                    UserId = addressDto.UserId,
                    AddressLine1 = addressDto.AddressLine1,
                    AddressLine2 = addressDto.AddressLine2,
                    LandMark = addressDto.LandMark,
                    City = addressDto.City,
                    State = addressDto.State,
                    ZipCode = addressDto.ZipCode,
                    Country = addressDto.Country,
                    IsPrimary = addressDto.IsPrimary,
                };

                await _addressRepository.Add(address);

                // Commit the transaction
                transaction.Commit();

                // Prepare the response model
                var returnAddress = new AddressModel
                {
                    AddressId = address.AddressId,
                    UserId = address.UserId,
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    LandMark = address.LandMark,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode,
                    Country = address.Country,
                    IsPrimary = address.IsPrimary
                };

                return returnAddress;
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                transaction.Rollback();
                throw new Exception("An error occurred while adding the address.", ex);
            }
        }


        public async Task<IEnumerable<AddressDto>> GetAddressesByUserId(long? userId)
        {
            var addresses = await _addressRepository.GetByUserId(userId);
            return addresses.Select(a => new AddressDto
            {
                AddressId = a.AddressId,
                UserId = a.UserId,
                City = a.City,
                State = a.State,
                ZipCode = a.ZipCode,
                Country = a.Country,
                AddressLine1 = a.AddressLine1,
                AddressLine2 = a.AddressLine2,
                IsPrimary  = a.IsPrimary,
                LandMark = a.LandMark,
            });
        }

        public async Task<AddressModel> UpdateAddress(AddressDto addressDto)
        {
            // Retrieve the existing address from the database
            var address = await _addressRepository.GetById(addressDto.AddressId);

            if (address != null)
            {
                // Check if the updated address is set as primary
                if (addressDto.IsPrimary == true)
                {
                    // Retrieve all addresses for the user
                    var userAddresses = await GetAddressesByUserId(addressDto.UserId);

                    // Find the current primary address
                    var existingPrimaryAddress = userAddresses.FirstOrDefault(a => a.IsPrimary == true);

                    // If there's another primary address, update it to be non-primary
                    if (existingPrimaryAddress != null && existingPrimaryAddress.AddressId != addressDto.AddressId)
                    {
                        var existingAddressEntity = await _addressRepository.GetById(existingPrimaryAddress.AddressId);
                        if (existingAddressEntity != null)
                        {
                            existingAddressEntity.IsPrimary = false;
                            await _addressRepository.Update(existingAddressEntity);
                        }
                    }
                }

                // Update the current address details
                address.City = addressDto.City;
                address.State = addressDto.State;
                address.ZipCode = addressDto.ZipCode;
                address.Country = addressDto.Country;
                address.LandMark = addressDto.LandMark;
                address.AddressLine1 = addressDto.AddressLine1;
                address.AddressLine2 = addressDto.AddressLine2;
                address.IsPrimary = addressDto.IsPrimary;

                // Update the address in the database
                await _addressRepository.Update(address);
            }

            // Prepare and return the updated address as AddressModel
            var returnAddress = new AddressModel
            {
                AddressId = address.AddressId,
                UserId = address.UserId,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                LandMark = address.LandMark,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country,
                IsPrimary = address.IsPrimary
            };

            return returnAddress;
        }


        public async Task DeleteAddress(long addressId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                // Retrieve the address to be deleted
                var addressToDelete = await _addressRepository.GetById(addressId);

                if (addressToDelete == null)
                {
                    throw new Exception("Address not found.");
                }

                // Check if the address to be deleted is the primary address
                if (addressToDelete.IsPrimary == true)
                {
                    addressToDelete.IsPrimary = false;
                    await _addressRepository.Update(addressToDelete);
                    // Get all addresses for the user
                    var userAddresses = await GetAddressesByUserId(addressToDelete.UserId);

                    // Exclude the address that's being deleted
                    var otherAddresses = userAddresses.Where(a => a.AddressId != addressId).ToList();

                    if (otherAddresses.Any())
                    {
                        // Set another address as primary
                        var newPrimaryAddress = otherAddresses.First();
                        newPrimaryAddress.IsPrimary = true;

                        var newPrimaryEntity = await _addressRepository.GetById(newPrimaryAddress.AddressId);
                        newPrimaryEntity.IsPrimary = true;

                        _context.Entry(newPrimaryEntity).State = EntityState.Modified;

                        await _addressRepository.Update(newPrimaryEntity);
                    }
                }

                // Delete the address
                await _addressRepository.Delete(addressId);

                // Commit the transaction
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                transaction.Rollback();
                throw new Exception("An error occurred while deleting the address.", ex);
            }
        }


    }
}
