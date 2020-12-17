using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitShopSolution.WebApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string FristName { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
