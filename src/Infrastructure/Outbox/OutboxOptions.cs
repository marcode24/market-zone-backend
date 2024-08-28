namespace Infrastructure.Outbox;

/// <summary>
/// Represents the outbox options.
/// </summary>
public class OutboxOptions
{
  /// <summary>
  /// Gets or sets the interval in seconds.
  /// </summary>
  public int IntervalInSeconds { get; set; }

  /// <summary>
  /// Gets or sets the batch size.
  /// </summary>
  public int BatchSize { get; set; }
}
