using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Report;
using FruitShopSolution.ViewModel.Catalog.Report;
using Microsoft.AspNetCore.Mvc;

namespace FruitShopSolution.WebApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<IActionResult> Index(DateTime? fromdate,DateTime? todate)
        {
            var t = new DateTime(2020, 12, 11);
            List<ReportViewModel> listReports = new List<ReportViewModel>();
            while (t <= DateTime.Now.Date)
            {
                listReports.Add(await _reportService.GetRevenuePerDay(t));
                t=t.AddDays(1);
            }
            foreach(var i in listReports)
            {
                Console.WriteLine(i.TotalRevenue);
            }
            return View(await _reportService.GetRevenuePerDay(t));
        }
    }
}
