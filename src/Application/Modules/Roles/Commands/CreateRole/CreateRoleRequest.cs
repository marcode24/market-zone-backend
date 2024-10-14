namespace Application.Modules.Roles.Commands.CreateRole;

public record CreateRoleRequest(
  string Name,
  List<int> Permissions
);
