using Application.Core.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC.Mapping;

public static class MappingConfig
{
  public static IServiceCollection AddMappingConfig(this IServiceCollection services)
  {
    services.AddAutoMapper(typeof(PermissionMappingProfile));

    return services;
  }
}
