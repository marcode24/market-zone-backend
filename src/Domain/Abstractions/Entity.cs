namespace Domain.Abstractions;

/// <summary>
/// Represents an abstract entity in the domain.
/// </summary>
/// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
public abstract class Entity<TEntityId> : IEntity
{
  private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();

  /// <summary>
  /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
  /// </summary>
  protected Entity()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
  /// </summary>
  /// <param name="id">The entity identifier.</param>
  protected Entity(TEntityId id) => Id = id;

  /// <summary>
  /// Gets the identifier of the entity.
  /// </summary>
  public TEntityId? Id { get; init; }

  /// <summary>
  /// Gets the domain events associated with the entity.
  /// </summary>
  /// <returns>The list of domain events.</returns>
  public IReadOnlyList<IDomainEvent> GetDomainEvents() => domainEvents.ToList().AsReadOnly();

  /// <summary>
  /// Clears the domain events associated with the entity.
  /// </summary>
  public void ClearDomainEvents() => domainEvents.Clear();

  /// <summary>
  /// Raises a domain event.
  /// </summary>
  /// <param name="domainEvent">The domain event to raise.</param>
  protected void RaiseDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);
}
