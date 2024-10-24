using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;
using Domain.Repositories.Users;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Specific.Users;

internal sealed class UserRepository
  : EFCoreRepository<User, UserId>, IUserRepository
{
  public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

  public int CreateAsync(User user, CancellationToken cancellationToken)
  {
    _dbContext.Set<User>().Add(user);
    return user.Id!.Value;
  }

  public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken)
  {
    return await _dbContext.Set<User>()
      .AnyAsync(user => user.Email == email, cancellationToken);
  }

  public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
  {
    return await _dbContext.Set<User>()
      .SingleOrDefaultAsync(user => user.Email == email, cancellationToken);
  }
}
