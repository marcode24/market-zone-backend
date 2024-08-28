namespace Infrastructure.Outbox;

/// <summary>
/// Represents the outbox message.
/// </summary>
public sealed class OutboxMessage
{
  /// <summary>
  /// Initializes a new instance of the <see cref="OutboxMessage"/> class.
  /// </summary>
  /// <param name="id">The ID.</param>
  /// <param name="ocurrendOnUtc">The occurred on UTC.</param>
  /// <param name="type">The type.</param>
  /// <param name="content">The content.</param>
  public OutboxMessage(
    Guid id,
    DateTime ocurrendOnUtc,
    string? type,
    string? content)
  {
    Id = id;
    OcurredOnUtc = ocurrendOnUtc;
    Type = type;
    Content = content;
    ProcessedOnUtc = DateTime.UtcNow;
  }

  /// <summary>
  /// Gets or sets the ID.
  /// </summary>
  public Guid Id { get; set; }

  /// <summary>
  /// Gets or sets the occurred on UTC.
  /// </summary>
  public DateTime OcurredOnUtc { get; set; }

  /// <summary>
  /// Gets or sets the type.
  /// </summary>
  public string? Type { get; set; }

  /// <summary>
  /// Gets or sets the content.
  /// </summary>
  public string? Content { get; set; }

  /// <summary>
  /// Gets or sets the processed on UTC.
  /// </summary>
  public DateTime ProcessedOnUtc { get; set; }

  /// <summary>
  /// Gets or sets the error on UTC.
  /// </summary>
  public DateTime? ErrorOnUtc { get; set; }

  /// <summary>
  /// Gets or sets the error message.
  /// </summary>
  public string? ErrorMessage { get; set; }
}
