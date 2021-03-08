using FruitShopSolution.Data.EF;
using FruitShopSolution.ViewModel.Catalog.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using FruitShopSolution.Data.Entities;
using FruitShopSolution.Data.Enums;
using FruitShopSolution.Application.Catalog.Products;
using Microsoft.EntityFrameworkCore;

namespace FruitShopSolution.Application.Catalog.Order
{
    public class OrderService : IOrderService
    {
        private readonly FruitShopDbContext context;
        private readonly IProductService _proService;
        public OrderService(FruitShopDbContext _context, IProductService service)
        {
            context = _context;
            _proService = service;
        }
        public async Task<List<OrderViewModel>> GetOrders(int UserId)
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            var query = context.Orders.Where(x => x.UserId == UserId);
            /*            var query = from o in context.Orders select o;
                        query = query.Where(x => x.UserId == UserId);*/
            foreach (var i in query)
            {
                orders.Add(
                    new OrderViewModel()
                    {
                        OrderId = i.OrderId,
                        OrderDate = i.OrderDate,
                        UserId = UserId,
                        Shipname = i.Shipname,
                        ShipAddress = i.ShipAddress,
                        ShipEmail = i.ShipEmail,
                        ShipPhoneNumber = i.ShipPhoneNumber,
                        Status = i.Status.ToString(),
                        Discount = i.Discount,
                        ShipPrice = i.ShipPrice,
                        TotalPrice = i.TotalPrice,
                        Payment = i.Payment
                    }); ;
            }
            return orders;
        }
        public async Task<List<OrderDetailViewModel>> GetOrderDetails(int OrderId)
        {
            List<OrderDetailViewModel> orderDetails = new List<OrderDetailViewModel>();
            var query = context.OrderDetails.Where(x => x.OrderId == OrderId);
            foreach (var i in query)
            {
                orderDetails.Add(
                    new OrderDetailViewModel()
                    {
                        OrderId = i.OrderId,
                        Price = i.Price,
                        Quanity = i.Quanity,
                        ProductId = i.ProductId
                    });
            }
            foreach (var i in orderDetails)
            {
                i.Products = _proService.GetProductById(i.ProductId);
            }
            return orderDetails;
        }
        public async Task<bool> DeleteOrder(int OrderId)
        {
            var order = await context.Orders.FindAsync(OrderId);
            if (order == null) new Exception("Không ton tai order nay");
            order.Status = StatusOrder.Canceled;
            if (await context.SaveChangesAsync() > 0) return true;
            else return false;
        }
        public async Task<bool> Comfirm(UpdateOrderRequest request)
        {
            try
            {
                var order = context.Orders.Where(x => x.OrderId == request.OrderId).FirstOrDefault();
                var oderdetails = context.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                foreach (var i in oderdetails)
                {
                    if (!(await CheckQuantity(i.Quanity, i.ProductId))) return false;
                }
                var p = await context.Orders.FindAsync(request.OrderId);
                p.Status = (StatusOrder)request.Status;
                if (context.SaveChanges() <= 0)
                {
                    return false;
                }
                    
            }
            catch (NullReferenceException)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateStatus(UpdateOrderRequest request)
        {
            try
            {
                var order = await context.Orders.FindAsync(request.OrderId);
                order.Status = (StatusOrder)request.Status;
                if (context.SaveChanges() <= 0)
                {
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }
            return true;
        }
        async Task<bool> CheckQuantity(int sl, int proId)
        {
            var pro = await context.Products.FindAsync(proId);
            if (pro.Quantity - sl >= 0)
            {
                pro.Quantity -= sl;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<OrderViewModel>> GetAllOrders()
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            var query = from o in context.Orders select o;
            /*            var query = from o in context.Orders select o;
                        query = query.Where(x => x.UserId == UserId);*/
            foreach (var i in query)
            {
                orders.Add(
                    new OrderViewModel()
                    {
                        OrderId = i.OrderId,
                        OrderDate = i.OrderDate,
                        UserId = i.UserId,
                        Shipname = i.Shipname,
                        ShipAddress = i.ShipAddress,
                        ShipEmail = i.ShipEmail,
                        ShipPhoneNumber = i.ShipPhoneNumber,
                        Status = i.Status.ToString(),
                        Discount = i.Discount,
                        ShipPrice = i.ShipPrice,
                        TotalPrice = i.TotalPrice,
                        Payment=i.Payment
                    }); ;
            }
            return orders;
        }

        public async Task<List<OrderViewModel>> GetOrderByStatus(int status)
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            var query = from o in context.Orders select o;
            StatusOrder statusOrder = 0;
            switch (status)
            {
                case 0:
                    {
                        statusOrder = StatusOrder.InProgress;
                        break;
                    }
                case 1:
                    {
                        statusOrder = StatusOrder.Confirmed;
                        break;
                    }
                case 2:
                    {
                        statusOrder = StatusOrder.Shipping;
                        break;
                    }
                case 3:
                    {
                        statusOrder = StatusOrder.Success;
                        break;
                    }
                case 4:
                    {
                        statusOrder = StatusOrder.Canceled;
                        break;
                    }
                case 5:
                    {
                        statusOrder = StatusOrder.Packed;
                        break;
                    }
                case 6:
                    {
                        statusOrder = StatusOrder.Delivered;
                        break;
                    }
            }
            if (status >= 0 && status < 7)
            {
                query = query.Where(x => x.Status == statusOrder);
            }
            else if(status == 7)
            {
                query = query.Where(x => x.Payment == 0);
            }
            else if (status == 8)
            {
                query = query.Where(x => x.Payment == 1);
            }

            /*            var query = from o in context.Orders select o;
                        query = query.Where(x => x.UserId == UserId);*/
            foreach (var i in query)
            {
                orders.Add(
                    new OrderViewModel()
                    {
                        OrderId = i.OrderId,
                        OrderDate = i.OrderDate,
                        UserId = i.UserId,
                        Shipname = i.Shipname,
                        ShipAddress = i.ShipAddress,
                        ShipEmail = i.ShipEmail,
                        ShipPhoneNumber = i.ShipPhoneNumber,
                        Status = i.Status.ToString(),
                        Discount = i.Discount,
                        ShipPrice = i.ShipPrice,
                        TotalPrice = i.TotalPrice,
                        Payment=i.Payment
                    }); ;
            }
            return orders;
        }

        public async Task<OrderViewModel> GetOrderById(int id)
        {
            OrderViewModel order = new OrderViewModel();
            var query = context.Orders.Where(x => x.OrderId == id);
            foreach (var i in query)
            {
                order =
                    new OrderViewModel()
                    {
                        OrderId = i.OrderId,
                        OrderDate = i.OrderDate,
                        UserId = i.UserId,
                        Shipname = i.Shipname,
                        ShipAddress = i.ShipAddress,
                        ShipEmail = i.ShipEmail,
                        ShipPhoneNumber = i.ShipPhoneNumber,
                        Status = i.Status.ToString(),
                        Discount = i.Discount,
                        ShipPrice = i.ShipPrice,
                        TotalPrice = i.TotalPrice,
                        Payment=i.Payment
                    };
            }
            return order;
        }
    }
}
