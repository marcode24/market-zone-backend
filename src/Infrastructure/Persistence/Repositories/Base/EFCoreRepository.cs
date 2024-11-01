using System.Data;
using System.Text;
using Application.Abstractions.Data;
using Dapper;
using Domain.Abstractions;
using Domain.Repositories.Paginations;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

internal abstract class EFCoreRepository<TEntity, TEntityId>
  where TEntity : Entity<TEntityId>
  where TEntityId : class
{
  protected readonly ApplicationDbContext _dbContext;

  public EFCoreRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken)
  {
    return await _dbContext.Set<TEntity>()
      .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
  }

  public async Task<TEntity?> GetByIdIncludingDeletedAsync(TEntityId id, CancellationToken cancellationToken)
  {
    return await _dbContext.Set<TEntity>()
      .IgnoreQueryFilters()
      .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
  }

  public virtual void Add(TEntity entity)
  {
    _dbContext.Set<TEntity>().Add(entity);
  }

  public virtual void AddRange(IEnumerable<TEntity> entities)
  {
    _dbContext.Set<TEntity>().AddRange(entities);
  }
}
