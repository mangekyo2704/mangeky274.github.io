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
        public async Task<IActionResult> Index(string? keyword)
        {
            List<ProductInfoViewModel> listProduct =  await _proService.Searching(keyword);
            ViewBag.Keyword = keyword;
            return View(listProduct);
        }
    }
}
