using E_Shopping_BAL.Dto;
using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Interfaces
{
    public interface IOrderService
    {
        Task<long> PlaceOrderAsync(OrderDto order);
        Task<Order> GetOrderByOrderIdAsync(long orderId);
        Task<IEnumerable<Order>> GetPlaceOrdersByCustomerIdAsync(long customerId);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(long orderId);
        Task CancelOrderAsync(long orderId);
    }
}
