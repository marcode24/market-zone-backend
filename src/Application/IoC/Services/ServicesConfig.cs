using Application.Abstractions.Services;
using Application.Services.Filter;
using Application.Services.Ordering;
using Application.Services.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC.Services;

public static class ServicesConfig
{
  public static IServiceCollection AddServicesConfig(this IServiceCollection services)
  {
    services.AddScoped(typeof(IFilterService<>), typeof(FilterService<>));
    services.AddScoped(typeof(IOrderingService<>), typeof(OrderingService<>));
    services.AddScoped(typeof(IPaginationService<,>), typeof(PaginationService<,>));

    return services;
  }
}
