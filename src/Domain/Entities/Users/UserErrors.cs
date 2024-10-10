using Domain.Abstractions;

namespace Domain.Entities.Users;

public static class UserErrors
{
  public static readonly Error InvalidEmail = new(
    "INVALID_EMAIL",
    "The email is invalid."
  );

  public static readonly Error AlreadyExists = new(
    "ALREADY_EXISTS",
    "User with this email already exists."
  );

  public static readonly Error NotFound = new(
    "NOT_FOUND",
    "User not found."
  );

  public static readonly Error InvalidCredentials = new(
    "INVALID_CREDENTIALS",
    "Invalid credentials."
  );

}
