using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FruitShopSolution.Application.Catalog.Products;
using FruitShopSolution.ViewModel.Catalog.Products;
using FruitShopSolution.Application.Catalog.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;
using FruitShopSolution.ViewModel.Catalog.Categories;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using FruitShopSolution.ViewModel.Common;

namespace FruitShopSolution.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManageService _proService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductManageService proService, ICategoryService categoryService)
        {
            _proService = proService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 5)
        {
            //ViewData.Model = _proService.GetAll();
            var request = new GetManageProductPagingRequest()
            {
                Keywork = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryIds = categoryId
            };
            ViewBag.Page = new PageModel() { PageIndex = pageIndex, TotalPage = 5 };
            var data = await _proService.GetAllPaging(request);
            var categories = _categoryService.GetAll();
            /*            ViewBag.Categories = await categories.Select(x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString(),
                            Selected = categoryId.HasValue && categoryId.Value == x.Id
                        });*/
            List<SelectListItem> listCategories = new List<SelectListItem>();
            foreach (var i in categories)
            {
                listCategories.Add(
                    new SelectListItem()
                    {
                        Text = i.Title,
                        Value = i.CategoryId.ToString(),
                        Selected = categoryId.HasValue && categoryId.Value == i.CategoryId
                    });
            }
            ViewBag.Categories = listCategories;
            ViewBag.Categories2 = categories;
            //if(categoryId.HasValue)
            return View(data);
            //return View(_proService.GetAll());
        }
        [HttpPost]
        public IActionResult GetProduct(string searchString, int category)
        {

            return Ok(new { searchString, category });
            //return RedirectToAction("Index", new { keyword = keyword, categoryId = categoryId });
        }
        [BindProperty]
        public int[] selectedCategories { set; get; }
        [BindProperty]
        public string Unit { set; get; }
        [HttpGet]
        public IActionResult Create(int? categoryId)
        {
            var categories = _categoryService.GetAll();
            /*            List<SelectListItem> listCategories = new List<SelectListItem>();
                        foreach (var i in categories)
                        {
                            listCategories.Add(
                                new SelectListItem()
                                {
                                    Text = i.Title,
                                    Value = i.CategoryId.ToString(),
                                    Selected = categoryId.HasValue && categoryId.Value == i.CategoryId
                                });
                        }
                        ViewBag.Categories = listCategories;*/
            ViewBag.Categories = new MultiSelectList(categories, "CategoryId", "Title");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        { 
            request.DateCreated = DateTime.Now;
            request.categoryId = selectedCategories;
            //ViewData.Model = _proService.GetAll();
            if (ModelState.IsValid)
            {                             
                var result = await _proService.Create(request);
                if (result > 0)
                {

                    TempData["result"] = "Thêm mới sản phẩm thành công";
                    return RedirectToAction("Index");
                }                
            }
            var categories = _categoryService.GetAll();
            ViewBag.Categories = new MultiSelectList(categories, "CategoryId", "Title");
            return View(request);
        }
        /*        [HttpGet("{id}")]
                public async Task<IActionResult> Update(int id)
                {
                    var product = await _proService.GetById(id);
                    var editVm = new ProductUpdateRequest()
                    {
                        Content=product.Content,
                        //InputCount=product.InputCount,
                        Origin=product.Origin,
         *//*               OutputCount=product.OutputCount,
                        Quantity=product.Quantity,*//*
                        Title=product.Title
                    };
                    return View(editVm);
                }*/
        public async Task<IActionResult> Update(int id)
        {
            var product = await _proService.GetById(id);
            ViewBag.Product = product;
            ViewBag.Image = await _proService.GetProductImages(id);
            return View();
        }
        /*        [BindProperty]
                public string Content { get; set; }*/
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _proService.Update(request);
                if (result > 0)
                {
                    TempData["result"] = "Cập nhật sản phẩm thành công";
                    return RedirectToAction("Index");
                }
                var product = await _proService.GetById(request.ProductId);
                ViewBag.Product = product;
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _proService.Delete(id);
            if (result > 0)
            {
                TempData["result"] = "Xoá sản phẩm thành công";
            }
            else TempData["result"] = "Xoá sản phẩm thất bại";
            return RedirectToAction("Index");
        }
    }
}
