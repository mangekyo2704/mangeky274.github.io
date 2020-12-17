using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FruitShopSolution.Data.Entities
{
    public class OrderDetail
    {   
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quanity { set; get; }
        public Decimal Price { set; get; }
        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}
