namespace Application.Services;

public interface ILogService
{
  void LogError(string message);
  void LogInformation(string message);
  void LogDatabaseError(string message);
  void LogCustom(string message, string filePath);
}
