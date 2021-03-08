using FruitShopSolution.Data.Entities;
using FruitShopSolution.ViewModel.Catalog.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Application.Catalog.Order
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> GetOrders(int UserId);
        Task<List<OrderViewModel>> GetAllOrders();
        Task<List<OrderViewModel>> GetOrderByStatus(int status);
        Task<OrderViewModel> GetOrderById(int id);
        Task<bool> DeleteOrder(int OrderId);
        Task<bool> Comfirm(UpdateOrderRequest request);
        Task<bool> UpdateStatus(UpdateOrderRequest request);
        Task<List<OrderDetailViewModel>> GetOrderDetails(int OrderId);
    }
}
