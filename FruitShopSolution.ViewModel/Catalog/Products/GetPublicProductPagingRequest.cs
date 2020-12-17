using FruitShopSolution.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Products
{
    public class GetPublicProductPagingRequest : PageRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
