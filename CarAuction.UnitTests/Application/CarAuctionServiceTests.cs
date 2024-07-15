using AutoBogus;
using Bogus;
using CarAuction.Application.Dtos;
using CarAuction.Application.Services;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Interfaces;
using NSubstitute;

namespace CarAuction.UnitTests.Application
{
  [TestFixture]
  public class CarAuctionServiceTests
  {
    private IUnitOfWork _unitOfWorkMock;
    private IRepository<Vehicle> _vehicleRepositoryMock;
    private IRepository<Auction> _auctionRepositoryMock;
    private ICarAuctionService _sut;

    /// <summary>
    ///   Setup test environment for each test run.
    /// </summary>
    [SetUp]
    public void Setup()
    {
      _unitOfWorkMock = Substitute.For<IUnitOfWork>();
      _vehicleRepositoryMock = Substitute.For<IRepository<Vehicle>>();
      _auctionRepositoryMock = Substitute.For<IRepository<Auction>>();

      // Setup repositories in the unit of work
      _unitOfWorkMock.GetRepository<Vehicle>().Returns(_vehicleRepositoryMock);
      _unitOfWorkMock.GetRepository<Auction>().Returns(_auctionRepositoryMock);

      _sut = new CarAuctionService(_unitOfWorkMock);
    }

    /// <summary>
    ///   Teardown test environment for each test run.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
      _unitOfWorkMock = null;
      _vehicleRepositoryMock = null;
      _auctionRepositoryMock = null;
      _sut = null;
    }

    /// <summary>
    ///   Given
    ///     a valid vehicleId and a vehicle exists in the repository
    ///   When
    ///     AddAuctionAsync method is called with the vehicleId
    ///   Then
    ///     it should add a new auction and return the auction's Id.
    /// </summary>
    [Test]
    public async Task AddAuctionAsync_WhenVehicleIdIsValid_ShouldAddNewAuction()
    {
      // Arrange
      var vehicleId = Guid.NewGuid();
      var vehicle = AutoFaker.Generate<Sedan>();

      _vehicleRepositoryMock.Get(vehicleId).Returns(vehicle);
      _auctionRepositoryMock.GetAll().ReturnsForAnyArgs(new List<Auction>().AsQueryable());

      // Act
      var result = await _sut.AddAuctionAsync(vehicleId);

      // Assert
      await _auctionRepositoryMock.Received(1).Add(Arg.Any<Auction>());
      await _unitOfWorkMock.Received(1).SaveChangesAsync();
    }

    /// <summary>
    ///   Given
    ///     an invalid vehicleId
    ///   When
    ///     AddAuctionAsync method is called with the invalid vehicleId
    ///   Then
    ///     it should throw an InvalidOperationException.
    /// </summary>
    [Test]
    public async Task AddAuctionAsync_WhenVehicleIdIsInvalid_ShouldThrowInvalidOperationException()
    {
      // Arrange
      var vehicleId = Guid.NewGuid();

      _vehicleRepositoryMock.Get(vehicleId).Returns(default(Vehicle));

      // Act + Assert
      var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.AddAuctionAsync(vehicleId));
      Assert.That(ex.Message, Is.EqualTo($"Vehicle with Id: '{vehicleId}' does not exist."));

      await _auctionRepositoryMock.DidNotReceive().Add(Arg.Any<Auction>());
      await _unitOfWorkMock.DidNotReceive().SaveChangesAsync();
    }

    /// <summary>
    ///   Given
    ///     a valid vehicleId and an active auction already exists for the vehicle
    ///   When
    ///     AddAuctionAsync method is called with the vehicleId
    ///   Then
    ///     it should throw an InvalidOperationException indicating an active auction already exists.
    /// </summary>
    [Test]
    public async Task AddAuctionAsync_WhenExistingActiveAuction_ShouldThrowInvalidOperationException()
    {
      // Arrange
      var vehicleId = Guid.NewGuid();
      var vehicle = new Faker<Sedan>().RuleFor(fake => fake.Id, _ => vehicleId).Generate();
      var existingAuction = new List<Auction> { new Auction { VehicleId = vehicleId, IsActive = true } };

      _vehicleRepositoryMock.Get(vehicleId).Returns(vehicle);
      _auctionRepositoryMock.GetAll().ReturnsForAnyArgs(existingAuction.AsQueryable());

      // Act + Assert
      var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.AddAuctionAsync(vehicleId));
      Assert.That(ex.Message, Is.EqualTo($"An active auction already exists for vehicle with Id: '{vehicleId}'."));

      await _auctionRepositoryMock.DidNotReceive().Add(Arg.Any<Auction>());
      await _unitOfWorkMock.DidNotReceive().SaveChangesAsync();
    }

    /// <summary>
    ///   Given
    ///     a valid auctionId and an active auction
    ///   When
    ///     CloseAuctionAsync method is called with the auctionId
    ///   Then
    ///     it should mark the auction as inactive.
    /// </summary>
    [Test]
    public async Task CloseAuctionAsync_WhenAuctionIdIsValid_ShouldCloseAuction()
    {
      // Arrange
      var auctionId = Guid.NewGuid();
      var auction = new Auction { Id = auctionId, IsActive = true };

      _auctionRepositoryMock.Get(auctionId).Returns(auction);

      // Act
      await _sut.CloseAuctionAsync(auctionId);

      // Assert
      Assert.That(auction.IsActive, Is.False);
      await _auctionRepositoryMock.Received(1).Update(auction);
      await _unitOfWorkMock.Received(1).SaveChangesAsync();
    }

    /// <summary>
    ///   Given
    ///     an invalid auctionId
    ///   When
    ///     CloseAuctionAsync method is called with the invalid auctionId
    ///   Then
    ///     it should throw an InvalidOperationException.
    /// </summary>
    [Test]
    public async Task CloseAuctionAsync_WhenAuctionIdIsInvalid_ShouldThrowInvalidOperationException()
    {
      // Arrange
      var auctionId = Guid.NewGuid();
      _auctionRepositoryMock.Get(auctionId).Returns(default(Auction));

      // Act + Assert
      var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.CloseAuctionAsync(auctionId));
      Assert.That(ex.Message, Is.EqualTo($"Auction with id: {auctionId} not found."));

      await _auctionRepositoryMock.DidNotReceive().Update(Arg.Any<Auction>());
      await _unitOfWorkMock.DidNotReceive().SaveChangesAsync();
    }

    /// <summary>
    ///   Given
    ///     an auctionId of an inactive auction
    ///   When
    ///     CloseAuctionAsync method is called with the auctionId
    ///   Then
    ///     it should throw an InvalidOperationException indicating the auction is not active.
    /// </summary>
    [Test]
    public async Task CloseAuctionAsync_WhenAuctionIsNotActive_ShouldThrowInvalidOperationException()
    {
      // Arrange
      var auctionId = Guid.NewGuid();
      var auction = new Auction { Id = auctionId, IsActive = false };
      _auctionRepositoryMock.Get(auctionId).Returns(auction);

      // Act + Assert
      var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.CloseAuctionAsync(auctionId));
      Assert.That(ex.Message, Is.EqualTo($"Auction with id: {auctionId} is not active and cannot be closed."));

      await _auctionRepositoryMock.DidNotReceive().Update(Arg.Any<Auction>());
      await _unitOfWorkMock.DidNotReceive().SaveChangesAsync();
    }

    /// <summary>
    ///   Given
    ///     a valid bid with a bid amount higher than the current highest bid
    ///   When
    ///     PlaceBidAsync method is called with the bid
    ///   Then
    ///     it should update the auction's highest bid to the bid amount.
    /// </summary>
    [Test]
    public async Task PlaceBidAsync_WhenBidIsValid_ShouldPlaceBid()
    {
      // Arrange
      var bid = new BidDto(Guid.NewGuid(), 1500);
      var auction = new Auction { Id = bid.AuctionId, IsActive = true, CurrentHighestBid = 1000 };

      _auctionRepositoryMock.Get(bid.AuctionId).Returns(auction);

      // Act
      await _sut.PlaceBidAsync(bid);

      // Assert
      Assert.That(auction.CurrentHighestBid, Is.EqualTo(bid.BidAmount));
      await _auctionRepositoryMock.Received(1).Update(auction);
      await _unitOfWorkMock.Received(1).SaveChangesAsync();
    }

    /// <summary>
    ///   Given
    ///     an invalid auctionId
    ///   When
    ///     PlaceBidAsync method is called with the invalid auctionId
    ///   Then
    ///     it should throw an InvalidOperationException.
    /// </summary>
    [Test]
    public async Task PlaceBidAsync_WhenAuctionIdIsInvalid_ShouldThrowInvalidOperationException()
    {
      // Arrange
      var auctionId = Guid.NewGuid();
      var bid = new BidDto(auctionId, 1500);
      _auctionRepositoryMock.Get(auctionId).Returns(default(Auction));

      // Act + Assert
      var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.PlaceBidAsync(bid));
      Assert.That(ex.Message, Is.EqualTo($"Auction with Id: {auctionId} not found."));

      await _auctionRepositoryMock.DidNotReceive().Update(Arg.Any<Auction>());
      await _unitOfWorkMock.DidNotReceive().SaveChangesAsync();
    }


    /// <summary>
    ///   Given
    ///     an inactive auction
    ///   When
    ///     PlaceBidAsync method is called with a bid for the inactive auction
    ///   Then
    ///     it should throw an InvalidOperationException indicating the auction is already closed.
    /// </summary>
    [Test]
    public async Task PlaceBidAsync_InactiveAuction_ShouldThrowInvalidOperationException()
    {
      // Arrange
      var bid = new BidDto(Guid.NewGuid(), 1500);
      var auction = new Auction { Id = bid.AuctionId, IsActive = false };

      _auctionRepositoryMock.Get(bid.AuctionId).Returns(auction);

      // Act & Assert
      var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.PlaceBidAsync(bid));
      Assert.That(ex.Message, Is.EqualTo($"Auction with Id: {bid.AuctionId} is already closed."));

      await _auctionRepositoryMock.DidNotReceive().Update(Arg.Any<Auction>());
      await _unitOfWorkMock.DidNotReceive().SaveChangesAsync();
    }

    /// <summary>
    ///   Given
    ///     a bid with an amount not higher than the current highest bid of the auction
    ///   When
    ///     PlaceBidAsync method is called with the bid
    ///   Then
    ///     it should throw an InvalidOperationException indicating bid amount must be higher.
    /// </summary>
    [Test]
    public async Task PlaceBidAsync_WhenBidAmountNotHigher_ShouldThrowInvalidOperationException()
    {
      // Arrange
      var bid = new BidDto(Guid.NewGuid(), 1000);
      var auction = new Auction { Id = bid.AuctionId, IsActive = true, CurrentHighestBid = 1500 };

      _auctionRepositoryMock.Get(bid.AuctionId).Returns(auction);

      // Act & Assert
      var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.PlaceBidAsync(bid));
      Assert.That(ex.Message, Is.EqualTo("Bid amount must be higher than the current highest bid."));

      await _auctionRepositoryMock.DidNotReceive().Update(Arg.Any<Auction>());
      await _unitOfWorkMock.DidNotReceive().SaveChangesAsync();
    }
  }
}
