namespace Domain.Entities.Permissions;

using System.ComponentModel;
using Domain.Abstractions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Entities.Roles;
using Domain.Shared.ValueObjects;

public sealed class Permission : Entity<PermissionId>
{
  private Permission(Name name, TypePermission type)
    : base(DateTime.UtcNow, DateTime.UtcNow)
  {
    Name = name;
    Type = type;
    Roles = new List<Role>();
    RolePermissions = new List<RolePermission>();
  }

  [DisplayName("nombre")]
  public Name? Name { get; private set; }

  [DisplayName("tipo de permiso")]
  public TypePermission? Type { get; private set; }

  public ICollection<Role> Roles { get; private set; }

  public ICollection<RolePermission> RolePermissions { get; private set; }

  public static Permission Create(Name name, TypePermission type)
  {
    var permission = new Permission(name, type);
    return permission;
  }

  public void Update(
    Name? name,
    TypePermission? type,
    bool? isActive
  )
  {
    bool hasUpdated = false;

    if (isActive.HasValue
      && isActive.Value != IsActive)
    {
      ChangeStatus(isActive.Value);
      hasUpdated = true;
    }

    if (!string.IsNullOrWhiteSpace(name?.Value)
      && name.Value != Name?.Value)
    {
      Name = name;
      hasUpdated = true;
    }

    if (!string.IsNullOrWhiteSpace(type?.Value)
      && type.Value != Type?.Value)
    {
      Type = type;
      hasUpdated = true;
    }

    if (hasUpdated)
      UpdateTimestamps();
  }

  public void Delete()
  {
    SoftDelete();
  }
}
