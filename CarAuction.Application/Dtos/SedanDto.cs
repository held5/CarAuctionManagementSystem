namespace CarAuction.Application.Dtos
{
  public record SedanDto : VehicleDto
  {
    public SedanDto(Guid id, int numberOfDoors, string manufacturer, string model, int year, decimal startingBid, string vehicleType)
      : base(id, manufacturer, model, year, startingBid, vehicleType)
      => NumberOfDoors = numberOfDoors;

    public int NumberOfDoors { get; init; }
  }
}
