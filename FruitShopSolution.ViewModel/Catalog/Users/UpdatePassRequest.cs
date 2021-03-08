using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.ViewModel.Catalog.Users
{
    public class UpdatePassRequest
    {
        public int UserId { get; set; }
        public string NewPass { get; set; }
    }
}
