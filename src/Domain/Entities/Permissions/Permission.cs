namespace Domain.Entities.Permissions;

using Domain.Abstractions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Entities.Roles;
using Domain.Shared.ValueObjects;

public sealed class Permission : Entity<PermissionId>
{
  private Permission(Name name)
  : base(DateTime.UtcNow, DateTime.UtcNow)
  {
    Name = name;
  }

  private Permission() { }

  public Name? Name { get; private set; }

  public TypePermission? Type { get; private set; }

  public ICollection<Role>? Roles { get; private set; }

  public ICollection<RolePermission>? RolePermissions { get; private set; }

  public static Result<Permission> Create(Name name)
  {
    return new Permission(name);
  }
}
