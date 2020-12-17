using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Products
{
    public class ProductImageViewModel
    {
        public int productId { get; set; }
        public int productImageId { get; set; }
        public string imagepath { get; set; }
        public string caption { get; set; }
        public bool isDefault { get; set; }
    }
}
