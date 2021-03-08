using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Order
{
    public class UpdateOrderRequest
    {
        public int OrderId { get; set; }
        public int Status { get; set; }
    }
}
