namespace Application.Modules.Permissions.Commands.RestorePermission;

using Application.Abstractions.Messaging;
using Application.Modules.Permissions.DTOs.Responses;

public record RestorePermissionCommand(
  int Id
) : ICommand<RestorePermissionResponse>;
