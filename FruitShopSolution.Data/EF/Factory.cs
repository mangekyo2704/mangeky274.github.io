using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShopSolution.Data.EF
{
    public class Factory : IDesignTimeDbContextFactory<FruitShopDbContext>
    {
        public FruitShopDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<FruitShopDbContext>();
            options.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=FruitShopDatabase;Integrated Security=True");
            return new FruitShopDbContext(options.Options);
        }
    }
}
