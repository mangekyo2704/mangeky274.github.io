using FruitShopSolution.Data.EF;
using FruitShopSolution.ViewModel.Catalog.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FruitShopSolution.Application.Catalog.Promotion
{
    public class PromotionService:IPromotionService
    {
        private readonly FruitShopDbContext context;
        public PromotionService(FruitShopDbContext _context)
        {
            context = _context;
        }
        public async Task<bool> CreatePromotion(PromotionCreateRequest request)
        {
            var promotion = new Data.Entities.Promotion()
            {
                Title = request.Title,
                Content = request.Content,
                DiscountPercent = request.DiscountPercent,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                Status = 1
            };
            var query = await context.Promotions.AddAsync(promotion);
            if(await context.SaveChangesAsync()<=0) return false;
            if (request.ProductIds.Count > 0)
            {
                foreach(var i in request.ProductIds)
                {
                   await context.ProductsInPromotion.AddAsync(new Data.Entities.ProductsInPromotion() { ProductId = i, PromotionId = promotion.PromotionId });
                }
            }
            if(await context.SaveChangesAsync()>0) return true;
            return false;

        }
        public async Task<bool> CreatePromotionForAllProduct(PromotionCreateRequest request)
        {
            var product = await context.Products.Select(x=>x.ProductId).ToListAsync();
            request.ProductIds = product;
            if (await CreatePromotion(request)) return true;
            return false;
        }
        public async Task<bool> CreatePromotionForCategory(PromotionCreateRequest request,int categoryId)
        {
            try { 
            var query = from p in context.Products
                          join pc in context.ProductInCategories on p.ProductId equals pc.ProductId
                          join c in context.Categories on pc.CategoryId equals c.CategoryId
                          where c.CategoryId == categoryId
                          select p;
            var product = await query.Select(x => x.ProductId).ToListAsync();
            request.ProductIds = product;
            if (await CreatePromotion(request)) return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            return false;
        }
        public async Task<bool> UpdateStatus(int PromotionId,int Status)
        {
            var query = await context.Promotions.FindAsync(PromotionId);
            query.Status = Status;
            if (await context.SaveChangesAsync() > 0) return true;
            return false;
        }
        public async Task<bool> UpdatePromotion(PromotionUpdateRequest request)
        {
            var query = await context.Promotions.FindAsync(request.PromotionId);
            query.Title = request.Title;
            query.Content = request.Content;
            query.DiscountPercent = request.DiscountPercent;
            query.FromDate = request.FromDate;
            query.ToDate = request.ToDate;
            if (await context.SaveChangesAsync() > 0) return true;
            return false;
        }
        public async Task<List<PromotionViewModel>> GetPromotions() 
        {
            var query = context.Promotions;
            var promotions = new List<PromotionViewModel>();
            if (query != null)
            {
                var query1 =await query.ToListAsync();
                foreach (var i in query1)
                {
                    var query2 = from c in context.ProductsInPromotion select c;
                    var p = new PromotionViewModel()
                    {
                        PromotionId = i.PromotionId,
                        Content = i.Content,
                        DiscountPercent = i.DiscountPercent,
                        FromDate = i.FromDate,
                        ToDate = i.ToDate,
                        Status = i.Status,
                        Title = i.Title,

                    };
                    if (query2 != null) p.ProductIds = query2.Select(x => x.ProductId) as List<int>;
                    promotions.Add(p);
                }
            }
            return promotions;
        }
        public async Task<int> GetPromotionOfProduct(int proId)
        {
            try { 
            var query =  from a in context.Promotions
                        join b in context.ProductsInPromotion on a.PromotionId equals b.PromotionId
                        join c in context.Products on b.ProductId equals c.ProductId
                        where c.ProductId == proId
                        select a;
                  query = query.Where(x => x.Status == 1);
                if (query.Count() >0) {
                    
                    return query.FirstOrDefault().DiscountPercent;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return 100;
            }
            return 100;
        }

        public async Task<List<int>> GetListProductIdInPromotion(int promotionId)
        {
            try
            {
                var query = from a in context.Promotions
                            join b in context.ProductsInPromotion on a.PromotionId equals b.PromotionId
                            join c in context.Products on b.ProductId equals c.ProductId
                            where a.PromotionId == promotionId
                            select c;
                if (query.Count() > 0)
                {
                    var listproductId = await query.Select(x=>x.ProductId).ToListAsync();
                    return listproductId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<int>();
            }
            return new List<int>();
        }

        public async Task<PromotionViewModel> GetPromotionsById(int promotionId)
        {
            var promotion = await context.Promotions.FindAsync(promotionId);
            var p = new PromotionViewModel()
            {
                PromotionId = promotion.PromotionId,
                Content = promotion.Content,
                DiscountPercent = promotion.DiscountPercent,
                FromDate = promotion.FromDate,
                ToDate = promotion.ToDate,
                Status = promotion.Status,
                Title = promotion.Title
            };
            var query2 = from c in context.ProductsInPromotion select c;
            if (query2 != null) p.ProductIds = query2.Select(x => x.ProductId) as List<int>;
            return p;
        }
    }
}
