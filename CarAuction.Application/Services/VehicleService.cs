using System.Linq.Expressions;

using AutoMapper;

using CarAuction.Application.Dtos;
using CarAuction.Application.Exceptions;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using CarAuction.Domain.Interfaces;

namespace CarAuction.Application.Services
{
  public class VehicleService : IVehicleService
  {
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IRepository<Vehicle> _vehicleRepository;

    public VehicleService(IUnitOfWork uow, IMapper mapper)
    {
      _uow = uow;
      _mapper = mapper;
      _vehicleRepository = _uow.GetRepository<Vehicle>();
    }

    /// <inheritdoc />
    public async Task<Guid> AddVehicleAsync(AddVehicleRequestDto addVehicleRequest)
    {
      Vehicle vehicle = addVehicleRequest switch
                        {
                          AddSedanRequestDto sedanRequest => _mapper.Map<Sedan>(sedanRequest),
                          AddSuvRequestDto suvRequest => _mapper.Map<Suv>(suvRequest),
                          AddTruckRequestDto truckRequest => _mapper.Map<Truck>(truckRequest),
                          _ => throw new ValidationException("Invalid vehicle type")
                        };

      await _vehicleRepository.Add(vehicle);
      await _uow.SaveChangesAsync();

      return vehicle.Id;
    }

    /// <inheritdoc />
    public async Task<List<VehicleDto>> SearchAsync(string? type, string? manufacturer, string? model, int? year)
    {
      VehicleType? vehicleType = ParseVehicleType(type);

      Expression<Func<Vehicle, bool>> filter = v => true;

      if (vehicleType.HasValue)
      {
        filter = CombineFilters(filter, v => v.VehicleType == vehicleType.Value);
      }

      if (!string.IsNullOrEmpty(manufacturer))
      {
        filter = CombineFilters(filter, v => v.Manufacturer.Contains(manufacturer, StringComparison.CurrentCultureIgnoreCase));
      }

      if (!string.IsNullOrEmpty(model))
      {
        filter = CombineFilters(filter, v => v.Model.Contains(model, StringComparison.CurrentCultureIgnoreCase));
      }

      if (year.HasValue)
      {
        filter = CombineFilters(filter, v => v.Year == year.Value);
      }

      var vehicles = await _vehicleRepository.GetAll(filter);

      return _mapper.Map<List<VehicleDto>>(vehicles);
    }

    private static Expression<Func<Vehicle, bool>> CombineFilters(
      Expression<Func<Vehicle, bool>> filter1,
      Expression<Func<Vehicle, bool>> filter2)
    {
      var parameter = Expression.Parameter(typeof(Vehicle), "v");

      var combinedBody = Expression.AndAlso(
        Expression.Invoke(filter1, parameter),
        Expression.Invoke(filter2, parameter));

      return Expression.Lambda<Func<Vehicle, bool>>(combinedBody, parameter);
    }

    private static VehicleType? ParseVehicleType(string? type)
    {
      if (string.IsNullOrEmpty(type))
      {
        return null;
      }

      if (Enum.TryParse(type, true, out VehicleType parsedType))
      {
        return parsedType;
      }

      return null;
    }
  }
}
