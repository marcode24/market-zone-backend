namespace Infrastructure.Persistence.Outbox;

public sealed class OutboxMessage
{
  public OutboxMessage(
    Guid id,
    DateTime occurredOnUtc,
    string? type,
    string? content)
  {
    Id = id;
    OccurredOnUtc = occurredOnUtc;
    Type = type;
    Content = content;
    ProcessedOnUtc = DateTime.UtcNow;
  }
  public Guid Id { get; set; }
  public DateTime OccurredOnUtc { get; set; }
  public string? Type { get; set; }
  public string? Content { get; set; }
  public DateTime ProcessedOnUtc { get; set; }
  public DateTime? ErrorOnUtc { get; set; }
  public string? ErrorMessage { get; set; }
}
