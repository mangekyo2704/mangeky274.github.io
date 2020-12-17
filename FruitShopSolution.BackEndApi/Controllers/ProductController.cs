using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Products;
using FruitShopSolution.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FruitShopSolution.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductPublicService _productPublicService;
        private readonly IProductManageService _productManageService;
        public ProductController(IProductPublicService productPublicService, IProductManageService productManageService)
        {
            _productPublicService = productPublicService;
            _productManageService = productManageService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var product = await _productPublicService.GetAll();
            return Ok(product);
        }
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
        {
            var product = await _productPublicService.GetAllByCategoryId(request);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            var result = await _productManageService.Create(request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productManageService.GetById(id);
            if (result == null)
                return BadRequest("Can not find product:${id}");
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            var result = await _productManageService.Update(request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
        [HttpPut("price/{id}/{newPrice}")]
        public async Task<IActionResult> UpdateOutputPrice([FromQuery] int id, decimal newPrice)
        {
            var isSuccess = await _productManageService.UpdateOutputPrice(id, newPrice);
            if (!isSuccess)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productManageService.Delete(id);
            if (result == 0)
                return BadRequest("Can not find product:${id}");
            return Ok();
        }
        //Image Product

        [HttpPost("Image")]
        public async Task<IActionResult> AddImage(int productId,IFormFile files)
        {
            if (files.Length > 0)
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await files.CopyToAsync(stream);
                }
            }
            var result = await _productManageService.AddImage(productId, files);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("Image/{idimage}")]
        public async Task<IActionResult> RemoveImage(int idimage)
        {
            var result = await _productManageService.RemoveImages(idimage);
            if (result == 0)
                return BadRequest("Can not find product:${idimage}");
            return Ok();
        }
    }
}
