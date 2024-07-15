using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.Configurations
{
    internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    { 
      public void Configure(EntityTypeBuilder<Vehicle> builder)
      {
        // Configure TPH for entity hierarchy
        builder.HasDiscriminator<VehicleType>(nameof(VehicleType))
          .HasValue<Sedan>(VehicleType.Sedan)
          .HasValue<Suv>(VehicleType.Suv)
          .HasValue<Truck>(VehicleType.Truck);

        builder.HasKey(v => v.Id);

        builder.HasMany(c => c.Auctions)
          .WithOne(a => a.Vehicle)
          .HasForeignKey(a => a.VehicleId)
          .OnDelete(DeleteBehavior.Cascade);

      builder.Property(v => v.Manufacturer)
          .IsRequired()
          .HasMaxLength(100);

        builder.Property(v => v.Model)
          .IsRequired()
          .HasMaxLength(50);

        builder.Property(v => v.Year)
          .IsRequired();

        builder.Property(v => v.StartingBid).IsRequired();
      }
    }
}
