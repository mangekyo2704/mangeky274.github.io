using FruitShopSolution.ViewModel.Catalog.Cart;
using FruitShopSolution.ViewModel.Catalog.Order;
using FruitShopSolution.ViewModel.Catalog.Products;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitShopSolution.UI.Models
{
    public interface IProductService
    {
        public List<ProductInfoViewModel> GetAllProduct();
        public ProductInfoViewModel GetProductById(int proId);
        public Task<List<ProductInfoViewModel>> Searching(string keyword);
        public Task AddToCart(ProductInfoViewModel pro,int quantity);
        public List<CartViewModel> GetProductsInCart();
        public Task<List<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        public List<ProductImageViewModel> GetListProductImages(int productId);
        public Task<bool> FindProductInCart(int proId);
        public Task<int> CreateOrder(CreateOrderRequest request);
    }
}
