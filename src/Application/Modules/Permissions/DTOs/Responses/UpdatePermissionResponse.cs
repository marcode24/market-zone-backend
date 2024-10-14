using Domain.Entities.Permissions;

namespace Application.Modules.Permissions.DTOs.Responses;

public record UpdatePermissionResponse(
  int Id,
  string Name,
  string Type,
  bool IsActive,
  DateTime UpdatedAt
)
{
  public static UpdatePermissionResponse FromEntity(Permission permission) =>
    new(
      permission.Id!.Value,
      permission.Name!.Value,
      permission.Type!.Value,
      permission.IsActive!,
      permission.UpdatedAt!
    );
}
