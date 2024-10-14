using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;
using Application.Modules.Permissions.Interfaces;

namespace Application.Modules.Permissions.Commands.UpdatePermission;

public record UpdatePermissionCommand(
  int Id,
  string Name,
  string Type,
  bool? IsActive
) : ICommand<UpdateResponse<UpdatePermissionResponse>>, IPermissionCommand;
