using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_BAL.Models;
using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using E_Shopping_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Services
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRespository _wishListRespository;
        private readonly IProductService _productService;
        public WishListService(IWishListRespository wishListRespository, IProductService productService)
        {
            _wishListRespository = wishListRespository;
            _productService = productService;
        }
        public async Task AddWishList(WishListDto wishlist)
        {
            if (wishlist == null)
            {
                throw new ArgumentNullException(nameof(wishlist));
            }
            try
            {
                var newWishList = new Wishlist
                {
                    Name = wishlist.Name,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CustomerId = wishlist.CustomerId,
                };
                await _wishListRespository.Add(newWishList);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the wishlist.", ex);
            }
        }

        public async Task AddWishListItem(WishListItemModel wishlistItem)
        {
            if (wishlistItem == null)
            {
                throw new ArgumentNullException(nameof(wishlistItem));
            }

            try
            {
                // Check if the product is already in the wishlist
                if (await IsCheckProductInWishlist(wishlistItem.WishlistId, wishlistItem.ProductId))
                {
                    throw new InvalidOperationException("This product is already in the wishlist.");
                }

                // If the product is not in the wishlist, add it
                var newItem = new WishlistItem
                {
                    WishlistId = wishlistItem.WishlistId,
                    ProductId = wishlistItem.ProductId,
                    // Add any other necessary properties here
                };

                await _wishListRespository.AddItem(newItem);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the wishlist item.", ex);
            }
        }

        public async Task DeleteWishlist(int wishlistId)
        {
            if(wishlistId == 0)
            {
                throw new ArgumentNullException(nameof(wishlistId));
            }
            try
            {
                await _wishListRespository.DeleteWishlist(wishlistId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the wishlist.", ex);
            }
        }
        public async Task RemoveWishListItem(long wishlistItemId)
        {
            if (wishlistItemId == 0)
            {
                throw new ArgumentNullException(nameof(wishlistItemId));
            }
            try
            {
                await _wishListRespository.RemoveItem(wishlistItemId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the wishlistItem.", ex);
            }
        }
        public async Task<Wishlist> GetWishlistByUserId(long userId)
        {
            if(userId == 0)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            try
            {
                return await _wishListRespository.GetWishlistByUserId(userId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving  the wishlist.", ex);
            }
        }
        public async Task<bool> IsCheckProductInWishlist(long? wishlistId, long? productId)
        {
            if (wishlistId == 0 && productId == 0)
            {
                throw new ArgumentNullException();
            }
            try
            {
                return await _wishListRespository.IsProductInWishlist(wishlistId, productId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while checking the product.", ex);
            }
        }
        public async Task<IEnumerable<WishListItemDto>> GetWishlistItemsByWishListID(long wishlistId)
        {
            if (wishlistId == 0)
            {
                throw new ArgumentNullException(nameof(wishlistId));
            }

            try
            {
                // Retrieve wishlist items from the repository
                var items = await _wishListRespository.GetWishlistItems(wishlistId);

                // Map entities to DTOs
                var wishlistItemDtos = items.Select(item => new WishListItemDto
                {
                    WishlistItemId = item.WishlistItemId,
                    WishlistId = item.WishlistId,
                    ProductId = item.ProductId,
                    ProductDetails = item.Product != null ? new ProductDto()
                    {
                        ProductId = item.Product.ProductId,
                        ProductName = item.Product.ProductName,
                        Price = item.Product.Price,
                        QuantityInStock = item.Product.QuantityInStock,
                        CategoryId = item.Product.CategoryId,
                        Description = item.Product.Description,
                        SubcategoryId = item.Product.SubcategoryId,
                        VendorId = item.Product.VendorId
                    }: null
                }).ToList();

                return wishlistItemDtos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the wishlist items.", ex);
            }
        }


    }
}
