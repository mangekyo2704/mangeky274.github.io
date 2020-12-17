using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Products
{
    public class ProductUpdateRequest
    {
        public int ProductId { get; set; }
        public string Origin { get; set; }
        public string Title { get; set; }
        public decimal InputCount { get; set; }
        public decimal OutputCount { get; set; }
        public int Quantity { get; set; }
        public string Content { get; set; }
        public IFormFile ThumnailImage { get; set; }

    }
}
