using CarAuction.Domain.Enums;

namespace CarAuction.Domain.Entities
{
  public abstract class Vehicle : BaseEntity
  {
    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public int Year { get; set; }

    public decimal StartingBid { get; set; }

    public VehicleType VehicleType { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; }

  }
}
