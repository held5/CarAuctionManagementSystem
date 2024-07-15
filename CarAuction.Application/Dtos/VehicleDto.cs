namespace CarAuction.Application.Dtos
{
  public abstract record VehicleDto
  {
    protected VehicleDto(Guid id, string manufacturer, string model, int year, decimal startingBid, string vehicleType)
    {
      Id = id;
      Manufacturer = manufacturer;
      Model = model;
      Year = year;
      StartingBid = startingBid;
      VehicleType = vehicleType;
    }

    public Guid Id { get; init; }

    public string Manufacturer { get; init; }

    public string Model { get; init; }

    public int Year { get; init; }

    public decimal StartingBid { get; init; }

    public string VehicleType { get; init; }
  }
}
