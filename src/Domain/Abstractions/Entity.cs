namespace Domain.Abstractions;

public abstract class Entity<TEntityId> : IEntity
{
  private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();
  protected Entity()
  {
    CreatedAt = DateTime.UtcNow;
    UpdatedAt = DateTime.UtcNow;

  }
  protected Entity(DateTime createdAt, DateTime updatedAt)
  {
    CreatedAt = createdAt;
    UpdatedAt = updatedAt;
  }
  public TEntityId? Id { get; init; }
  public bool IsActive { get; protected set; } = true;
  public bool IsDeleted { get; protected set; } = false;
  public DateTime CreatedAt { get; protected set; }
  public DateTime UpdatedAt { get; protected set; }
  public DateTime? DeletedAt { get; protected set; }
  public IReadOnlyList<IDomainEvent> GetDomainEvents() => domainEvents.ToList().AsReadOnly();
  public void ClearDomainEvents() => domainEvents.Clear();
  public void UpdateTimestamps() => UpdatedAt = DateTime.UtcNow;
  public void ChangeStatus() => IsActive = !IsActive;
  public void SoftDelete()
  {
    IsDeleted = true;
    DeletedAt = DateTime.UtcNow;
  }
  protected void RaiseDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);
}
