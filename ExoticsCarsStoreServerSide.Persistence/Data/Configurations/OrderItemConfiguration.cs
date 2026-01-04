using ExoticsCarsStoreServerSide.Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExoticsCarsStoreServerSide.Persistence.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(OI =>OI.Price).HasPrecision(8,2);

            builder.OwnsOne(OI => OI.Product,OEntity => 
            {
                OEntity.Property(OI => OI.ProductName).HasMaxLength(100);
                OEntity.Property(OI => OI.PictureUrl).HasMaxLength(200);
            });
        }
    }
}
