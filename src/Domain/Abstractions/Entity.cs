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
  /// <param name="id">The identifier of the entity.</param>
  protected Entity(TEntityId id)
  {
    Id = id;
    CreatedAt = DateTime.UtcNow;
    UpdatedAt = DateTime.UtcNow;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
  /// </summary>
  /// <param name="id">The identifier of the entity.</param>
  /// <param name="createdAt">The date and time when the entity was created.</param>
  /// <param name="updatedAt">The date and time when the entity was last updated.</param>
  protected Entity(TEntityId id, DateTime createdAt, DateTime updatedAt)
  {
    Id = id;
    CreatedAt = createdAt;
    UpdatedAt = updatedAt;
  }

  /// <summary>
  /// Gets the identifier of the entity.
  /// </summary>
  public TEntityId? Id { get; init; }

  /// <summary>
  /// Gets or sets the date and time when the entity was created.
  /// </summary>
  public DateTime CreatedAt { get; protected set; }

  /// <summary>
  /// Gets or sets the date and time when the entity was last updated.
  /// </summary>
  public DateTime UpdatedAt { get; protected set; }

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
  /// Updates the timestamps of the entity.
  /// </summary>
  public void UpdateTimestamps()
  {
    UpdatedAt = DateTime.UtcNow;
  }

  /// <summary>
  /// Raises a domain event.
  /// </summary>
  /// <param name="domainEvent">The domain event to raise.</param>
  protected void RaiseDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);
}
