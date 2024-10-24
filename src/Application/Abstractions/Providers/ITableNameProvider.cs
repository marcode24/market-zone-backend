namespace Application.Abstractions.Providers;

public interface ITableNameProvider
{
  string GetTableName<TEntity>();
}
