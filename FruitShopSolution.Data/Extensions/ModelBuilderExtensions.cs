using FruitShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Admin>().HasData(new Admin()
            {
                AdminId = 1,
                Name = "Tài",
                Password = "admin",
                Username = "admin"
            });
            builder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 1,
                    Title = "Táo Ambrosia - Táo Mỹ"
                    ,
                    DateCreated = DateTime.Now
                    ,
                    Content = "Ambrosia có nghĩa là thức ăn của các vị thần như mô tả trong thần thoại Hy Lạp cổ đại. Tên gọi này được lựa chọn bởi Wilfrid Mennell và vợ, họ phát hiện ra cây táo Ambrosia gốc trong vườn tại thung lũng Similkameen, British Columbia, Canada. Hiện nay táo Ambrosia được trồng ở Bắc Mỹ , Châu Âu, Chile và New Zealand, là những nơi có khí hậu đêm mát mẻ và ngày nắng ấm để táo có những trái táo màu đỏ tươi trên nền vàng kem. Táo Ambrosia Mỹ có một hương vị ngọt ngào, giòn, nhiều nước và thơm."
                    ,
                    Origin = "Mỹ"
                    ,
                    InputCount = 100000
                    ,
                    OutputCount = 20000
                    ,
                    Quantity = 100
                });
            builder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 2,
                    Title = "Nho đỏ Candy Heart Mỹ"
                    ,
                    DateCreated = DateTime.Now
                    ,
                    Content = "Candy Hearts là sản phẩm của chương trình nhân giống nho tại International Fruit Genetics, California. Đây là kết quả lai giữa Princess x A2798 (một giống chưa được đặt tên từ Đại học Arkansas"
                    ,
                    Origin = "Mỹ"
                    ,
                    InputCount = 100000
                    ,
                    OutputCount = 20000
                    ,
                    Quantity = 100
                });
            builder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Title = "Trái Cây Mỹ",
                    Content = "Trái cây được sản xuất tại Mỹ và nhập khẩu vào VN với chất lượng cao"
                });
            builder.Entity<ProductInCategory>().HasData(
                new ProductInCategory
                {
                    CategoryId = 1,
                    ProductId = 1
                });
            builder.Entity<ProductInCategory>().HasData(
                new ProductInCategory
                {
                    CategoryId = 1,
                    ProductId = 2
                });
            builder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    FristName = "Chu Nam",
                    LastName = "Thắng",
                    Email = "chunamthang2000@gmail.com",
                    PhoneNumber = "09292921031",
                    UserName = "thanguit",
                    PasswordHash = "12345"

                });
            builder.Entity<Cart>().HasData(
                new Cart
                {
                    CartId = 1,
                    ProductId = 1,
                    UserId = 1,
                    Quantity = 10
                });
            builder.Entity<Cart>().HasData(
                new Cart
                {
                    CartId = 2,
                    ProductId = 2,
                    UserId = 1,
                    Quantity = 10
                });
            builder.Entity<Order>().HasData(
                new Order
                {
                    UserId = 1,
                    OrderDate = DateTime.Now,
                    OrderId = 1,
                    ShipAddress = "12/9 Kp4",
                    ShipEmail = "dnwajkdnawk@gmail.com",
                    Discount = 20,
                    Shipname = "Hiếu Nghĩa",
                    ShipPhoneNumber = "093232132",
                    Status = Enums.StatusOrder.Success,
                    ShipPrice = 10000,
                    TotalPrice = Decimal.Parse((400000 * 0.8 + 10000).ToString())
                });
            builder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 1,
                    Quanity = 10,
                    Price = 200000
                });
            builder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 2,
                    Quanity = 10,
                    Price = 200000
                });
        }
    }
}
