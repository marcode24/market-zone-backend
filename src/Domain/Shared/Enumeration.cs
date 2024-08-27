namespace Domain.Shared;

using System.Reflection;

/// <summary>
/// Represents an abstract base class for enumerations.
/// </summary>
/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="Enumeration{TEnum}"/> class.
/// </remarks>
/// <param name="id">The ID of the enumeration.</param>
/// <param name="name">The name of the enumeration.</param>
public abstract class Enumeration<TEnum>(int id, string name)
: IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="Enumeration{TEnum}"/> class.
  /// </summary>
  private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

  /// <summary>
  /// Gets the ID of the enumeration.
  /// </summary>
  public int Id { get; protected init; } = id;

  /// <summary>
  /// Gets the name of the enumeration.
  /// </summary>
  public string? Name { get; protected init; } = name;

  /// <summary>
  /// Retrieves an enumeration value based on its ID.
  /// </summary>
  /// <param name="id">The ID of the enumeration value.</param>
  /// <returns>The enumeration value.</returns>
  public static TEnum FromValue(int id)
  {
    return Enumerations.TryGetValue(id, out TEnum? enumeration)
      ? enumeration
      : default!;
  }

  /// <summary>
  /// Retrieves an enumeration value based on its name.
  /// </summary>
  /// <param name="name">The name of the enumeration value.</param>
  /// <returns>The enumeration value.</returns>
  public static TEnum? FromName(string name) =>
    Enumerations.Values.SingleOrDefault(e => e.Name == name);

  /// <summary>
  /// Retrieves all enumeration values.
  /// </summary>
  /// <returns>A list of enumeration values.</returns>
  public static List<TEnum> GetValues() => Enumerations.Values.ToList();

  /// <summary>
  /// Creates a dictionary of enumerations.
  /// </summary>
  /// <returns>A dictionary of enumerations.</returns>
  public static Dictionary<int, TEnum> CreateEnumerations()
  {
    var enumerationType = typeof(TEnum);
#pragma warning disable SA1009 // Closing parenthesis should be spaced correctly
    var fields = enumerationType
      .GetFields(
        BindingFlags.Public |
        BindingFlags.Static |
        BindingFlags.FlattenHierarchy)
      .Where(f => enumerationType.IsAssignableFrom(f.FieldType))
      .Select(f => (TEnum)f.GetValue(null)!);
#pragma warning restore SA1009 // Closing parenthesis should be spaced correctly

    return fields.ToDictionary(e => e.Id);
  }

  /// <summary>
  ///  Determines whether the current instance is equal to another instance.
  ///  </summary>
  /// <param name="other"> The instance to compare with.</param>
  /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
  public bool Equals(Enumeration<TEnum>? other) =>
  other is not null && GetType() == other.GetType() && Id == other.Id;

  /// <summary>
  /// Determines whether the current instance is equal to another instance.
  /// </summary>
  /// <param name="obj">The instance to compare with.</param>
  /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
  public override bool Equals(object? obj) => obj is Enumeration<TEnum> other && Equals(other);

  /// <summary>
  /// Retrieves the hash code for the current instance.
  /// </summary>
  /// <returns>The hash code for the current instance.</returns>
  public override int GetHashCode() => Id.GetHashCode();

  /// <summary>
  /// Retrieves the name of the enumeration value.
  /// </summary>
  /// <returns>The name of the enumeration value.</returns>
  public override string ToString() => Name!;
}
