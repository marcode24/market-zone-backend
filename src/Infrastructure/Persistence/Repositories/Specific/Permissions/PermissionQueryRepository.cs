namespace Infrastructure.Persistence.Repositories.Specific.Permissions;

using Application.Abstractions.Data;
using Application.Abstractions.Providers;
using Application.Modules.Permissions.DTOs.Responses;
using Domain.Entities.Permissions;

internal sealed class PermissionQueryRepository
  : DapperRepository<Permission, PermissionResponse>
{
  public PermissionQueryRepository(
    ISqlConnectionFactory sqlConnectionFactory,
    ITableNameProvider tableNameProvider)
    : base(sqlConnectionFactory, tableNameProvider) { }
}
