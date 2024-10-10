using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.ApiVersioning;

public static class ApiVersioningConfig
{
  public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
  {
    services
      .AddApiVersioning(options =>
      {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
      })
      .AddMvc()
      .AddApiExplorer(options =>
      {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
      });

    return services;
  }
}
