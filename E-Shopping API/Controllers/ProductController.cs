using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_Common.Models;
using E_Shopping_DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopping_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody]ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(new ApiResponse<string>(400, "prodcut details cannot be null"));
            }
            try
            {
               var newProductId = await _productService.AddProduct(productDto);
                return CreatedAtRoute("GetProduct",new { productID = newProductId },
                new ApiResponse<ProductDto>(201, "Product registered successfully.",productDto));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(500, "Something Went wrong", ex.Message));
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            if(productDto == null)
            {
                return BadRequest(new ApiResponse<string>(400, "prodcut details cannot be null"));
            }
            try
            {
                await _productService.UpdateProduct(productDto);
                return Ok(new ApiResponse<string>(200, "Product updated successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later.",ex.Message));
            }
        }

        [HttpDelete("DeleteProduct/{productID}")]
        public async Task<IActionResult> DeleteProduct(long productID)
        {
            if (productID <= 0)
            {
                return BadRequest(new ApiResponse<string>(400, "Invalid product ID."));
            }

            try
            {
                await _productService.DeleteProduct(productID);
                return Ok(new ApiResponse<string>(200, "Product deleted successfully."));
            }
            catch (ApplicationException ex)
            {
                return NotFound(new ApiResponse<string>(404, ex.Message));
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ApiResponse<string>(500, "An unexpected error occurred. Please try again later.", ex.Message));
            }
        }


        [HttpGet("GetProductByID/{productID}", Name = "GetProduct")]
        public async Task<IActionResult> GetProductByID(long productID)
        {
            if (productID == null)
            {
                return BadRequest(new ApiResponse<string>(400, "prodcutID cannot be null"));
            }
            try
            {
                var product = await _productService.GetProductByID(productID);
                return Ok(new ApiResponse<ProductDto>(200, "Product fetch successfully.",product));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later.", ex.Message));
            }
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var products = await _productService.GetAllProduct();
                return Ok(new ApiResponse<IEnumerable<ProductDto>>(200,"Fatch Product List succesfully",products));
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later.", ex.Message));
            }
        }
    }
}
 