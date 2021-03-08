using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FruitShopSolution.ViewModel.Catalog.Users;
using Microsoft.EntityFrameworkCore;
using FruitShopSolution.Data.Entities;
namespace FruitShopSolution.Application.Catalog.Users
{
    public interface IUserService
    {
        Task<UserViewModel> Accuracy(LoginRequest loginReqstue);
        Task<int> Register(RegisterRequest loginRequest);
        Task<List<UserViewModel>> GetAll();
        Task<UserViewModel> GetById(int id);
        Task<bool> Update(UpdateRequest request);
        Task<bool> UpdatePass(UpdatePassRequest request);
    }
}
