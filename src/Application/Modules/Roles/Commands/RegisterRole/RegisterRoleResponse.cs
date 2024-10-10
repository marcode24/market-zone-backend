using Domain.Entities.Roles;

namespace Application.Modules.Permissions.Commands.RegisterPermission;

public record RegisterRoleResponse(
  int Id,
  string Name,
  bool IsActive,
  DateTime CreatedAt,
  List<RegisterPermissionResponse> Permissions
)
{
  public static RegisterRoleResponse FromEntity(Role role)
  {
    return new RegisterRoleResponse(
      role.Id!.Value,
      role.Name!.Value,
      role.IsActive!,
      role.CreatedAt!,
      role.Permissions
        .Select(RegisterPermissionResponse.FromEntity)
        .ToList()
    );
  }
}
