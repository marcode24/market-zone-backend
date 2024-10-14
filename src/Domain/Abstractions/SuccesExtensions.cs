namespace Domain.Abstractions;

public static class SuccessExtensions
{
  public static Success Format(this Success success, string entityName) =>
    new(success.Code, string.Format(success.Message, entityName));
}
