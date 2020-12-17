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

namespace FruitShopSolution.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /*        private readonly IProductManageService _proService;*/
        private readonly IProductService _proService;
        //private readonly IProductImageService _proImageService;
        //private readonly ICategoryService _categoryService;
        /*        public HomeController(IProductManageService proService)
                {
        *//*            _proService = proService;*//*
                    // _proImageService = proImageService;
                }*/
        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _proService = productService;
        }
        public IActionResult Index()
        {
            // List<ProductViewModel> listpro = await _proService.GetByCategory(1);
            //List<ProductInfoViewModel> listproinfo = new List<ProductInfoViewModel>();
            /*            foreach (var i in listpro)
                        {
                            List<ProductImageViewModel> listImages = await _proImageService.GetListProductImages(i.ProductId);
                            listproinfo.Add(
                                new ProductInfoViewModel()
                                {
                                    pro = i,
                                    listImages = listImages
                                });

                        }*/

            ViewBag.Product = _proService.GetAllProduct();

            return View();
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
