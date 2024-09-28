using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class CartItem
{
    public long CartItemId { get; set; }

    public long? CartId { get; set; }

    public long? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTime AddedDate { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Product? Product { get; set; }
}
