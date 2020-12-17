using FruitShopSolution.Data.EF;
using FruitShopSolution.ViewModel.Catalog.Cart;
using FruitShopSolution.ViewModel.Catalog.Order;
using FruitShopSolution.ViewModel.Catalog.Products;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FruitShopSolution.UI.Models
{
    public class ProductService : IProductService
    {
        private readonly FruitShopDbContext context;
        private List<CartViewModel> listProductInCart = new List<CartViewModel>();
        public ProductService(FruitShopDbContext _context)
        {
            context = _context;
        }
        public async Task<List<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1.Join
            var query = from p in context.Products
                        join pt in context.ProductInCategories on p.ProductId equals pt.ProductId
                        join c in context.Categories on pt.CategoryId equals c.CategoryId
                        select new { p, c, pt };
            //2.Query
            if (!String.IsNullOrEmpty(request.Keywork))
            {
                query = query.Where(x => x.p.Title.Contains(request.Keywork));

            }
            if (request.CategoryIds >= 0)
            {
                query = query.Where(p => request.CategoryIds == p.pt.CategoryId);
            }
            //3.Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ProductId = x.p.ProductId,
                    Title = x.p.Title,
                    Content = x.p.Content,
                    Origin = x.p.Origin,
                    Quantity = x.p.Quantity,
                    DateCreated = x.p.DateCreated,
                    InputCount = x.p.InputCount,
                    OutputCount = x.p.OutputCount
                }).ToListAsync();
            //4.Select and projection
            /*            var pageResult = new PageResult<ProductViewModel>()
                        {
                            TotalRecord = totalRow,
                            Items = data
                        };
                        return pageResult;*/
            return data;
        }
        public async Task AddToCart(int proId, int userId)
        {

        }

        public async Task AddToCart(ProductInfoViewModel pro, int quantity)
        {
            listProductInCart.Add(new CartViewModel() { Quantity = quantity, Products = pro });
        }
        public List<CartViewModel> GetProductsInCart()
        {
            return listProductInCart;
        }

        public async Task<bool> FindProductInCart(int proId)
        {
            var query = context.Carts.Where(x => x.ProductId == proId);
            if (query.Count() > 0)
                return true;
            return false;
        }

        public List<ProductInfoViewModel> GetAllProduct()
        {
            List<ProductInfoViewModel> listData = new List<ProductInfoViewModel>();
            List<ProductViewModel> proList = new List<ProductViewModel>();

            var query = from p in context.Products
                        select p;
            foreach (var i in query)
            {
                ProductViewModel pro = new ProductViewModel()
                {
                    Content = i.Content,
                    DateCreated = i.DateCreated,
                    InputCount = i.InputCount,
                    Origin = i.Origin,
                    OutputCount = i.OutputCount,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Title = i.Title
                };
                proList.Add(pro);
            }
            foreach (var i in proList)
            {
                List<ProductImageViewModel> listImages = this.GetListProductImages(i.ProductId);
                listData.Add(
                    new ProductInfoViewModel()
                    {
                        pro = i,
                        ListImages = listImages
                    });
            }
            return listData;
        }
        public List<ProductImageViewModel> GetListProductImages(int productId)
        {
            var product = context.Products.Find(productId);
            if (product == null) new Exception("Không tim thấy sản phẩm");
            var query = from i in context.ProductImages
                        join p in context.Products on i.productId equals p.ProductId
                        select i;
            query = query.Where(x => x.productId == productId);
            var listImages = new List<ProductImageViewModel>();
            if (query.Count() > 0)
            {
                foreach (var i in query)
                {
                    listImages.Add(
                        new ProductImageViewModel()
                        {
                            caption = i.caption,
                            imagepath = i.imagepath,
                            isDefault = i.isDefault,
                            productId = i.productId,
                            productImageId = i.productImageId
                        }
                        );
                }
            }
            return listImages;
        }

        public ProductInfoViewModel GetProductById(int proId)
        {
            var query = from p in context.Products
                        where p.ProductId == proId
                        select p;

            ProductViewModel product = new ProductViewModel()
            {
                Content = query.First().Content,
                DateCreated = query.First().DateCreated,
                InputCount = query.First().InputCount,
                Origin = query.First().Origin,
                OutputCount = query.First().OutputCount,
                ProductId = query.First().ProductId,
                Quantity = query.First().Quantity,
                Title = query.First().Title
            };

            List<ProductImageViewModel> listImages = this.GetListProductImages(proId);
            ProductInfoViewModel info = new ProductInfoViewModel()
            {
                pro = product,
                ListImages = listImages
            };
            return info;
        }

        public async Task<List<ProductInfoViewModel>> Searching(string keyword)
        {
            var query = from p in context.Products select p;
            query = query.Where(x => x.Title.Contains(keyword));
            List<ProductInfoViewModel> listData = new List<ProductInfoViewModel>();
            List<ProductViewModel> listProduct = new List<ProductViewModel>();
            foreach (var i in query)
            {
                listProduct.Add(new ProductViewModel()
                {
                    Content = i.Content,
                    DateCreated = i.DateCreated,
                    InputCount = i.InputCount,
                    Origin = i.Origin,
                    OutputCount = i.OutputCount,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Title = i.Title
                });
            }
            foreach (var i in listProduct)
            {
                listData.Add(GetProductById(i.ProductId));
            }

            return listData;
        }

        public async Task<int> CreateOrder(CreateOrderRequest request)
        {

            List<CreateOrderDetailRequest> listOrderDetails = new List<CreateOrderDetailRequest>();
            foreach (var i in request.ListProduct)
            {
                listOrderDetails.Add(
                    new CreateOrderDetailRequest()
                    {

                    });
            }
            Data.Entities.Order order = new Data.Entities.Order()
            {
                OrderDate = request.OrderDate,
                Discount = request.Discount,
                ShipAddress = request.ShipAddress,
                ShipEmail = request.ShipEmail,
                Shipname=request.ShipName,
                ShipPhoneNumber = request.ShipPhoneNumber,
                TotalPrice = request.TotalPrice,
                ShipPrice = request.ShipPrice,
                UserId=request.UserId
            };
            await context.Orders.AddAsync(order);
            int a = await context.SaveChangesAsync();
            int id = order.OrderId;
            foreach (var i in request.ListProduct)
            {
                await context.OrderDetails.AddAsync(new Data.Entities.OrderDetail()
                {
                    OrderId=id,
                    ProductId=i.ProductId,
                    Price=i.Price,
                    Quanity=i.Quanity
                });
            }
            int b = await context.SaveChangesAsync();
            if(a>0 && b > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
