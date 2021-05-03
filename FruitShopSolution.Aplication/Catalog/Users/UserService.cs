using FruitShopSolution.Data.EF;
using FruitShopSolution.Data.Entities;
using FruitShopSolution.ViewModel.Catalog.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace FruitShopSolution.Application.Catalog.Users
{
    public class UserService : IUserService
    {
        private readonly FruitShopDbContext _context;
        public UserService(FruitShopDbContext context)
        {
            _context = context;
        }
        public async Task<UserViewModel> Accuracy(LoginRequest loginRequest)
        {
            var user = await _context.Users.FindAsync(loginRequest.UserName);
            if (user == null) throw new Exception("User chua duoc dang ky");
            if (user.Password == loginRequest.Password)
                return new UserViewModel()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    DateCreated = user.DateCreated,
                    Email = user.Email,
                    FristName = user.FristName,
                    LastLogin = user.LastLogin,
                    LastName = user.LastName,
                    Phone = user.Phone
                };
            else
                return null;
        }
        public async Task<List<UserViewModel>> GetAll()
        {
            List<UserViewModel> listData = new List<UserViewModel>();
            var query = from p in _context.Users
                        select p;
            foreach (var i in query)
            {
                UserViewModel user = new UserViewModel()
                {
                    UserId = i.UserId,
                    DateCreated = i.DateCreated,
                    Email = i.Email,
                    FristName = i.FristName,
                    LastLogin = i.LastLogin,
                    LastName = i.LastName,
                    Password = i.Password,
                    Phone = i.Phone,
                    UserName = i.UserName
                };
                listData.Add(user);
            }
            return listData;
        }

        public async Task<UserViewModel> GetById(int id)
        {
            var query = from p in _context.Users
                        where p.UserId == id
                        select p;
            return new UserViewModel()
            {
                UserId = query.First().UserId,
                DateCreated = query.First().DateCreated,
                Email = query.First().Email,
                FristName = query.First().FristName,
                LastLogin = query.First().LastLogin,
                LastName = query.First().LastName,
                Password = query.First().Password,
                Phone = query.First().Phone,
                UserName = query.First().UserName
            };
        }

        public async Task<bool> Register(RegisterRequest registerRequest)
        {
            var username = await _context.Users.FindAsync(registerRequest.UserName);
            if (username != null) throw new Exception("User da duoc dang ky");
            var email = await _context.Users.FindAsync(registerRequest.Email);
            if (email != null) throw new Exception("Email da duoc dang ky");
            var phone = await _context.Users.FindAsync(registerRequest.Phone);
            if (phone != null) throw new Exception("Phone da duoc dang ky");
            var query = from u in _context.Users select u;
            if (!String.IsNullOrEmpty(registerRequest.UserName))
            {
                query = query.Where(u => u.UserName.Equals(registerRequest.UserName));
                Console.WriteLine(query.Count());
            }
            if (query.Count() > 0) throw new Exception("User da duoc dang ky");
            var user = new AppUser
            {
                FristName = registerRequest.FristName,
                LastName = registerRequest.LastName,
                Phone = registerRequest.Phone,
                Email = registerRequest.Email,
                DateCreated = DateTime.Now,
                Password = registerRequest.Password,
                UserName = registerRequest.UserName

            };
            _context.Users.Add(user);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
