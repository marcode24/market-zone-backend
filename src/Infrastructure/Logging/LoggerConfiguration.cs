namespace Infrastructure.Logging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

public static class LoggerConfiguration
{
  public static void ConfigureLogging(IConfiguration configuration)
  {
    Log.Logger = new Serilog.LoggerConfiguration()
      .ReadFrom.Configuration(configuration)
      .MinimumLevel.Debug()
      .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
      .MinimumLevel.Override("System", LogEventLevel.Information)
      .Enrich.FromLogContext()
      .CreateLogger();
  }

  public static void AddSerilog(this IServiceCollection services)
  {
    services.AddLogging(loggingBuilder =>
    {
      loggingBuilder.ClearProviders();
      loggingBuilder.AddSerilog(dispose: true);
    });
  }
}
