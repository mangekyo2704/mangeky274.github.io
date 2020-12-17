using FruitShopSolution.ViewModel.Catalog.Admin;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace FruitShopSolution.Application
{
    public class StoreContext
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public StoreContext() //phuong thuc khoi tao
        {
            ConnectionString = "Data Source =.\\sqlexpress; Initial Catalog = FruitShopDatabase; Integrated Security = True";
        }

        public SqlConnection GetConnection() //lấy connection 
        {
            return new SqlConnection(ConnectionString);
        }
        public List<AdminViewModel> ViewAdmin()
        {
            //Khoa kh = new Khoa("MK01","HTTT");
            List<AdminViewModel> ListAdmin = new List<AdminViewModel>();
            using (SqlConnection conn = GetConnection())
            {
                
                conn.Open();
                var str = "select * from Admin";
                SqlCommand cmd = new SqlCommand(str, conn);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) {
                        AdminViewModel kh = new AdminViewModel();
                        kh.UserName = reader["UserName"].ToString();
                        kh.Password = reader["Password"].ToString();
                        kh.Name = reader["Name"].ToString();
                        ListAdmin.Add(kh);
                    }
                }
            }
            return ListAdmin;
        }

    }

}

