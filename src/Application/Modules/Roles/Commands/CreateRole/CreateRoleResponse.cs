using Application.Modules.Permissions.DTOs.Responses;
using Domain.Entities.Roles;

namespace Application.Modules.Roles.Commands.CreateRole;

public record CreateRoleResponse(
  int Id,
  string Name,
  bool IsActive,
  DateTime CreatedAt,
  List<CreatePermissionResponse> Permissions
)
{
  public static CreateRoleResponse FromEntity(Role role)
  {
    return new CreateRoleResponse(
      role.Id!.Value,
      role.Name!.Value,
      role.IsActive!,
      role.CreatedAt!,
      role.Permissions
        .Select(CreatePermissionResponse.FromEntity)
        .ToList()
    );
  }
}
