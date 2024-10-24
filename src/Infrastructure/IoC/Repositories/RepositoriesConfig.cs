using Application.Modules.Permissions.DTOs.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Repositories.Paginations;
using Domain.Repositories.Permissions;
using Domain.Repositories.Roles;
using Domain.Repositories.Users;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Specific.Permissions;
using Infrastructure.Persistence.Repositories.Specific.Roles;
using Infrastructure.Persistence.Repositories.Specific.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.Repositories;

public static class RepositoriesConfig
{
  public static IServiceCollection AddRepositoriesConfig(this IServiceCollection services)
  {
    services.AddScoped<IPermissionRepository, PermissionCommandRepository>();
    services.AddScoped<IPaginationRepository<Permission, PermissionResponse>, PermissionQueryRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IRoleRepository, RoleRepository>();

    services.AddScoped<IUnitOfWork, IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

    return services;
  }
}
