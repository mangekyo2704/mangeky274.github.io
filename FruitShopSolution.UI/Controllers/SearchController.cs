using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.UI.Models;
using FruitShopSolution.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Mvc;

namespace FruitShopSolution.UI.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductService _proService;
        public SearchController(IProductService product)
        {
            _proService = product;
        }
        public async Task<IActionResult> Index(string Search)
        {
            List<ProductInfoViewModel> listProduct;
            if (Search == null)
            {
                listProduct = await _proService.GetAllProduct();
            }
             listProduct =  await _proService.Searching(Search);
            ViewBag.Keyword = Search;
            ViewBag.Products = listProduct;
            ViewBag.User = new SessionService(HttpContext.Session).GetUserLogined();
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }
    }
}
