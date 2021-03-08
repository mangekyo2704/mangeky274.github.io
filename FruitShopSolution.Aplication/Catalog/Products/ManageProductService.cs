
using FruitShopSolution.Data.EF;
using FruitShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FruitShopSolution.ViewModel.Catalog.Products.Manage;
using FruitShopSolution.ViewModel.Catalog.Products;
using FruitShopSolution.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using FruitShopSolution.Application.Common;

namespace FruitShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IProductManageService
    {
        private readonly FruitShopDbContext context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "Image/Products";
        public ManageProductService(FruitShopDbContext _context, IStorageService storageService)
        {
            context = _context;
            _storageService = storageService;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{ Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;

            //return "/img/" + fileName;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            if (request.Unit == null) request.Unit = "Trái";
            var product = new Product
            {
                InputCount = request.InputCount,
                Origin = request.Origin,
                Title = request.Title,
                OutputCount = request.OutputCount,                
                Quantity = request.Quantity,
                Content = request.Content,
                DateCreated = DateTime.Now
            };

            //Save image
            /*            if (request.ThumnailImage != null)
                        {
                            foreach (var i in request.ThumnailImage)
                            {
                                product.ProductImages = new List<ProductImage>()
                                {
                                    new ProductImage()
                                    {
                                        //productId=product.ProductId,
                                        caption = $"ảnh {product.Title}",
                                        imagepath=await this.SaveFile(i),
                                        isDefault = true
                                    }

                                };
                            }
                        }*/
            context.Products.Add(product);
            int pro = await context.SaveChangesAsync();
            int productId = product.ProductId;
            int img = await AddImages(product.ProductId, request.ThumnailImage);
            foreach(var i in request.categoryId)
            {
                context.ProductInCategories.Add(new ProductInCategory() { CategoryId = i, ProductId = productId });
            }           
            if(await context.SaveChangesAsync()<=0) throw new Exception("Không thêm được vào danh mục");
            if (pro < 0) return 0;
            if (img < 0) return -1;
            return 1;

        }
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await context.Products.FindAsync(request.ProductId);
            if (product == null) new Exception("Không tim thấy sản phẩm");
            product.Content = request.Content;
            product.Title = request.Title;
            product.Origin = request.Origin;
            product.Quantity = request.Quantity;
            product.OutputCount = request.Quantity;
            //product.InputCount = request.InputCount;
            product.ProductId = request.ProductId;
            //update anh
            return await context.SaveChangesAsync();
        }
        public async Task<int> Delete(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) new Exception("Không tim thấy sản phẩm");
            var productImages = context.ProductImages.Where(i => i.productId == id);
            foreach (var image in productImages)
            {
                try
                {
                    await _storageService.DeleteFileAsync(image.imagepath);
                }
                catch (Exception)
                {

                }

            }

            context.Products.Remove(product);
            return await context.SaveChangesAsync();
        }

        public List<ProductViewModel> GetAll()
        {
            List<ProductViewModel> listData = new List<ProductViewModel>();
            var query = from p in context.Products
                        select p;
            query = query.OrderByDescending(x => x.ProductId);
            foreach (var i in query)
            {
                ProductViewModel pro = new ProductViewModel()
                {
                    Content = i.Content,
                    DateCreated = i.DateCreated,
                    InputCount = i.InputCount,
                    Origin = i.Origin,
                    OutputCount = i.OutputCount,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Title = i.Title
                };
                listData.Add(pro);
            }
            return listData;
        }

        public async Task<List<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1.Join
            var query = from p in context.Products
                        join pt in context.ProductInCategories on p.ProductId equals pt.ProductId
                        join c in context.Categories on pt.CategoryId equals c.CategoryId
                        select new { p, c, pt };
            query = query.OrderByDescending(x => x.p.ProductId);
            //2.Query
            if (!String.IsNullOrEmpty(request.Keywork))
            {
                query = query.Where(x => x.p.Title.Contains(request.Keywork));

            }
            if (request.CategoryIds >= 0)
            {
                query = query.Where(p => request.CategoryIds == p.pt.CategoryId);
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
            /*            var pageResult = new PageResult<ProductViewModel>()
                        {
                            TotalRecord = totalRow,
                            Items = data
                        };
                        return pageResult;*/
            return data;
        }

        public async Task<ProductViewModel> GetById(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            var data = new ProductViewModel()
            {
                ProductId = product.ProductId,
                Content = product.Content,
                DateCreated = product.DateCreated,
                InputCount = product.InputCount,
                Origin = product.Origin,
                OutputCount = product.OutputCount,
                Quantity = product.Quantity,
                Title = product.Title

            };
            return data;
        }
        public async Task<List<ProductViewModel>> GetByCategory(int categoryId)
        {
            List<ProductViewModel> listData = new List<ProductViewModel>();
            var query = from p in context.Products
                        join pt in context.ProductInCategories on p.ProductId equals pt.ProductId
                        join c in context.Categories on pt.CategoryId equals c.CategoryId
                        where c.CategoryId == categoryId
                        select p;


            foreach (var i in query)
            {
                ProductViewModel pro = new ProductViewModel()
                {
                    Content = i.Content,
                    DateCreated = i.DateCreated,
                    InputCount = i.InputCount,
                    Origin = i.Origin,
                    OutputCount = i.OutputCount,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Title = i.Title,                   
                };
                listData.Add(pro);
            }
            return listData;
        }

        public async Task<bool> UpdateOutputPrice(int productId, decimal newPrice)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null) new Exception("Không tim thấy sản phẩm");
            product.OutputCount = newPrice;
            if (await context.SaveChangesAsync() != 0)
                return true;
            return false;
        }


        public async Task<int> AddImage(int productId, IFormFile files)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null) new Exception("Không tim thấy sản phẩm");
            //foreach(var file in files)
            if (files.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    await files.CopyToAsync(stream);
                }
            }
            {
                var productImage = new ProductImage()
                {
                    productId = productId,
                    caption = $"ảnh {product.Title}",
                    imagepath = await this.SaveFile(files),
                    isDefault = true
                };
                await context.ProductImages.AddAsync(productImage);
            }
            return await context.SaveChangesAsync();
        }
        public async Task<int> AddImages(int productId, List<IFormFile> files)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null) new Exception("Không tim thấy sản phẩm");
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    var productImage = new ProductImage()
                    {
                        productId = productId,
                        caption = $"ảnh {product.Title}",
                        imagepath = await this.SaveFile(file),
                        isDefault = true
                    };
                    await context.ProductImages.AddAsync(productImage);
                }
            }
            return await context.SaveChangesAsync();
        }

        public async Task<int> RemoveImages(int ImageId)
        {
            var productImage = await context.ProductImages.FindAsync(ImageId);
            if (productImage == null) new Exception("Không tim thấy anh");
            await _storageService.DeleteFileAsync(productImage.imagepath);
            context.ProductImages.Remove(productImage);
            return await context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetProductImages(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null) new Exception("Không tim thấy sản phẩm");
            var query = from i in context.ProductImages
                        join p in context.Products on i.productId equals p.ProductId
                        select i;
            var listImages = new List<ProductImageViewModel>();
            if(query.Count() > 0)
            {
                foreach(var i in query)
                {
                    listImages.Add(
                        new ProductImageViewModel()
                        {
                            caption=i.caption,
                            imagepath=i.imagepath,
                            isDefault=i.isDefault,
                            productId=i.productId,
                            productImageId=i.productImageId
                        }
                        );
                }
            }
            return listImages;
        }
    }
}
