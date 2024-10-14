namespace Application.Modules.Permissions.DTOs.Requests;

public record UpdatePermissionRequest(
  string Name,
  string Type,
  bool? IsActive
);
