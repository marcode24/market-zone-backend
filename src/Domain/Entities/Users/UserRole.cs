namespace Domain.Entities.Users;

using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;
using Domain.Entities.Users.ValueObjects;

public sealed class UserRole
{
  public RoleId? RoleId { get; set; }
  public UserId? UserId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
