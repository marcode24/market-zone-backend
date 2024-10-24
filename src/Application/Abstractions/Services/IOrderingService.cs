namespace Application.Abstractions.Services;

public interface IOrderingService<TEntity>
  where TEntity : class
{
  IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, string? orderBy, bool orderAsc);
}
