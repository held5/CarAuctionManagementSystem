using CarAuction.Application.Dtos;

namespace CarAuction.Application.Services
{
  /// <summary>
  ///   Represents a service interface for managing vehicles.
  /// </summary>
  public interface IVehicleService
  {
    /// <summary>
    ///   Adds a new vehicle based on the provided request.
    /// </summary>
    /// <param name="addVehicleRequest">The data transfer object containing information to add a new vehicle.</param>
    /// <returns>The task result with the unique identifier of the newly added vehicle.</returns>
    Task<Guid> AddVehicleAsync(AddVehicleRequestDto addVehicleRequest);

    /// <summary>
    ///   Searches for vehicles based on optional search criteria.
    /// </summary>
    /// <param name="type">Optional. The type of vehicle to search for.</param>
    /// <param name="manufacturer">Optional. The manufacturer of the vehicle.</param>
    /// <param name="model">Optional. The model of the vehicle.</param>
    /// <param name="year">Optional. The year of the vehicle.</param>
    /// <returns>The task result with a list of vehicle data transfer objects.</returns>
    Task<List<VehicleDto>> SearchAsync(string? type, string? manufacturer, string? model, int? year);
  }
}
