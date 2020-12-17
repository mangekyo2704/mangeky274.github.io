using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.Data.Entities
{
    public class ProductImage
    {
        public int productId { get; set; }
        public int productImageId { get; set; }
        public string imagepath { get; set; }
        public string caption { get; set; }
        public bool isDefault { get; set; }
        public Product  Product { get; set; }
    }
}
