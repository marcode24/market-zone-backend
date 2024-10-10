namespace Infrastructure.Logging;

using Application.Services;
using Serilog;
using Serilog.Core;

public class LogService(
  ILogger generalLogger,
  ILogger databaseLogger,
  ILogger applicationLogger) : ILogService
{
  private readonly ILogger _generalLogger = generalLogger;
  private readonly ILogger _databaseLogger = databaseLogger;
  private readonly ILogger _applicationLogger = applicationLogger;

  public void LogError(string message)
  {
    _generalLogger.Error(message);
  }

  public void LogInformation(string message)
  {
    _applicationLogger.Information(message);
  }

  public void LogDatabaseError(string message)
  {
    _databaseLogger.Error("Database Error: " + message);
  }

  public void LogCustom(string message, string filePath)
  {
    var customLogger = new Serilog.LoggerConfiguration()
        .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
        .CreateLogger();

    customLogger.Information(message);
  }
}
