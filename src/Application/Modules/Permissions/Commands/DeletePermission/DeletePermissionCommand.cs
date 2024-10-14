namespace Application.Modules.Permissions.Commands.DeletePermission;

using Application.Abstractions.Messaging;
using Application.Core.Responses;

public record DeletePermissionCommand(
  int Id
) : ICommand<CreateResponse<DeletePermissionResponse>>;
