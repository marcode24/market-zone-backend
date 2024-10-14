using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.IoC.Database;

public static class DatabaseConfig
{
  public static IServiceCollection AddDatabaseConfig(
    this IServiceCollection services,
    IConfiguration configuration,
    string connectionString)
  {

    services
      .AddDbContext<ApplicationDbContext>(options =>
      {
        options
          .UseNpgsql(connectionString)
          .UseSnakeCaseNamingConvention()
          .LogTo(
            Console.WriteLine,
            new[] { DbLoggerCategory.Database.Command.Name },
            LogLevel.Information)
          .EnableSensitiveDataLogging();
      });
    return services;
  }
}
