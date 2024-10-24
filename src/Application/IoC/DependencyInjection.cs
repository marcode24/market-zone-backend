namespace Application.IoC;

using Application.Abstractions.Behaviors;
using Application.Abstractions.Providers;
using Application.Filters;
using Application.IoC.Mapping;
using Application.IoC.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<ValidateIdAttribute>();

    services.AddMediatR(configuration =>
    {
      configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
      configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
      configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });
    // services.AddValidatorsFromAssemblyContaining<CreatePermissionCommandValidator>();
    services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

    services.AddServicesConfig();
    services.AddMappingConfig();

    return services;
  }
}
