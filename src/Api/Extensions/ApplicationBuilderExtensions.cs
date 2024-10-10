using Api.Middlewares;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
{
  public static async Task ApplyMigration(this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
      var context = service.GetRequiredService<ApplicationDbContext>();
      await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
      var logger = loggerFactory.CreateLogger<Program>();
      logger.LogError(ex, "An error occurred while migrating the database.");
    }
  }

  public static void UseCustomExceptionHandler(this IApplicationBuilder app)
  {
    app.UseMiddleware<ExceptionHandlingMiddleware>();
  }
}
