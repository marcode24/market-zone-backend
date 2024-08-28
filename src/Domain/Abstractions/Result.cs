namespace Domain.Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a result of an operation.
/// </summary>
public class Result
{
  /// <summary>
  /// Initializes a new instance of the <see cref="Result"/> class.
  /// </summary>
  /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
  /// <param name="error">The error that occurred during the operation.</param>
  protected Result(bool isSuccess, Error error)
  {
    if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
      throw new InvalidOperationException();

    IsSuccess = isSuccess;
    Error = error;
  }

  /// <summary>
  /// Gets a value indicating whether the operation was successful.
  /// </summary>
  public bool IsSuccess { get; }

  /// <summary>
  /// Gets a value indicating whether the operation was a failure.
  /// </summary>
  public bool IsFailure => !IsSuccess;

  /// <summary>
  /// Gets the error that occurred during the operation.
  /// </summary>
  public Error Error { get; } = Error.None;

  /// <summary>
  /// Represents a successful operation.
  /// </summary>
  /// <returns>A successful result.</returns>
  public static Result Success() => new(true, Error.None);

  /// <summary>
  /// Represents a failure operation.
  /// </summary>
  /// <param name="error">The error that occurred during the operation.</param>
  /// <returns>A failure result.</returns>
  public static Result Failure(Error error) => new(false, error);

  /// <summary>
  /// Represents a successful operation.
  /// </summary>
  /// <typeparam name="TValue">The type of the value.</typeparam>
  /// <param name="value">The value of the operation.</param>
  /// <returns>A successful result with a value.</returns>
  public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

  /// <summary>
  /// Represents a failure operation.
  /// </summary>
  /// <param name="error">The error that occurred during the operation.</param>
  /// <returns>A failure result.</returns>
  /// <typeparam name="TValue">The type of the value.</typeparam>
  public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

  /// <summary>
  /// Represents a successful operation.
  /// </summary>
  /// <typeparam name="TValue">The type of the value.</typeparam>
  /// <param name="value">The value of the operation.</param>
  /// <returns>A successful result with a value.</returns>
  public static Result<TValue> Create<TValue>(TValue value) =>
    value is not null
      ? Success(value)
      : Failure<TValue>(Error.NullValue);
}

/// <summary>
/// Represents a result of an operation with a value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public class Result<TValue> : Result
{
  private readonly TValue? value;

  /// <summary>
  /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
  /// </summary>
  /// <param name="value">The value of the operation.</param>
  /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
  /// <param name="error">The error that occurred during the operation.</param>
  /// <returns>An instance of the <see cref="Result{TValue}"/> class.</returns>
  protected internal Result(TValue? value, bool isSuccess, Error error)
    : base(isSuccess, error) => this.value = value;

  /// <summary>
  /// Gets the value of the operation.
  /// </summary>
  /// <returns>The value of the operation.</returns>
  [NotNull]
  public TValue Value =>
    IsSuccess
      ? value!
      : throw new InvalidOperationException("There is no value for failure.");

  /// <summary>
  /// Represents a successful operation.
  /// </summary>
  /// <param name="value">The value of the operation.</param>
  /// <returns>A successful result with a value.</returns>
  public static implicit operator Result<TValue>(TValue value) => Create(value);
}
