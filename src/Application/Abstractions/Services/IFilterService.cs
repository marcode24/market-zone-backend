using LinqKit;

namespace Application.Abstractions.Services;

public interface IFilterService<TEntity>
{
  ExpressionStarter<TEntity> BuildFilterPredicate(List<(string PropertyName, string SearchValue)> searchCriteria);
}
