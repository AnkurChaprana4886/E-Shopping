using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Interfaces
{
    public interface IWishListRespository
    {
        Task Add(Wishlist wishlist);
        Task AddItem(WishlistItem wishlistItem);
        Task DeleteWishlist(int wishlistId);
        Task RemoveItem(long wishlistItemId);

        // Method to get a wishlist by user ID
        Task<Wishlist> GetWishlistByUserId(long userId);

        // Method to check if a product is already in the wishlist
        Task<bool> IsProductInWishlist(long? wishlistId, long? productId);

        // Method to get all items in a wishlist
        Task<IEnumerable<WishlistItem>> GetWishlistItems(long wishlistId);
    }
}
