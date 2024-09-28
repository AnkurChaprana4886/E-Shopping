using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class Product
{
    public long ProductId { get; set; }

    public long? VendorId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? QuantityInStock { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? SubcategoryId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ProductImage? ProductImage { get; set; }

    public virtual Subcategory? Subcategory { get; set; }

    public virtual Vendor? Vendor { get; set; }

    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}
