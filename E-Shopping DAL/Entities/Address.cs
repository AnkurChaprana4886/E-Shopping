using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class Address
{
    public long AddressId { get; set; }

    public long? UserId { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? LandMark { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public string? Country { get; set; }

    public bool? IsPrimary { get; set; }

    public virtual ICollection<Order> OrderBillingAddresses { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderShippingAddresses { get; set; } = new List<Order>();

    public virtual User? User { get; set; }
}
