using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;

namespace ExoticsCarsStoreServerSide.Persistence.Data.Context
{
    public class ExoticsCarsStoreDbContext(DbContextOptions<ExoticsCarsStoreDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExoticsCarsStoreDbContext).Assembly);

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
    }
}


