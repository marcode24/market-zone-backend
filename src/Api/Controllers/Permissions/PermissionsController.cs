using Api.Common;
using Api.Utils;
using Application.Filters;
using Application.Modules.Permissions.Commands.CreatePermission;
using Application.Modules.Permissions.Commands.UpdatePermission;
using Application.Modules.Permissions.DTOs.Requests;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Permissions;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route(ApiRoutes.PermissionsRoute)]
public class PermissionsController : ControllerBase
{
  private readonly ISender _sender;

  public PermissionsController(ISender sender)
  {
    _sender = sender;
  }

  [HttpPost("register")]
  [MapToApiVersion(ApiVersions.V1)]
  public async Task<IActionResult> Register(
    [FromBody] CreatePermissionRequest request,
    CancellationToken cancellationToken)
  {
    var registerPermissionCommand = new CreatePermissionCommand(
      request.Name,
      request.Type
    );

    var result = await _sender.Send(registerPermissionCommand, cancellationToken);

    return result.IsSuccess
      ? Ok(result.Value)
      : BadRequest(result.Error);
  }

  [HttpPut("update/{id}")]
  [MapToApiVersion(ApiVersions.V1)]
  [ServiceFilter(typeof(ValidateIdAttribute))]
  public async Task<IActionResult> Update(
    [FromRoute] string id,
    [FromBody] UpdatePermissionRequest request,
    CancellationToken cancellationToken)
  {

    var updatePermissionCommand = new UpdatePermissionCommand(
      int.Parse(id),
      request.Name,
      request.Type,
      request.IsActive
    );

    var result = await _sender.Send(updatePermissionCommand, cancellationToken);

    return result.IsSuccess
      ? Ok(result.Value)
      : BadRequest(result.Error);
  }
}
