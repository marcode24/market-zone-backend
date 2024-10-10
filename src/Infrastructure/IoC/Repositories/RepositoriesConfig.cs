using Domain.Abstractions;
using Domain.Repositories.Permissions;
using Domain.Repositories.Roles;
using Domain.Repositories.Users;
using Infrastructure.Persistence.Repositories.Specific.Permissions;
using Infrastructure.Persistence.Repositories.Specific.Roles;
using Infrastructure.Persistence.Repositories.Specific.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.Repositories;

public static class RepositoriesConfig
{
  public static IServiceCollection AddRepositoriesConfig(this IServiceCollection services)
  {
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IRoleRepository, RoleRepository>();
    services.AddScoped<IPermissionRepository, PermissionRepository>();

    services.AddScoped<IUnitOfWork, IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

    return services;
  }
}
