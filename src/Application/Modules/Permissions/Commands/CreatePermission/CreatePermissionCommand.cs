using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;
using Application.Modules.Permissions.Interfaces;

namespace Application.Modules.Permissions.Commands.CreatePermission;
public record CreatePermissionCommand(
  string Name,
  string Type
) : ICommand<CreateResponse<CreatePermissionResponse>>, IPermissionCommand;
