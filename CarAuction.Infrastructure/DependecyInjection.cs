using CarAuction.Domain.Interfaces;
using CarAuction.Infrastructure.Persistence;
using CarAuction.Infrastructure.Persistence.Interceptors;
using CarAuction.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuction.Infrastructure
{
  public static class DependecyInjection
  {
      public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
      {
        services.AddDbContext<CarAuctionContext>((sp, options) =>
        {
          options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
          options.UseInMemoryDatabase(connectionString);
        });

        services.AddScoped<ISaveChangesInterceptor, EntityInterceptor>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton(TimeProvider.System);

        return services;
      }
  }
}
