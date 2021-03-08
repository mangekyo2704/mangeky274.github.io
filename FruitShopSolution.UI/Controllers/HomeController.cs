using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FruitShopSolution.UI.Models;
using FruitShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using FruitShopSolution.ViewModel.Catalog.Users;
using FruitShopSolution.Application.Catalog.Promotion;

namespace FruitShopSolution.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /*        private readonly IProductManageService _proService;*/
        private readonly IProductService _proService;
        private readonly IPromotionService _promotionService;
        //private readonly IProductImageService _proImageService;
        //private readonly ICategoryService _categoryService;
        /*        public HomeController(IProductManageService proService)
                {
        *//*            _proService = proService;*//*
                    // _proImageService = proImageService;
                }*/
        public HomeController(ILogger<HomeController> logger, IProductService productService,IPromotionService promotionService)
        {
            _logger = logger;
            _proService = productService;
            _promotionService = promotionService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _proService.GetAllProduct();
            foreach(var i in products)
            {
                i.Discount = await _promotionService.GetPromotionOfProduct(i.pro.ProductId);
            }
            ViewBag.Product = products;

            ViewBag.User = new SessionService(HttpContext.Session).GetUserLogined();
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.User = new SessionService(HttpContext.Session).GetUserLogined();
            ViewBag.Products = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
