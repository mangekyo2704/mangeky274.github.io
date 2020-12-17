using FruitShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasOne(i => i.Product).WithMany(i => i.ProductImages).HasForeignKey(p => p.productId);
            builder.Property(i => i.imagepath).HasMaxLength(200).IsRequired();
            builder.HasKey(i => i.productImageId);
        }
    }
}
