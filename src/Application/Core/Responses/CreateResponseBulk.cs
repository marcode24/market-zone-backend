namespace Application.Core.Responses;

public class CreateResponseBulk(int createdCount, string message = "")
{
  public int CreatedCount { get; set; } = createdCount;
  public string Message { get; set; } = string.IsNullOrWhiteSpace(message)
    ? $"{createdCount} item{(createdCount > 1 ? "s" : "")} created successfully"
    : message;

  public static CreateResponseBulk Success(int count, string message = "") =>
    new(count, message);
}
