using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Cart;
using FruitShopSolution.Application.Catalog.Products;
using FruitShopSolution.Application.Catalog.Promotion;
using FruitShopSolution.UI.Models;
using FruitShopSolution.ViewModel.Catalog.Products;
using FruitShopSolution.ViewModel.Catalog.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FruitShopSolution.UI.Models;

namespace FruitShopSolution.UI.Controllers
{
    public class LoveController : Controller
    {

        private readonly ICartService _cartService;
        private readonly IPromotionService _promotionService;
        private readonly Models.IProductService _productService;

        public LoveController( ICartService cartService, IPromotionService promotionService, Models.IProductService productService)
        {
            _productService = productService;
            _cartService = cartService;
            _promotionService = promotionService;
        }
        public async Task<IActionResult> Index()
        {

            var user = GetUserLogined();
            
            if(user.UserName != null)
            {
                var products = new List<ProductInfoViewModel>();
                var loves = await _cartService.GetItems(user.UserId);
                foreach(var i in loves)
                {
                    products.Add( _productService.GetProductById(i.ProductId));
                }
                foreach (var i in products)
                {
                    i.Discount = await _promotionService.GetPromotionOfProduct(i.pro.ProductId);
                }
                ViewBag.User = user;
                ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
                ViewBag.Products = products;
            }
           
            return View();
        }
        public UserViewModel GetUserLogined()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString("user");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<UserViewModel>(jsoncart);
            }
            return new UserViewModel();
        }
    }
}
