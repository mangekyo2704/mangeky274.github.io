using FruitShopSolution.Application.Catalog.Users;
using FruitShopSolution.Data.EF;
using FruitShopSolution.Data.Entities;
using FruitShopSolution.ViewModel.Catalog.Cart;
using FruitShopSolution.ViewModel.Catalog.Categories;
using FruitShopSolution.ViewModel.Catalog.Order;
using FruitShopSolution.ViewModel.Catalog.Products;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FruitShopSolution.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly FruitShopDbContext context;
        private readonly IUserService _userServie;
        private List<CartViewModel> listProductInCart = new List<CartViewModel>();
        public ProductService(FruitShopDbContext _context, IUserService userService)
        {
            context = _context;
            _userServie = userService;
        }
        public async Task<List<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1.Join
            /*            var query = from p in context.Products
                                    join pt in context.ProductInCategories on p.ProductId equals pt.ProductId
                                    join c in context.Categories on pt.CategoryId equals c.CategoryId
                                    select new { p, c, pt };*/
            var query = from p in context.Products
                        select new { p };
            //2.Query
            if (!string.IsNullOrEmpty(request.Keywork))
            {
                // query = query.Where(x => x.p.Title.Contains(request.Keywork));
                query = query.Where(x => x.p.Title.Contains(request.Keywork));

            }
            /*            if (request.CategoryIds >= 0)
                        {
                            query = query.Where(p => request.CategoryIds == p.pt.CategoryId);
                        }*/
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
                List<ProductImageViewModel> listImages = GetListProductImages(i.ProductId);
                listData.Add(
                    new ProductInfoViewModel()
                    {
                        pro = i,
                        ListImages = listImages
                    });
            }
            return listData;
        }
        public async Task<List<CategoriesViewModel>> GetCategoryId(int ProductId)
        {
            var query = from c in context.Categories
                        join pt in context.ProductInCategories on c.CategoryId equals pt.CategoryId
                        join p in context.Products on pt.ProductId equals p.ProductId
                        where p.ProductId == ProductId
                        select c;
            List<CategoriesViewModel> categories = new List<CategoriesViewModel>();
            foreach (var i in query)
            {
                categories.Add(new CategoriesViewModel()
                {
                    CategoryId = i.CategoryId,
                    Content = i.Content,
                    ParentId = i.ParentId,
                    Title = i.Title
                });
            }
            return categories;
        }
        public async Task<List<ProductInfoViewModel>> GetByCategory(int categoryId)
        {
            List<ProductInfoViewModel> listData = new List<ProductInfoViewModel>();
            List<ProductViewModel> listpro = new List<ProductViewModel>();
            var query = from p in context.Products
                        join pt in context.ProductInCategories on p.ProductId equals pt.ProductId
                        join c in context.Categories on pt.CategoryId equals c.CategoryId
                        where c.CategoryId == categoryId
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
                    Title = i.Title,
                };
                listpro.Add(pro);
            }
            foreach (var i in listpro)
            {
                List<ProductImageViewModel> listImages = GetListProductImages(i.ProductId);
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

            List<ProductImageViewModel> listImages = GetListProductImages(proId);
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
            /*            foreach (var i in request.ListProduct)
                        {
                            listOrderDetails.Add(
                                new CreateOrderDetailRequest()
                                {

                                });
                        }*/
            Data.Entities.Order order = new Data.Entities.Order()
            {
                OrderDate = request.OrderDate,
                Discount = request.Discount,
                ShipAddress = request.ShipAddress,
                ShipEmail = request.ShipEmail,
                Shipname = request.ShipName,
                ShipPhoneNumber = request.ShipPhoneNumber,
                TotalPrice = request.TotalPrice,
                ShipPrice = request.ShipPrice,
                UserId = request.UserId
            };
            await context.Orders.AddAsync(order);
            int a = await context.SaveChangesAsync();
            int id = order.OrderId;
            foreach (var i in request.ListProduct)
            {
                await context.OrderDetails.AddAsync(new OrderDetail()
                {
                    OrderId = id,
                    ProductId = i.ProductId,
                    Price = i.Price,
                    Quanity = i.Quanity
                });
            }
            int b = await context.SaveChangesAsync();
            if (a > 0 && b > 0)
            {
                return 1;
            }
            return 0;
        }

        public async Task<List<CommentViewModel>> GetComment(int ProductId)
        {
            var query = context.Comments.Where(x => x.ProductId == ProductId);
            /*            var query = from c in context.Comments select c;
                        query = query.Where(x => x.ProductId == ProductId);*/
            List<CommentViewModel> listComment = new List<CommentViewModel>();
            if (query.Count() <= 0) return new List<CommentViewModel>();
            foreach (var item in query)
            {
                listComment.Add(new CommentViewModel()
                {
                    ProductId = item.ProductId,
                    UserId = item.UserId,
                    Text = item.Text,
                    Time_Comment = item.Time_Comment,
                });
            }
            foreach (var item in listComment)
            {
                item.User = await _userServie.GetById(item.UserId);
            }
            return listComment;

        }

        public async Task<CommentViewModel> Comment(int ProductId, string text, int UserId)
        {
            try
            {
                var comments = new Comment() { ProductId = ProductId, UserId = UserId, Text = text };
                string str = comments.Text;
                context.Comments.Add(comments);
            }
            catch (Exception e)
            {
                string err = e.Message;
            }
            context.SaveChanges();


            return new CommentViewModel() { ProductId = ProductId, UserId = UserId, Text = text, Time_Comment = DateTime.Now };
        }
    }
}
