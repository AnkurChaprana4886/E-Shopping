using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Dto
{
    public class ProductDto
    {
        [Key]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Vendor ID is required.")]
        public long? VendorId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string? ProductName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Quantity in stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be a non-negative value.")]
        public int? QuantityInStock { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Subcategory ID is required.")]
        public int? SubcategoryId { get; set; }

        [Required(ErrorMessage = "Updated date is required.")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
