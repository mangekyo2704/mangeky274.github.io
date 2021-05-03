using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.Data.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public AppUser User { get; set; }
        public Product Product { get; set; }

    }
}
