using Domain.Abstractions;
using Domain.Shared;

namespace Application.Abstractions.Services;

public interface IPaginationService<TEntity, TResponse>
{
  Task<PagedResults<TResponse>> GetAllAsync(IQueryable<TEntity> query, CancellationToken cancellationToken);
  Task<PagedResults<TResponse>> GetPagedAsync(IQueryable<TEntity> query, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
