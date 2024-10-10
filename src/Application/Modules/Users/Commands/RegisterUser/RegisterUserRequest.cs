namespace Application.Modules.Users.Commands.RegisterUser;

public record RegisterUserRequest(
  string Name,
  string LastName,
  string Email,
  string Password,
  int RoleId,
  string? Photo
);
