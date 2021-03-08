using System;
using System.Collections.Generic;
using System.Text;
using FruitShopSolution.ViewModel.Catalog.Products;

namespace FruitShopSolution.ViewModel.Catalog.Order
{
    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }
        public int OrderId { set; get; }
        public int Quanity { set; get; }
        public decimal Price { set; get; }
        public ProductInfoViewModel Products { get; set; }
    }
}