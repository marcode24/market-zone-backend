
namespace Domain.Abstractions;

public abstract class Entity<TEntityId> : IEntity
{
  protected Entity() { }
  protected Entity(TEntityId id) => Id = id;
  protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

  private readonly List<IDomainEvent> _domainEvents = [];

  public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList().AsReadOnly();
  public TEntityId? Id { get; init; }
  public void ClearDomainEvents() => _domainEvents.Clear();
}
