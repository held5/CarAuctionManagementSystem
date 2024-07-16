using AutoMapper;

using CarAuction.Application.Dtos;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;

namespace CarAuction.Application.Profiles
{
  internal class VehicleProfile : Profile
  {
    public VehicleProfile()
    {
      CreateMap<AddVehicleRequestDto, Vehicle>()
        .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => Enum.Parse<VehicleType>(src.VehicleType, true)));

      CreateMap<AddSedanRequestDto, Sedan>();
      CreateMap<AddTruckRequestDto, Truck>();
      CreateMap<AddSuvRequestDto, Suv>();

      CreateMap<Vehicle, VehicleDto>()
        .Include<Truck, TruckDto>()
        .Include<Suv, SuvDto>()
        .Include<Sedan, SedanDto>()
        .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType.ToString()));

      CreateMap<Truck, TruckDto>();
      CreateMap<Suv, SuvDto>();
      CreateMap<Sedan, SedanDto>();
    }
  }
}
