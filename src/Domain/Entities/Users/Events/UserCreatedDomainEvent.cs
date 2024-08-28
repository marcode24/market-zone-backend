namespace Domain.Entities.Users.Events;

using Domain.Abstractions;
using Domain.Entities.Users.ValueObjects;

/// <summary>
/// Represents the domain event that is raised when a user is created.
/// </summary>
/// <param name="UserId">The ID of the user that was created.</param>
public sealed record UserCreatedDomainEvent(UserId UserId)
  : IDomainEvent;
