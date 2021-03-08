using FruitShopSolution.Data.EF;
using FruitShopSolution.Data.Entities;
using FruitShopSolution.ViewModel.Catalog.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Data.SqlClient;
using FruitShopSolution.ViewModel.Catalog.Users;

namespace FruitShopSolution.Application.Catalog.Admin
{
    public class AdminService : IAdminService
    {
        //private readonly StoreContext _context = new StoreContext();
        private readonly FruitShopDbContext _context;
        public AdminService(FruitShopDbContext context)
        {
            _context = context;
        }
        public AdminViewModel Accuracy(AdminLoginRequest loginRequest)
        {
            //var admin = await _context.Admins.FindAsync(loginRequest.UserName);
            var admin = from p in _context.Admins
                        where p.Username == loginRequest.UserName
                        select p;
            if (admin == null) throw new Exception("User chua duoc dang ky");
            admin.Where(x => x.Password.Contains(loginRequest.Password));
            if (admin != null)
            {
                return new AdminViewModel()
                {
                    UserName = admin.First().Username,
                    Password = admin.First().Password,
                    Name = admin.First().Name
                };
            }
            else
                return null;
            /*AdminViewModel admin = new AdminViewModel();
            using (SqlConnection conn = new SqlConnection("Data Source =.\\sqlexpress; Initial Catalog = FruitShopDatabase; Integrated Security = True")) 
            {

                conn.Open();
                var str = $"select * from Admins where Username = {loginRequest.UserName}";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) return null;
                    reader.Read();
                    admin.Name = reader["Name"].ToString();
                    admin.UserName = reader["UserName"].ToString();
                    admin.Password = reader["Password"].ToString();
                }
            }
            return admin;*/
        }
    }


}
