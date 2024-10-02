namespace Infrastructure.Abstractions.Clock;

using Application.Abstractions.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
  public DateTime CurrentTime => DateTime.UtcNow;
}
