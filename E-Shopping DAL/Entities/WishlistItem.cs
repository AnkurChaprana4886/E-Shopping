using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class WishlistItem
{
    public long WishlistItemId { get; set; }

    public long? WishlistId { get; set; }

    public long? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Wishlist? Wishlist { get; set; }
}
