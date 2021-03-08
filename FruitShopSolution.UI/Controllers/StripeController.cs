using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace FruitShopSolution.UI.Controllers
{
    public class StripeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Charge(string stripeEmail,string stripeToken)
        {

            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions 
            { 
                Email= stripeEmail,
                Source = stripeToken,

            });
            var charge = charges.Create(new ChargeCreateOptions() 
            {
                Amount=50000000,
                Description = "Chos tuan",
                Currency = "VND",
                Customer = customer.Id
            });
            if(charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                return View();
            }
            return View();
        }
    }
}
