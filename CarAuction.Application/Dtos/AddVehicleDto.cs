using System.ComponentModel.DataAnnotations;

namespace CarAuction.Application.Dtos
{
  public abstract record AddVehicleRequestDto
  {
    protected AddVehicleRequestDto(string manufacturer, string model, int year, decimal startingBid, string vehicleType)
    {
      Manufacturer = manufacturer;
      Model = model;
      Year = year;
      StartingBid = startingBid;
      VehicleType = vehicleType;
    }

    [Required(ErrorMessage = "Manufacturer is required")]
    public string Manufacturer { get; init; }

    [Required(ErrorMessage = "Model is required")]
    public string Model { get; init; }

    [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
    public int Year { get; init; }

    [Range(0, double.MaxValue, ErrorMessage = "Starting bid must be non-negative")]
    public decimal StartingBid { get; init; }

    [Required(ErrorMessage = "Vehicle type is required")]
    public string VehicleType { get; init; }
  }
}
