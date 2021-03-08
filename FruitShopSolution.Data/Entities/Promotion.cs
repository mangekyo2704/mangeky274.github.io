using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Data.Entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public int DiscountPercent { get; set; }
        public string Title { get; set; }
        public string  Content { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Status { get; set; }

    }
}
