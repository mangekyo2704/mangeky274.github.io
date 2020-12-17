using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FruitShopSolution.Data.Migrations
{
    public partial class addAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 11, 16, 21, 24, 906, DateTimeKind.Local).AddTicks(5716),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 26, 12, 30, 30, 219, DateTimeKind.Local).AddTicks(1195));

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Name", "Password", "Username" },
                values: new object[] { 1, "Tài", "admin", "admin" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2020, 12, 11, 16, 21, 24, 915, DateTimeKind.Local).AddTicks(8256));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 11, 16, 21, 24, 914, DateTimeKind.Local).AddTicks(3957));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2020, 12, 11, 16, 21, 24, 914, DateTimeKind.Local).AddTicks(7207));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 26, 12, 30, 30, 219, DateTimeKind.Local).AddTicks(1195),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 11, 16, 21, 24, 906, DateTimeKind.Local).AddTicks(5716));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2020, 11, 26, 12, 30, 30, 227, DateTimeKind.Local).AddTicks(7423));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 11, 26, 12, 30, 30, 225, DateTimeKind.Local).AddTicks(495));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2020, 11, 26, 12, 30, 30, 226, DateTimeKind.Local).AddTicks(6438));
        }
    }
}
