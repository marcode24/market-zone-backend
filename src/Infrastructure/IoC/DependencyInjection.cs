namespace Infrastructure.IoC;

using Application.Abstractions.Clock;
using Application.Abstractions.Data;
using Dapper;
using Domain.Abstractions;
using Infrastructure.Abstractions.Clock;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("DataBaseConnection")
      ?? throw new ArgumentNullException(nameof(configuration));

    services.Configure<OutboxOptions>(configuration.GetSection(nameof(OutboxOptions)));
    services.AddQuartz();
    services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    services.ConfigureOptions<ProcessOutboxMessageSetup>();

    services.AddDbContext<ApplicationDbContext>(options =>
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

    services.AddTransient<IDateTimeProvider, DateTimeProvider>();

    services.AddScoped<IUnitOfWork, IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

    services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
    SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

    services.AddHttpContextAccessor();

    return services;
  }
}
