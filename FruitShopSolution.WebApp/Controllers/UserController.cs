using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Data.EF;
using FruitShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using FruitShopSolution.ViewModel.Catalog.Users;
using FruitShopSolution.Application.Catalog.Users;

namespace FruitShopSolution.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAll());
        }

        public async Task<IActionResult> ViewDetails(int id)
        {
            return View(await _userService.GetById(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if (result > 0)
            {
                TempData["result"] = "Xoá sản phẩm thành công";
            }
            else TempData["result"] = "Xoá sản phẩm thất bại";
            return RedirectToAction("Index");
        }
    }

}
