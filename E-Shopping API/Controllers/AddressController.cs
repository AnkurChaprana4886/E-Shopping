using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using E_Shopping_Common.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using E_Shopping_BAL.Services;
using E_Shopping_BAL.Models;
using E_Shopping_DAL.Entities;

namespace E_Shopping_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressServices;

        public AddressController(IAddressService addressServices)
        {
            _addressServices = addressServices;
        }

        [HttpPost("RegisterAddress")]
        public async Task<IActionResult> RegisterAddress([FromBody] AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest(new ApiResponse<string>(400, "Request body cannot be null"));
            }
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ApiResponse<List<string>>(400, "Validation occured error", error));
            }
            try
            {
                var addedAddress = await _addressServices.AddAddress(addressDto);

                if (addedAddress == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "Failed to register address."));
                }

                return Ok(new ApiResponse<object>(200, "Address registered successfully.", new { AddressId = addedAddress.AddressId }));
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An unexpected error occurred while registering the address.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later.", ex.Message));
            }
        }

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdatePassword(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest(new ApiResponse<string>(400, "Request body cannot be null"));
            }

            try
            {
                var updatedAddress = await _addressServices.UpdateAddress(addressDto);

                if (updatedAddress == null)
                {
                    return NotFound(new ApiResponse<string>(404, "Address not found."));
                }

                return Ok(new ApiResponse<AddressModel>(200, "Address updated successfully.", updatedAddress));
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An unexpected error occurred while updating the address.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later."));
            }
        }

        [HttpGet("GetAddressesByUserId/{userId}")]
        public async Task<IActionResult> GetAddressesByUserId(long? userId)
        {
            if (userId == null)
            {
                return BadRequest(new ApiResponse<string>(400, "UserId Cannot be null"));
            }

            try
            {
                var userAddresses = await _addressServices.GetAddressesByUserId(userId);
                return Ok(new ApiResponse<IEnumerable<AddressDto>>(200, "Succuss", userAddresses));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later."));
            }
        }

        [HttpDelete("DeleteAddress/{addressId}")]
        public async Task<IActionResult> DeleteAddress(long addressId)
        {
            try
            {
                // Attempt to delete the address
                await _addressServices.DeleteAddress(addressId);

                // Return a success response if deletion is successful
                return Ok(new ApiResponse<string>(200, "Address deleted successfully."));
            }
            catch (Exception ex)
            {
                // Catch any other exceptions and return a generic error response
                //_logger.LogError(ex, "An unexpected error occurred while deleting the address with ID {AddressId}.", addressId);
                return StatusCode(500, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later."));
            }
        }
    }
}
