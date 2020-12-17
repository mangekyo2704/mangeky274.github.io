using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FruitShopSolution.Data.Migrations
{
    public partial class AddDataInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Content", "ParentId", "Title" },
                values: new object[] { 1, "Trái cây được sản xuất tại Mỹ và nhập khẩu vào VN với chất lượng cao", null, "Trái Cây Mỹ" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Content", "DateCreated", "InputCount", "Origin", "OutputCount", "Quantity", "Title" },
                values: new object[,]
                {
                    { 1, "Ambrosia có nghĩa là thức ăn của các vị thần như mô tả trong thần thoại Hy Lạp cổ đại. Tên gọi này được lựa chọn bởi Wilfrid Mennell và vợ, họ phát hiện ra cây táo Ambrosia gốc trong vườn tại thung lũng Similkameen, British Columbia, Canada. Hiện nay táo Ambrosia được trồng ở Bắc Mỹ , Châu Âu, Chile và New Zealand, là những nơi có khí hậu đêm mát mẻ và ngày nắng ấm để táo có những trái táo màu đỏ tươi trên nền vàng kem. Táo Ambrosia Mỹ có một hương vị ngọt ngào, giòn, nhiều nước và thơm.", new DateTime(2020, 11, 19, 19, 29, 12, 589, DateTimeKind.Local).AddTicks(5526), 100000m, "Mỹ", 20000m, 100, "Táo Ambrosia - Táo Mỹ" },
                    { 2, "Candy Hearts là sản phẩm của chương trình nhân giống nho tại International Fruit Genetics, California. Đây là kết quả lai giữa Princess x A2798 (một giống chưa được đặt tên từ Đại học Arkansas", new DateTime(2020, 11, 19, 19, 29, 12, 591, DateTimeKind.Local).AddTicks(9310), 100000m, "Mỹ", 20000m, 100, "Nho đỏ Candy Heart Mỹ" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FristName", "LastLogin", "LastName", "Password", "Phone", "UserName" },
                values: new object[] { 1, "chunamthang2000@gmail.com", "Chu Nam", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thắng", "12345", "09292921031", "thanguit" });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "ProductId", "Quantity", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 10, 1 },
                    { 2, 2, 10, 1 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Discount", "OrderDate", "ShipAddress", "ShipEmail", "ShipPhoneNumber", "ShipPrice", "Shipname", "Status", "TotalPrice", "UserId" },
                values: new object[] { 1, 20, new DateTime(2020, 11, 19, 19, 29, 12, 593, DateTimeKind.Local).AddTicks(1328), "12/9 Kp4", "dnwajkdnawk@gmail.com", "093232132", 10000m, "Hiếu Nghĩa", 3, 330000m, 1 });

            migrationBuilder.InsertData(
                table: "ProductInCategorys",
                columns: new[] { "ProductId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId", "Price", "Quanity" },
                values: new object[] { 1, 1, 200000m, 10 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId", "Price", "Quanity" },
                values: new object[] { 1, 2, 200000m, 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductInCategorys",
                keyColumns: new[] { "ProductId", "CategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductInCategorys",
                keyColumns: new[] { "ProductId", "CategoryId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
