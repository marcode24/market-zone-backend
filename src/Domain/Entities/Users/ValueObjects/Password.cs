namespace Domain.Entities.Users.ValueObjects;

/// <summary>
/// Represents a user's password.
/// </summary>
/// <param name="Value">The password value.</param>
public record Password(string Value)
{
  /// <summary>
  /// Creates a new instance of the Password value object.
  /// </summary>
  /// <param name="value">The password value.</param>
  /// <returns>A new instance of the Password value object.</returns>
  public static Password New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Password cannot be empty", nameof(value));

    return new Password(value);
  }

  /// <summary>
  /// Returns a string that represents the current password.
  /// </summary>
  /// <returns>A string that represents the current password.</returns>
  public override string ToString() => Value;
}
