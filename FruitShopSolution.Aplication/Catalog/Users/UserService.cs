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
            if(loginRequest.UserName == null || loginRequest.Password == null) throw new Exception("Vui lòng nhập đầy đủ");
            var query = from u in _context.Users where u.UserName == loginRequest.UserName select u;
            var user = query.FirstOrDefault();
            if (user == null) throw new Exception("User chua duoc dang ky");
            if (user.Password == loginRequest.Password)
                return new UserViewModel()
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Password = user.Password,
                    BirthDay = user.DateCreated,
                    Email = user.Email,
                    FristName = user.FristName,
                    LastLogin = user.LastLogin,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Gender = user.Gender,
                    Address = user.Address
                };
            else
                 throw new Exception("Password chua duoc dang ky"); ;
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
                    BirthDay = i.DateCreated,
                    Email = i.Email,
                    FristName = i.FristName,
                    LastLogin = i.LastLogin,
                    LastName = i.LastName,
                    Password = i.Password,
                    Phone = i.Phone,
                    UserName = i.UserName,
                    Gender = i.Gender,
                    Address = i.Address
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
                BirthDay = query.First().DateCreated,
                Email = query.First().Email,
                FristName = query.First().FristName,
                LastLogin = query.First().LastLogin,
                LastName = query.First().LastName,
                Password = query.First().Password,
                Phone = query.First().Phone,
                UserName = query.First().UserName,
                Gender = query.FirstOrDefault().Gender,
                Address = query.FirstOrDefault().Address
            };
        }

        public async Task<int> Register(RegisterRequest registerRequest)
        {
            /*            var username = await _context.Users.FindAsync(registerRequest.UserName);
                        if (username != null) throw new Exception("User da duoc dang ky");
                        var email = await _context.Users.FindAsync(registerRequest.Email);
                        if (email != null) throw new Exception("Email da duoc dang ky");
                        var phone = await _context.Users.FindAsync(registerRequest.Phone);
                        if (phone != null) throw new Exception("Phone da duoc dang ky");*/
            var query = from u in _context.Users select u;
            if (!String.IsNullOrEmpty(registerRequest.UserName))
            {
                query = query.Where(u => u.UserName.Equals(registerRequest.UserName));
            }
            if (query.Count() > 0) throw new Exception("User da duoc dang ky");
            var user = new User
            {
                FristName = registerRequest.FristName,
                LastName = registerRequest.LastName,
                Phone = registerRequest.Phone,
                Email = registerRequest.Email,
                DateCreated = DateTime.Now,
                Password = registerRequest.Password,
                UserName = registerRequest.UserName,
                Gender = registerRequest.Gender,
                BirthDay = registerRequest.BirthDay
            };
           await  _context.Users.AddAsync(user);
            if (await _context.SaveChangesAsync() <= 0)
                throw new Exception("Thêm không thành công");
            return user.UserId;
        }

        public async Task<bool> Update(UpdateRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);

            if (user == null ) throw new Exception($"Cannot find a product with id: {request.UserId}");

            user.FristName = request.FristName;
            user.LastName = request.LastName;
            user.BirthDay = request.BirthDay;
            user.Email = request.Email;
            user.Gender = request.Gender;
            user.Phone = request.Phone;
            if(await _context.SaveChangesAsync()<=0) throw new Exception("Cập nhật thất bại");
            return true;
        }
        public async Task<bool> UpdatePass(UpdatePassRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);

            if (user == null ) throw new Exception($"Cannot find user with id: {request.UserId}");
            user.Password = request.NewPass;
            if(await _context.SaveChangesAsync()<=0) throw new Exception("Cập nhật thất bại");
            return true;
        }
    }
}
