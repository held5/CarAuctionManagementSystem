using System.Net;
using System.Text.Json;

using CarAuction.API.Models;

namespace CarAuction.API.Middlewares
{
  internal class ExceptionHandlingMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    // TODO: Better error handling.
    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An unexpected exception occurred: {Exception}", ex);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ErrorResponse 
                       {
                         StatusCode = context.Response.StatusCode,
                         Message = ex.Message
                       };
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
      }
    }
  }
}
