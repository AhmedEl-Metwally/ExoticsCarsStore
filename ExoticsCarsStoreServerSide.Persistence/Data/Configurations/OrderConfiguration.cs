using ExoticsCarsStoreServerSide.Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExoticsCarsStoreServerSide.Persistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.SubTotal).HasPrecision(8,2);

            builder.OwnsOne(O => O.Address,OEntity => 
            {
                OEntity.Property(O => O.FirstName).HasMaxLength(50);
                OEntity.Property(O => O.LastName).HasMaxLength(50);
                OEntity.Property(O => O.City).HasMaxLength(50);
                OEntity.Property(O => O.Country).HasMaxLength(50);
                OEntity.Property(O => O.Street).HasMaxLength(50);
            });
        }
    }
}
