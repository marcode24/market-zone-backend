namespace Domain.Abstractions;

using System.Diagnostics.CodeAnalysis;

public class Result
{
  protected Result(bool isSuccess, Error error)
  {
    if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
      throw new InvalidOperationException();

    IsSuccess = isSuccess;
    Error = error;
  }
  public bool IsSuccess { get; }
  public bool IsFailure => !IsSuccess;
  public Error Error { get; } = Error.None;
  public static Result Success() => new(true, Error.None);
  public static Result Failure(Error error) => new(false, error);
  public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
  public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
  public static Result<TValue> Create<TValue>(TValue value) =>
    value is not null
      ? Success(value)
      : Failure<TValue>(Error.NullValue);
}
public class Result<TValue> : Result
{
  private readonly TValue? value;
  protected internal Result(TValue? value, bool isSuccess, Error error)
    : base(isSuccess, error) => this.value = value;

  [NotNull]
  public TValue Value =>
    IsSuccess
      ? value!
      : throw new InvalidOperationException("There is no value for failure.");

  public static implicit operator Result<TValue>(TValue value) => Create(value);
}
