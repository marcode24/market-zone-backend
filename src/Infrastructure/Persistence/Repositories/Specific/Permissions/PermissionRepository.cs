using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Specific.Permissions;

internal sealed class PermissionRepository
  : Repository<Permission, PermissionId>, IPermissionRepository
{
  public PermissionRepository(ApplicationDbContext dbContext)
    : base(dbContext) { }

  public void CreateAsync(Permission permission, CancellationToken cancellationToken)
  {
    _dbContext.Set<Permission>().Add(permission);
  }

  public Task<bool> ExistsByIdAsync(PermissionId id, CancellationToken cancellationToken)
  {
    return _dbContext.Set<Permission>()
      .AnyAsync(permission => permission.Id == id, cancellationToken);
  }
}
