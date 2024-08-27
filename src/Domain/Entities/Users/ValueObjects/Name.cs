namespace Domain.Entities.Users.ValueObjects;

/// <summary>
/// Represents a name value object.
/// </summary>
public record Name(string Value)
{
  /// <summary>
  /// Creates a new instance of the Name value object.
  /// </summary>
  /// <param name="value">The value of the name.</param>
  /// <returns>A new instance of the Name value object.</returns>
  public static Name New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Name cannot be empty", nameof(value));

    return new Name(value);
  }

  /// <summary>
  /// Returns a string that represents the current object.
  /// </summary>
  /// <returns>A string that represents the current object.</returns>
  public override string ToString() => Value;
}
