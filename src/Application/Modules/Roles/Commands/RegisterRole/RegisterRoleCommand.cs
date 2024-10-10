using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.Commands.RegisterPermission;

namespace Application.Modules.Roles.Commands.RegisterRole;

public sealed record RegisterRoleCommand(
  string Name,
  List<int> Permissions
) : ICommand<CreateResponse<RegisterRoleResponse>>;
