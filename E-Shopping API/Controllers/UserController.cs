using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopping_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(CustomerDto customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is null");
                }

                await _userService.RegisterUser(customer);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                // Log the exception (assuming you have a logger configured)
                //_logger.LogError(ex, "Error occurred while registering the user");

                // Return a generic error response
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
