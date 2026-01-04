using ExoticsCarsStoreServerSide.Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExoticsCarsStoreServerSide.Persistence.Data.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(DM => DM.Price).HasPrecision(8,2);
            builder.Property(DM => DM.ShortName).HasMaxLength(50);
            builder.Property(DM => DM.DeliveryTime).HasMaxLength(50);
            builder.Property(DM => DM.Description).HasMaxLength(100);
        }
    }
}
