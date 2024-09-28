using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class Vendor
{
    public long UserId { get; set; }

    public string? BusinessName { get; set; }

    public string? BusinessAddress { get; set; }

    public string? Gstnumber { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User User { get; set; } = null!;
}
