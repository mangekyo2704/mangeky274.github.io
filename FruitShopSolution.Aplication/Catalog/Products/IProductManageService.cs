using FruitShopSolution.ViewModel.Catalog.Products;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using FruitShopSolution.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Application.Catalog.Products
{
    public interface IProductManageService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<ProductViewModel> GetById( int productId);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int id);
        Task<bool> UpdateOutputPrice(int productId, decimal newPrice); 
        Task<List<ProductViewModel>> GetByCategory(int categoryId);
        List<ProductViewModel> GetAll();
        Task<List<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<List<ProductImageViewModel>> GetProductImages(int productId);
        Task<int> AddImage(int productId, IFormFile files);
        Task<int> AddImages(int productId, List<IFormFile>files);
        Task<int> RemoveImages(int ImageId);


        //Task<int> UpdateImages(int ImageId);
    }
}
