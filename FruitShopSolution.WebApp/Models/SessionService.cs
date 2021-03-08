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
using FruitShopSolution.ViewModel.Catalog.Cart;
using FruitShopSolution.Data.EF;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using FruitShopSolution.Application.Common.Email;
using FruitShopSolution.ViewModel.Catalog.Order;
using FruitShopSolution.ViewModel.Catalog.Users;
using FruitShopSolution.Application.Catalog.Cart;
using FruitShopSolution.Application.Catalog.Users;
using FruitShopSolution.ViewModel.Catalog.Admin;

namespace FruitShopSolution.WebApp.Models
{
    public class SessionService<T>
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
        public AdminViewModel GetUserLogined()
        {

            string jsoncart = _session.GetString("user");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<AdminViewModel>(jsoncart);
            }
            return new AdminViewModel();
        }
        public void SaveCartSession( T ls,string key)
        {    
            string jsoncart = JsonConvert.SerializeObject(ls);
            _session.SetString(key, jsoncart);
        }
        public T GetItems(string key)
        {

            string jsoncart = _session.GetString(key);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<T>(jsoncart);
            }
            T obj = (T)Activator.CreateInstance(typeof(T));
            return obj;
        }
        public void ClearSession(string key)
        {           
            _session.Remove(key);
        }
    }
}
