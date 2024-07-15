using CarAuction.API.Converters;
using CarAuction.API.Filters;
using CarAuction.API.Middlewares;
using CarAuction.Application;
using CarAuction.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(options =>
{
  options.Filters.Add(typeof(ValidateModelFilter));
})
.AddJsonOptions(options =>
{
  options.JsonSerializerOptions.Converters.Add(new AddVehicleRequestDtoConverter());
  options.JsonSerializerOptions.Converters.Add(new VehicleDtoConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
