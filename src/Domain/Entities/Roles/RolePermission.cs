namespace Domain.Entities.Roles;

using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Entities.Roles.ValueObjects;

public sealed class RolePermission
{
  public RoleId? RoleId { get; set; }

  public Role? Role { get; set; }

  public PermissionId? PermissionId { get; set; }

  public Permission? Permission { get; set; }
}
