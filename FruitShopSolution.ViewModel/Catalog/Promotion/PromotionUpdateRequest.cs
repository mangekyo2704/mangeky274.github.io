using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Promotion
{
    public class PromotionUpdateRequest
    {
        public int PromotionId { get; set; }
        public int DiscountPercent { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
