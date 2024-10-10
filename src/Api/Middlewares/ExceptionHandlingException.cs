using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionHandlingMiddleware> _logger;
  private readonly IHostEnvironment _env;

  public ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger,
    IHostEnvironment env)
  {
    _next = next;
    _logger = logger;
    _env = env;
  }

  public async Task InvokeAsync(HttpContext httpContext)
  {
    try
    {
      await _next(httpContext);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred: {Message}", ex.Message);

      var exceptionDetails = GetExceptionDetails(ex);
      var problemDetails = new ProblemDetails
      {
        Status = exceptionDetails.StatusCode,
        Title = exceptionDetails.Title,
        Type = exceptionDetails.Type,
        Detail = exceptionDetails.Detail,
        Instance = httpContext.Request.Path
      };

      if (exceptionDetails.Errors is not null)
        problemDetails.Extensions.Add("errors", exceptionDetails.Errors);

      httpContext.Response.StatusCode = exceptionDetails.StatusCode;
      await httpContext.Response.WriteAsJsonAsync(problemDetails);
    }
  }

  private ExceptionDetails GetExceptionDetails(Exception exception)
  {
    return exception switch
    {
      ValidationException validationException => new ExceptionDetails(
        statusCode: StatusCodes.Status400BadRequest,
        title: "One or more validation errors occurred.",
        type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        detail: validationException.Message,
        errors: validationException.Errors
      ),
      _ => new ExceptionDetails(
        statusCode: StatusCodes.Status500InternalServerError,
        title: _env.IsDevelopment()
            ? exception.Message
            : "An error occurred while processing your request.",
        type: "https://tools.ietf.org/html/rfc7231#section-6.6.1",
        detail: _env.IsDevelopment()
            ? exception.StackTrace?.ToString()!
            : "The error has been logged and will be investigated.",
        errors: _env.IsDevelopment()
            ? [exception.Data.ToString()!]
            : null!
      )
    };
  }
}
