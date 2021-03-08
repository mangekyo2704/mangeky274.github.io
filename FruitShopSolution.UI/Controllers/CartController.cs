using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FruitShopSolution.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Products = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }
        public IActionResult Add()
        {
            ViewBag.Products = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }
    }
}
