namespace Infrastructure.Outbox;

using Microsoft.Extensions.Options;
using Quartz;

/// <summary>
/// Represents the process outbox message setup.
/// </summary>
public class ProcessOutboxMessageSetup : IConfigureOptions<QuartzOptions>
{
  private readonly OutboxOptions outboxOptions;

  /// <summary>
  /// Initializes a new instance of the <see cref="ProcessOutboxMessageSetup"/> class.
  /// </summary>
  /// <param name="outboxOptions">The outbox options.</param>
  public ProcessOutboxMessageSetup(IOptions<OutboxOptions> outboxOptions)
  {
    this.outboxOptions = outboxOptions.Value;
  }

  /// <summary>
  /// Configures the options.
  /// </summary>
  /// <param name="options">The options.</param>
  public void Configure(QuartzOptions options)
  {
    const string jobName = nameof(InvokeOutboxMessagesJob);
    options
      .AddJob<InvokeOutboxMessagesJob>(configure => configure.WithIdentity(jobName))
      .AddTrigger(trigger =>
        trigger
          .ForJob(jobName)
          .WithSimpleSchedule(schedule =>
          {
            schedule
              .WithIntervalInSeconds(outboxOptions.IntervalInSeconds)
              .RepeatForever();
          }));
  }
}
