namespace Domain.Entities.Users.ValueObjects;

public record UserId(int Value) : IEquatable<UserId>
{
  public static UserId New(int value)
  {
    if (value <= 0)
      throw new ArgumentException("User id must be greater than 0", nameof(value));

    return new UserId(value);
  }

  public override int GetHashCode() => Value.GetHashCode();

  public override string ToString() => Value.ToString();
}
