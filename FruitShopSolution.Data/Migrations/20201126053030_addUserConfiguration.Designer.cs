﻿// <auto-generated />
using System;
using FruitShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FruitShopSolution.Data.Migrations
{
    [DbContext(typeof(FruitShopDbContext))]
    [Migration("20201126053030_addUserConfiguration")]
    partial class addUserConfiguration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FruitShopSolution.Data.Entities.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");

                    b.HasData(
                        new
                        {
                            CartId = 1,
                            ProductId = 1,
                            Quantity = 10,
                            UserId = 1
                        },
                        new
                        {
                            CartId = 2,
                            ProductId = 2,
                            Quantity = 10,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Content = "Trái cây được sản xuất tại Mỹ và nhập khẩu vào VN với chất lượng cao",
                            Title = "Trái Cây Mỹ"
                        });
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShipAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ShipPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Shipname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            Discount = 20,
                            OrderDate = new DateTime(2020, 11, 26, 12, 30, 30, 227, DateTimeKind.Local).AddTicks(7423),
                            ShipAddress = "12/9 Kp4",
                            ShipEmail = "dnwajkdnawk@gmail.com",
                            ShipPhoneNumber = "093232132",
                            ShipPrice = 10000m,
                            Shipname = "Hiếu Nghĩa",
                            Status = 3,
                            TotalPrice = 330000m,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quanity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            ProductId = 1,
                            Price = 200000m,
                            Quanity = 10
                        },
                        new
                        {
                            OrderId = 1,
                            ProductId = 2,
                            Price = 200000m,
                            Quanity = 10
                        });
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(5000);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("InputCount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OutputCount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Content = "Ambrosia có nghĩa là thức ăn của các vị thần như mô tả trong thần thoại Hy Lạp cổ đại. Tên gọi này được lựa chọn bởi Wilfrid Mennell và vợ, họ phát hiện ra cây táo Ambrosia gốc trong vườn tại thung lũng Similkameen, British Columbia, Canada. Hiện nay táo Ambrosia được trồng ở Bắc Mỹ , Châu Âu, Chile và New Zealand, là những nơi có khí hậu đêm mát mẻ và ngày nắng ấm để táo có những trái táo màu đỏ tươi trên nền vàng kem. Táo Ambrosia Mỹ có một hương vị ngọt ngào, giòn, nhiều nước và thơm.",
                            DateCreated = new DateTime(2020, 11, 26, 12, 30, 30, 225, DateTimeKind.Local).AddTicks(495),
                            InputCount = 100000m,
                            Origin = "Mỹ",
                            OutputCount = 20000m,
                            Quantity = 100,
                            Title = "Táo Ambrosia - Táo Mỹ"
                        },
                        new
                        {
                            ProductId = 2,
                            Content = "Candy Hearts là sản phẩm của chương trình nhân giống nho tại International Fruit Genetics, California. Đây là kết quả lai giữa Princess x A2798 (một giống chưa được đặt tên từ Đại học Arkansas",
                            DateCreated = new DateTime(2020, 11, 26, 12, 30, 30, 226, DateTimeKind.Local).AddTicks(6438),
                            InputCount = 100000m,
                            Origin = "Mỹ",
                            OutputCount = 20000m,
                            Quantity = 100,
                            Title = "Nho đỏ Candy Heart Mỹ"
                        });
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.ProductImage", b =>
                {
                    b.Property<int>("productImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imagepath")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("isDefault")
                        .HasColumnType("bit");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.HasKey("productImageId");

                    b.HasIndex("productId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.ProductInCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductInCategorys");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1
                        });
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 11, 26, 12, 30, 30, 219, DateTimeKind.Local).AddTicks(1195));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("FristName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "chunamthang2000@gmail.com",
                            FristName = "Chu Nam",
                            LastLogin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Thắng",
                            Password = "12345",
                            Phone = "09292921031",
                            UserName = "thanguit"
                        });
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.Cart", b =>
                {
                    b.HasOne("FruitShopSolution.Data.Entities.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FruitShopSolution.Data.Entities.User", "User")
                        .WithMany("Cart")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.Order", b =>
                {
                    b.HasOne("FruitShopSolution.Data.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.OrderDetail", b =>
                {
                    b.HasOne("FruitShopSolution.Data.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FruitShopSolution.Data.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.ProductImage", b =>
                {
                    b.HasOne("FruitShopSolution.Data.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FruitShopSolution.Data.Entities.ProductInCategory", b =>
                {
                    b.HasOne("FruitShopSolution.Data.Entities.Category", "Category")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FruitShopSolution.Data.Entities.Product", "Product")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
