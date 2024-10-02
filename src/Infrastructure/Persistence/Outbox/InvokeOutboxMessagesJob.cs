namespace Infrastructure.Persistence.Outbox;

using System.Data;
using Application.Abstractions.Clock;
using Application.Abstractions.Data;
using Dapper;
using Domain.Abstractions;
using Infrastructure.Persistence.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

public record OutboxMessageData(Guid Id, string Content);

[DisallowConcurrentExecution]
internal sealed class InvokeOutboxMessagesJob : IJob
{
  private static readonly JsonSerializerSettings JsonSerializerSettings = new()
  {
    TypeNameHandling = TypeNameHandling.All,
  };

  private readonly ISqlConnectionFactory sqlConnectionFactory;
  private readonly IPublisher publisher;
  private readonly IDateTimeProvider dateTimeProvider;
  private readonly OutboxOptions outboxOptions;
  private readonly ILogger<InvokeOutboxMessagesJob> logger;
  private readonly string tableOutboxMessages = Tables.OutboxMessages;
  private readonly string schema = Schemas.Outbox;

  public InvokeOutboxMessagesJob(
    ISqlConnectionFactory sqlConnectionFactory,
    IPublisher publisher,
    IDateTimeProvider dateTimeProvider,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<InvokeOutboxMessagesJob> logger)
  {
    this.sqlConnectionFactory = sqlConnectionFactory;
    this.publisher = publisher;
    this.dateTimeProvider = dateTimeProvider;
    this.outboxOptions = outboxOptions.Value;
    this.logger = logger;
  }

  public async Task Execute(IJobExecutionContext context)
  {
    logger.LogInformation("Processing outbox messages.");
    using var connection = sqlConnectionFactory.CreateConnection();
    using var transaction = connection.BeginTransaction();

    var sql = $@"
      SELECT
        Id,
        Content
      FROM {schema}.{tableOutboxMessages}
      WHERE processed_on_utc IS NULL
      ORDER BY occurred_on_utc
      LIMIT {outboxOptions.BatchSize}
      FOR UPDATE
    ";

    var records = (await connection.QueryAsync<OutboxMessageData>(sql, transaction)).ToList();

    foreach (var message in records)
    {
      Exception? exception = null;

      try
      {
        var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, JsonSerializerSettings)
          ?? throw new InvalidOperationException("Failed to deserialize the domain event.");
        await publisher.Publish(domainEvent, context.CancellationToken);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while processing the outbox message with id: {Id}", message.Id);
        exception = ex;
      }

      transaction.Commit();
      logger.LogInformation("Processed outbox message with id: {Id}", message.Id);
    }
  }
  private async Task UpdateOutboxMessage(
    IDbConnection connection,
    IDbTransaction transaction,
    OutboxMessageData message,
    Exception? exception)
  {
    string sql = $@"
      UPDATE {schema}.{tableOutboxMessages}
      SET
        processed_on_utc = @ProcessedOnUtc,
        error = @Error
      WHERE id = @Id";

    await connection.ExecuteAsync(
      sql,
      new
      {
        Id = message.Id,
        ProcessedOnUtc = dateTimeProvider.CurrentTime,
        Error = exception?.ToString(),
      },
      transaction);
  }
}
