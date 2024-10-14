namespace Application.Core.Responses;

public class UpdateResponse<T>(T updatedItem, string message)
{
  public T UpdatedItem { get; set; } = updatedItem;
  public string Message { get; set; } = string.IsNullOrWhiteSpace(message)
    ? "Item updated successfully"
    : message;

  public static UpdateResponse<T> Success(T item, string message = "") =>
    new(item, message);
}
