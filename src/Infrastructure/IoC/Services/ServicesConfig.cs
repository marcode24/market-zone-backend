using Application.Abstractions.Clock;
using Application.Abstractions.Data;
using Dapper;
using Infrastructure.Abstractions.Clock;
using Infrastructure.Persistence.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.Services;

public static class ServicesConfig
{
  public static IServiceCollection AddServicesConfig(this IServiceCollection services, string connectionString)
  {

    services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
    SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

    services.AddHttpContextAccessor();

    return services;
  }
}
