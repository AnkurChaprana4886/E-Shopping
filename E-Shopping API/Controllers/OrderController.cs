using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopping_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody]OrderDto orderDto)
        {
            try
            {
                long data = await _orderService.PlaceOrderAsync(orderDto);
                return Ok(new ApiResponse<long>(200, "Successfull", data));
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
