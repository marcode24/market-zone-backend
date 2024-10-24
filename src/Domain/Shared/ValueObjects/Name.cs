using System.Net.NetworkInformation;

namespace Domain.Shared.ValueObjects;

public record Name(string Value)
{
  public static Name New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Name cannot be empty", nameof(value));

    return new Name(value);
  }

  public override string ToString() => Value;

  public static implicit operator string(Name name) => name.Value;
}
