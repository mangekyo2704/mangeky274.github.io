using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Report
{
    public class ReportViewModel
    {
        public DateTime Date { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal Discount { get; set; }
        public decimal Profit { get; set; }
        public decimal Capital { get; set; }
        public int OrderCount { get; set; }
    }
}
