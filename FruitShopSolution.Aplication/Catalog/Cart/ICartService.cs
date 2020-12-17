using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Application.Catalog.Cart
{
    public interface ICartService
    {
        Task AddToCart(int id);
        Task Delete(int id);
        Task Update(int id);
    }

}
