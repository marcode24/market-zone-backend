using Application.Abstractions.Data;
using Application.Abstractions.Providers;
using Infrastructure.Abstractions.Table;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Database;
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

    services.AddSingleton<ISqlConnectionFactory>(_ =>
      new SqlConnectionFactory(connectionString));

    services.AddScoped<ITableNameProvider, TableNameProvider>();

    return services;
  }
}
