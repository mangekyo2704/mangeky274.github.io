using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Order;
using FruitShopSolution.Application.Catalog.Users;
using FruitShopSolution.UI.Models;
using FruitShopSolution.ViewModel.Catalog.Order;
using FruitShopSolution.ViewModel.Catalog.Products;
using FruitShopSolution.ViewModel.Catalog.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FruitShopSolution.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IProductService _proService;
        public UserController(IUserService userService, IOrderService orderService, IProductService proService)
        {
            _userService = userService;
            _orderService = orderService;
            _proService = proService;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var user = GetUserLogined();
            if (user.LastName == null)
                return View();
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            string exception = "";
            //if (ModelState.IsValid)
            {

                try
                {
                    //var user = await _userService.Accuracy(new LoginRequest() { UserName = username,Password=password});
                    var user = await _userService.Accuracy(request);
                    SaveSession(user);
                }
                catch (Exception e)
                {
                    exception = e.Message;
                    ViewBag.Exception = e.Message;
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            ClearSession();
            return RedirectToAction("Login", "User");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            int id;
            try
            {
                id = await _userService.Register(request);
            }
            catch (Exception e)
            {
                ViewBag.Exception = e.Message;
                return View();
            }
            UserViewModel user = new UserViewModel()
            {
                UserName = request.UserName,
                Password = request.Password,
                BirthDay = request.BirthDay,
                Email = request.Email,
                FristName = request.FristName,
                LastName = request.LastName,
                Phone = request.Phone,
                Gender = request.Gender,
                UserId = id
            };
            SaveSession(user);
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> UpdateInfoUser(int id, string firstname, string lastname, string email, string phone, string gender, string birthday)
        {
            var request = new UpdateRequest()
            {
                BirthDay = DateTime.Parse(birthday),
                Email = email,
                FristName = firstname,
                Gender = gender,
                LastName = lastname,
                Phone = phone,
                UserId = id
            };
            if (await _userService.Update(request)) return Ok();
            return BadRequest();
        }
        public async Task<IActionResult> UpdatePass(int id, string oldpass, string newpass, string newpassagain)
        {
            var request = new UpdatePassRequest()
            {
                UserId = id,
                NewPass = newpass,
            };
            var user = GetUserLogined();
            if (user.FristName != null && user.Password != oldpass) return Ok("Mật khẩu không khớp!");
            if (newpass != newpassagain) return Ok("Mật khẩu mới không khớp!");
            if (await _userService.UpdatePass(request))
                return Ok("Cập nhật thành công!");
            else return BadRequest("Cập nhật thất bại");
        }
        void SaveSession(UserViewModel user)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(user);
            session.SetString("user", jsoncart);
        }
        void ClearSession()
        {
            var session = HttpContext.Session;
            session.Remove("user");
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
        public async Task<IActionResult> ViewProfile()
        {
            ViewBag.User = new SessionService(HttpContext.Session).GetUserLogined();
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }
        public async Task<IActionResult> ViewInfo()
        {
            ViewBag.User = new SessionService(HttpContext.Session).GetUserLogined();
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }

        public async Task<IActionResult> ViewInfoUser()
        {
            var user = GetUserLogined();
            /*            ViewBag.User = user;*/
            if (user.UserName == null)
            {
                ViewBag.User = await _userService.GetById(1);
            }
            else ViewBag.User = user;
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }
        public async Task<IActionResult> ViewOrder()
        {
            var user = GetUserLogined();
            var orders = await _orderService.GetOrders(user.UserId);
            var products = new List<ProductInfoViewModel>();
            /*            ViewBag.User = user;*/
            if (orders.Count != 0)
            {
                orders = orders.OrderByDescending(x => x.OrderId).ToList();
                ViewBag.Order = orders;
                foreach (var i in orders)
                {
                    i.OrderDetails = await _orderService.GetOrderDetails(i.OrderId);
                    foreach (var orderdetail in i.OrderDetails)
                    {
                        orderdetail.Products = _proService.GetProductById(orderdetail.ProductId);
                    }
                }
                ViewBag.OrderDetail = products;
            }
            else ViewBag.Order = null;
            ViewBag.User = user;
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }

        public async Task<IActionResult> OrderInprocess()
        {
            var user = GetUserLogined();
            var orders = await _orderService.GetOrders(user.UserId);
            orders = orders.Where(x => x.Status == "InProgress").ToList();
            var products = new List<ProductInfoViewModel>();
            /*            ViewBag.User = user;*/
            if (orders.Count != 0)
            {
                orders = orders.OrderByDescending(x => x.OrderId).ToList();
                ViewBag.Order = orders;
                foreach (var i in orders)
                {
                    i.OrderDetails = await _orderService.GetOrderDetails(i.OrderId);
                    foreach (var orderdetail in i.OrderDetails)
                    {
                        orderdetail.Products = _proService.GetProductById(orderdetail.ProductId);
                    }
                }
                ViewBag.OrderDetail = products;
            }
            else ViewBag.Order = null;
            ViewBag.User = user;
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }

        public async Task<IActionResult> OrderShipping()
        {
            var user = GetUserLogined();
            var orders = await _orderService.GetOrders(user.UserId);
            orders = orders.Where(x => x.Status == "Delivered").ToList();
            var products = new List<ProductInfoViewModel>();
            /*            ViewBag.User = user;*/
            if (orders.Count != 0)
            {
                orders = orders.OrderByDescending(x => x.OrderId).ToList();
                ViewBag.Order = orders;
                foreach (var i in orders)
                {
                    i.OrderDetails = await _orderService.GetOrderDetails(i.OrderId);
                    foreach (var orderdetail in i.OrderDetails)
                    {
                        orderdetail.Products = _proService.GetProductById(orderdetail.ProductId);
                    }
                }
                ViewBag.OrderDetail = products;
            }
            else ViewBag.Order = null;
            ViewBag.User = user;
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }

        public async Task<IActionResult> OrderWaitingGetProduct()
        {
            var user = GetUserLogined();
            var orders = await _orderService.GetOrders(user.UserId);
            var ordercomfirmed = orders.Where(x => x.Status == "Confirmed").ToList();
            var orderpacked = orders.Where(x => x.Status == "Packed").ToList();
            var neworders = new List<OrderViewModel>();
            neworders.AddRange(orderpacked);
            neworders.AddRange(ordercomfirmed);
            var products = new List<ProductInfoViewModel>();
            ViewBag.User = user;
            if (neworders.Count != 0)
            {
                neworders = neworders.OrderByDescending(x => x.OrderId).ToList();
                ViewBag.Order = neworders;
                foreach (var i in neworders)
                {
                    i.OrderDetails = await _orderService.GetOrderDetails(i.OrderId);
                    foreach (var orderdetail in i.OrderDetails)
                    {
                        orderdetail.Products = _proService.GetProductById(orderdetail.ProductId);
                    }
                }
                ViewBag.OrderDetail = products;
            }
            else ViewBag.Order = null;
            ViewBag.User = user;
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }

        public async Task<IActionResult> OrderSuccess()
        {
            var user = GetUserLogined();
            var orders = await _orderService.GetOrders(user.UserId);
            orders = orders.Where(x => x.Status == "Success").ToList();
            var products = new List<ProductInfoViewModel>();
            /*            ViewBag.User = user;*/
            if (orders.Count != 0)
            {
                orders = orders.OrderByDescending(x => x.OrderId).ToList();
                ViewBag.Order = orders;
                foreach (var i in orders)
                {
                    i.OrderDetails = await _orderService.GetOrderDetails(i.OrderId);
                    foreach (var orderdetail in i.OrderDetails)
                    {
                        orderdetail.Products = _proService.GetProductById(orderdetail.ProductId);
                    }
                }
                ViewBag.OrderDetail = products;
            }
            else ViewBag.Order = null;
            ViewBag.User = user;
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }

        public async Task<IActionResult> OrderCancel()
        {
            var user = GetUserLogined();
            var orders = await _orderService.GetOrders(user.UserId);
            orders = orders.Where(x => x.Status == "Canceled").ToList();
            var products = new List<ProductInfoViewModel>();
            /*            ViewBag.User = user;*/
            if (orders.Count != 0)
            {
                orders = orders.OrderByDescending(x => x.OrderId).ToList();
                ViewBag.Order = orders;
                foreach (var i in orders)
                {
                    i.OrderDetails = await _orderService.GetOrderDetails(i.OrderId);
                    foreach (var orderdetail in i.OrderDetails)
                    {
                        orderdetail.Products = _proService.GetProductById(orderdetail.ProductId);
                    }
                }
                ViewBag.OrderDetail = products;
            }
            else ViewBag.Order = null;
            ViewBag.User = user;
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }

        public async Task<IActionResult> DeleteOrder(int OrderId)
        {
            await _orderService.DeleteOrder(OrderId);
            return Ok();
        }
    }
}
