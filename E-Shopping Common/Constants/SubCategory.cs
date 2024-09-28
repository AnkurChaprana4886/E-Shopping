using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_Common.Constants
{
    public class SubCategory
    {
        public enum Subcategory
        {
            // Category 1: Apparel
            TShirts = 1,
            Jeans,
            Jackets,

            // Category 2: Footwear
            Sneakers = 4,
            FormalShoes,

            // Category 3: Accessories
            Belts = 6,
            Hats,

            // Category 4: Electronics
            Smartphones = 8,
            Laptops,

            // Category 5: Home & Kitchen
            Furniture = 10,
            Cookware,

            // Category 6: Sports & Outdoors
            RunningGear = 12,
            CampingEquipment
        }

    }
}
