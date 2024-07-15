using CarAuction.Application.Dtos;
using CarAuction.Application.Services;

using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuctionController : ControllerBase
  {
    private readonly ICarAuctionService _carAuctionService;

    public AuctionController(ICarAuctionService carAuctionService)
    {
      _carAuctionService = carAuctionService;
    }

    [HttpPost("[action]/{vehicleId}")]
    public async Task<ActionResult> Create([FromRoute] Guid vehicleId)
    {
      var result = await _carAuctionService.AddAuctionAsync(vehicleId);

      return Ok(result);
    }

    [HttpPut("[action]/{auctionId}")]
    public async Task<ActionResult> Close([FromRoute] Guid auctionId)
    {
      await _carAuctionService.CloseAuctionAsync(auctionId);

      return Ok();
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> PlaceBid([FromBody] BidDto bid)
    {
      await _carAuctionService.PlaceBidAsync(bid);

      return Ok();
    }
  }
}
