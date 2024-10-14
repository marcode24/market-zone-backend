namespace Domain.Shared.ValueObjects;

public record Name(string Value) : BaseValueObject(Value)
{
  public static Name New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Name cannot be empty", nameof(value));

    return new Name(value);
  }

  public override string ToString() => Value;
}
