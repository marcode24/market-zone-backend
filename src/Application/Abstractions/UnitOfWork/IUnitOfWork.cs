using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Abstractions;

/// <summary>
/// Represents a unit of work.
/// </summary>
public interface IUnitOfWork
{
  /// <summary>
  /// Save changes.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task that represents the asynchronous save operation.</returns>
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
  Task CommitTransactionAsync(CancellationToken cancellationToken = default);
  Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
