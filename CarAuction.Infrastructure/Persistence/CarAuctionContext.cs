using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarAuction.Infrastructure.Persistence
{
  internal class CarAuctionContext : DbContext
  {
      public CarAuctionContext(DbContextOptions<CarAuctionContext> options)
        : base(options)
      {
      }

      public DbSet<Vehicle> Vehicles { get; set; }

      public DbSet<Auction> Auctions { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      }
    }
}
