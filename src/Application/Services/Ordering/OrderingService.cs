namespace Application.Services.Ordering;

using Application.Abstractions.Services;
using Application.Extensions;

public class OrderingService<TEntity> : IOrderingService<TEntity>
  where TEntity : class
{
  public IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, string? orderBy, bool orderAsc)
  {
    if (!string.IsNullOrEmpty(orderBy))
    {
      return orderAsc
       ? query.OrderByProperty(orderBy)
       : query.OrderByDescendingProperty(orderBy);
    }
    return query;
  }
}
