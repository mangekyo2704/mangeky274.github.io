using FruitShopSolution.Data.EF;
using FruitShopSolution.ViewModel.Catalog.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FruitShopSolution.Data.Enums;

namespace FruitShopSolution.Application.Catalog.Report
{
    public class ReportService:IReportService
    {
        private readonly FruitShopDbContext _context;
        public ReportService(FruitShopDbContext context)
        {
            _context = context;
        }
        public async Task<ReportViewModel> GetRevenuePerDay(DateTime date)
        {
            /*           var query = from o in _context.Orders
                                   join od in _context.OrderDetails on o.OrderId equals od.OrderId
                                   where o.OrderDate.ToShortDateString() == date.ToShortDateString()
                                   select new { o, od };
                       var arr = await query.Select(x =>
                       new ReportViewModel()
                       {
                           Date = date,
                           Discount = query.FirstOrDefault().o.Discount,
                           TotalRevenue = query.Select(x => x.od.Price).ToList().Sum(),
                           Capital = query.Select(x => x.od.Product.InputCount * x.od.Quanity).Sum()
                       }
                       ).ToListAsync();*/
            var query = from o in _context.Orders where o.OrderDate.Date == date.Date select o;

            var query1 = query.Where(x=>x.Status == (StatusOrder)3).ToList();
            decimal total = 0;
            decimal totalcapital = 0;
            int discount = 0;
            int ordercount = 0;
            foreach(var i in query1)
            {
                var orderDetails = _context.OrderDetails.Where(x => x.OrderId == i.OrderId).ToList();
                total += i.TotalPrice;
                discount += i.Discount;
                ordercount++;
                foreach(var j in orderDetails)
                {
                    var products = _context.Products.Where(x => x.ProductId == j.ProductId).First();
                    totalcapital += j.Quanity * products.InputCount;
                }
            }
            ReportViewModel report = new ReportViewModel()
            {
                Date = date,
                Discount = discount,
                TotalRevenue = total,
                Capital = totalcapital,
                OrderCount = ordercount
            };
            report.Date = date;

            report.Profit = report.TotalRevenue - report.Capital;
            return report;
        }
        public async Task<ReportViewModel> GetRevenueFromDays(DateTime date1,DateTime date2)
        {

            var query = _context.Orders.Where(x => x.OrderDate.Date <= date1.Date && x.OrderDate >= date2.Date);
/*            var query = from o in _context.Orders
                        join od in _context.OrderDetails on o.OrderId equals od.OrderId
                        where o.OrderDate.Date <= date1.Date && o.OrderDate >= date2.Date
                        select new { o, od };*/
            var query1 = query.ToList();
            decimal total = 0;
            decimal totalcapital = 0;
            int discount = 0;
            DateTime date = new DateTime();
            foreach(var i in query1)
            {
                var orderDetails = _context.OrderDetails.Where(x => x.OrderId == i.OrderId).ToList();
                date = i.OrderDate;
                total += i.TotalPrice;
                discount += i.Discount;
                foreach (var j in i.OrderDetails)
                {
                    var products = _context.Products.Where(x => x.ProductId == j.ProductId).First();
                    totalcapital += j.Quanity * products.InputCount;
                }
            }
            ReportViewModel report = new ReportViewModel()
            {
                Date = date,
                Discount = discount,
                TotalRevenue = total,
                Capital = totalcapital,
            };
            report.Date = date;
            report.Profit = report.TotalRevenue - report.Capital;
            return report;
        }
    }
}
