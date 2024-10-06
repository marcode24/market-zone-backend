namespace Domain.Abstractions;

public record Error(string Code, string Message)
{
  public static readonly Error None = new(string.Empty, string.Empty);
  public static readonly Error NullValue = new("NULL_VALUE", "The value should not be null.");
}
