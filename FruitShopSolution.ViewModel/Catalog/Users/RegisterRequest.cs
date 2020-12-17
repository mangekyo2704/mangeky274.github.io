﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Users
{
    public class RegisterRequest
    {
        public string  UserName { get; set; }
        public string  Password { get; set; }
        public string LastName { get; set; }
        public string FristName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
