namespace Domain.Abstractions;

public record Success(string Code, string Message)
{
  public static readonly Success None = new(string.Empty, string.Empty);
}
