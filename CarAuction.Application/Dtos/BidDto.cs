using System.ComponentModel.DataAnnotations;

namespace CarAuction.Application.Dtos
{
    public record BidDto(
      [Required(ErrorMessage = "Auction id is required")]
      Guid AuctionId,
      [Range(0, double.MaxValue, ErrorMessage = "Bid amount must be non-negative")]
      decimal BidAmount);
}
