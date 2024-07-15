using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.Configurations
{
  internal class AuctionConfiguration : IEntityTypeConfiguration<Auction>
  {
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
      builder.HasKey(a => a.Id);

      builder.Property(a => a.VehicleId)
        .IsRequired();

      builder.HasOne(a => a.Vehicle)
        .WithMany(v => v.Auctions)
        .HasForeignKey(a => a.VehicleId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
