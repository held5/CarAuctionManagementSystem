namespace CarAuction.API.Models
{
  internal struct ErrorResponse
  {
    public int StatusCode { get; set; }

    public string Message { get; set; }
  }
}
