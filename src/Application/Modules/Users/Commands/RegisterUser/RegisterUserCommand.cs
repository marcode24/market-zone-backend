using Application.Abstractions.Messaging;

namespace Application.Modules.Users.Commands.RegisterUser;

public sealed record RegisterUserCommand(
  string Name,
  string LastName,
  string Email,
  string Password,
  int RoleId,
  string? Photo
) : ICommand<int>;
