using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FruitShopSolution.WebApp.Models;
using FruitShopSolution.Application.Catalog.Report;
using FruitShopSolution.ViewModel.Catalog.Report;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using FruitShopSolution.ViewModel.Catalog.Admin;

namespace FruitShopSolution.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReportService _service;

        public HomeController(ILogger<HomeController> logger, IReportService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var admin = new SessionService<AdminViewModel>(HttpContext.Session).GetItems("admin");
            if (admin.UserName != null)
            {
                ViewData.Model = admin;

            List<ReportViewModel> listReport = new List<ReportViewModel>();
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();
            var _session = HttpContext.Session;
            string jsoncart = _session.GetString("FromDate");
            if (jsoncart != null)
            {
                 FromDate = DateTime.ParseExact(JsonConvert.DeserializeObject<string>(jsoncart), "d", null);
            }
            jsoncart = _session.GetString("ToDate");
            if (jsoncart != null)
            {
                 ToDate = DateTime.ParseExact(JsonConvert.DeserializeObject<string>(jsoncart), "d", null);
            }           
            if (FromDate.Year != 1 && ToDate.Year != 1)
            {
                DateTime newdate = FromDate;
                
                while (newdate.Date <= ToDate.Date)
                {
                    listReport.Add(await _service.GetRevenuePerDay(newdate));
                    newdate = newdate.AddDays(1);
                }
            }
            else
            {
                DateTime newdate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
                while (newdate <=DateTime.Now.Date)
                {
                    listReport.Add(await _service.GetRevenuePerDay(newdate));
                    newdate = newdate.AddDays(1);
                }
            } 
            ViewBag.Reports = listReport;
            ViewBag.Today = await _service.GetRevenuePerDay(DateTime.Now);
            return View();
            }
            else
                return RedirectToAction("Login","Admin");
        }
        public void GetDate(string fromdate,string todate)
        {
            var _session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(fromdate);
            _session.SetString("FromDate", jsoncart);
             jsoncart = JsonConvert.SerializeObject(todate);
            _session.SetString("ToDate", jsoncart);
        } 
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
