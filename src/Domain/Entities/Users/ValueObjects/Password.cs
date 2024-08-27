namespace Domain.Entities.Users.ValueObjects;
public record Password(string Value)
{
  public static Password New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Password cannot be empty", nameof(value));

    return new Password(value);
  }

  public override string ToString() => Value;
}