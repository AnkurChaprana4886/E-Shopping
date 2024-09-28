using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Repository
{
    public class WishListRepository : IWishListRespository
    {
        private readonly EshoppingContext _context;
        public WishListRepository(EshoppingContext context)
        {
            _context = context;
        }

        public async Task Add(Wishlist wishlist)
        {
             _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task AddItem(WishlistItem wishlistItem)
        {
            _context.WishlistItems.Add(wishlistItem);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteWishlist(int wishlistId)
        {
            var wishlist = _context.Wishlists.FirstOrDefault(w=> w.WishlistId == wishlistId);
            if (wishlist != null)
            {
                _context.Wishlists.Remove(wishlist);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveItem(long wishlistItemId)
        {
            var wishlistItem = _context.WishlistItems.FirstOrDefault(item => item.WishlistItemId == wishlistItemId);
            if(wishlistItem != null)
            {
                _context.WishlistItems.Remove(wishlistItem);
                await _context.SaveChangesAsync();
            }
        }

        // Method to get a wishlist by user ID
        public async Task<Wishlist> GetWishlistByUserId(long userId)
        {
            var wishlist = await _context.Wishlists
                           .Include(w => w.WishlistItems) // Optional: Include related entities like WishlistItems
                           .FirstOrDefaultAsync(w => w.CustomerId == userId);

            return wishlist;
        }

        // Method to check if a product is already in the wishlist
        public async Task<bool> IsProductInWishlist(long? wishlistId, long? productId)
        {
            var wishlistItem = _context.WishlistItems
                             .FirstOrDefault(item=> item.ProductId == productId
                               && item.WishlistId == wishlistId);
            if (wishlistItem != null)
            {
                return true;
            }
            return false;
        }

        // Method to get all items in a wishlist
        public async Task<IEnumerable<WishlistItem>> GetWishlistItems(long wishlistId)
        {
            var items = _context.WishlistItems.Where(i => i.WishlistId == wishlistId).Include(i => i.Product).ToList();
            if (items.Count != 0)
            {
                return items;
            }
            return Enumerable.Empty<WishlistItem>();
        }
    }
}
