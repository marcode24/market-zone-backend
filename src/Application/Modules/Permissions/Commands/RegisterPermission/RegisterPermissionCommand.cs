using Application.Abstractions.Messaging;
using Application.Core.Responses;

namespace Application.Modules.Permissions.Commands.RegisterPermission;
public record RegisterPermissionCommand(
  string Name,
  string Type
) : ICommand<CreateResponse<RegisterPermissionResponse>>;
