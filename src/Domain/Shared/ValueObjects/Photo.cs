namespace Domain.Shared.ValueObjects;

/// <summary>
/// Represents a user's photo.
/// </summary>
/// <param name="Value">The photo value.</param>
public record Photo(string Value)
{
  /// <summary>
  /// Creates a new instance of the <see cref="Photo"/> class.
  /// </summary>
  /// <param name="value">The value of the photo.</param>
  /// <returns>A new instance of the <see cref="Photo"/> class.</returns>
  public static Photo New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Photo cannot be empty", nameof(value));

    return new Photo(value);
  }

  /// <summary>
  /// Returns a string that represents the current object.
  /// </summary>
  /// <returns>A string that represents the current object.</returns>
  public override string ToString() => Value;
}
