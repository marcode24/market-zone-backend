using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure.IoC.Quartz;

public static class QuartzConfig
{
  public static IServiceCollection AddQuartzConfig(this IServiceCollection services)
  {
    services.AddQuartz();
    services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

    return services;
  }
}
