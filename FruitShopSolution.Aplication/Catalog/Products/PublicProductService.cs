
using FruitShopSolution.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FruitShopSolution.ViewModel.Catalog.Products;
using FruitShopSolution.ViewModel.Common;

namespace FruitShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IProductPublicService
    {
        private readonly FruitShopDbContext context;
        public PublicProductService(FruitShopDbContext _context)
        {
            context = _context;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            //1.Join
            var query = from p in context.Products
                        join pt in context.ProductInCategories on p.ProductId equals pt.ProductId
                        join c in context.Categories on pt.CategoryId equals c.CategoryId
                        select new { p, c, pt };
            int totalRow = await query.CountAsync();
            var data = await query
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
            return data;
        }

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            //1.Join
            var query = from p in context.Products
                        join pt in context.ProductInCategories on p.ProductId equals pt.ProductId
                        join c in context.Categories on pt.CategoryId equals c.CategoryId
                        select new { p, c, pt };
            //2.Query
            if (request.CategoryId > 0 && request.CategoryId.HasValue)
            {
                query = query.Where(p => p.pt.CategoryId == request.CategoryId);
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
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }
    }
}
