namespace Domain.Entities.Users;

using Domain.Abstractions;
using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;
using Domain.Entities.Users.Events;
using Domain.Entities.Users.ValueObjects;
using Domain.Shared.ValueObjects;

public sealed class User : Entity<UserId>
{
  private readonly List<Role> roles = new();

  private User(
    Name name,
    LastName lastName,
    Email email,
    Password password,
    Photo photo
  ) : base(DateTime.UtcNow, DateTime.UtcNow)
  {
    Name = name;
    LastName = lastName;
    Email = email;
    Password = password;
    Photo = photo;
  }
  private User() { }

  public Name? Name { get; private set; }
  public LastName? LastName { get; private set; }
  public Email? Email { get; private set; }
  public Password? Password { get; private set; }
  public Photo? Photo { get; private set; }
  public RoleId? RoleId { get; private set; }
  public Role? Role { get; private set; }
  public IReadOnlyCollection<Role>? Roles => roles.ToList();

  public static User Create(Name name, LastName lastName, Email email, Password password, Photo photo)
  {
    var user = new User(name, lastName, email, password, photo);
    user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id!));

    return user;
  }
  public void Update(Name name, LastName lastName, Email email, Password password, Photo photo)
  {
    Name = name;
    LastName = lastName;
    Email = email;
    Password = password;
    Photo = photo;
    UpdateTimestamps(); // Actualiza la fecha de modificación
  }
}
