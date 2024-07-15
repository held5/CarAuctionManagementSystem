using CarAuction.Application.Dtos;
using CarAuction.Application.Services;

using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuctionsController : ControllerBase
  {
    private readonly ICarAuctionService _carAuctionService;

    public AuctionsController(ICarAuctionService carAuctionService)
    {
      _carAuctionService = carAuctionService;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Guid vehicleId)
    {
      var result = await _carAuctionService.AddAuctionAsync(vehicleId);

      return Ok(result);
    }

    [HttpPut("{auctionId}/close")]
    public async Task<ActionResult> Close([FromRoute] Guid auctionId)
    {
      await _carAuctionService.CloseAuctionAsync(auctionId);

      return Ok();
    }

    [HttpPut("bid")]
    public async Task<ActionResult> PlaceBid([FromBody] PlaceBidRequestDto bidRequest)
    {
      await _carAuctionService.PlaceBidAsync(bidRequest);

      return Ok();
    }
  }
}
