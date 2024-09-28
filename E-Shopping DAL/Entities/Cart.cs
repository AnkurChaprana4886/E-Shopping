using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class Cart
{
    public long CartId { get; set; }

    public long? CustomerId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Customer? Customer { get; set; }
}
