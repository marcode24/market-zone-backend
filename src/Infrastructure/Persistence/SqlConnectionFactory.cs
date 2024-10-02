namespace Infrastructure.Persistence.Database;

using System.Data;
using Application.Abstractions.Data;
using Npgsql;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
  private readonly string connectionString;

  public SqlConnectionFactory(string connectionString)
  {
    this.connectionString = connectionString;
  }

  public IDbConnection CreateConnection()
  {
    var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    return connection;
  }
}
