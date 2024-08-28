namespace Domain.Entities.Users;

using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;
using Domain.Entities.Users.ValueObjects;

/// <summary>
/// Represents a user role.
/// </summary>
public sealed class UserRole
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
  /// Gets or sets the user ID.
  /// </summary>
  public UserId? UserId { get; set; }

  /// <summary>
  /// Gets or sets the user.
  /// </summary>
  public User? User { get; set; }

  /// <summary>
  /// Gets or sets the creation date and time.
  /// </summary>
  public DateTime CreatedAt { get; set; }

  /// <summary>
  /// Gets or sets the last update date and time.
  /// </summary>
  public DateTime UpdatedAt { get; set; }
}
