using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Categories
{
    public class CategoriesViewModel
    {
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
