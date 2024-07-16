using CarAuction.API.Models;
using CarAuction.Application.Exceptions;
using System.Net;

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

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      _logger.LogError(exception, "An unexpected error occurred.");

      ExceptionResponse response = exception switch
                                   {
                                     NotFoundException _ => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message),
                                     ValidationException _ => new ExceptionResponse(HttpStatusCode.NotFound, exception.Message),
                                     _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "An unexpected exception occurred")
                                   };

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)response.StatusCode;
      await context.Response.WriteAsJsonAsync(response);
    }
  }
}
