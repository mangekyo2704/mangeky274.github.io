using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Categories;
using FruitShopSolution.Application.Catalog.Products;
using FruitShopSolution.Application.Catalog.Promotion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FruitShopSolution.ViewModel.Catalog.Promotion;
using FruitShopSolution.ViewModel.Catalog.Products;

namespace FruitShopSolution.WebApp.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IPromotionService promotionService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public PromotionController(IPromotionService _promotionService,IProductService _productService, ICategoryService _categoryService)
        {
            promotionService = _promotionService;
            productService = _productService;
            categoryService = _categoryService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Promotions = await promotionService.GetPromotions();
            return View();
        }
        public async Task<IActionResult> Create()
        {

            var categories =  categoryService.GetAll();
            //           List<SelectListItem> listCategories = new List<SelectListItem>();
            /*            if (categories != null)
                        {
                            int count = 0;
                            foreach (var i in categories)
                            {
                                var select = new SelectListItem()
                                {
                                    Text = i.Title,
                                    Value = i.CategoryId.ToString(),
                                    Selected =  count == i.CategoryId
                                };
                                listCategories.Add(select);
                            }
                        }*/
            //ViewBag.Categories = new MultiSelectList(categories, "CategoryId", "Title");
            ViewBag.Categories = categories;
            ViewBag.Promotions = await promotionService.GetPromotions();
            ViewBag.Products = productService.GetAllProduct();
            return View();
        }
        public async Task<IActionResult> ProductInPromotion(int? promotionId)
        {
            var products =  productService.GetAllProduct();
            /*            if (status.HasValue)
                        {
                            orders = await _orderService.GetOrderByStatus((int)status - 1);
                        }*/
            var promotions = await promotionService.GetPromotions();
            /*            ViewBag.User = user;*/
            List<SelectListItem> listPromotion = new List<SelectListItem>();
            if(promotions != null)
            {
                foreach (var i in promotions)
                {
                    var select = new SelectListItem()
                    {
                        Text=i.Title,
                        Value = i.PromotionId.ToString(),
                        Selected = promotionId.HasValue && promotionId.Value == i.PromotionId
                    };
                    listPromotion.Add(select);
                }
            }
            ViewBag.Promotions = listPromotion;
            if (products.Count != 0)
            {
                ViewBag.Products = products;

                /*                foreach (var i in orders)
                                {
                                    i.OrderDetails = await _orderService.GetOrderDetails(i.OrderId);
                                    foreach (var orderdetail in i.OrderDetails)
                                    {
                                        orderdetail.Products = _proService.GetProductById(orderdetail.ProductId);
                                    }
                                }
                                ViewBag.OrderDetail = products;*/
            }
            else ViewBag.Order = null;
            return View(products);
        }

        public async Task<IActionResult> CreatePromotion(int choose,string Title,int DiscountPercent,string FromDate,string ToDate,string Content,int[] categoryId,int[] productIds)
        {
            if (choose == 1)
            {
                var date1 = DateTime.Parse(FromDate);
                var date2 = DateTime.Parse(ToDate);
                var request = new PromotionCreateRequest()
                {
                    Title = Title,
                    DiscountPercent = DiscountPercent,
                    FromDate = date1,
                    ToDate = date2,
                    Content = Content
                };
                var check = await promotionService.CreatePromotionForAllProduct(request);
                if (check) return RedirectToAction("Index");
            }
            else if (choose == 2)
            {
                var date1 = DateTime.Parse(FromDate);
                var date2 = DateTime.Parse(ToDate);
                var request = new PromotionCreateRequest()
                {
                    Title = Title,
                    DiscountPercent = DiscountPercent,
                    FromDate = date1,
                    ToDate = date2,
                    Content = Content
                };
                foreach (var i in categoryId)
                {
                    if (!(await promotionService.CreatePromotionForCategory(request, i))) return BadRequest();
                }
                return RedirectToAction("Index");
            }
            else if (choose == 3)
            {
                var date1 = DateTime.Parse(FromDate);
                var date2 = DateTime.Parse(ToDate);
                var request = new PromotionCreateRequest()
                {
                    Title = Title,
                    DiscountPercent = DiscountPercent,
                    FromDate = date1,
                    ToDate = date2,
                    Content = Content,
                    ProductIds = productIds.ToList()
                };
                if (!(await promotionService.CreatePromotion(request))) return BadRequest();
                 return RedirectToAction("Index");
            }
                return BadRequest();
        }
        public async  Task<IActionResult> ViewDetail(int promotionId)
        {
            
            var productIds = await promotionService.GetListProductIdInPromotion(promotionId);
            var products = new List<ProductInfoViewModel>();
            if(productIds.Count > 0)
            {
                foreach(var i in productIds)
                {
                    products.Add( productService.GetProductById(i));
                }
                ViewBag.Products = products;
                ViewBag.Promotions = await promotionService.GetPromotionsById(promotionId);
            }
            return View(products);
        }
        public async Task<IActionResult> CancelPromotions(int cancelPromotionId)
        {
            if (await promotionService.UpdateStatus(cancelPromotionId, 0)) return Ok();
            return BadRequest();
        }
    }
}
