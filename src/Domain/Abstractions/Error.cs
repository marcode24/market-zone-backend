namespace Domain.Abstractions;

/// <summary>
/// Represents an error in the domain.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Message">The error message.</param>
public record Error(string Code, string Message)
{
  /// <summary>
  /// Represents an error that indicates that the value is null.
  /// </summary>
  public static readonly Error None = new(string.Empty, string.Empty);

  /// <summary>
  /// Represents an error that indicates that the value is null.
  /// </summary>
  public static readonly Error NullValue = new("NULL_VALUE", "The value should not be null.");
}
