
using FruitShopSolution.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.UI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using FruitShopSolution.ViewModel.Catalog.Cart;
using FruitShopSolution.Data.EF;
using Microsoft.AspNetCore.Session;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using FruitShopSolution.Application.Common.Email;
using FruitShopSolution.ViewModel.Catalog.Order;
using FruitShopSolution.ViewModel.Catalog.Users;
using FruitShopSolution.Application.Catalog.Cart;
using FruitShopSolution.Application.Catalog.Users;
using FruitShopSolution.Application.Catalog.Promotion;
using FruitShopSolution.ViewModel.Common;

namespace FruitShopSolution.UI.Controllers

{
    public class ProductController : Controller
    {
        private readonly IProductService _proService;
        private readonly ISendMailService _mailService;
        private readonly ICartService _cartService;
        private readonly IPromotionService _promotionService;
        private readonly FruitShopDbContext context;
        public const string CARTKEY = "cart";
        public ProductController(IProductService product, FruitShopDbContext fruitShopDbContext, ISendMailService mailService, ICartService cartService, IPromotionService promotionService)
        {
            _proService = product;
            context = fruitShopDbContext;
            _mailService = mailService;
            _cartService = cartService;
            _promotionService = promotionService;
        }
        public async Task<IActionResult> ViewDetail(int proId = 1)
        {
            //ViewBag.Data = productInfo
            ViewBag.User = GetUserLogined();
            List<CommentViewModel> comments = new List<CommentViewModel>();
            comments = await _proService.GetComment(proId);

            if (comments.Count() > 0)
                ViewBag.Comment = comments;
            else ViewBag.Commnet = new List<CommentViewModel>();
            var product = _proService.GetProductById(proId);
            product.Discount = await _promotionService.GetPromotionOfProduct(proId);
            ViewBag.Data = product;
            var categories = await _proService.GetCategoryId(product.pro.ProductId);
            var relatedproducts = await _proService.GetByCategory(categories.FirstOrDefault().CategoryId);
            foreach (var i in relatedproducts)
            {
                i.Discount = await _promotionService.GetPromotionOfProduct(i.pro.ProductId);
            }
            ViewBag.Related = relatedproducts;
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
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
        [HttpPost]
        public IActionResult Comment(int proId, string text, string date)
        {
            var user = GetUserLogined();
            _proService.Comment(proId, text, user.UserId, DateTime.Parse(date));
            return Ok();
        }
        /*        [Route("/viewall", Name = "viewall")]*/

        public async Task<IActionResult> ViewAll(int PageIndex = 1, int PageSize = 12, int? sort = 0)
        {
            //ViewBag.Data = productInfo
            GetManageProductPagingRequest request = new GetManageProductPagingRequest() { PageIndex = PageIndex, PageSize = PageSize };
             List<ProductViewModel> listpro = await _proService.GetAllPaging(request);
            List<ProductInfoViewModel> listProductInfo = new List<ProductInfoViewModel>();
            foreach (var i in listpro)
            {
                listProductInfo.Add(
                    new ProductInfoViewModel()
                    {
                        pro = i,
                        ListImages = _proService.GetListProductImages(i.ProductId),
                        Discount = await _promotionService.GetPromotionOfProduct(i.ProductId)
                    });
            }

            /*            listProductInfo = await _proService.GetAllProduct();*/
            var allproduct = await _proService.GetAllProduct();
            int totalpage = allproduct.Count() / PageSize;
            PageModel page = new PageModel() { PageIndex = PageIndex, TotalPage = 2 };
            ViewBag.Page = page;
            if (sort == 1)
            {
                var a = listProductInfo.OrderBy(x => x.pro.OutputCount);
                listProductInfo = new List<ProductInfoViewModel>();
                foreach (var i in a)
                {
                    listProductInfo.Add(new ProductInfoViewModel() { pro = i.pro, Categories = i.Categories, Discount = i.Discount, ListImages = i.ListImages });
                }
            }
            if (sort == 2)
            {
                var a = listProductInfo.OrderByDescending(x => x.pro.OutputCount);
                listProductInfo = new List<ProductInfoViewModel>();
                foreach (var i in a)
                {
                    listProductInfo.Add(new ProductInfoViewModel() { pro = i.pro, Categories = i.Categories, Discount = i.Discount, ListImages = i.ListImages });
                }
            }
            if (sort == 3)
            {
                var a = listProductInfo.OrderBy(x => x.pro.Title);
                listProductInfo = new List<ProductInfoViewModel>();
                foreach (var i in a)
                {
                    listProductInfo.Add(new ProductInfoViewModel() { pro = i.pro, Categories = i.Categories, Discount = i.Discount, ListImages = i.ListImages });
                }
            }
            if (sort == 4)
            {
                var a = listProductInfo.OrderByDescending(x => x.pro.Title);
                listProductInfo = new List<ProductInfoViewModel>();
                foreach (var i in a)
                {
                    listProductInfo.Add(new ProductInfoViewModel() { pro = i.pro, Categories = i.Categories, Discount = i.Discount, ListImages = i.ListImages });
                }
            }
            ViewBag.Products = listProductInfo;
            ViewBag.PageIndex = PageIndex;
            ViewBag.User = new SessionService(HttpContext.Session).GetUserLogined();
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }
        public IActionResult Sort(int sort,int PageIndex)
        {
            JsonResult result = new JsonResult(JsonConvert.SerializeObject(new {Sort = sort,PageIndex = PageIndex}));
            return result;
        }
        [Route("/adcart", Name = "addcart")]
        public async Task<IActionResult> AddToCartAsync([FromForm] int productid, [FromForm] int quantity)
        {

            var product = _proService.GetProductById(productid);
            product.Discount = await _promotionService.GetPromotionOfProduct(productid);
            if (product == null)
                return NotFound("Không có sản phẩm");
            //Kiêrm tra đăng nhập chưa?
            /*var session = HttpContext.Session;
            var json = session.GetString("user");

            if (json != null)
            {
                var user = JsonConvert.DeserializeObject<UserViewModel>(json);
                try
                {
                    _cartService.AddToCart(new CartViewModel() { Quantity = quantity, Products = product }, user.UserId);
                    var cartItems = _cartService.GetItems(user.UserId);
                    var
                    SaveCartSession()
                }
                catch (Exception e)
                {
                    ViewBag.Exception = e.Message;
                }

            }
            else
            {*/
            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Products.pro.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartViewModel() { Quantity = quantity, Products = product });
                /*_proService.AddToCart(product,quantity);*/
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return Json(GetCartItems().Count);
        }
        public async Task<IActionResult> AddToLove( int productid)
        {

            var product = _proService.GetProductById(productid);
            var user = GetUserLogined();
            if (product == null)
                return NotFound("Không có sản phẩm");
            if(user.UserName != null)
            {
                await _cartService.AddToCart(productid, user.UserId);
            }
            // Chuyển đến trang hiện thị Cart
            return Ok();
        }

        [Route("/cart", Name = "cart")]
        public IActionResult ViewCart()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("user");
            if (jsoncart != null)
            {

                ViewBag.User = JsonConvert.DeserializeObject<UserViewModel>(jsoncart);
            }
            ViewBag.Data = GetCartItems();
            ViewBag.User = new SessionService(HttpContext.Session).GetUserLogined();
            ViewBag.ProductsCount = new SessionService(HttpContext.Session).GetCartItems().Count();
            return View();
        }
        public async Task<IActionResult> ViewComment(int ProductId)
        {
            var Comments = await _proService.GetComment(ProductId);
            return Ok();
        }
        // Lấy cart từ Session (danh sách CartItem)
        List<CartViewModel> GetCartItems()
        {

            var session = HttpContext.Session;
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
            if (quantity <= 0)
            {
                RemoveCart(productid);
            }
            else
            {
                var cart = GetCartItems();
                var cartitem = cart.Find(p => p.Products.pro.ProductId == productid);
                if (cartitem != null)
                {
                    // Đã tồn tại, tăng thêm 1
                    cartitem.Quantity = quantity;
                }
                SaveCartSession(cart);
            }
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
            var html = "<table border='1' align='center'>";
            html+= "<tr align='center'><th>SỐ THỨ TỰ</th><th>TÊN SẢN PHẨM</th><th>ĐƠN GIÁ</th><th>SỐ LƯỢNG</th><th>THÀNH TIỀN</th></tr>";
            var user = GetUserLogined();
            request.UserId = user.UserId;
            request.OrderDate = DateTime.Now;
            List<CreateOrderDetailRequest> createOrderDetailRequest = new List<CreateOrderDetailRequest>();
            List<CartViewModel> data = GetCartItems();
            int count = 0;
            decimal totaldiscount = 0;
            foreach (var i in data)
            {
                count++;
                html += $"<tr align='center'><td>{count}</td><td>" + i.Products.pro.Title + "</td><td>";
                var order = new CreateOrderDetailRequest()
                {
                    ProductId = i.Products.pro.ProductId,
                    Quanity = i.Quantity,
                    Price = i.Quantity * i.Products.pro.OutputCount,
                    Discount = i.Products.Discount
                };
                var discountprice = order.Price;
                string productprice = Convert.ToDecimal(i.Products.pro.OutputCount).ToString("#,##0");
                if (i.Products.Discount != 100 && i.Products.Discount != 0)
                {
                    order.Price = i.Quantity * (i.Products.pro.OutputCount * (1 - (decimal)i.Products.Discount / 100));
                    productprice = Convert.ToDecimal(i.Products.pro.OutputCount * (1 - (decimal)i.Products.Discount / 100)).ToString("#,##0");
                    totaldiscount = discountprice - order.Price;
                }
                html+= productprice+"</td><td>" + order.Quanity + "</td><td>" + Convert.ToDecimal(order.Price).ToString("#,##0") + "</td></tr>";
                createOrderDetailRequest.Add(order);
            }
               
            request.TotalPrice = createOrderDetailRequest.Select(x => x.Price).Sum();
            request.Discount = (int)totaldiscount;
            html += "<tr align='center'><th colspan='4'>TỔNG TIỀN</th><th>" + Convert.ToDecimal(request.TotalPrice).ToString("#,##0") + "</td></tr>" + "</th></tr></table>";

            MailContent content = new MailContent
            {
                To = request.ShipEmail,
                Subject = "Thông tin đơn hàng từ Trái cây sạch - TNTFRUIT",
                Body = $"<h1>Kính chào {request.ShipName}, cảm ơn bạn đã đặt hàng từ Trái cây sạch - TNT FRUIT.</h1>"
                    + "<h4>Dưới đây là thông tin về đơn hàng của bạn. Nếu có bất kì sai sót nào hãy liên hệ với chúng tôi qua email này hoặc số điện thoại +84 (0961600587)</h4>"
                    + "<hr>"
                    + "<br>"
                    +html
                    + "<hr>"
                    + $"<h2>Cảm ơn bạn đã đọc. Chúc {request.ShipName} một ngày tốt lành nhé! </h2>"
                    + "<p>Theo dõi fanpage của chúng tôi để xem thêm nhiều sản phẩm nữa nhé: <a href='https://www.facebook.com/TNTFRUIT'>Trái cây sạch - TNT FRUIT</a></p>"
            };
            request.ListProduct = createOrderDetailRequest;
            await Task.WhenAll(SendMail(content), _proService.CreateOrder(request));
            ClearCart();
            return RedirectToAction("ViewOrder", "User");
        }
    }
}
