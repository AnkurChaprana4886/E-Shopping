using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopping_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetCartByCustomerId/{customerId}")]
        public async Task<IActionResult> GetCartByCustomerId(long customerId)
        {
            try
            {
                var cart = await _cartService.GetCartByCustomerId(customerId);
                if (cart == null) return NotFound(new ApiResponse<string>(404, "Cart not found"));

                return Ok(new ApiResponse<CartDto>(200, "Cart retrieved successfully", cart));
            }
            catch (ApplicationException ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, "An unexpected error occurred"));
            }
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart([FromBody] CartDto cartDto)
        {
            try
            {
                await _cartService.AddCart(cartDto);
                return Ok(new ApiResponse<string>(200, "Cart added successfully"));
            }
            catch (ApplicationException ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, "An unexpected error occurred"));
            }
        }

        [HttpPost("AddCartItem")]
        public async Task<IActionResult> AddCartItem([FromBody] CartItemDto cartItemDto)
        {
            try
            {
                await _cartService.AddCartItem(cartItemDto);
                return Ok(new ApiResponse<string>(200, "Cart item added successfully"));
            }
            catch (ApplicationException ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, "An unexpected error occurred"));
            }
        }

        [HttpPut("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemDto cartItemDto)
        {
            try
            {
                await _cartService.UpdateCartItem(cartItemDto);
                return Ok(new ApiResponse<string>(200, "Cart item updated successfully"));
            }
            catch (ApplicationException ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, "An unexpected error occurred"));
            }
        }

        [HttpDelete("items/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(long cartItemId)
        {
            try
            {
                await _cartService.RemoveCartItem(cartItemId);
                return Ok(new ApiResponse<string>(200, "Cart item removed successfully"));
            }
            catch (ApplicationException ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, "An unexpected error occurred"));
            }
        }

        [HttpDelete("RemoveCart/{cartId}")]
        public async Task<IActionResult> RemoveCart(long cartId)
        {
            try
            {
                await _cartService.RemoveCart(cartId);
                return Ok(new ApiResponse<string>(200, "Cart removed successfully"));
            }
            catch (ApplicationException ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, new ApiResponse<string>(500, "An unexpected error occurred"));
            }
        }
    }
}
