using FruitShopSolution.Data.EF;
using FruitShopSolution.ViewModel.Catalog.Cart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FruitShopSolution.Application.Catalog.Cart
{
    public class CartService : ICartService
    {
        private readonly FruitShopDbContext context;
        public CartService(FruitShopDbContext _context)
        {
            context = _context;
        }
        public async Task AddToCart(int proId,int userid)
        {
            var user = context.Users.Where(x => x.UserId == userid).Single();
            if (user != null)
            {
                var query = context.Carts.Where(c => c.UserId == userid&& c.ProductId == proId).FirstOrDefault();
                if(query != null)
                {
                }
                else { 
                    context.Carts.Add(
                        new Data.Entities.Cart()
                        {
                            ProductId = proId,                           
                            UserId = userid
                        });
                }
                if(await context.SaveChangesAsync()<0) throw new Exception("Thêm không thành công");
            }
            else throw new Exception("User khong ton tai");
        }

        public async Task Delete(int iduser,int idpro)
        {
            var query = context.Carts.Where(c => c.UserId == iduser && c.ProductId == idpro).FirstOrDefault();
             context.Carts.Remove(query);
            if(await context.SaveChangesAsync()<0) throw new Exception("Xoa khong thanh cong");
        }

        public async Task<List<CartItems>> GetItems(int id)
        {
            var query = context.Carts.Where(c => c.UserId == id);
            if (query.Count() <= 0) return new List<CartItems>();
            List<CartItems> cartItems = new List<CartItems>();
            foreach(var i in query)
            {
                /*var cart = new CartViewModel()
                {
                    Quantity=i.Quantity,
                    Products = new ViewModel.Catalog.Products.ProductInfoViewModel()
                    {
                        pro =new ViewModel.Catalog.Products.ProductViewModel()
                        {
                            Content=i.Product.Content,
                            InputCount = i.Product.InputCount,
                            OutputCount=i.Product.OutputCount,
                            Origin=i.Product.Origin,
                            ProductId=i.ProductId,
                            Title=i.Product.Title
                        },
                        ListImages = 
                    }
                };*/
                var car = new CartItems() { ProductId = i.ProductId, UserId = i.UserId };
                cartItems.Add(car);
            }
            return cartItems;
        }

        public Task Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
