using FruitShopSolution.ViewModel.Catalog.Cart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Application.Catalog.Cart
{
    public interface ICartService
    {
        Task AddToCart(int proId,int id);
        Task Delete(int iduser,int idpro);
        Task Update(int id);
        Task<List<CartItems>> GetItems(int id);
    }

}
