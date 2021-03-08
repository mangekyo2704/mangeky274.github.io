using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using FruitShopSolution.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using FruitShopSolution.Application.Catalog.Products;
using FruitShopSolution.UI.Models;
using FruitShopSolution.ViewModel.Catalog.Cart;
using FruitShopSolution.Data.EF;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using FruitShopSolution.Application.Common.Email;
using FruitShopSolution.ViewModel.Catalog.Order;
using FruitShopSolution.ViewModel.Catalog.Users;
using FruitShopSolution.Application.Catalog.Cart;
using FruitShopSolution.Application.Catalog.Users;
namespace FruitShopSolution.UI.Models
{
    public class SessionService
    {
        private readonly ISession _session;
        public SessionService(ISession _session1)
        {
            _session = _session1;
        }
        public List<CartViewModel> GetCartItems()
        {

            string jsoncart = _session.GetString("cart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartViewModel>>(jsoncart);
            }
            return new List<CartViewModel>();
        }
        public UserViewModel GetUserLogined()
        {

            string jsoncart = _session.GetString("user");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<UserViewModel>(jsoncart);
            }
            return new UserViewModel();
        }

    }
}
