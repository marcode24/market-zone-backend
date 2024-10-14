namespace Application.Modules.Permissions.DTOs.Responses;

using Domain.Entities.Permissions;

public record CreatePermissionResponse(
  int Id,
  string Name,
  string Type,
  bool IsActive,
  DateTime CreatedAt
)
{
  public static CreatePermissionResponse FromEntity(Permission permission) =>
    new(
      permission.Id!.Value,
      permission.Name!.Value,
      permission.Type!.Value,
      permission.IsActive!,
      permission.CreatedAt!
    );
}
