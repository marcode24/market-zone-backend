namespace Infrastructure.Persistence.Outbox;

using Microsoft.Extensions.Options;
using Quartz;

public class ProcessOutboxMessageSetup : IConfigureOptions<QuartzOptions>
{
  private readonly OutboxOptions outboxOptions;
  public ProcessOutboxMessageSetup(IOptions<OutboxOptions> outboxOptions)
  {
    this.outboxOptions = outboxOptions.Value;
  }
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
