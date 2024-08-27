namespace Domain.Entities.Users.ValueObjects;

public record Email(string Value)
{
  public static Email New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Email cannot be empty", nameof(value));

    return new Email(value);
  }

  public override string ToString() => Value;
}