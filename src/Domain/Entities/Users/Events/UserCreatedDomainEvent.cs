namespace Domain.Entities.Users.Events;

using Domain.Abstractions;
using Domain.Entities.Users.ValueObjects;

public sealed record UserCreatedDomainEvent(UserId UserId)
  : IDomainEvent;
