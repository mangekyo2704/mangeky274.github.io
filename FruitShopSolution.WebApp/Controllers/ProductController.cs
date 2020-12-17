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
        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            //ViewData.Model = _proService.GetAll();
            var request = new GetManageProductPagingRequest()
            {
                Keywork = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryIds = categoryId
            };
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
            if(categoryId.HasValue)
            return View(data);
            return View( _proService.GetAll());
        } 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            //ViewData.Model = _proService.GetAll();
            request.DateCreated = DateTime.Now;
            var result = await _proService.Create(request);
            if (result > 0)
            {

                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }
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

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateRequest request)
        {

            var result = await _proService.Update(request);
            if (result > 0)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }
            return View(request);
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
