using E_Shopping_BAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartByCustomerId(long customerId);
        Task AddCart(CartDto cartDto);
        Task AddCartItem(CartItemDto cartItemDto);
        Task UpdateCartItem(CartItemDto cartItemDto);
        Task RemoveCartItem(long cartItemId);
        Task RemoveCart(long cartId);
    }
}
