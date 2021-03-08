/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.ViewModel.Catalog.ZaloPay;
using Microsoft.AspNetCore.Mvc;
using ZaloPayDemo.ZaloPay.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using FruitShopSolution.UI.Models;

namespace FruitShopSolution.UI.Controllers
{
    
    public class ZaloPayController : Controller
    {
        public ActionResult Index()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("orderurl");
            string jsoncart1 = session.GetString("QRCodeBase64Image");
            string jsoncart2 = session.GetString("apptransid");
            if (jsoncart != null)
            {
                ViewBag.OrderUrl = JsonConvert.DeserializeObject<string>(jsoncart);
                ViewBag.QRCodeBase64Image = JsonConvert.DeserializeObject<string>(jsoncart1);
                ViewBag.Apptransid = JsonConvert.DeserializeObject<string>(jsoncart2);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Post(long amount,string description,string bankcode)
        {
            *//*var amount1 = long.Parse(Request.Form.Get("amount"));
            var description1 = Request.Form.Get("description");*//*
            var embeddata = NgrokHelper.CreateEmbeddataWithPublicUrl();
            *//*var bankcode = Request.Form.Get("bankcode");*//*

            var orderData = new OrderData(amount, description, bankcode, embeddata);
            var order = await ZaloPayHelper.CreateOrder(orderData);

            var returncode = (long)order["returncode"];
            if (returncode == 1)
            {
                *//*                using (var db = new ZaloPayDemoContext())
                                {
                                    db.Orders.Add(new Models.Order
                                    {
                                        Apptransid = orderData.Apptransid,
                                        Amount = orderData.Amount,
                                        Timestamp = orderData.Apptime,
                                        Description = orderData.Description,
                                        Status = 0
                                    });

                                    db.SaveChanges();*//*
            }

            var orderurl = order["orderurl"].ToString();
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(orderurl);
            session.SetString("orderurl", jsoncart);
            string jsoncart1 = JsonConvert.SerializeObject(QRCodeHelper.CreateQRCodeBase64Image(orderurl));
            session.SetString("QRCodeBase64Image", jsoncart1);
            string jsoncart2 = JsonConvert.SerializeObject(orderData.Apptransid);
            session.SetString("apptransid", jsoncart2);

            return RedirectToAction("/");
        }
    }
}
*/