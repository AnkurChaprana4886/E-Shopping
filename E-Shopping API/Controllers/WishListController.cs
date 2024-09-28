using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_BAL.Models;
using E_Shopping_BAL.Services;
using E_Shopping_Common.Models;
using E_Shopping_DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopping_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWishListService _wishListService;
        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpPost("AddWishList")]
        public async Task<IActionResult> AddWishList(WishListDto wishListDto)
        {
            if (wishListDto == null)
            {
                return BadRequest(new ApiResponse<string>(400, "wishlist details cannot be null"));
            }
            try
            {
                await _wishListService.AddWishList(wishListDto);
                return Ok(new ApiResponse<string>(200, "Wishlist registered successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(500, "Something Went wrong", ex.Message));
            }
        }

        [HttpPost("AddWishListItem")]
        public async Task<IActionResult> AddWishListItem(WishListItemModel wishListItemModel)
        {
            if(wishListItemModel == null)
            {
                return BadRequest(new ApiResponse<string>(400, "wishlist details cannot be null"));
            }
            try
            {
                await _wishListService.AddWishListItem(wishListItemModel);
                return Ok(new ApiResponse<string>(200, "Wishlistitem registered successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(500, "Something Went wrong", ex.Message));
            }
        }

        [HttpGet("GetWishlistByUserId/{userID}")]
        public async Task<IActionResult> GetWishlistByUserId(long userID)
        {
            if (userID == null)
            {
                return BadRequest(new ApiResponse<string>(400, "userID cannot be null"));
            }
            try
            {
                var wishlist = await _wishListService.GetWishlistByUserId(userID);
                return Ok(new ApiResponse<Wishlist>(200, "Wishlist fetch successfully.", wishlist));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later.", ex.Message));
            }
        }

        [HttpDelete("DeleteWishlist/{wishlistId}")]
        public async Task<IActionResult> DeleteWishlist(int wishlistId)
        {
            if (wishlistId == null)
            {
                return BadRequest(new ApiResponse<string>(400, "wishlistID cannot be null"));
            }
            try
            {
                await _wishListService.DeleteWishlist(wishlistId);
                return Ok(new ApiResponse<string>(200, "Product delete successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later.", ex.Message));
            }
        }

        [HttpDelete("RemoveWishListItem/{wishlistItemId}")]
        public async Task<IActionResult> RemoveWishListItem(long wishlistItemId)
        {
            if (wishlistItemId == null)
            {
                return BadRequest(new ApiResponse<string>(400, "wishlistItemId cannot be null"));
            }
            try
            {
                await _wishListService.RemoveWishListItem(wishlistItemId);
                return Ok(new ApiResponse<string>(200, "Product delete successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later.", ex.Message));
            }
        }
        [HttpGet("GetWishlistItemsByWishListID/{wishlistId}")]
        public async Task<IActionResult> GetWishlistItemsByWishListID(long wishlistId)
        {
            if (wishlistId == null)
            {
                return BadRequest(new ApiResponse<string>(400, "userID cannot be null"));
            }
            try
            {
                var wishlist = await _wishListService.GetWishlistItemsByWishListID(wishlistId);
                return Ok(new ApiResponse<IEnumerable<WishListItemDto>>(200, "WishlistItems fetch successfully.", wishlist));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(500, "An unexpected error occurred. Please try again later.", ex.Message));
            }
        }
    }
}
