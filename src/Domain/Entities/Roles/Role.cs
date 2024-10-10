namespace Domain.Entities.Roles;

using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Entities.Roles.ValueObjects;
using Domain.Shared.ValueObjects;

public sealed class Role : Entity<RoleId>
{
  private Role(Name name)
    : base(DateTime.UtcNow, DateTime.UtcNow)
  {
    Name = name;
    Permissions = new List<Permission>();
  }
  private Role()
  {
    Permissions = new List<Permission>();
  }

  public Name? Name { get; private set; }
  public ICollection<Permission> Permissions { get; private set; }
  public ICollection<RolePermission>? RolePermissions { get; set; }

  public static Role Create(
    Name name,
    List<Permission> permissions
  )
  {
    var role = new Role(name);
    if (permissions.Count != 0)
      foreach (var permission in permissions)
        role.AddPermission(permission);

    return role;
  }

  public void AddPermission(Permission permission)
  {
    if (!Permissions.Any(p => p.Id == permission.Id))
      Permissions.Add(permission);
  }
}
