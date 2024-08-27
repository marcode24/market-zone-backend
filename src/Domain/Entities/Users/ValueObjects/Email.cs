namespace Domain.Entities.Users.ValueObjects;

/// <summary>
/// Represents an email value object.
/// </summary>
public record Email(string Value)
{
  /// <summary>
  /// Creates a new instance of the Email value object.
  /// </summary>
  /// <param name="value">The email value.</param>
  /// <returns>A new instance of the Email value object.</returns>
  public static Email New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Email cannot be empty", nameof(value));

    return new Email(value);
  }

  /// <summary>
  /// Returns a string that represents the current email value.
  /// </summary>
  /// <returns>A string representation of the email value.</returns>
  public override string ToString() => Value;
}
