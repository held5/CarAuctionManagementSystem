namespace CarAuction.Application.Dtos
{
  public record SuvDto : VehicleDto
  {
    public SuvDto(Guid id, int numberOfSeats, string manufacturer, string model, int year, decimal startingBid, string vehicleType)
      : base(id, manufacturer, model, year, startingBid, vehicleType)
      => NumberOfSeats = numberOfSeats;

    public int NumberOfSeats { get; init; }
  }
}
