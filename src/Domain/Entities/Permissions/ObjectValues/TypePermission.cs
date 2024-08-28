namespace Domain.Entities.Permissions.ObjectValues;

/// <summary>
/// Represents the type permission value object.
/// </summary>
/// <param name="Value">The value of the type permission.</param>
public record TypePermission(string Value)
{
  /// <summary>
  /// Creates a new instance of the TypePermission value object.
  /// </summary>
  /// <param name="value">The value of the type permission.</param>
  /// <returns>A new instance of the TypePermission value object.</returns>
  public static TypePermission New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Type permission cannot be empty", nameof(value));

    return new TypePermission(value);
  }
}
