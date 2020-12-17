
using FruitShopSolution.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Products;
using FruitShopSolution.UI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using FruitShopSolution.ViewModel.Catalog.Cart;
using FruitShopSolution.Data.EF;
using Microsoft.AspNetCore.Session;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using FruitShopSolution.Application.Common.Email;
using FruitShopSolution.ViewModel.Catalog.Order;

namespace FruitShopSolution.UI.Controllers

{
    public class ProductController : Controller
    {
        private readonly IProductService _proService;
        private readonly ISendMailService _mailService;
        private readonly FruitShopDbContext context;
        public const  string CARTKEY = "cart";
        public ProductController(IProductService product,FruitShopDbContext fruitShopDbContext, ISendMailService mailService)
        {
            _proService = product;
            context = fruitShopDbContext;
            _mailService = mailService;
        }
        public  async Task<IActionResult> ViewDetail(int proId = 1)
        {
            //ViewBag.Data = productInfo
            ViewBag.Data = _proService.GetProductById(proId);
            return  View();
        }
        [Route("/viewall", Name = "viewall")]
        public async Task<IActionResult> ViewAll( )
        {
            //ViewBag.Data = productInfo
            GetManageProductPagingRequest request = new GetManageProductPagingRequest() { PageIndex = 6, PageSize = 2 };
            List<ProductViewModel>  listpro =await _proService.GetAllPaging(request);
            List<ProductInfoViewModel>  listProductInfo =new List<ProductInfoViewModel>();
            foreach(var i in listpro)
            {
                listProductInfo.Add(
                    new ProductInfoViewModel()
                    {
                        pro=i,
                        ListImages=_proService.GetListProductImages(i.ProductId)
                    });
            }
            ViewBag.Products = listProductInfo;
            return  View();
        }
        
        [Route("/adcart",Name ="addcart")]
        public  IActionResult AddToCart([FromForm] int productid,[FromForm]int quantity)
        {

            var product = _proService.GetProductById(productid);
            if (product == null)
                return NotFound("Không có sản phẩm");
            // Xử lý đưa vào Cart ...
            

            var cart =  GetCartItems();

            var cartitem =  cart.Find(p=> p.Products.pro.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartViewModel() { Quantity = 1, Products = product });
                /*_proService.AddToCart(product,quantity);*/
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return Ok();
        }

        [Route("/cart", Name = "cart")]
        public IActionResult ViewCart()
        {
            ViewBag.Data = GetCartItems();
            return View();
        }

        // Lấy cart từ Session (danh sách CartItem)
        List<CartViewModel> GetCartItems()
        {

            var  session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartViewModel>>(jsoncart);
            }
            return new List<CartViewModel>();
        }

        public async Task SendMail(MailContent content)
        {
            await _mailService.SendMail(content);
            //return RedirectToRoutePreserveMethod("cart");
        }
        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartViewModel> ls)
        {   
/*            createOrderDetailRequest.Clear();
            foreach(var i in ls)
            {
                
                createOrderDetailRequest.Add(
                    new CreateOrderDetailRequest()
                    {
                        ProductId = i.Products.pro.ProductId,
                        Quanity = i.Quantity,
                        Price = i.Quantity * i.Products.pro.OutputCount
                    }) ;
            }*/
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Products.pro.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }
        /// xóa item trong cart
        [Route("/removecart", Name = "removecart")]
        public IActionResult RemoveCart([FromForm] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Products.pro.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            MailContent content = new MailContent
            {
                To = request.ShipEmail,
                Subject = "Đơn mua hàng từ FruitShop",
                Body = "<p><strong>Bạn đã đặt đơn hàng từ FruitShop</strong></p>"
            };
            request.UserId = 1;
            request.OrderDate = DateTime.Now;
            List<CreateOrderDetailRequest> createOrderDetailRequest = new List<CreateOrderDetailRequest>();
            List<CartViewModel> data = GetCartItems();
            foreach (var i in data)
            {
                createOrderDetailRequest.Add(
                    new CreateOrderDetailRequest() 
                    {
                        ProductId=i.Products.pro.ProductId,
                        Quanity=i.Quantity,
                        Price=i.Quantity*i.Products.pro.OutputCount
                    });
            }
            request.TotalPrice = createOrderDetailRequest.Select(x => x.Price).Sum();
            request.ListProduct = createOrderDetailRequest;
            await Task.WhenAll(SendMail(content), _proService.CreateOrder(request));
            return  View(request);
        }
    }
}
