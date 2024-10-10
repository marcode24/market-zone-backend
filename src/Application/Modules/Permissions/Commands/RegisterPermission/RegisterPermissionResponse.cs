namespace Application.Modules.Permissions.Commands.RegisterPermission;

using Domain.Entities.Permissions;

public record RegisterPermissionResponse(
  int Id,
  string Name,
  string Type,
  bool IsActive,
  DateTime CreatedAt
)
{
  public static RegisterPermissionResponse FromEntity(Permission permission)
  {
    return new RegisterPermissionResponse(
      permission.Id!.Value,
      permission.Name!.Value,
      permission.Type!.Value,
      permission.IsActive!,
      permission.CreatedAt!
    );
  }
}
