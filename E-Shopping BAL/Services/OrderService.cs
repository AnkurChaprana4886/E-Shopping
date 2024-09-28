using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Services
{
    public class OrderService : IOrderService 
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<long> PlaceOrderAsync(OrderDto order)
        {
            try
            {
                // Validate order details
                if (order == null || order.OrderItems == null || !order.OrderItems.Any())
                    throw new ArgumentException("Invalid order details provided. Order must have at least one item.");

                // Create new order entity
                var newOrder = new Order
                {
                    CustomerId = order.CustomerId,
                    OrderDate = DateTime.UtcNow,
                    OrderStatus = OrderStatusEnum.Pending.ToString(), // Using enum
                    ShippingAddressId = order.ShippingAddressId,
                    BillingAddressId = order.BillingAddressId,
                    TotalAmount = order.TotalAmount,
                    PaymentMethod = order.PaymentMethod,
                };

                // Save order to the database
                long generatedOrderID = await _orderRepository.CreateOrderAsync(newOrder);
                if (generatedOrderID == 0)
                {
                    throw new InvalidOperationException("Failed to create the order.");
                }

                // Add order items
                foreach (var item in order.OrderItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = generatedOrderID,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.Quantity * item.UnitPrice
                    };

                    await _orderRepository.AddOrderItemAsync(orderItem);
                }

                return generatedOrderID;
            }
            catch (ArgumentException argEx)
            {
                // Log and rethrow the validation-related exception
                throw new ApplicationException("Validation error while placing the order.", argEx);
            }
            catch (InvalidOperationException invEx)
            {
                // Log and rethrow order creation failure
                throw new ApplicationException("Order creation failed.", invEx);
            }
            catch (Exception ex)
            {
                // General exception handling
                throw new ApplicationException("An error occurred while placing the order.", ex);
            }
        }

        public async Task<Order> GetOrderByOrderIdAsync(long orderId)
        {
            return null;
        }
        public async Task<IEnumerable<Order>> GetPlaceOrdersByCustomerIdAsync(long customerId)
        {
            return Enumerable.Empty<Order>();
        }
        public async Task UpdateOrderAsync(Order order)
        {

        }
        public async Task DeleteOrderAsync(long orderId)
        {

        }
        public async Task CancelOrderAsync(long orderId)
        {

        }
    }
}
