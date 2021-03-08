using FruitShopSolution.Data.EF;
using FruitShopSolution.ViewModel.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FruitShopSolution.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly  FruitShopDbContext _context;

        public CategoryService(FruitShopDbContext context)
        {
            _context = context;
        }
        public int GetCategoryId(string name)
        {
            return _context.Categories.Where(x => x.Title.Contains(name)).FirstOrDefault().CategoryId;
        }
        public List<CategoriesViewModel> GetAll()
        {
            var listData = new List<CategoriesViewModel>();
            var query =  from c in _context.Categories select c;
            /*            return await query.Select(x => new CategoriesViewModel()
                        {
                            CategoryId = x.CategoryId,
                            Content = x.Content,
                            ParentId = x.ParentId,
                            Title = x.Title
                        }).ToListAsync();*/

            foreach (var i in query)
            {
                CategoriesViewModel pro = new CategoriesViewModel()
                {
                    CategoryId = i.CategoryId,
                    Content = i.Content,
                    ParentId = i.ParentId,
                    Title = i.Title
                };
                listData.Add(pro);
            }
            return listData;
        }
    }

}
