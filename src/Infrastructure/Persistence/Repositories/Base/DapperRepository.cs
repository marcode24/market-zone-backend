using System.Data;
using System.Text;
using Application.Abstractions.Data;
using Application.Abstractions.Providers;
using Dapper;
using Domain.Abstractions;
using Domain.Repositories.Paginations;
using Domain.Shared;

internal abstract class DapperRepository<TEntity, TResponse>
  : IPaginationRepository<TEntity, TResponse>
{
  private readonly ISqlConnectionFactory _sqlConnectionFactory;
  private readonly ITableNameProvider _tableNameProvider;

  protected DapperRepository(
      ISqlConnectionFactory sqlConnectionFactory,
      ITableNameProvider tableNameProvider)
  {
    _sqlConnectionFactory = sqlConnectionFactory;
    _tableNameProvider = tableNameProvider;
  }

  public async Task<PagedResults<TResponse>> GetPagedResults(
    ISpecification<TResponse> specification,
    CancellationToken cancellationToken)
  {
    using var connection = _sqlConnectionFactory.CreateConnection();

    var sqlBuilder = new StringBuilder();

    var tableName = _tableNameProvider.GetTableName<TEntity>();

    sqlBuilder.Append($"SELECT {specification.SelectedFields} FROM {tableName}");

    if (!string.IsNullOrEmpty(specification.WhereClause))
      sqlBuilder.Append($" WHERE {specification.WhereClause}");

    if (!string.IsNullOrEmpty(specification.OrderBy))
      sqlBuilder.Append($" ORDER BY {specification.OrderBy}");

    if (specification.IsPagingEnabled)
      sqlBuilder.Append(" OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY");

    var parameters = new DynamicParameters();

    // Agregar parámetros desde la especificación
    foreach (var parameter in specification.Parameters)
    {
      parameters.Add(parameter.Key, parameter.Value);
    }

    if (specification.IsPagingEnabled)
    {
      parameters.Add("Skip", specification.Skip);
      parameters.Add("Take", specification.Take);
    }

    // imprimir consulta SQL
    Console.WriteLine(sqlBuilder.ToString());
    // imprimir parámetros
    foreach (var parameter in parameters.ParameterNames)
    {
      Console.WriteLine($"{parameter}: {parameters.Get<object>(parameter)}");
    }

    var results = await connection.QueryAsync<TResponse>(sqlBuilder.ToString(), parameters);

    var totalCount = await GetTotalCountAsync(tableName, specification, connection);

    return new PagedResults<TResponse>
    {
      Results = results.AsList(),
      PageNumber = specification.Skip / specification.Take + 1,
      PageSize = specification.Take,
      TotalRecords = totalCount,
      TotalPages = (int)Math.Ceiling(totalCount / (double)specification.Take)
    };
  }

  private static async Task<int> GetTotalCountAsync(string tableName, ISpecification<TResponse> specification, IDbConnection connection)
  {
    var sqlBuilder = new StringBuilder();

    // Usar TableName desde la especificación
    sqlBuilder.Append($"SELECT COUNT(*) FROM {tableName}");

    if (!string.IsNullOrEmpty(specification.WhereClause))
      sqlBuilder.Append($" WHERE {specification.WhereClause}");

    return await connection.ExecuteScalarAsync<int>(sqlBuilder.ToString(), specification.Parameters);
  }
}
