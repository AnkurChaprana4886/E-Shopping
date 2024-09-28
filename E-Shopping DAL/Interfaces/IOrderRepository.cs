using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<long> CreateOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(long orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(long customerId);
        Task UpdateAsync(Order order);
        Task DeleteAsync(long orderId);
        Task CancelAsync(long orderId);
        Task AddOrderItemAsync(OrderItem orderItem);
    }
}
