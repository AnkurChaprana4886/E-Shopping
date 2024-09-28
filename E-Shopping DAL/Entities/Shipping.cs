using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class Shipping
{
    public long ShippingId { get; set; }

    public long? OrderId { get; set; }

    public DateTime? ShippingDate { get; set; }

    public string? Carrier { get; set; }

    public string? TrackingNumber { get; set; }

    public string? ShippingStatus { get; set; }

    public virtual Order? Order { get; set; }
}
