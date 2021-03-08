using FruitShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.Data.Configurations
{
    public class ProductsInPromotionConfiguration : IEntityTypeConfiguration<ProductsInPromotion>
    {
        public void Configure(EntityTypeBuilder<ProductsInPromotion> builder)
        {
            builder.HasKey(x=>new { x.ProductId,x.PromotionId});

        }
    }
}
