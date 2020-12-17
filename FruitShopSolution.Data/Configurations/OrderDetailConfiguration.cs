using System;
using System.Collections.Generic;
using System.Text;
using FruitShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitShopSolution.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
             builder.HasKey(x => new { x.OrderId, x.ProductId });
            builder.HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId);
            builder.HasOne(x => x.Product).WithMany(o => o.OrderDetails).HasForeignKey(o => o.ProductId);
        }
    }
}
