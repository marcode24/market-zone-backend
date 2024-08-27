namespace Domain.Entities.Users.ValueObjects;

/// <summary>
/// Represents the last name value object.
/// </summary>
public record LastName(string Value)
{
  /// <summary>
  /// Creates a new instance of the LastName value object.
  /// </summary>
  /// <param name="value">The value of the last name.</param>
  /// <returns>A new instance of the LastName value object.</returns>
  public static LastName New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Last name cannot be empty", nameof(value));

    return new LastName(value);
  }

  /// <summary>
  /// Returns a string that represents the current object.
  /// </summary>
  /// <returns>A string that represents the current object.</returns>
  public override string ToString() => Value;
}
