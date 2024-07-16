using CarAuction.Application.Dtos;
using CarAuction.Application.Exceptions;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Interfaces;

namespace CarAuction.Application.Services
{
  public class CarAuctionService : ICarAuctionService 
  {
    private readonly IUnitOfWork _uow;
    private readonly IRepository<Vehicle> _vehicleRepository;
    private readonly IRepository<Auction> _auctionRepository;

    public CarAuctionService(IUnitOfWork uow)
    {
      _uow = uow;
      _vehicleRepository = _uow.GetRepository<Vehicle>();
      _auctionRepository = _uow.GetRepository<Auction>();
    }

    /// <inheritdoc />
    public async Task<Guid> AddAuctionAsync(Guid vehicleId)
    {
      var vehicle = await _vehicleRepository.Get(vehicleId);

      if (vehicle == null)
      {
        throw new NotFoundException($"Vehicle with Id: '{vehicleId}' does not exist.");
      }

      var existingAuction = await _auctionRepository.GetAll(a => a.VehicleId.Equals(vehicleId) && a.IsActive);

      if (existingAuction.Any())
      {
        throw new ValidationException($"An active auction already exists for vehicle with Id: '{vehicleId}'.");
      }

      var newAuction = new Auction
                       {
                         VehicleId = vehicleId,
                         IsActive = true
                       };

      await _auctionRepository.Add(newAuction);
      await _uow.SaveChangesAsync();

      return newAuction.Id;
    }

    /// <inheritdoc />
    public async Task CloseAuctionAsync(Guid auctionId)
    {
      var auction = await _auctionRepository.Get(auctionId);

      if (auction == null)
      {
        throw new NotFoundException($"Auction with id: {auctionId} not found.");
      }

      if (!auction.IsActive)
      {
        throw new ValidationException($"Auction with id: {auctionId} is not active and cannot be closed.");
      }

      auction.IsActive = false;

      await _auctionRepository.Update(auction);
      await _uow.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task PlaceBidAsync(PlaceBidRequestDto bidRequest)
    {
      var auction = await _auctionRepository.Get(bidRequest.AuctionId);

      if (auction == null)
      {
        throw new NotFoundException($"Auction with Id: {bidRequest.AuctionId} not found.");
      }

      if (!auction.IsActive)
      {
        throw new ValidationException($"Auction with Id: {bidRequest.AuctionId} is already closed.");
      }

      if (auction.CurrentHighestBid >= bidRequest.BidAmount)
      {
        throw new ValidationException("Bid amount must be higher than the current highest bid.");
      }

      auction.CurrentHighestBid = bidRequest.BidAmount;
      await _auctionRepository.Update(auction);
      await _uow.SaveChangesAsync();
    }
  }
}
