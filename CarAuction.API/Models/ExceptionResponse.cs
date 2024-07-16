using System.Net;

namespace CarAuction.API.Models
{
  public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
}
