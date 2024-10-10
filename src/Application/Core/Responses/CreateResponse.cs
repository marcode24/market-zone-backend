namespace Application.Core.Responses;

public class CreateResponse<T>(T createdItem, string message, int? id = null)
{
  public T CreatedItem { get; set; } = createdItem;
  public string Message { get; set; } = string.IsNullOrWhiteSpace(message)
    ? "Item created successfully"
    : message;

  public int? Id { get; set; } = id;

  public static CreateResponse<T> Success(T item, int? id = null, string message = "") =>
    new(item, message, id);
}
