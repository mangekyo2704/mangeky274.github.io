using FruitShopSolution.ViewModel.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Text;
namespace FruitShopSolution.ViewModel.Catalog.Cart
{
    public class CartViewModel
    {
        public ProductInfoViewModel Products { get; set; }
        public int Quantity { get; set; }
    }
}
