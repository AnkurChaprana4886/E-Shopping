using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Models
{
    public class WishListItemModel
    {
        public long WishlistItemId { get; set; }

        public long? WishlistId { get; set; }

        public long ProductId { get; set; }


    }
}
