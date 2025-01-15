using System.Net;
using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;
using Domain.Shared.ValueObjects;

namespace Application.Modules.Permissions.Commands.CreatePermission;

internal class CreatePermissionCommandHandler(
  IPermissionRepository permissionRepository,
  IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePermissionCommand, CreatePermissionResponse>
{
  private readonly IPermissionRepository _permissionRepository = permissionRepository;
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Response<CreatePermissionResponse>> Handle(
    CreatePermissionCommand request,
    CancellationToken cancellationToken)
  {
    var newPermission = Permission.Create(
      new Name(request.Name),
      new TypePermission(request.Type)
    );

    _permissionRepository.Add(newPermission);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    if (newPermission.Id is null)
      return Response.Failure<CreatePermissionResponse>(
        error: PermissionErrors.ErrorCreating,
        statusCode: (int)HttpStatusCode.InternalServerError
      );

    return Response.Success(
      CreatePermissionResponse.FromEntity(newPermission),
      PermissionMessages.Created.Message,
      (int)HttpStatusCode.Created);
  }
}
