using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Models
{
    public class ProductModel
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
    }
}
