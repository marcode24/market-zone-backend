using Domain.Abstractions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base;

internal abstract class Repository<TEntity, TEntityId>(ApplicationDbContext dbContext)
  where TEntity : Entity<TEntityId>
  where TEntityId : class
{
  protected readonly ApplicationDbContext _dbContext = dbContext;

  public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken)
  {
    return await _dbContext.Set<TEntity>()
      .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
  }

  public virtual void Add(TEntity entity)
  {
    _dbContext.Set<TEntity>().Add(entity);
  }

  public IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TEntityId> specification)
  {
    return SpecificationEvaluator<TEntity, TEntityId>
      .GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification);
  }

  public async Task<IReadOnlyList<TEntity>> GetAllWithSpec(
    ISpecification<TEntity, TEntityId> specification,
    CancellationToken cancellationToken)
  {
    return await ApplySpecification(specification)
      .ToListAsync(cancellationToken);
  }

  public async Task<int> CountAsync(
    ISpecification<TEntity, TEntityId> specification,
    CancellationToken cancellationToken)
  {
    return await ApplySpecification(specification)
      .CountAsync(cancellationToken);
  }
}
