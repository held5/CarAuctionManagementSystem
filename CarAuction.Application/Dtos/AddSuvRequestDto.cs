using System.ComponentModel.DataAnnotations;

namespace CarAuction.Application.Dtos
{
    public record AddSuvRequestDto : AddVehicleRequestDto
  {
    public AddSuvRequestDto(
      string manufacturer,
      string model,
      int year,
      decimal startingBid,
      int numberOfSeats,
      string vehicleType
    ) : base(manufacturer, model, year, startingBid, vehicleType)
      => NumberOfSeats = numberOfSeats;

    [Range(4, 9, ErrorMessage = "Number of seats must be between 4 and 9")]
    public int NumberOfSeats { get; init; }
    }
}
