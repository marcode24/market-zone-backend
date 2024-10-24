namespace Domain.Repositories.Paginations;

using Domain.Abstractions;
using Domain.Shared;

public interface IPaginationRepository<TEntity, TResponse>
{
  Task<PagedResults<TResponse>> GetPagedResults(ISpecification<TResponse> specification, CancellationToken cancellationToken);
}
