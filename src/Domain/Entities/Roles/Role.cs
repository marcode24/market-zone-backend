namespace Domain.Entities.Roles;

using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Roles.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;
using Domain.Shared.ValueObjects;

public sealed class Role : Entity<RoleId>
{
  private Role(Name name)
    : base(DateTime.UtcNow, DateTime.UtcNow)
  {
    Name = name;
  }
  private Role() { }

  public Name? Name { get; private set; }
  public ICollection<User>? Users { get; private set; }
  public ICollection<Permission>? Permissions { get; set; }
  public ICollection<RolePermission>? RolePermissions { get; set; }
}
