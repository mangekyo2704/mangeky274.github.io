using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Admin;
using FruitShopSolution.Application.Catalog.Categories;
using FruitShopSolution.ViewModel.Catalog.Admin;
using FruitShopSolution.WebApp.Models;
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
        [Route("admin-page",Name = "admin")]
        [Route("/admin/index")]
        //[Route("")]
        public IActionResult Index()
        {
            /*            if (checkLogin)
                        {*/
            var admin = new SessionService<AdminViewModel>(HttpContext.Session).GetItems("admin");
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
            var user = new SessionService<AdminViewModel>(HttpContext.Session).GetItems("admin");
            if (user.Name == null)
                return View();
            else return RedirectToAction("Index","Home");
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
                new SessionService<AdminViewModel>(HttpContext.Session).SaveCartSession(result, "admin");
                return RedirectToAction("Index","Home");
            }
        }
        public IActionResult Logout()
        {
            new SessionService<AdminViewModel>(HttpContext.Session).ClearSession("admin");
            return RedirectToAction("Login");
        }
        public IActionResult ViewProduct()
        {
            return View();
        }
    }
}
