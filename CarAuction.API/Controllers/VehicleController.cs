using CarAuction.Application.Dtos;
using CarAuction.Application.Services;

using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VehicleController : ControllerBase
  {
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
      _vehicleService = vehicleService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Create([FromBody] AddVehicleRequestDto addVehicleRequest)
    {
      var result = await _vehicleService.AddVehicleAsync(addVehicleRequest);

      return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> Search([FromQuery] string? type, [FromQuery] string? manufacturer, [FromQuery] string? model, [FromQuery] int? year)
    {
      var vehicles = await _vehicleService.SearchAsync(type, manufacturer, model, year);

      return Ok(vehicles);
    }
  }
}
