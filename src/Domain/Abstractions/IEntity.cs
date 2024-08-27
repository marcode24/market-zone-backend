namespace Domain.Abstractions;

/// <summary>
/// Represents an entity in the domain.
/// </summary>
public interface IEntity
{
  /// <summary>
  /// Gets the domain events associated with the entity.
  /// </summary>
  /// <returns>The list of domain events.</returns>
  IReadOnlyList<IDomainEvent> GetDomainEvents();

  /// <summary>
  /// Clears the domain events associated with the entity.
  /// </summary>
  void ClearDomainEvents();
}
