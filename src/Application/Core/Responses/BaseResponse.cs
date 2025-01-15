using System.Diagnostics.CodeAnalysis;
using System.Net;
using Domain.Abstractions;

namespace Application.Core.Responses;

public class Response
{
  public bool IsSuccess { get; set; }
  public Error? Error { get; set; } = Error.None;
  public int StatusCode { get; set; } = 200;
  public string Message { get; set; } = string.Empty;

  protected Response(
    bool isSuccess,
    Error? error,
    int statusCode,
    string? message)
  {
    if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
      throw new InvalidOperationException();

    IsSuccess = isSuccess;
    Error = error;
    StatusCode = statusCode;
    Message = message!;
  }

  public static Response Success(string message = "Operation completed successfully.", int statusCode = (int)HttpStatusCode.OK) =>
    new(true, Error.None, statusCode, message);
  public static Response Failure(Error error, int statusCode = (int)HttpStatusCode.BadRequest) =>
    new(false, error, statusCode, error.Message);

  public static Response<TValue> Success<TValue>(
    TValue value,
    string message,
    int statusCode = 200) => new(true, value, Error.None, statusCode, message);

  public static Response<TValue> Failure<TValue>(
    Error error,
    int statusCode = 400) => new(false, default, error, statusCode, error.Message);
}

public class Response<TValue> : Response
{
  private readonly TValue? _Data;

  protected internal Response(
    bool isSuccess,
    TValue? value,
    Error? error,
    int statusCode,
    string? message)
    : base(isSuccess, error!, statusCode, message)
  {
    _Data = value;
  }

  public TValue Result => _Data!;
}
