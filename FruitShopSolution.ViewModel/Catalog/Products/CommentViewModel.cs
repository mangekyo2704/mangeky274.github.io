using System;
using System.Collections.Generic;
using System.Text;
using FruitShopSolution.ViewModel.Catalog.Users;
namespace FruitShopSolution.ViewModel.Catalog.Products
{
    public class CommentViewModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public DateTime? Time_Comment { get; set; }
        public UserViewModel User { get; set; }
    }
}
