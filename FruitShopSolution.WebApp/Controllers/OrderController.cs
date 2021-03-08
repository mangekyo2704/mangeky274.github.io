using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Order;
using FruitShopSolution.Data.Enums;
using FruitShopSolution.ViewModel.Catalog.Order;
using FruitShopSolution.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FruitShopSolution.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index(int? status = 0)
        {
            var orders = await _orderService.GetAllOrders();
            if (status.HasValue)
            {
                orders = await _orderService.GetOrderByStatus((int)status-1);
            }
            var products = new List<ProductInfoViewModel>();
            /*            ViewBag.User = user;*/
            List<SelectListItem> listStatus = new List<SelectListItem>();
            List<string> ListStat = new List<string>();
            ListStat.Add("InProgress");
            ListStat.Add("Confirmed");
            ListStat.Add("Shipping");
            ListStat.Add("Success");
            ListStat.Add("Cancel");
            ListStat.Add("Unpaid");
            ListStat.Add("Paid");
            int count = 0;
            foreach (var i in ListStat)
            {
                count++;
                listStatus.Add(
                    new SelectListItem()
                    {
                        Text = i,
                        Value = count.ToString(),
                        Selected = status.HasValue && status.Value == count
                    });
            }
            ViewBag.Status = listStatus;
            if (orders.Count != 0)
            {
                orders = orders.OrderByDescending(c => c.OrderId).ToList();
                ViewBag.Order = orders;

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
            else ViewBag.Order = new OrderViewModel();
            return View(orders);
        }

        public async Task<IActionResult> NewOrder()
        {
            var orders = await _orderService.GetAllOrders();
            orders = orders.Where(x => x.Status == "InProgress").ToList();
            if (orders != null)
            {
                orders = orders.OrderByDescending(c => c.OrderId).ToList();
                ViewBag.Order = orders;
            }
            else ViewBag.Order = new OrderViewModel();
            return View(orders);
        }
        public async Task<IActionResult> ComfirmOrder()
        {
            var orders = await _orderService.GetAllOrders();
            var list = orders.Where(x => x.Status == "Confirmed").ToList();
            if (orders != null)
            {
                orders = orders.OrderByDescending(c => c.OrderId).ToList();
                ViewBag.Order = orders;
            }
            else ViewBag.Order = new OrderViewModel();
            return View(list);
        }
        public async Task<IActionResult> CancelOrder()
        {
            var orders = await _orderService.GetAllOrders();
            orders = orders.Where(x => x.Status == "Cancel").ToList();
            if (orders != null)
            {
                orders = orders.OrderByDescending(c => c.OrderId).ToList();
                ViewBag.Order = orders;
            }
            else ViewBag.Order = new OrderViewModel();
            return View(orders);
        }

        public async Task<IActionResult> ShippingOrder()
        {
            var orders = await _orderService.GetAllOrders();
            orders = orders.Where(x => x.Status == "Shipping").ToList();
            if (orders != null)
            {
                orders = orders.OrderByDescending(c => c.OrderId).ToList();
                ViewBag.Order = orders;
            }
            else ViewBag.Order = new OrderViewModel();
            return View(orders);
        }
        public async Task<IActionResult> SuccessOrder()
        {
            var orders = await _orderService.GetAllOrders();
            orders = orders.Where(x => x.Status == "Success").ToList();
            if (orders != null)
            {
                orders = orders.OrderByDescending(c => c.OrderId).ToList();
                ViewBag.Order = orders;
            }
            else ViewBag.Order = new OrderViewModel();
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var order = await _orderService.GetOrderById(id);
            List<SelectListItem> listStatus = new List<SelectListItem>();
            List<string> ListStat = new List<string>();
            ListStat.Add("InProgress");
            ListStat.Add("Confirmed");
            ListStat.Add("Shipping");
            ListStat.Add("Success");
            ListStat.Add("Cancel");
            ListStat.Add("Packed");
            ListStat.Add("Delivered");
            int count = 0;
            foreach (var i in ListStat)
            {
                count++;
                listStatus.Add(
                    new SelectListItem()
                    {
                        Text = i,
                        Value = count.ToString(),
                        Selected = (order.Status == i)
                    });
            }
            ViewBag.Status = listStatus;
            ViewBag.Order = order;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(UpdateOrderRequest request)
        {
            if (request.Status == 1) await _orderService.Comfirm(request);
            else
            await _orderService.UpdateStatus(request);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Shipping(int orderId)
        {
            UpdateOrderRequest request = new UpdateOrderRequest()
            {
                OrderId = orderId,
                Status = 2
            }; 
            if(await _orderService.Comfirm(request))
                return Ok();
            return BadRequest("Đã hết hàng!");
        }
        public async Task<IActionResult> Cancel(int orderId)
        {
            UpdateOrderRequest request = new UpdateOrderRequest()
            {
                OrderId = orderId,
                Status = 4
            };
            await _orderService.UpdateStatus(request);
            return Ok();
        }
        public async Task<IActionResult> Complete (int orderId)
        {
            UpdateOrderRequest request = new UpdateOrderRequest()
            {
                OrderId = orderId,
                Status = 3
            };
            await _orderService.UpdateStatus(request);
            return Ok();
        }
        public async Task<IActionResult> Comfirm(int orderId,int status)
        {
            UpdateOrderRequest request = new UpdateOrderRequest()
            {
                OrderId = orderId,
                Status = status
            };
            await _orderService.UpdateStatus(request);
            return Ok();
        }
        public async Task<IActionResult> ViewOrderDetail(int id)
        {
            var orderdetails =  await _orderService.GetOrderDetails(id);
            ViewBag.OrderDetail = orderdetails;
            return View(orderdetails);
        }
    }
}
