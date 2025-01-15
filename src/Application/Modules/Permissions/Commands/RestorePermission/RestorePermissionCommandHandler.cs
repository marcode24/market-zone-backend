namespace Application.Modules.Permissions.Commands.RestorePermission;

using System.Net;
using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Modules.Permissions.DTOs.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;

internal class RestorePermissionCommandHandler
  : ICommandHandler<RestorePermissionCommand, RestorePermissionResponse>
{
  private readonly IPermissionRepository _permissionRepository;
  private readonly IUnitOfWork _unitOfWork;

  public RestorePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork)
  {
    _permissionRepository = permissionRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Response<RestorePermissionResponse>> Handle(
    RestorePermissionCommand request,
    CancellationToken cancellationToken)
  {
    var permission = await _permissionRepository.GetByIdIncludingDeletedAsync(
      new PermissionId(request.Id),
      cancellationToken);

    if (permission is null)
      return Response.Failure<RestorePermissionResponse>(
        error: PermissionErrors.NotFound,
        statusCode: (int)HttpStatusCode.NotFound
      );

    if (permission.IsDeleted is false)
      return Response.Success(
        RestorePermissionResponse.FromEntity(permission),
        PermissionErrors.NotDeleted.Message,
        (int)HttpStatusCode.OK
      );

    permission.RestoreSoftDelete();

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Response.Success(
      RestorePermissionResponse.FromEntity(permission),
      PermissionMessages.Restored.Message,
      (int)HttpStatusCode.OK
    );
  }
}
