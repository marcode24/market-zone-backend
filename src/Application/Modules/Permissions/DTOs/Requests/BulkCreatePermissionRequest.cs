using Microsoft.AspNetCore.Http;

namespace Application.Modules.Permissions.DTOs.Requests;

public record BulkCreatePermissionRequest(
  IFormFile File
);
