using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class Order
{
    public long OrderId { get; set; }

    public long? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public long? ShippingAddressId { get; set; }

    public long? BillingAddressId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? OrderStatus { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public virtual Address? BillingAddress { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Address? ShippingAddress { get; set; }

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
}
