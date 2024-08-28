namespace Infrastructure;

using Infrastructure.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

/// <summary>
/// Represents the dependency injection.
/// </summary>
public static class DependencyInjection
{
  /// <summary>
  /// Adds the infrastructure.
  /// </summary>
  /// <param name="services">The services.</param>
  /// <param name="configuration">The configuration.</param>
  /// <returns> The service collection.</returns>
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
  {
    services.Configure<OutboxOptions>(configuration.GetSection(nameof(OutboxOptions)));
    services.AddQuartz();
    services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    services.ConfigureOptions<ProcessOutboxMessageSetup>();

    return services;
  }
}
