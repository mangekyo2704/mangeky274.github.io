/*using FruitShopSolution.Data.EF;
using FruitShopSolution.ViewModel.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace FruitShopSolution.Application.Catalog.Products
{
    public class ProductImageService : IProductImageService
    {
        private readonly FruitShopDbContext _context;
        private readonly IProductImageService _productImageService;
        public ProductImageService(FruitShopDbContext context, IProductImageService productImageService)
        {
            _context = context;
            _productImageService = productImageService;
        }
        public async Task<List<ProductImageViewModel>> GetListProductImages(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) new Exception("Không tim thấy sản phẩm");
            var query = from i in _context.ProductImages
                        join p in _context.Products on i.productId equals p.ProductId
                        select i;
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
    }
}
*/