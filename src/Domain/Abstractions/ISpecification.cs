namespace Domain.Abstractions;

public interface ISpecification<TResponse>
{
  string? WhereClause { get; }
  Dictionary<string, object> Parameters { get; }
  string? OrderBy { get; }
  bool IsPagingEnabled { get; }
  int Take { get; }
  int Skip { get; }
  string? SelectedFields { get; }
}
