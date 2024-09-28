using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Models;
using E_Shopping_Common.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Interfaces
{
    public interface IAddressService
    {
        Task<AddressModel> AddAddress(AddressDto addressDto);
        Task<IEnumerable<AddressDto>> GetAddressesByUserId(long? userId);
        Task<AddressModel> UpdateAddress(AddressDto addressDto);
        Task DeleteAddress(long addressId);
    }
}
