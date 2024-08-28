namespace Application.Abstractions.Data;

using System.Data;

/// <summary>
/// Represents the SQL connection factory.
/// </summary>
public interface ISqlConnectionFactory
{
  /// <summary>
  /// Creates a new connection.
  /// </summary>
  /// <returns>The connection.</returns>
  IDbConnection CreateConnection();
}
