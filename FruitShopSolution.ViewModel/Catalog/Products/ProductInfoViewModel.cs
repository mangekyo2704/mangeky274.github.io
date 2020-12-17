using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Products
{
    public class ProductInfoViewModel
    {
        public ProductViewModel pro { get; set; }
        public List<ProductImageViewModel> ListImages { get; set; }
    }
}
