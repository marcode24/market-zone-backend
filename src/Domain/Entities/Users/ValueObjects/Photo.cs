namespace Domain.Entities.Users.ValueObjects;
public record Photo(string Value)
{
  public static Photo New(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new ArgumentException("Photo cannot be empty", nameof(value));

    return new Photo(value);
  }

  public override string ToString() => Value;
}