using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByCustomerId(long customerId);
        Task AddCart(Cart cart);
        Task AddCartItem(CartItem cartItem);
        Task UpdateCartItem(CartItem cartItem);
        Task RemoveCartItem(long cartItemId);
        Task RemoveCart(long cartId);
    }
}
