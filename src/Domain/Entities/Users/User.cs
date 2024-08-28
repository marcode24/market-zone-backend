namespace Domain.Entities.Users;

using Domain.Abstractions;
using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;
using Domain.Entities.Users.Events;
using Domain.Entities.Users.ValueObjects;
using Domain.Shared.ValueObjects;

/// <summary>
/// Represents a user entity.
/// </summary>
public sealed class User : Entity<UserId>
{
  private readonly List<Role> roles = new();

  /// <summary>
  /// Initializes a new instance of the <see cref="User"/> class.
  /// </summary>
  /// <param name="name">The name of the user.</param>
  /// <param name="lastName">The last name of the user.</param>
  /// <param name="email">The email of the user.</param>
  /// <param name="password">The password of the user.</param>
  /// <param name="photo">The photo of the user.</param>
  public User(Name name, LastName lastName, Email email, Password password, Photo photo)
        : base(default!)
  {
    Name = name;
    LastName = lastName;
    Email = email;
    Password = password;
    Photo = photo;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="User"/> class.
  /// </summary>
  /// <param name="id">The identifier of the user.</param>
  /// <param name="name">The name of the user.</param>
  /// <param name="lastName">The last name of the user.</param>
  /// <param name="email">The email of the user.</param>
  /// <param name="password">The password of the user.</param>
  /// <param name="photo">The photo of the user.</param>
  public User(UserId id, Name name, LastName lastName, Email email, Password password, Photo photo)
        : base(id)
  {
    Name = name;
    LastName = lastName;
    Email = email;
    Password = password;
    Photo = photo;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="User"/> class.
  /// </summary>
  /// <param name="name">The name of the user.</param>
  /// <param name="lastName">The last name of the user.</param>
  /// <param name="email">The email of the user.</param>
  /// <param name="password">The password of the user.</param>
  /// <param name="photo">The photo of the user.</param>
  /// <param name="createdDate">The date and time when the user was created.</param>
  /// <param name="updatedDate">The date and time when the user was last updated.</param>
  public User(Name name, LastName lastName, Email email, Password password, Photo photo, DateTime createdDate, DateTime updatedDate)
      : base(default!, createdDate, updatedDate)
  {
    Name = name;
    LastName = lastName;
    Email = email;
    Password = password;
    Photo = photo;
  }

  private User()
  {
  }

  /// <summary>
  /// Gets the name of the user.
  /// </summary>
  public Name? Name { get; private set; }

  /// <summary>
  /// Gets the last name of the user.
  /// </summary>
  public LastName? LastName { get; private set; }

  /// <summary>
  /// Gets the email of the user.
  /// </summary>
  public Email? Email { get; private set; }

  /// <summary>
  /// Gets the password of the user.
  /// </summary>
  public Password? Password { get; private set; }

  /// <summary>
  /// Gets the photo of the user.
  /// </summary>
  public Photo? Photo { get; private set; }

  /// <summary>
  /// Gets the roles of the user.
  /// </summary>
  public RoleId? RoleId { get; private set; }

  /// <summary>
  /// Gets the role of the user.
  /// </summary>
  public Role? Role { get; private set; }

  /// <summary>
  /// Creates a new user.
  /// </summary>
  /// <param name="name">The name of the user.</param>
  /// <param name="lastName">The last name of the user.</param>
  /// <param name="email">The email of the user.</param>
  /// <param name="password">The password of the user.</param>
  /// <param name="photo">The photo of the user.</param>
  /// <returns>The new user.</returns>
  public static User Create(Name name, LastName lastName, Email email, Password password, Photo photo)
  {
    var user = new User(name, lastName, email, password, photo, DateTime.UtcNow, DateTime.UtcNow);
    user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id!));

    return user;
  }

  /// <summary>
  /// Updates the user.
  /// </summary>
  /// <param name="name">The name of the user.</param>
  /// <param name="lastName">The last name of the user.</param>
  /// <param name="email">The email of the user.</param>
  /// <param name="password">The password of the user.</param>
  /// <param name="photo">The photo of the user.</param>
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
