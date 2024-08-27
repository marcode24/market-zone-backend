using Domain.Abstractions;
using Domain.Entities.Users.ValueObjects;

namespace Domain.Entities.Users;

public sealed class User : Entity<UserId>
{
  private User() { }
  private User(UserId id, Name name, Email email, Password password, Photo photo)
  {
    Id = id;
    Name = name;
    Email = email;
    Password = password;
    Photo = photo;
  }

  public Name? Name { get; private set; }
  public Email? Email { get; private set; }
  public Password? Password { get; private set; }
  public Photo? Photo { get; private set; }
}
