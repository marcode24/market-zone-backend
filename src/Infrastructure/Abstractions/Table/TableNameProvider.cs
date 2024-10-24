using Application.Abstractions.Providers;
using Domain.Entities.Permissions;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Abstractions.Table;

public class TableNameProvider : ITableNameProvider
{
  public string GetTableName<TEntity>()
  {
    var entityType = typeof(TEntity);
    return entityType.Name switch
    {
      nameof(User) => $"{Schemas.Market}.{Tables.Users}",
      nameof(Role) => $"{Schemas.Market}.{Tables.Roles}",
      nameof(RolePermission) => $"{Schemas.Market}.{Tables.RolesPermissions}",
      nameof(Permission) => $"{Schemas.Market}.{Tables.Permissions}",
      _ => throw new NotImplementedException($"Table name for {entityType.Name} is not implemented.")
    };
  }
}
