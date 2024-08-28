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
}
