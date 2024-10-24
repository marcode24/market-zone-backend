using Domain.Abstractions;

namespace Domain.Specifications;

public abstract class BaseSpecification<TEntity, TResponse> : ISpecification<TResponse>
{
  public string? WhereClause { get; protected set; }
  public Dictionary<string, object> Parameters { get; protected set; } = new();
  public string? SelectedFields { get; protected set; }
  public string? OrderBy { get; protected set; }
  public bool IsPagingEnabled { get; protected set; }
  public int Take { get; protected set; }
  public int Skip { get; protected set; }

  protected void AddCondition(string condition, string parameterName, object value)
  {
    if (string.IsNullOrWhiteSpace(WhereClause))
      WhereClause = condition;
    else
      WhereClause += $" AND {condition}";

    if (!Parameters.ContainsKey(parameterName))
    {
      Parameters.Add(parameterName, value);
    }
    else
    {
      throw new InvalidOperationException($"A parameter with the name '{parameterName}' already exists.");
    }
  }

  protected void AddOrderBy(string orderByClause)
  {
    if (string.IsNullOrWhiteSpace(OrderBy))
      OrderBy = orderByClause;
    else
      OrderBy += $", {orderByClause}"; // Permite múltiples campos de ordenación
  }

  protected void ApplyPaging(int skip, int take)
  {
    Skip = skip;
    Take = take;
    IsPagingEnabled = true;
  }

  protected void SelectFields(string fields)
  {
    SelectedFields = fields;
  }
}
