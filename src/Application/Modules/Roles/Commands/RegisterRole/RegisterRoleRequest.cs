namespace Application.Modules.Roles.Commands.RegisterRole;

public record RegisterRoleRequest(
  string Name,
  List<int> Permissions
);
