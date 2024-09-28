using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class Wishlist
{
    public long WishlistId { get; set; }

    public long? CustomerId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string Name { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}
