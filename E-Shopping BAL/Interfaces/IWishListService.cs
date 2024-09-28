using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Models;
using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Interfaces
{
    public interface IWishListService
    {
        Task AddWishList(WishListDto wishlist);
        Task AddWishListItem(WishListItemModel wishlistItem);
        Task DeleteWishlist(int wishlistId);
        Task RemoveWishListItem(long wishlistItemId);
        Task<Wishlist> GetWishlistByUserId(long userId);
        Task<bool> IsCheckProductInWishlist(long? wishlistId, long? productId);
        Task<IEnumerable<WishListItemDto>> GetWishlistItemsByWishListID(long wishlistId);
    }
}
