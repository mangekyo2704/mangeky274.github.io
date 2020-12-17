using FruitShopSolution.ViewModel.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopSolution.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        public List<CategoriesViewModel> GetAll( );
    }
}
