namespace Application.Abstractions.Clock;

/// <summary>
/// Represents the date time provider.
/// </summary>
public interface IDateTimeProvider
{
  /// <summary>
  /// Gets the current date time.
  /// </summary>
  DateTime CurrentTime { get; }
}
