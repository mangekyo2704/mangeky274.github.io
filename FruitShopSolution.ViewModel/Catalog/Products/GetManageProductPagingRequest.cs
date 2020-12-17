using FruitShopSolution.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Products.Manage
{
    public class GetManageProductPagingRequest : PageRequestBase
    {
        public string Keywork { get; set; }
        public int? CategoryIds { get; set; }
    }
}
