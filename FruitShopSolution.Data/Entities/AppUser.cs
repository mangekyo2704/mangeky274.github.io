using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.Data.Entities
{
    public class AppUser: IdentityUser<int>
    {
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string FristName { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime DateCreated { get; set; }
        public string  Phone { get; set; }
        public string Password { get; set; }
        public List<Order> Orders { get; set; }
        public List<Cart> Cart { get; set; }
        
    }
}
