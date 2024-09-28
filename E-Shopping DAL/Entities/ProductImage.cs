using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class ProductImage
{
    public long ImageId { get; set; }

    public long? ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool? IsPrimary { get; set; }

    public virtual Product? Product { get; set; }
}
