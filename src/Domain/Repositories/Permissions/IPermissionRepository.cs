using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;

namespace Domain.Repositories.Permissions;

public interface IPermissionRepository
{
  Task<bool> ExistsByIdAsync(PermissionId id, CancellationToken cancellationToken);
  void Add(Permission permission);
  Task<Permission?> GetByIdAsync(PermissionId id, CancellationToken cancellationToken);
  Task<Permission?> GetByIdIncludingDeletedAsync(PermissionId id, CancellationToken cancellationToken);
  IQueryable<Permission> Query();
  void AddRange(IEnumerable<Permission> permissions);
  Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken);
}
