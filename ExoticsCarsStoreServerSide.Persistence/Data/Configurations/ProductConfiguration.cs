using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExoticsCarsStoreServerSide.Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(100);
            builder.Property(P => P.Description).HasMaxLength(500);
            builder.Property(P => P.PictureUrl).HasMaxLength(200);
            builder.Property(P => P.Price).HasPrecision(18,2);


            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(P => P.BrandId);
            builder.HasOne(P => P.ProductType).WithMany().HasForeignKey(P => P.TypeId);
        }
    }
}
