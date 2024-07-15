using System.ComponentModel.DataAnnotations;

namespace CarAuction.Application.Dtos
{
  public record AddSedanRequestDto : AddVehicleRequestDto
  {
    public AddSedanRequestDto(string manufacturer, string model, int year, decimal startingBid, int numberOfDoors, string vehicleType)
      : base(manufacturer, model, year, startingBid, vehicleType)
      => NumberOfDoors = numberOfDoors;

    [Range(2, 5, ErrorMessage = "Number of doors must be between 2 and 5")]
    public int NumberOfDoors { get; init; }
  }
}
