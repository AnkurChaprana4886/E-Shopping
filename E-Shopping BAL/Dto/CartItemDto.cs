using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Dto
{
    public class CartItemDto
    {
        public long CartItemId { get; set; }
        public long? CartId { get; set; }
        public long? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
