using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Admin;
using FruitShopSolution.Application.Catalog.Categories;
using FruitShopSolution.ViewModel.Catalog.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FruitShopSolution.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        private bool checkLogin = false;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index(AdminViewModel admin)
        {
            /*            if (checkLogin)
                        {*/
            if (admin.UserName != null)
            {
                ViewData.Model = admin;
                return View();
            }
            else
                return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AdminLoginRequest request)
        {
            ViewData["thongbao"] = null;
            var result = _adminService.Accuracy(request);
            if (result == null)
            {
                ViewData["thongbao"] = "Dang nhap khong thanh cong";
                return View();
            }
            else
            {
                checkLogin = true;
                return RedirectToAction("Index", result);
            }
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
        public IActionResult ViewProduct()
        {
            return View();
        }
    }
}
