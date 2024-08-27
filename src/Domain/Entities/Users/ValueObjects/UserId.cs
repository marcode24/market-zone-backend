namespace Domain.Entities.Users.ValueObjects;

/// <summary>
/// Represents a user ID.
/// </summary>
public record UserId(int Value)
: IEquatable<UserId>
{
  /// <summary>
  /// Creates a new instance of the UserId class.
  /// </summary>
  /// <param name="value">The value of the user id.</param>
  /// <returns>A new instance of the UserId class.</returns>
  public static UserId New(int value)
  {
    if (value <= 0)
      throw new ArgumentException("User id must be greater than 0", nameof(value));

    return new UserId(value);
  }

  /// <summary>
  /// Returns the hash code for the UserId.
  /// </summary>
  /// <returns>The hash code for the UserId.</returns>
  public override int GetHashCode() => Value.GetHashCode();

  /// <summary>
  /// Returns a string that represents the current object.
  /// </summary>
  /// <returns>A string that represents the current object.</returns>
  public override string ToString() => Value.ToString();
}
