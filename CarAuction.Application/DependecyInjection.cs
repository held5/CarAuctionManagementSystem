using CarAuction.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuction.Application
{
  public static class DependecyInjection
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddTransient<IVehicleService, VehicleService>();
      services.AddTransient<ICarAuctionService, CarAuctionService>();

      return services;
    }
  }
}
