using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Microsoft.AspNetCore.Http;

namespace Application.Modules.Permissions.Commands.BulkCreatePermission;

public record BulkCreatePermissionCommand(
  IFormFile ExcelFile,
  int BatchSize = 500
) : ICommand<CreateResponseBulk>;
