using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FruitShopSolution.ViewModel.Catalog.Products
{
    public class ProductUpdateRequest
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public string Origin { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public decimal OutputCount { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public string Content { get; set; }
        public IFormFile ThumnailImage { get; set; }

    }
}
