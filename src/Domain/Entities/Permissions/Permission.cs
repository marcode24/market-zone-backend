namespace Domain.Entities.Permissions;

using Domain.Abstractions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Entities.Roles;
using Domain.Shared.ValueObjects;

/// <summary>
/// Represents a permission entity.
/// </summary>
public sealed class Permission : Entity<PermisssionId>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="Permission"/> class.
  /// </summary>
  /// <param name="id">The permission identifier.</param>
  /// <param name="name">The name of the permission.</param>
  /// <returns>An instance of the <see cref="Permission"/> class.</returns>
  public Permission(PermisssionId id, Name name)
  : base(id)
  {
    Name = name;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Permission"/> class.
  /// </summary>
  /// <param name="name">The name of the permission.</param>
  /// <returns>An instance of the <see cref="Permission"/> class.</returns>
  public Permission(Name name)
  {
    Name = name;
  }

  private Permission()
  {
  }

  /// <summary>
  /// Gets the name of the permission.
  /// </summary>
  public Name? Name { get; private set; }

  /// <summary>
  /// Gets the type of the permission.
  /// </summary>
  public TypePermission? Type { get; private set; }

  /// <summary>
  /// Gets o the roles of the permission.
  /// </summary>
  public ICollection<Role>? Roles { get; private set; }

  /// <summary>
  /// Gets the role permissions of the permission.
  /// </summary>
  public ICollection<RolePermission>? RolePermissions { get; private set; }

  /// <summary>
  /// Creates a new instance of the Permission class.
  /// </summary>
  /// <param name="name">The name of the permission.</param>
  /// <returns>A new instance of the Permission class.</returns>
  public static Result<Permission> Create(Name name)
  {
    return new Permission(name);
  }
}
