namespace Domain.Entities.Roles;

using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Entities.Roles.ValueObjects;

/// <summary>
/// Represents a role permission.
/// </summary>
public sealed class RolePermission
{
  /// <summary>
  /// Gets or sets the role ID.
  /// </summary>
  public RoleId? RoleId { get; set; }

  /// <summary>
  /// Gets or sets the role.
  /// </summary>
  public Role? Role { get; set; }

  /// <summary>
  /// Gets or sets the permission ID.
  /// </summary>
  public PermisssionId? PermissionId { get; set; }

  /// <summary>
  /// Gets or sets the permission.
  /// </summary>
  public Permission? Permission { get; set; }
}
