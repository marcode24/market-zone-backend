namespace Domain.Entities.Roles.ValueObjects;

/// <summary>
/// Represents the identifier of a role.
/// </summary>
public record RoleId(int Value)
    : IEquatable<RoleId>
{
  /// <summary>
  /// Creates a new instance of the <see cref="RoleId"/> class.
  /// </summary>
  /// <param name="value">The value of the role identifier.</param>
  /// <returns>A new instance of the <see cref="RoleId"/> class.</returns>
  public static RoleId New(int value)
  {
    if (value <= 0)
      throw new ArgumentException("Role id must be greater than 0", nameof(value));

    return new RoleId(value);
  }

  /// <summary>
  /// Returns the hash code for the <see cref="RoleId"/>.
  /// </summary>
  /// <returns>The hash code for the <see cref="RoleId"/>.</returns>
  public override int GetHashCode() => Value.GetHashCode();

  /// <summary>
  /// Returns a string that represents the current object.
  /// </summary>
  /// <returns>A string that represents the current object.</returns>
  public override string ToString() => Value.ToString();
}
