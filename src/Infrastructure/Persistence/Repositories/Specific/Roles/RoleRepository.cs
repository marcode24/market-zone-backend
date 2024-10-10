using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;
using Domain.Repositories.Roles;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Specific.Roles;

internal sealed class RoleRepository
  : Repository<Role, RoleId>, IRoleRepository
{
  public RoleRepository(ApplicationDbContext dbContext)
    : base(dbContext) { }

  public Task<bool> ExistsByIdAsync(RoleId id, CancellationToken cancellationToken)
  {
    return _dbContext.Set<Role>()
      .AnyAsync(role => role.Id == id, cancellationToken);
  }
}
