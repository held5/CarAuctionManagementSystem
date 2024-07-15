using CarAuction.API.Controllers;
using CarAuction.Application.Dtos;
using CarAuction.Application.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace CarAuction.UnitTests.API
{
  [TestFixture]
  public class AuctionControllerTests
  {
    private ICarAuctionService _carAuctionServiceMock;
    private AuctionController _sut;

    /// <summary>
    ///   Setup test environment for each test run.
    /// </summary>
    [SetUp]
    public void Setup()
    {
      _carAuctionServiceMock = Substitute.For<ICarAuctionService>();
      _sut = new AuctionController(_carAuctionServiceMock);
    }

    /// <summary>
    ///   Teardown test environment for each test run.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
      _carAuctionServiceMock = null;
      _sut = null;
    }

    /// <summary>
    ///   Given
    ///     a valid vehicleId
    ///   When
    ///     Create method is called with the vehicleId
    ///   Then
    ///     it should return OkObjectResult with the expected new auctionId.
    /// </summary>
    [Test]
    public async Task Create_WhenValid_ShouldReturnOkWithAuctionId()
    {
      // Arrange
      var vehicleId = Guid.NewGuid();
      var expectedResult = Guid.NewGuid();

      _carAuctionServiceMock.AddAuctionAsync(vehicleId).Returns(expectedResult);

      // Act
      var result = await _sut.Create(vehicleId);

      // Assert
      Assert.That(result, Is.TypeOf<OkObjectResult>());
      var okResult = result as OkObjectResult;
      Assert.That(okResult.StatusCode, Is.EqualTo(200));
      Assert.That(okResult.Value, Is.EqualTo(expectedResult));

      _carAuctionServiceMock.Received(1).AddAuctionAsync(vehicleId);
    }

    /// <summary>
    ///   Given
    ///     an invalid vehicleId
    ///   When
    ///     Create method is called with the invalid vehicleId
    ///   Then
    ///     it should throw an InvalidOperationException.
    /// </summary>
    [Test]
    public void Create_WhenAnExceptionOccurs_ShouldThrowAnInvalidOperationException()
    {
      // Arrange
      var vehicleId = Guid.NewGuid();
      _carAuctionServiceMock.AddAuctionAsync(vehicleId).Throws(new InvalidOperationException("Mock exception"));

      // Act
      Assert.Throws<InvalidOperationException>(() => _carAuctionServiceMock.AddAuctionAsync(vehicleId));
    }

    /// <summary>
    ///   Given
    ///     a valid auctionId
    ///   When
    ///     Close method is called with the auctionId
    ///   Then
    ///     it should return OkResult.
    /// </summary>
    [Test]
    public async Task Close_ShouldReturnOk()
    {
      // Arrange
      var auctionId = Guid.NewGuid();

      // Act
      var result = await _sut.Close(auctionId);

      // Assert
      Assert.That(result, Is.TypeOf<OkResult>());
      var okResult = result as OkResult;
      Assert.That(okResult.StatusCode, Is.EqualTo(200));
      _carAuctionServiceMock.Received(1).CloseAuctionAsync(auctionId);
    }

    /// <summary>
    ///   Given
    ///     an invalid auctionId
    ///   When
    ///     Close method is called with the invalid auctionId
    ///   Then
    ///     it should throw an InvalidOperationException.
    /// </summary>
    [Test]
    public void Close_WhenAnExceptionOccurs_ShouldThrowAnInvalidOperationException()
    {
      // Arrange
      var auctionId = Guid.NewGuid();
      _carAuctionServiceMock.CloseAuctionAsync(auctionId).Throws(new InvalidOperationException("Mock exception"));

      // Act
      Assert.Throws<InvalidOperationException>(() => _carAuctionServiceMock.CloseAuctionAsync(auctionId));
    }

    /// <summary>
    ///   Given
    ///     a valid bid
    ///   When
    ///     PlaceBid method is called with the bid
    ///   Then
    ///     it should return OkResult.
    /// </summary>
    [Test]
    public async Task PlaceBid_ShouldReturnOk()
    {
      // Arrange
      var bid = new BidDto(Guid.NewGuid(), 1000);

      // Act
      var result = await _sut.PlaceBid(bid);

      // Assert
      Assert.That(result, Is.TypeOf<OkResult>());
      var okResult = result as OkResult;

      Assert.That(okResult.StatusCode, Is.EqualTo(200));
      _carAuctionServiceMock.Received(1).PlaceBidAsync(bid);
    }

    /// <summary>
    ///   Given
    ///     an invalid bid
    ///   When
    ///     PlaceBid method is called with the invalid bid
    ///   Then
    ///     it should throw an InvalidOperationException.
    /// </summary>
    [Test]
    public void PlaceBid_WhenAnExceptionOccurs_ShouldThrowAnInvalidOperationException()
    {
      // Arrange
      var bid = new BidDto(Guid.NewGuid(), 1000);
      _carAuctionServiceMock.PlaceBidAsync(bid).Throws(new InvalidOperationException("Mock exception"));

      // Act
      Assert.Throws<InvalidOperationException>(() => _carAuctionServiceMock.PlaceBidAsync(bid));
    }
  }
}
