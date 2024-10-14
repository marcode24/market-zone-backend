using Domain.Entities.Permissions;

public record DeletePermissionResponse(
  int Id,
  string Name,
  string Type,
  DateTime? DeletedAt
)
{
  public static DeletePermissionResponse FromEntity(Permission permission) =>
    new(
      permission.Id!.Value,
      permission.Name!.Value,
      permission.Type!.Value,
      permission.DeletedAt!
    );
}
