using Api.Common;
using Api.Utils;
using Application.Modules.Users.Commands.RegisterUser;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route(ApiRoutes.UsersRoute)]
public class UsersController : ControllerBase
{
  private readonly ISender _sender;

  public UsersController(ISender sender)
  {
    _sender = sender;
  }

  [HttpPost("register")]
  [MapToApiVersion(ApiVersions.V1)]
  public async Task<IActionResult> Register(
    [FromBody] RegisterUserRequest request,
    CancellationToken cancellationToken)
  {
    var registerUserCommand = new RegisterUserCommand(
      request.Name,
      request.LastName,
      request.Email,
      request.Password,
      request.RoleId,
      request.Photo
    );

    var result = await _sender.Send(registerUserCommand, cancellationToken);

    return result.IsSuccess
      ? Ok(result.Value)
      : BadRequest(result.Error);
  }

}
