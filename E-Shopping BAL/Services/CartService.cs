using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using E_Shopping_Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartDto> GetCartByCustomerId(long customerId)
        {
            if (customerId <= 0) throw new ArgumentException("Invalid customer ID.");

            // Try to retrieve cart from session first
            var httpContext = _httpContextAccessor.HttpContext;
            CartDto sessionCart = httpContext.Session.Get<CartDto>($"Cart_{customerId}");
            if (sessionCart != null) return sessionCart;

            try
            {
                var cart = await _cartRepository.GetCartByCustomerId(customerId);
                if (cart == null) return null;

                var cartDto = new CartDto
                {
                    CartId = cart.CartId,
                    CustomerId = cart.CustomerId,
                    CreatedDate = cart.CreatedDate,
                    TotalAmount = cart.TotalAmount,
                    CartItems = cart.CartItems.Select(ci => new CartItemDto
                    {
                        CartItemId = ci.CartItemId,
                        CartId = ci.CartId,
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        Price = ci.Price,
                        AddedDate = ci.AddedDate
                    }).ToList()
                };

                // Store cart in session
                httpContext.Session.Set($"Cart_{customerId}", cartDto);
                return cartDto;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while retrieving the cart by customer ID.", ex);
            }
        }


        public async Task AddCart(CartDto cartDto)
        {
            if (cartDto == null) throw new ArgumentNullException(nameof(cartDto));

            try
            {
                var cart = new Cart
                {
                    CustomerId = cartDto.CustomerId,
                    CreatedDate = cartDto.CreatedDate,
                    TotalAmount = cartDto.TotalAmount
                };
                await _cartRepository.AddCart(cart);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while adding the cart.", ex);
            }
        }

        public async Task AddCartItem(CartItemDto cartItemDto)
        {
            if (cartItemDto == null) throw new ArgumentNullException(nameof(cartItemDto));

            try
            {
                var cartItem = new CartItem
                {
                    CartId = cartItemDto.CartId,
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity,
                    Price = cartItemDto.Price,
                    AddedDate = cartItemDto.AddedDate
                };
                await _cartRepository.AddCartItem(cartItem);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while adding the cart item.", ex);
            }
        }

        public async Task UpdateCartItem(CartItemDto cartItemDto)
        {
            if (cartItemDto == null) throw new ArgumentNullException(nameof(cartItemDto));

            try
            {
                var cartItem = new CartItem
                {
                    CartItemId = cartItemDto.CartItemId,
                    CartId = cartItemDto.CartId,
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity,
                    Price = cartItemDto.Price,
                    AddedDate = cartItemDto.AddedDate
                };
                await _cartRepository.UpdateCartItem(cartItem);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while updating the cart item.", ex);
            }
        }

        public async Task RemoveCartItem(long cartItemId)
        {
            if (cartItemId <= 0) throw new ArgumentException("Invalid cart item ID.");

            try
            {
                await _cartRepository.RemoveCartItem(cartItemId);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while removing the cart item.", ex);
            }
        }

        public async Task RemoveCart(long cartId)
        {
            if (cartId <= 0) throw new ArgumentException("Invalid cart ID.");

            try
            {
                await _cartRepository.RemoveCart(cartId);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ApplicationException("An error occurred while removing the cart.", ex);
            }
        }
    }

}
