namespace Domain.Entities.Roles;

using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Roles.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;
using Domain.Shared.ValueObjects;

/// <summary>
/// Represents a role entity.
/// </summary>
public sealed class Role : Entity<RoleId>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="Role"/> class.
  /// </summary>
  /// <param name="id">The role identifier.</param>
  /// <param name="name">The name of the role.</param>
  public Role(RoleId id, Name name)
    : base(id)
  {
    Name = name;
  }

  /// <summary>
  /// Gets the name of the role.
  /// </summary>
  public Name? Name { get; private set; }

  /// <summary>
  /// Gets the user ID of the role.
  /// </summary>
  public UserId? UserId { get; private set; }

  /// <summary>
  /// Gets the user of the role.
  /// </summary>
  public ICollection<User>? Users { get; private set; }

  /// <summary>
  /// Gets or sets the permissions of the role.
  /// </summary>
  public ICollection<Permission>? Permissions { get; set; }

  /// <summary>
  /// Gets or sets the role permissions.
  /// </summary>
  public ICollection<RolePermission>? RolePermissions { get; set; }
}
