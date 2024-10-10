using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;

namespace Domain.Repositories.Roles;

public interface IRoleRepository
{
  Task<bool> ExistsByIdAsync(RoleId id, CancellationToken cancellationToken);
  void Add(Role role);
}
