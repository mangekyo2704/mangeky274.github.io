using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace FruitShopSolution.WebApp.Models
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
/*        public User UserData(string Id)
        {
            //Khoa kh = new Khoa("MK01","HTTT");
            Khoa kh = new Khoa();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from KHOA where MaKhoa=@makhoa";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makhoa", Id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    kh.MaKhoa = reader["MaKhoa"].ToString();
                    kh.TenKhoa = reader["TenKhoa"].ToString();
                }
            }
            return (kh);
        }*/
    }
       
}

