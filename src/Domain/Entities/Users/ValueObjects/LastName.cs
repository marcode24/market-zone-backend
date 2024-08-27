namespace Domain.Entities.Users.ValueObjects;

public record LastName(string Value)
{
  public static LastName New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Last name cannot be empty", nameof(value));

    return new LastName(value);
  }

  public override string ToString() => Value;
}