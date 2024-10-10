using Application.Services;
using Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.IoC.Logging;

public static class LoggingConfig
{
  public static IServiceCollection AddLoggingConfig(this IServiceCollection services)
  {
    services.AddSingleton<ILogger>(Log.Logger);
    services.AddSingleton<ILogService, LogService>();

    return services;
  }
}
