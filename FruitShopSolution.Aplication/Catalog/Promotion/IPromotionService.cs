using FruitShopSolution.ViewModel.Catalog.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Application.Catalog.Promotion
{
    public interface IPromotionService
    {
        Task<bool> CreatePromotion(PromotionCreateRequest request);
        Task<bool> CreatePromotionForAllProduct(PromotionCreateRequest request);
        Task<bool> CreatePromotionForCategory(PromotionCreateRequest request,int categoryId);
        Task<bool> UpdateStatus(int id,int Status);
        Task<bool> UpdatePromotion(PromotionUpdateRequest request);
        Task<List<PromotionViewModel>> GetPromotions( );
        Task<PromotionViewModel> GetPromotionsById( int promotionId);
        Task<List<int>> GetListProductIdInPromotion( int promotionId);
        Task<int> GetPromotionOfProduct(int proId);
    }
}
