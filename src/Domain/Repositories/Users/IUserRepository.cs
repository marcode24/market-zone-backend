using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;

namespace Domain.Repositories.Users;

public interface IUserRepository
{
  Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken);
  int CreateAsync(User user, CancellationToken cancellationToken);
  Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken);
  Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
}
