using Api.Common;
using Api.Utils;
using Application.Modules.Roles.Commands.RegisterRole;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Roles;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route(ApiRoutes.RolesRoute)]
public class RolesController : ControllerBase
{
  private readonly ISender _sender;
  public RolesController(ISender sender)
  {
    _sender = sender;
  }

  [HttpPost("register")]
  [MapToApiVersion(ApiVersions.V1)]
  public async Task<IActionResult> Register(
    [FromBody] RegisterRoleRequest request,
    CancellationToken cancellationToken)
  {
    var registerRoleCommand = new RegisterRoleCommand(
      request.Name,
      request.Permissions
    );

    var result = await _sender.Send(registerRoleCommand, cancellationToken);

    return result.IsSuccess
      ? Ok(result.Value)
      : BadRequest(result.Error);
  }
}
