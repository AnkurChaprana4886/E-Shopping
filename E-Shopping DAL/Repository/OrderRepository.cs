using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Exceptions;
using E_Shopping_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EshoppingContext _context;
        public OrderRepository(EshoppingContext context)
        {
            _context = context;
        }

        public async Task<long> CreateOrderAsync(Order order)
        {
            if(order == null)
            {
                return 0;
            }
            _context.Add(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }
        public async Task<Order?> GetOrderByIdAsync(long orderId)
        {
            if (orderId <= 0) // Handle invalid ID values
            {
                throw new ArgumentException("Invalid order ID", nameof(orderId));
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                // Handle the case when the order is not found, e.g., by throwing an exception or returning a default value
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(long customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentException("Invalid customer ID", nameof(customerId));
            }

            try
            {
                var orders = await _context.Orders
                    .Include(o => o.OrderItems) // Include related OrderItems if needed
                    .Where(o => o.CustomerId == customerId) // Filter by customerId
                    .ToListAsync(); // Fetch the results

                return orders;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while retrieving orders from the database.", ex);
            }
        }

        public async Task UpdateAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }

            try
            {
                var existingorder = await GetOrderByIdAsync(order.OrderId);

                if (existingorder != null)
                {
                    existingorder.OrderStatus = order.OrderStatus;
                    existingorder.BillingAddressId = order.BillingAddressId;
                    existingorder.ShippingAddressId = order.ShippingAddressId;
                    existingorder.TotalAmount = order.TotalAmount;
                    existingorder.PaymentMethod = order.PaymentMethod;

                    _context.Orders.Update(existingorder);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("An error occurred while updating orders from the database.", ex);
            }
        }
        public async Task DeleteAsync(long orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Invalid order ID", nameof(orderId));
            }

            try
            {
                var order = await _context.Orders.FindAsync(orderId);

                if (order == null)
                {
                    throw new KeyNotFoundException($"Order with ID {orderId} not found.");
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log and handle the exception
                throw new DataAccessException("An error occurred while deleting the order.", ex);
            }
        }

        public async Task CancelAsync(long orderId)
        {
            if (orderId == 0)
            {
                throw new ArgumentNullException(nameof(orderId), "Order cannot be null.");
            }

            try
            {
                var existingorder = await GetOrderByIdAsync(orderId);

                if (existingorder != null)
                {
                    existingorder.OrderStatus = "Cancelled";

                    _context.Orders.Update(existingorder);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred while cancelling the order from the database.", ex);
            }
        }
        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
