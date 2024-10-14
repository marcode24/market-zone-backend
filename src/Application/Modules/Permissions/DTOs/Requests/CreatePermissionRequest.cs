namespace Application.Modules.Permissions.DTOs.Requests;

public record CreatePermissionRequest(
  string Name,
  string Type
);
