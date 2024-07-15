using CarAuction.Application.Dtos;

namespace CarAuction.Application.Services
{
  /// <summary>
  ///   Represents a service interface for managing car auctions.
  /// </summary>
  public interface ICarAuctionService
  {
    /// <summary>
    ///   Adds a new auction for the specified vehicle.
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle.</param>
    /// <returns>The task result with the unique identifier of the newly created auction.</returns>
    Task<Guid> AddAuctionAsync(Guid vehicleId);

    /// <summary>
    ///   Closes an existing auction.
    /// </summary>
    /// <param name="auctionId">The unique identifier of the auction to be closed.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CloseAuctionAsync(Guid auctionId);

    /// <summary>
    ///   Places a bid on an active auction.
    /// </summary>
    /// <param name="bidRequest">The data transfer object containing bid information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PlaceBidAsync(PlaceBidRequestDto bidRequest);
  }
}
