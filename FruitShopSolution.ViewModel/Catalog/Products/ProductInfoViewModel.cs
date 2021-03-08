using FruitShopSolution.ViewModel.Catalog.Categories;
using FruitShopSolution.ViewModel.Catalog.Promotion;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Products
{
    public class ProductInfoViewModel
    {
        public ProductViewModel pro { get; set; }
        public List<CategoriesViewModel> Categories { get; set; }
        public List<ProductImageViewModel> ListImages { get; set; }
        public int Discount { get; set; }
    }
}
