using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FruitShopSolution.ViewModel.Validation;
namespace FruitShopSolution.ViewModel.Catalog.Products
{
    public class ProductCreateRequest
    {
        // public decimal InputPrice { get; internal set; }
        [Required(ErrorMessage = "Vui lòng không bỏ trống!")]
        [StringLength(100)]
        public string Origin { get; set; }
        [Required(ErrorMessage = "Vui lòng không bỏ trống!")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Vui lòng không bỏ trống!")]
        [Range(0, 9999999.99)]
        public decimal InputCount { get; set; }
        [Range(0, 9999999.99, ErrorMessage = "Giá trị không hợp lệ!")]
        public decimal OutputCount { get; set; }
        [Range(0,9999,ErrorMessage = "Giá trị không hợp lệ!")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Vui lòng không bỏ trống!")]
        public string Content { get; set; }
        public int[] categoryId { get; set; }
        public string Unit { get; set; }
        public DateTime DateCreated { get; set; }
     /*   [ValidateFile(ErrorMessage = "Vui lòng chọn ảnh!")]*/
        public List<IFormFile> ThumnailImage { get; set; }
    }
}
