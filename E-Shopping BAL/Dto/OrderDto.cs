using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Dto
{
    public class OrderDto
    {
        public long CustomerId { get; set; } // Required

        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Default to current date

        public long ShippingAddressId { get; set; } // Required

        public long BillingAddressId { get; set; } // Required

        public decimal TotalAmount { get; set; } // Required

        public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.Pending; // Using enum

        public string PaymentMethod { get; set; } = null!; // Required

        public List<OrderItemDto> OrderItems { get; set; } = new(); // Collection of order items
    }

    public class OrderItemDto
    {
        public long OrderItemId { get; set; } // For existing items, if needed

        public long ProductId { get; set; } // Required

        public int Quantity { get; set; } // Required

        public decimal UnitPrice { get; set; } // Required

        public decimal TotalPrice => Quantity * UnitPrice; // Computed property
    }

    public enum OrderStatusEnum
    {
        Pending,
        Completed,
        Cancelled,
        Shipped
    }
}
