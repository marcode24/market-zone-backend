namespace Application.Modules.Permissions.DTOs.Responses;

using Domain.Entities.Permissions;

public record RestorePermissionResponse(
  int Id,
  string Name,
  string Type,
  bool IsDeleted,
  DateTime? UpdatedAt
)
{
  public static RestorePermissionResponse FromEntity(Permission permission) =>
    new(
      permission.Id!.Value,
      permission.Name!.Value,
      permission.Type!.Value,
      permission.IsDeleted,
      permission.UpdatedAt!
    );
}
