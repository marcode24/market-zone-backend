using Api.Documentation;
using Asp.Versioning.ApiExplorer;

namespace Api.Extensions;

public static class SwaggerServiceExtensions
{
  public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.ConfigureOptions<ConfigureSwaggerOptions>();
    services.AddSwaggerGen(options =>
    {
      options.CustomSchemaIds(type => type.ToString());
    });

    return services;
  }

  public static IApplicationBuilder UseSwaggerDocumentation(
      this IApplicationBuilder app,
      IApiVersionDescriptionProvider provider)
  {
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
      var descriptions = provider.ApiVersionDescriptions;
      foreach (var description in descriptions)
      {
        var url = $"/swagger/{description.GroupName}/swagger.json";
        var name = description.GroupName.ToUpperInvariant();
        options.SwaggerEndpoint(url, name);
      }
    });

    return app;
  }
}
