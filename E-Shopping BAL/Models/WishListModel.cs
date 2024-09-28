using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Models
{
    public class WishListModel
    {
        public long WishlistId { get; set; }

        public long? CustomerId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Name { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

    }
}
