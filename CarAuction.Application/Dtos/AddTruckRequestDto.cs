using System.ComponentModel.DataAnnotations;

namespace CarAuction.Application.Dtos
{
  public record AddTruckRequestDto : AddVehicleRequestDto
  {
    public AddTruckRequestDto(
      string manufacturer,
      string model,
      int year,
      decimal startingBid,
      double loadCapacity,
      string vehicleType
    ) : base(manufacturer, model, year, startingBid, vehicleType)
      => LoadCapacity = loadCapacity;

    [Range(0.1, double.MaxValue, ErrorMessage = "Load capacity must be positive")]
    public double LoadCapacity { get; init; }
    }
}
