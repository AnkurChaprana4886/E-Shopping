using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Dto
{
    public class CartDto
    {
        public long CartId { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
