using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Order
{
    public class CreateOrderDetailRequest
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quanity { set; get; }
        public Decimal Price { set; get; }
        public int Discount { set; get; }

    }
}
