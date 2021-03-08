using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ZaloPayDemo.ZaloPay.Crypto;
using Newtonsoft.Json;
using FruitShopSolution.ViewModel.Catalog.ZaloPay.Crypto;
using FruitShopSolution.ViewModel.Catalog.ZaloPay;

namespace ZaloPayDemo.ZaloPay.Models
{

    public class OrderData
    {
        public string Appid { get; set; }
        public string Apptransid { get; set; }
        public long Apptime { get; set; }
        public string Appuser { get; set; }
        public string Item { get; set; }
        public string Embeddata { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string Bankcode { get; set; }
        public string Mac { get; set; }

        private string key2 = "eG4r0GcoNtRGbO8";
        static string appid = "2553";
        static string key1 = "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL";
        private long uid = Utils.GetTimeStamp();
        static string create_order_url = "https://sb-openapi.zalopay.vn/v2/create";
        static string query_order_url = "https://sb-openapi.zalopay.vn/v2/query";
        private string PublicKey = "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAOfB6/x0b5UiLkU3pOdcnXIkuCSzmvlVhDJKv1j3yBCyvsgAHacVXd+7WDPcCJmjSEKlRV6bBJWYam5vo7RB740CAwEAAQ==";
        public OrderData(long amount, string description = "", string bankcode = "", object embeddata = null, object item = null, string appuser = "")
        {
            Appid = appid;
            Apptransid = ZaloPayHelper.GenTransID();
            Apptime = Utils.GetTimeStamp();
            Appuser = appuser;
            Amount = amount;
            Bankcode = bankcode;
            Description = description;
            Embeddata = JsonConvert.SerializeObject(embeddata);
            Item = JsonConvert.SerializeObject(item);
            Mac = ComputeMac();
        }

        public virtual string GetMacData()
        {
            return Appid + "|" + Apptransid + "|" + Appuser + "|" + Amount + "|" + Apptime + "|" + Embeddata + "|" + Item;
        }

        public string ComputeMac()
        {
            return HmacHelper.Compute(ZaloPayHMAC.HMACSHA256,key1 , GetMacData());
        }
    }

    public class QuickPayOrderData : OrderData
    {
        public string Paymentcode { get; set; }

        public QuickPayOrderData(long amount, string paymentcodeRaw, string description = "", object embeddata = null, object item = null, string appuser = "")
            : base(amount, description, "", embeddata, item, appuser)
        {
            Paymentcode = RSAHelper.Encrypt(paymentcodeRaw, "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAOfB6/x0b5UiLkU3pOdcnXIkuCSzmvlVhDJKv1j3yBCyvsgAHacVXd+7WDPcCJmjSEKlRV6bBJWYam5vo7RB740CAwEAAQ==");
            Mac = ComputeMac();
        }

        public override string GetMacData()
        {
            return base.GetMacData() + "|" + Paymentcode;
        }
    }
}