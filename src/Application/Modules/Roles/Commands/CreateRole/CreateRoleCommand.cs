using Application.Abstractions.Messaging;
using Application.Core.Responses;

namespace Application.Modules.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand(
  string Name,
  List<int> Permissions
) : ICommand<CreateResponse<CreateRoleResponse>>;
