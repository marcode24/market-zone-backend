namespace Infrastructure.IoC;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.IoC.ApiVersioning;
using Infrastructure.IoC.Database;
using Infrastructure.IoC.Repositories;
using Infrastructure.IoC.Outbox;
using Infrastructure.IoC.Services;
using Infrastructure.IoC.Quartz;
using Infrastructure.IoC.Logging;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("DataBaseConnection")
        ?? throw new ArgumentNullException(nameof(configuration));

    // services.AddLoggingConfig();
    services.AddApiVersioningConfig();
    services.AddQuartzConfig();
    services.AddOutboxConfig(configuration);
    services.AddDatabaseConfig(configuration, connectionString);
    services.AddServicesConfig(connectionString);
    services.AddRepositoriesConfig();

    return services;
  }
}
