namespace Application.Modules.Permissions.DTOs.Responses;

public record PermissionResponse(
  int Id,
  string Name,
  string Type,
  bool IsActive,
  DateTime CreatedAt
);
