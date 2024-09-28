using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Dto
{
    public class WishListDto
    {
        public long WishlistId { get; set; }

        public long? CustomerId { get; set; }

        public string Name { get; set; } = null!;

    }
}
