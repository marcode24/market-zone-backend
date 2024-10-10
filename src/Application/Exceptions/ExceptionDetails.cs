namespace Application.Exceptions;

public class ExceptionDetails
{
  public int StatusCode { get; set; }
  public string Title { get; set; }
  public string Type { get; set; }
  public string Detail { get; set; }
  public IEnumerable<object>? Errors { get; set; }

  public ExceptionDetails(
    int statusCode,
    string title,
    string type,
    string detail,
    IEnumerable<object>? errors)
  {
    StatusCode = statusCode;
    Title = title;
    Type = type;
    Detail = detail;
    Errors = errors;
  }
}
