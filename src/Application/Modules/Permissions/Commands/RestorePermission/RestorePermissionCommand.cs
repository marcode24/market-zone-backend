namespace Application.Modules.Permissions.Commands.RestorePermission;

using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;

public record RestorePermissionCommand(
  int Id
) : ICommand<CreateResponse<RestorePermissionResponse>>;
