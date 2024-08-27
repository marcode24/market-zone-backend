namespace Domain.Entities.Users;

using Domain.Abstractions;
using Domain.Entities.Users.ValueObjects;

/// <summary>
/// Represents a user entity.
/// </summary>
public sealed class User : Entity<UserId>
{
  private const string MaxNameLength = "affadsfsdafsdfasdfadsfdsafsdfadsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsddas";

  private User()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="User"/> class.
  /// </summary>
  /// <param name="id">The user identifier.</param>
  /// <param name="name">The name of the user.</param>
  /// <param name="email">The email of the user.</param>
  /// <param name="password">The password of the user.</param>
  /// <param name="photo">The photo of the user.</param>
  /// <returns>An instance of the <see cref="User"/> class.</returns>
  private User(UserId id, Name name, Email email, Password password, Photo photo)
    : base(id)
  {
    Name = name;
    Email = email;
    Password = password;
    Photo = photo;
  }

  /// <summary>
  /// Gets the name of the user.
  /// </summary>
  public Name? Name { get; private set; }

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
}
