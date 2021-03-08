using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string FristName { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string  Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } 
        public string Gender { get; set; } 
        public string Address { get; set; } 
        public DateTime BirthDay { get; set; } 
        public List<Order> Orders { get; set; }
        public List<Cart> Cart { get; set; }
        
    }
}
