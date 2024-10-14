namespace Application.Modules.Permissions.Commands.DeletePermission;

using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;

public record DeletePermissionCommand(
  int Id
) : ICommand<CreateResponse<DeletePermissionResponse>>;
