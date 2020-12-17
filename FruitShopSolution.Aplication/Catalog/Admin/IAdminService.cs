using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FruitShopSolution.ViewModel.Catalog.Admin;
using Microsoft.EntityFrameworkCore;
using FruitShopSolution.Data.Entities;
using FruitShopSolution.ViewModel.Catalog.Users;

namespace FruitShopSolution.Application.Catalog.Admin
{
    public interface IAdminService
    {
        AdminViewModel Accuracy(AdminLoginRequest loginReqstue);
/*        Task<int> Update(User request);
        Task<int> Delete(int id);*/
    }
}
