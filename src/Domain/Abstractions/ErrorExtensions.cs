namespace Domain.Abstractions;

public static class ErrorExtensions
{
  public static Error Format(this Error error, string entityName) =>
    new(error.Code, string.Format(error.Message, entityName));
}
