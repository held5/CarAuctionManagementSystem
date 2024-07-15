namespace CarAuction.Application.Dtos
{
  public record TruckDto : VehicleDto
  {
    public TruckDto(
      Guid id,
      double loadCapacity,
      string manufacturer,
      string model,
      int year,
      decimal startingBid,
      string vehicleType)
      : base(id, manufacturer, model, year, startingBid, vehicleType)
      => LoadCapacity = loadCapacity;

    public double LoadCapacity { get; init; }
  }
}
