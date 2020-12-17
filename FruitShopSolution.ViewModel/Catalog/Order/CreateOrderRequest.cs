using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Order
{
    public class CreateOrderRequest
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public string ShipAddress { get; set; }
        public string ShipName { get; set; }
        public string ShipEmail { get; set; }
        public string ShipPhoneNumber { get; set; }
        public int Discount { get; set; }
        public decimal ShipPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CreateOrderDetailRequest> ListProduct{ get; set; }
    }
}
