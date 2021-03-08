using FruitShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int? UserId { get; set; }
        public string Shipname { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public string ShipPhoneNumber { get; set; }
        public StatusOrder Status { get; set; }
        public int Payment { get; set; }
        public int Discount { get; set; }
        public decimal ShipPrice { get; set; }
        public decimal  TotalPrice { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
