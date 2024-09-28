using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class Subcategory
{
    public int SubcategoryId { get; set; }

    public int? CategoryId { get; set; }

    public string SubcategoryName { get; set; } = null!;

    public virtual Category? Category { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
