using Api.Common;
using Api.Utils;
using Application.Filters;
using Application.Modules.Permissions.Commands.BulkCreatePermission;
using Application.Modules.Permissions.Commands.CreatePermission;
using Application.Modules.Permissions.Commands.DeletePermission;
using Application.Modules.Permissions.Commands.RestorePermission;
using Application.Modules.Permissions.Commands.UpdatePermission;
using Application.Modules.Permissions.DTOs.Requests;
using Application.Modules.Permissions.Queries.GetPermissions;
using Application.Modules.Permissions.Queries.GetPermissionTemplate;
using Asp.Versioning;
using Domain.Shared;
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

  [HttpGet("")]
  [MapToApiVersion(ApiVersions.V1)]
  public async Task<IActionResult> GetPermissions(
    [FromQuery] PaginationParams paginationParams,
    CancellationToken cancellationToken)
  {
    var getPermissionsQuery = new GetPermissionsQuery
    {
      GetAll = paginationParams.GetAll,
      Search = paginationParams.Search,
      OrderBy = paginationParams.OrderBy,
      OrderAsc = paginationParams.OrderAsc,
      PageNumber = paginationParams.PageNumber,
      PageSize = paginationParams.PageSize,
    };

    var result = await _sender.Send(getPermissionsQuery, cancellationToken);

    return result.IsSuccess
      ? Ok(result.Value)
      : BadRequest(result.Error);
  }

  [HttpGet("template")]
  [MapToApiVersion(ApiVersions.V1)]
  public async Task<IActionResult> GetTemplate(
    CancellationToken cancellationToken
  )
  {
    var permissionTemplateQuery = new GetPermissionTemplateQuery();

    var result = await _sender.Send(permissionTemplateQuery, cancellationToken);

    return result.IsSuccess
      ? File(result.Value, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PermisionTemplate.xlsx")
      : BadRequest(result.Error);
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

  [HttpPost("register/bulk")]
  [MapToApiVersion(ApiVersions.V1)]
  public async Task<IActionResult> BulkRegister(
    [FromForm] BulkCreatePermissionRequest request,
    CancellationToken cancellationToken)
  {
    var bulkRegisterPermissionCommand = new BulkCreatePermissionCommand(
      request.File
    );

    var result = await _sender.Send(bulkRegisterPermissionCommand, cancellationToken);

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

  [HttpDelete("delete/{id}")]
  [MapToApiVersion(ApiVersions.V1)]
  [ServiceFilter(typeof(ValidateIdAttribute))]
  public async Task<IActionResult> Delete(
    [FromRoute] string id,
    CancellationToken cancellationToken)
  {
    var deletePermissionCommand = new DeletePermissionCommand(int.Parse(id));

    var result = await _sender.Send(deletePermissionCommand, cancellationToken);

    return result.IsSuccess
      ? Ok(result.Value)
      : BadRequest(result.Error);
  }

  [HttpPatch("activate/{id}")]
  [MapToApiVersion(ApiVersions.V1)]
  [ServiceFilter(typeof(ValidateIdAttribute))]
  public async Task<IActionResult> Activate(
    [FromRoute] string id,
    CancellationToken cancellationToken)
  {
    var restorePermissionCommand = new RestorePermissionCommand(int.Parse(id));

    var result = await _sender.Send(restorePermissionCommand, cancellationToken);

    return result.IsSuccess
      ? Ok(result.Value)
      : BadRequest(result.Error);
  }
}
