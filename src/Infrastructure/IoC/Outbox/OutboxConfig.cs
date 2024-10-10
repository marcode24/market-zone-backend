using Infrastructure.Persistence.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.Outbox;

public static class OutboxConfig
{
  public static IServiceCollection AddOutboxConfig(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<OutboxOptions>(configuration.GetSection(nameof(OutboxOptions)));
    services.ConfigureOptions<ProcessOutboxMessageSetup>();

    return services;
  }
}
