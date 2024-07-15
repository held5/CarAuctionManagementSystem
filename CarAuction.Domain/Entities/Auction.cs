namespace CarAuction.Domain.Entities
{
  public class Auction : BaseEntity
  {
    public Guid VehicleId { get; set; }

    public virtual Vehicle Vehicle { get; set; }

    public bool IsActive { get; set; }

    public decimal CurrentHighestBid { get; set; }
  }
}
