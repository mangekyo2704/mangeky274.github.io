using System;
using System.Collections.Generic;
using System.Text;
using FruitShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitShopSolution.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.CartId);
            builder.HasOne(x => x.User).WithMany(x=>x.Cart).HasForeignKey(x=>x.UserId);
            builder.HasOne(x => x.Product).WithMany(x=>x.Carts).HasForeignKey(x=>x.ProductId);

        }
    }
}
